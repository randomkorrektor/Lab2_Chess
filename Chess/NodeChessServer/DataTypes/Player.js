'use strict';
class Player {
    constructor(name, money, refresh, lose) {
        this.name = name;
        this.money = money;
        this.isCurrent = false;
        this.refresh = refresh;
        this.lose = lose
        this.hand = [];
    }

    Refresh() {
        this.refresh(this);
    }
}

module.exports = Player;