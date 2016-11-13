'use strict';
class IOManager {
    constructor() {

        this.userManager = null;
    }

    AddUser(name, refresh) {
        return this.userManager.AddUser(name, refresh);
    }

    StartTimer() {
        this.timer = setInterval(this.TimerTickHandler.bind(this), 100);
        this.timerTicks = 0;
    }
    TimerTickHandler() {
        this.timerTicks++;
        if (this.timerTicks >= 1200) {
            this.TimerEndHandler();
        }
        this.TimerTick(this.timerTicks);
    }
    TimerEndHandler() {
        this.timerIsEnded = true;

        clearInterval(this.timer);
        this.TemerEnd();
    }

    Lose(player) {
        player.lose();
    }
}

module.exports = IOManager;