using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    public class Player
    {
        public string Name { get; set; } = "";
        public int Money { get; set; } = 0;
        public bool IsCurrent = false;
        private Action<Player> refresh;
        public Card[] Hand;

        public Player(string name, int money, Action<Player> refresh)
        {
            Name = name;
            Money = money;
            this.refresh = refresh;
        }

        public void Refresh()
        {
            refresh(this);
        }

    }
}
