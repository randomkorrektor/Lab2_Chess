class Player {
    constructor(name, money, refresh) {
        this.name = name;
        this.money = money;
        this.isCurrent = false;
        this.refresh = refresh;
        this.hand = [];
    }

    Refresh() {
        this.refresh(this);
    }
}

module.exports = Player;