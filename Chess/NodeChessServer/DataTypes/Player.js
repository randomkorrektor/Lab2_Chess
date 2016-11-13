'use strict';
class Player {
    constructor(name, money, refresh, lose, win, step) {
        this.name = name;
        this.money = money;
        this.isCurrent = false;
        this.refresh = () => { refresh(this) };
        this.step = (num) => { step(num); }
        this.lose = lose
        this.win = win;
        this.hand = [];
    }

    Refresh() {
        this.refresh(this);
    }
}

module.exports = Player;