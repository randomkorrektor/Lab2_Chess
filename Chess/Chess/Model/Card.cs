using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Card
    {
        protected string _lear, _raiting;
        protected int _intLear, _intRaiting;
        public string lear { get { return _lear; } }
        public string raiting { get { return _raiting; } }
        public int intLear { get { return _intLear; } }
        public int intRaiting { get { return _intRaiting; } }
        protected bool _open = false;
        public bool open {get{ return _open;} }

        public Card(string lear, string raiting, int intLear, int intRaiting)
        {
            this._lear = lear;
            this._raiting = raiting;
            this._intLear = intLear;
            this._intRaiting = intRaiting;
        }




    }
}
