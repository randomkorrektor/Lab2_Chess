const Player = require('../DataTypes/Player');
class UserManager {
    static AddUser(name, refresh) {
        UserManager.users || (UserManager.users = []);
        UserManager.users.push(new Player(name, 10000, refresh));
        switch (Users.Count) {
            case 1:
                UserManger.users..IsCurrent = true;
                break;
            case 2:
                IOManager.Instance.StartTimer();
                break;
            default:
                IOManager.Instance.DisposeTimer();
                break;
        }
        return Users;
    }
}