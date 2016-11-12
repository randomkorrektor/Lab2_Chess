const Player = require('../DataTypes/Player');
const IOManager = require('./IOManager');
class UserManager {
    static AddUser(name, refresh) {
        UserManager.users || (UserManager.users = []);
        UserManager.users.push(new Player(name, 10000, refresh));
        switch (Users.Count) {
            case 1:
                UserManger.users[UserManager.users.length - 1].IsCurrent = true;
                break;
            case 2:
                IOManager.StartTimer();
                break;
            default:
                if (UserManager.users.length >= 6) {
                    IOManager.DisposeTimer();
                }
                break;
        }
        return Users;
    }


    static SolvencyControl(raise) {
        for (const user of UserManager.users) {
            if (user.Money < raise) {
                const i = UserManager.users.indexOf(user);
                UserManager.users.splice(i, 1);
            }
        }
    }
}