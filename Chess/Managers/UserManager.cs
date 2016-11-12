using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Managers
{
    class UserManager
    {
        public static UserManager Instance = new UserManager();

        private UserManager() { }

        public List<Player> Users = new List<Player>();

        public List<Player> AddUser(string name, Action<Player> refresh)
        {
            Users.Add(new Player(name, 10000, refresh));
            switch (Users.Count)
            {
                case 1:
                    Users.Last().IsCurrent = true;
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

        public void SolvencyControl(int raise)
        {
            foreach (var user in Users)
            {
                if (user.Money < raise)
                {
                    Users.Remove(user);
                }
            }
        }
    }
}
