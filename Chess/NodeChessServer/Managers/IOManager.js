class IOManager {
    constructor() {
        this.timer = null;
        this.TimerTick = null;
        this.TimerDisposed = null;

        this.StartGame = null;
        this.StartRound = null;
        this.RefreshTable = null;
        this.PlayerWin = null;
        this.userManager = null;
    }

    StartTimer() {
        this.timer.Start();
    }

    DisposeTimer() {
        timer.Dispose();
    }

    Timer_Elapsed() {
        this.TimerTick(timer.date);
        if (timer.date >= 1200000) {
            timer.Dispose();
        }
    }

    Timer_Disposed() {
        TimerDisposed();
    }

    AddUser(name, refresh) {
        return this.userManager.AddUser(name, refresh);
    }

    RateUser(player, com, rise) {
        GameManager.Instance.RateOfPayer(player, com, rise);
    }

    StartingGame() {
        StartGame();
    }

    StartingRound() {
        StartRound();
    }

    RefreshingTable(table) {
        RefreshTable(table);
    }

    RefreshingPlayers() {
        var users = this.UserManager.Users;

        for (let user of users) {
            user.Refresh();
        }
    }

    PlayerWin(player) {
        PlayerIsWin(player);
    }
}

module.exports = IOManager;