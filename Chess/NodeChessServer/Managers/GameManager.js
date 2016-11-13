const Table = require('../DataTypes/Table');
const RoundType = require('../DataTypes/RoundType');
const PlayersCommand = require('../DataTypes/PlayersCommand');

class GameManager {
    constructor(startSmallBlind, userManager, ioManager) {
        this.table = new Table(startSmallBlind);
        this.userManager = userManager;
        this.ioManager = ioManager;
    }

    StartGame() {
        this.table.gameNumber++;
        this.userManager.SolvencyControl(this.table.bank.bigBlind);
        if (this.userManager.users.length <= 1) {
            this.ioManager.PlayerWin(this.userManager.users[0]);
            return;
        }
        this.ioManager.StartingGame();

        for (let player of this.table.Players)
        {
            player.Hand = this.table.CardDeck.GetHand();
        }

        this.table.players[this.table.currentPlayer].money -= this.table.bank.smallBlind;

        this.table.bank.tableBank += this.table.bank.smallBlind;

        this.NextPlayer();
        this.RateToBank(this.table.currentPlayer);
        this.NextPlayer();

        this.ioManager.RefreshingPlayers();
        this.ioManager.RefreshingTable(table);

        this.StartRound();
    }



    StartRound() {
        if (this.userManager.users.length <= 1) {
            this.ioManager.PlayerWin(this.userManager.users[0]);
            return;
        }
        this.ioManager.StartingRound();
        if (this.table.roundType == RoundType.Flop) {
            this.table.cards = this.table.cardDeck.GetFlop();
        }
        else {
            this.table.cards.push(this.table.cardDeck.TopCard());
        }
        this.ioManager.RefreshingTable(this.table);
        this.ioManager.RefreshingPlayers();
        this.table.roundType = ((this.table.RoundType + 1) % 3);
    }

    EndofGame() {
        this.table.players[this.table.players.length - 1].money += this.table.bank.tableBank;
        this.table.bank.tableBank = 0;
        this.table.players = [];
        this.table.players.addRange(this.userManager.users);
    }


    RateToBank(playerNumber) {
        if (this.table.players[playerNumber].money > this.table.bank.rate) {
            this.table.players[playerNumber].money -= this.table.bank.rate;
            this.table.bank.tableBank += this.table.bank.rate;
        }
        else {
            throw new Error("Error! Not enough money.");
        }
    }

    public string RateOfPayer(playerNumber, command, raise) {
        switch (command) {
            case PlayersCommand.call:
                this.RateToBank(playerNumber);
                this.table.ratePlayers++;
                this.NextPlayer();
                break;
            case PlayersCommand.raise:
                for (let player of this.table.Players)
                {
                    if (this.table.players[i].money < this.table.bank.rate + raise) {
                        return "Player" + (i + 1) + "is not enough money!";
                    }
                }
                this.table.bank.RaiseRate(raise);
                this.RateToBank(playerNumber);
                this.table.ratePlayers = 1;
                this.NextPlayer();
                break;
            case PlayersCommand.fold:
                this.table.players.splice(playerNumber);
                break;
            default:
                return "Invalid command!";
        }
        return command.toString();
    }

    NextPlayer() {
        this.table.players[this.table.currentPlayer].isCurrent = false;
        this.table.currentPlayer = (this.table.currentPlayer + 1) % this.table.players.length;
        this.table.players[this.table.currentPlayer].isCurrent = true;

    }
}
