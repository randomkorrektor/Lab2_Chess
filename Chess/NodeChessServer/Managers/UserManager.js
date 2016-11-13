'use strict';
const Player = require('../DataTypes/Player');
class UserManager {
    constructor(ioManager, startCache) {
        this.ioManager = ioManager;
        this.startCache = startCache;
        this.users = [];
    }
    AddUser(name, refresh, lose, win, step, info) {
        if (this.ioManager.timerIsEnded != true) {
            this.users.push(new Player(name, this.startCache, refresh, lose, win, step, info));
            this.ioManager.RefreshUserList(this.users);
            switch (this.users.length) {
                case 1:
                    this.users[this.users.length - 1].isCurrent = true;
                    break;
                case 2:
                    this.ioManager.StartTimer();
                    break;
                default:
                    if (this.users.length >= 6) {
                        this.ioManager.TimerEndHandler();
                    }
                    break;
            }
        }
    }

    SolvencyControl(raise) {
        for (const user of this.users) {
            if (user.Money < raise) {
                this.ioManager.Lose(user);
                const i = this.users.indexOf(user);
                this.users.splice(i, 1);
                this.ioManager.RefreshUserList(this.users);
            }
        }
    }
}

module.exports = UserManager;