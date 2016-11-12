class Bank {

    constructor(smallBlind) {
        this.smallBlind = smallBlind;
        this.rate = this.bigBlind;
        this.tableBank = 0;
    }

    get bigBlind() {
        return this.smallBlind;
    }

    SetSmallBlind(smallBlind) {
        this.smallBlind = smallBlind;
    }

    ClearRate() {
        this.rate = this.bigBlind;
    }
    
    RaiseRate(raise) {
        if (raise < 1) {
            throw "Raise should be a positive number!";
        }
        this.rate += raise;
    }
}

module.exports = Bank;