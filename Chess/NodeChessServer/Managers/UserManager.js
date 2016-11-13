const Player = require('../DataTypes/Player');
class UserManager {
    constructor(ioManager) {
        this.ioManager = ioManager;
    }
    AddUser(name, refresh) {
        this.users || (this.users = []);
        this.users.push(new Player(name, 10000, refresh));
        switch (Users.Count) {
            case 1:
                this.users[this.users.length - 1].isCurrent = true;
                break;
            case 2:
                this.ioManager.StartTimer();
                break;
            default:
                if (this.users.length >= 6) {
                    this.ioManager.DisposeTimer();
                }
                break;
        }
        return this.users;
    }


    static SolvencyControl(raise) {
        for (const user of this.users) {
            if (user.Money < raise) {
                const i = this.users.indexOf(user);
                this.users.splice(i, 1);
            }
        }
    }
}