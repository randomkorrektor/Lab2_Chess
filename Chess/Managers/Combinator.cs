using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers
{
    public class Combinator
    {

        public int IsFRoyalFlush(List<Card> cards)
        {
            if(cards.Last().Rating == Rating.Ace && (int)(cards[2].Rating)>9)
            {

            }
            return -1;
        }
    }
}
