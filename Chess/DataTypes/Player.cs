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
        public Card[] Hand;

        public Player(string name, int money)
        {
            Name = name;
            Money = money;
        }

    }
}
