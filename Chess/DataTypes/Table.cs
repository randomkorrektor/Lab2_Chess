using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    public class Table
    {
        public List<Player> Players = new List<Player>();
        public Card[] Cards = new Card[5];
        public Bank Bank;
        public Table(Player[] players, int smallBlind )
        {
            Players.AddRange(players);
            Bank = new Bank(smallBlind);
        }


        public void SetSmallBlind(int smallBlind)
        {
            Bank.SetSmallBlind(smallBlind);
        }
        public void SetCards(Card[] cards)
        {
            Cards = cards;
        }
    }
}
