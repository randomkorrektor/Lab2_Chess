

const RoundType = require('./RoundType');
const CardDeck = require('./CardDeck');
const Bank = require('./Bank');

class Table {
    constructor(smallBlind) {
        this.gameNumber = 0;
        this.roundType = RoundType.flop;
        this.button = 0;
        this.currentPlayer = 1;
        this.ratePlayers = 0;
        this.players = [];
        this.cards = [];
        this.cardDeck = new CardDeck();
        this.bank = new Bank(smallBlind);
    }

    SetSmallBlind(smallBlind) {
        this.bank.SetSmallBlind(smallBlind);
    }

    SetCards(cards) {
        this.cards = cards;
    }
}
module.exports = Table;