using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model
{
    class Bank
    {
        protected int _bank = 0, _bigBlind, _smallBlind, _rate;
        public int bank { get { return _bank; } }
        public int bigBlind { get { return _bigBlind; } }
        public int smallBlind{ get { return _smallBlind; } }
        public int rate { get { return _rate; } }

        public Bank(int smallBlind)
        {
            this._smallBlind = smallBlind;
            this._bigBlind = smallBlind * 2;
            this._rate = bigBlind;
        }

        public void RaiseRate(int raise)
        {
            if(raise<1)
            {
                throw new Exception("Raise should be a positive number!");
            }
            this._rate += raise;
        }




    }
}
