using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    public class CardDeck
    {
        public List<Card> Deck = new List<Card>();
        public Stack<Card> GameDeck = new Stack<Card>();

        public CardDeck()
        {
            for(int i=0; i<4; i++)
            {
                for(int j=0; j<13; j++)
                {
                    Deck.Add(new Card((Lear)i, (Rating)j));
                }
            }
        }

        public void MakeMixList()
        {
            Random rand = new Random();
            SortedList<int, Card> mixedList = new SortedList<int, Card>();
            foreach (Card item in Deck)
                mixedList.Add(rand.Next(), item.Clone());
            for (int i = 0; i < mixedList.Count; i++)
            {
                GameDeck.Push(mixedList.Values[i]);
            }
        }

        public Card TopCard()
        {
            return GameDeck.Pop();
        }

        public void Discard()
        {
            GameDeck.Pop();
        }

        public Card[] GetHand()
        {
            return new Card[] { TopCard(), TopCard() };
        }

        public Card[] Get5Cards()
        {
            return new Card[] { TopCard(), TopCard(), TopCard(), TopCard(), TopCard() };
        }
    }
}
