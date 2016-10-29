using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Managers
{
    public class UserManager
    {
        public static UserManager Instance = new UserManager();

        private UserManager()
        {
            timer.Elapsed += Timer_Elapsed;
            timer.Disposed += Timer_Disposed;
        }

        public List<Player> Users = new List<Player>();    
        private Timer timer = new Timer(100);
        public event Action<int> TimerTick;
        public event Action StartGame;
        public event Action StartRound;
        public event Action RefreshTable;
        public event Action<Player> PlayerIsWin;

        public event Action endOfTimer;

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimerTick((int)e.SignalTime.Ticks);
            if ((int)e.SignalTime.Ticks >= 1200000)
            {
                timer.Dispose();
            }
        }

        private void Timer_Disposed(object sender, EventArgs e)
        {
            endOfTimer();
        }

        public List<Player> AddUser(string name, Action<Player> refresh)
        {
            Users.Add(new Player(name, 10000, refresh));
            if (Users.Count == 1)
            {
                Users.Last().IsCurrent = true;
            }
            if (Users.Count == 2)
            {
                timer.Start();
            }
            if (Users.Count > 4)
            {
                timer.Dispose();
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

        public void PlayerWin(Player player)
        {
            PlayerIsWin(player);
        }
        public void GameStart()
        {
            StartGame();
        }

        public void RoundStart()
        {
            StartRound();
        }

        public void TableRefresh()
        {
            RefreshTable();
        }
        public void RefreshUsers()
        {
            foreach (var user in UserManager.Instance.Users)
            {
                user.Refresh();
            }
        }
    }
}
