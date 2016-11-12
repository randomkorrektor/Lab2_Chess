using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    public class Table
    {
        public int GameNumber = 0;
        public RoundType RoundType = RoundType.Flop;
        public int Button = 0;
        public int CurrentPlayer = 1;
        public int RatePlayers = 0;


        public List<Player> Players = new List<Player>();
        public List<Card> Cards = new List<Card>();
        public CardDeck CardDeck = new CardDeck();
        public Bank Bank;
        public Table( int smallBlind )
        {
            Bank = new Bank(smallBlind);
        }


        public void SetSmallBlind(int smallBlind)
        {
            Bank.SetSmallBlind(smallBlind);
        }

        public void SetCards(List<Card> cards)
        {
            Cards = cards;
        }
    }
}
