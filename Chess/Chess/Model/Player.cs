using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model
{
    class Player
    {

        Card[] hand = new Card[2];
        protected int _money;
        protected string _name;  // ?
        public int money { get { return _money; } }
        public string name { get; set; }

        public Player(string name, Card[] hand, int money)
        {
            this._name = name;
            this.hand[0] = hand[0];
            this.hand[1] = hand[1];
            this._money = money;

        }

    }
}