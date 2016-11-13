'use strict';
class IOManager {
    constructor() {

        this.userManager = null;
    }

    AddUser(name, refresh, lose, win, step, info) {
        return this.userManager.AddUser(name, refresh, lose, win, step, info);
    }

    StartTimer() {
        this.timer = setInterval(this.TimerTickHandler.bind(this), 100);
        this.timerTicks = 0;
    }
    TimerTickHandler() {
        this.timerTicks++;
        if (this.timerTicks >= 50) {
            this.TimerEndHandler();
        }
        this.TimerTick(this.timerTicks);
    }
    TimerEndHandler() {
        this.timerIsEnded = true;

        clearInterval(this.timer);
        this.TimerEnd();
    }

    Lose(player) {
        player.lose();
    }

    Win(player) {
        player.win();
    }

    Info(player, msg) {
        player.info(msg);
    }

    PlayerStep(player,num) {
        player.step(num);
    }

    RefreshPlayers(players) {
        for (const player of players) {
            player.refresh();
        }
    }

    StartGameHandler() {
        this.StartGame();
    }

    StartRoundHandler() {
        this.StartRound();
    }

    RefreshTable(table) {
        this.RefreshTableHandler(table);
    }

    RateOfPlayer(playerNumber, command, raise) {
        this.gameManager.RateOfPlayer(playerNumber, command, raise);
    }
}

module.exports = IOManager;