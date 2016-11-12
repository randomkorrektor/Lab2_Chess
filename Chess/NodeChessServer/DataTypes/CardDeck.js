const Card = require('./Card');

class CardDeck {
    constructor() {
        this.deck = [];
        for (let lear = 0; lear < 4; lear++) {
            for (let rating = 2; rating <= 14; rating++) {
                this.deck.push(new Card(lear, rating));
            }
        }
        this.MakeMixList();
    }

    MakeMixList() {
        this.gameDeck = [];
        for (const card of this.deck) {
            this.gameDeck.push(card);
        }
        this.gameDeck.sort(() => (5 - Math.random() * 10));
    }

    TopCard() {
        return this.gameDeck.pop();
    }

    Discard() {
        this.gameDeck.pop();
    }

    GetHand() {
        return [
            this.TopCard(),
            this.TopCard()
        ];
    }

    GetFloop() {
        return [
            this.TopCard(),
            this.TopCard(),
            this.TopCard()
        ];
    }
}

module.exports = CardDeck;