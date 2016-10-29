using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Managers
{
    class IOManager
    {
        public static IOManager Instance = new IOManager();


        private Timer timer = new Timer(100);

        public event Action<int> TimerTick;
        public event Action TimerDisposed;


        public event Action StartGame;
        public event Action StartRound;
        public event Action<Table> RefreshTable;
        public event Action<Player> PlayerIsWin;


        private IOManager()
        {
            timer.Elapsed += Timer_Elapsed;
            timer.Disposed += Timer_Disposed;
        }


        public void StartTimer()
        {
            timer.Start();
        }

        public void DisposeTimer()
        {
            timer.Dispose();
        }

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
            TimerDisposed();
        }




        public List<Player> AddUser(string name, Action<Player> refresh)
        {
            return UserManager.Instance.AddUser(name, refresh);
        }

        public void RateUser(int player, PlayersCommand com, int rise)
        {
            GameManager.Instance.RateOfPayer(player, com, rise);
        }

        public void StartingGame()
        {
            StartGame();
        }

        public void StartingRound()
        {
            StartRound();
        }

        public void RefreshingTable(Table table)
        {
            RefreshTable(table);
        }

        public void RefreshingPlayers()
        {
            var users = UserManager.Instance.Users;

            foreach (var user in users)
            {
                user.Refresh();
            }
        }

        public void PlayerWin(Player player)
        {
            PlayerIsWin(player);
        }
    }
}
