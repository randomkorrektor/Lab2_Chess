'use strict';
const Table = require('../DataTypes/Table');
const RoundType = require('../DataTypes/RoundType');
const PlayersCommand = require('../DataTypes/PlayerCommand');
const Combinator = require('./Combinator');

class GameManager {
    constructor(startSmallBlind, userManager, ioManager) {
        this.table = new Table(startSmallBlind);
        this.userManager = userManager;
        this.ioManager = ioManager;
    }

    StartGame() {
        this.table.gameNumber++;
        this.userManager.SolvencyControl(this.table.bank.bigBlind);
        if (this.userManager.users.length == 1) {
            this.ioManager.Win(this.userManager.users[0]);
            return;
        }
        else if (this.userManager.users.length == 0) {
            return;
        }
        this.table.players = this.userManager.users.map(u => (u));
        for (let player of this.table.players) {
            player.hand = this.table.cardDeck.GetHand();
        }
        for (const player of this.table.players) {
            player.rate = 0;
        }
        this.table.players[this.table.currentPlayer].money -= this.table.bank.smallBlind;
        this.table.bank.tableBank += this.table.bank.smallBlind;
        this.table.players[this.table.currentPlayer].rate = this.table.bank.smallBlind;
        this.NextPlayer(true);
        this.RateToBank(this.table.currentPlayer);
        this.NextPlayer();

        this.ioManager.RefreshPlayers(this.table.players);
        this.ioManager.RefreshTable(this.table);
        this.ioManager.StartGameHandler();

    }

    NextPlayer(flag) {
        this.table.players[this.table.currentPlayer].isCurrent = false;
        this.table.currentPlayer = (this.table.currentPlayer + 1) % this.table.players.length;
        this.table.players[this.table.currentPlayer].isCurrent = true;

        !flag && this.ioManager.PlayerStep(this.table.players[this.table.currentPlayer], this.table.currentPlayer);
    }

    RateToBank(playerNumber) {
        if (this.table.players[playerNumber].money >= this.table.bank.rate - this.table.players[playerNumber].rate) {
            this.table.players[playerNumber].money -= this.table.bank.rate - this.table.players[playerNumber].rate;
            this.table.bank.tableBank += this.table.bank.rate - this.table.players[playerNumber].rate;
            this.table.players[playerNumber].rate = this.table.bank.rate;
        }
        else {
            this.ioManager.info(this.table.players[playerNumber], 'You not have enough money');
        }
    }

    RateOfPlayer(playerNumber, command, raise) {
        if (playerNumber == this.table.currentPlayer) {
            switch (command) {
                case PlayersCommand.call:
                    this.RateToBank(playerNumber);
                    this.table.ratePlayers++;
                    break;
                case PlayersCommand.raise:
                    for (let player of this.table.Players) {
                        if (this.table.player.money < this.table.bank.rate + raise) {
                            this.ioManager.info(this.table.players[playerNumber], `Player ${player.name} is not enough money!`);
                            return;
                        }
                    }
                    this.table.bank.RaiseRate(raise);
                    this.RateToBank(playerNumber);
                    this.table.ratePlayers = 1;
                    break;
                case PlayersCommand.fold:
                    this.table.players.splice(playerNumber);
                    this.table.currentPlayer--;
                    break;
                default:
                    this.ioManager.Info(this.table.players[playerNumber], "Invalid command!");
            }

            if (this.userManager.users.length == 1) {
                this.ioManager.Win(this.userManager.users[0]);
                this.EndofGame();
                return;
            }
            else if (this.userManager.users.length == 0) {
                return;
            }
            if (this.table.ratePlayers == this.table.players.length) {
                this.EndRateCircle();
            }
            else {
                this.NextPlayer();
            }
            return;
        }

        this.ioManager.Info(this.table.players[this.table.currentPlayer], "Not your step.");
    }

    EndRateCircle() {
        for (const player of this.table.players) {
            player.rate = 0;
        }

        this.StartRound();
    }

    StartRound() {
        if (this.userManager.users.length == 1) {
            this.ioManager.Win(this.userManager.users[0]);
            this.EndofGame();
            return;
        }
        else if (this.userManager.users.length == 0) {
            return;
        }
        switch (this.table.roundType) {
            case RoundType.flop:
                this.table.cards = this.table.cardDeck.GetFlop();
                break;
            case RoundType.turn:
                this.table.cards.push(this.table.cardDeck.TopCard());
                break;
            case RoundType.river:
                this.table.cards.push(this.table.cardDeck.TopCard());
                break;
            case RoundType.show:
                this.RemoveLosers();
                this.EndofGame();
                break;
        }
        this.ioManager.RefreshPlayers(this.table.players);
        this.ioManager.RefreshTable(this.table);
        this.ioManager.StartRoundHandler();
        this.table.roundType = ((this.table.roundType + 1) % 4);
        this.table.currentPlayer = (this.table.button);
        this.table.ratePlayers = 0;
        this.table.bank.rate = 0;
        this.NextPlayer();
    }
    RemoveLosers() {
        let combs = [];
        for (const player of this.table.players) {
            combs.push(Combinator.FindCombination([
                this.table.cards[0],
                this.table.cards[1],
                this.table.cards[2],
                this.table.cards[3],
                this.table.cards[4],
                player.hand[0],
                player.hand[1]
            ]));
            combs[combs.length - 1].player = player;
        }
        combs = combs.sort((a, b) => (b.code - a.code));
        const topCode = combs[0].code;
        combs = combs.filter((a) => (a.code == topCode));
        if (combs.length == 1) {
            this.PlayerWin(combs[0].player);
            this.table.players = [combs[0].player];
        } else {
            combs = combs.sort((a, b) => (b.topCard - a.topCard));
            const topCard = combs[0].topCard;
            combs = combs.filter((a) => (a.topCard == topCard));
            if (combs.length == 1) {
                this.PlayerWin(combs[0].player);
                this.table.players = [combs[0].player];
            } else {
                combs = combs.sort((a, b) => (b.kicker - a.kicker));
                const topkicker = combs[0].kicker;
                combs = combs.filter((a) => (a.kicker == topkicker));
                if (combs.length == 1) {
                    this.PlayerWin(combs[0].player);
                    this.table.players = [combs[0].player];
                } else {
                    const wplayers = [];
                    for (const comb of combs) {
                        wplayers.push(comb.player);
                    }
                    this.DeadHeat(wplayers);
                    this.table.players = wplayers;
                }
            }
        }


    }
    EndofGame() {
        if (this.table.players.length == 1) {

            this.table.players[this.table.players.length - 1].money += this.table.bank.tableBank;
        } else {
            const money = this.table.bank.tableBank / this.table.players.length;
            for (const player of this.table.players) {
                player.money = money;
            }
        }
        this.table.bank.tableBank = 0;
    }
    PlayerWin(player) {
        const index = this.table.players.indexOf(player);
        let i = 0;
        for (const pl of this.table.players) {
            if (i != index) {
                this.ioManager.Lose(player);
            } else {
                this.ioManager.Win(player);
            }
        }
    }

    DeadHeat(wplayers) {
        const winners = [];
        const players = this.table.players.map(u => (u));
        for (const player of wplayers) {
            const index = players.indexOf(player);
            players.splice(index, 1);
            this.ioManager.Win(player);
        }

        for (const pl of players) {
            this.ioManager.Lose(player);
        }
    }

}

module.exports = GameManager;