using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    public class Bank
    {
        public int SmallBlind = 0;
        public int BigBlind
        {
            get
            {
                return SmallBlind * 2;
            }
        }
        public int Rate = 0;
        public int TableBank = 0;

        public Bank(int smallBlind)
        {
            this.SmallBlind = smallBlind;
            this.Rate = BigBlind;
        }

        public void SetSmallBlind(int smallBlind)
        {
            SmallBlind = smallBlind;
        }

        public void ClearRate()
        {
            this.Rate = BigBlind;
        }
        public void RaiseRate(int raise)
        {
            if (raise < 1)
            {
                throw new Exception("Raise should be a positive number!");
            }
            this.Rate += raise;
        }

    }
}
