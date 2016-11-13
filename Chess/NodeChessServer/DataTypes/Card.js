'use strict';
class Card {
    constructor(lear, rating) {
        this.lear = lear;
        this.rating = rating;
        this.open = false;
    }

    clone() {
        return new Card(this.lear, this.rating);
    }
}

module.exports = Card;