using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    public class Card
    {
        public Lear Lear;
        public Rating Rating;
        public bool Open;

        public Card(Lear lear, Rating rating)
        {
            Lear = lear;
            Rating = rating;
            Open = false;
        }

        public Card Clone()
        {
            return new Card(Lear, Rating);
        }
    }
}
