using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model
{
    class CardDeck
    {
        Random rand = new Random();
        List<Card> deck = new List<Card>();
        List<Card> gameDeck = new List<Card>();

        public void DeckAdd(string lear, string raiting, int intLear, int intRaiting)
        {
            deck.Add(new Card(lear, raiting, intLear, intRaiting));
        }

        //MixDeck. Переделать!
        public void MakeMixList<t>(IList<t> list, IList<t> mixList)
        {
            SortedList<int, t> mixedList = new SortedList<int, t>();
            foreach (t item in list)
                mixedList.Add(rand.Next(), item);
            list.Clear();
            for (int i = 0; i < mixedList.Count; i++)
            {
                mixList.Add(mixedList.Values[i]);
            } 
        }

        public void MixDeck()
        {
            if (deck.Count < 1)
                throw new Exception("Deck is empty!");
            gameDeck.Clear();
            MakeMixList(deck,gameDeck);
        }

        public Card TopCard()
        {
            if(gameDeck.Count<1)
                    throw new Exception("Gamedeck is empty!");
            Card card = gameDeck[0];
            gameDeck.Remove(gameDeck[0]);
            return card;
        }

        public void Discard()
        {
            if (gameDeck.Count < 1)
                throw new Exception("Gamedeck is empty!");
            gameDeck.Remove(gameDeck[0]);
        }

        public Card[] GetHand()
        {
            Card[] hand = new Card[2];
            hand[0] = TopCard();
            hand[1] = TopCard();
            return hand;
        }
    }
}
