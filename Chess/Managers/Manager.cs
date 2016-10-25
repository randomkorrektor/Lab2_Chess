using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Managers
{
    public class Manager
    {
        public static Manager Instance = new Manager();
        private Manager() { }

        public static List<Player> Users = new List<Player>();
        public static CardDeck CardDeck = new CardDeck();
        public static Table Table;
        public Timer timer;
        public static List<SystemMessage> SystemMessageList = new List<SystemMessage>();
        public static int ratePlayers = 0;
        public static int Button = 0;
        public static int currentPlayer = 1;

        TimerCallback timeCB = new TimerCallback(GameStart);


        public List<Player> AddUser(string name)
        {
            Users.Add(new Player(name, 10000));
            if (Users.Count == 1)
            {
                timer = new Timer(GameStart, null, 120000, Timeout.Infinite);
            }
            if (Users.Count > 4)
            {
                timer.Dispose();
                GameStart(true);
            }
            return Users;

        }

        public static void SolvencyControl(int rise)
        {
            int outP = 0;
            for (int i = 0; i < Users.Count; i++)
            {

                if (Users[i].Money < rise)
                {
                    SystemMessageList.Add(new SystemMessage("Not enough money to continue the game.", i + outP));
                    Users.RemoveAt(i);
                    outP++;
                }
            }
            outP = 0;
        }

        public static void RateToBank(Player player)
        {
            if(player.Money > Table.Bank.Rate)
            {
                player.Money -= Table.Bank.Rate;
                Table.Bank.TableBank += Table.Bank.Rate;
            }
            else
            {
                new Exception("Error! Not enough money.");
            }
        }

        public string RateOfPayer(Player player,string answer,int raise)
        {
            switch (answer)
            {
                case "Call":
                    RateToBank(player);
                    ratePlayers++;
                    NextPlayer();
                    break;
                case "Raise":
                    for (int i = 0; i < Table.Players.Count; i++)
                    {
                        if (Table.Players[i].Money < Table.Bank.Rate + raise)
                        {
                            return "Player" + (i + 1) + "is not enough money!";
                        }
                    }
                    Table.Bank.RaiseRate(raise);
                    RateToBank(player);
                    ratePlayers = 1;
                    NextPlayer();
                    break;
                case "Fold":
                    Table.Players.Remove(player);
                    break;
            default:
                    return "Invalid command!";
            }
            return answer;
        }

        public static void NextPlayer()
        {
            currentPlayer = (currentPlayer + 1) % Table.Players.Count;
        }

        public static void GameStart(object state)
        {
            SolvencyControl(10);
            Table = new Table(Users.ToArray(), 10);
            while(Users.Count>1)
            {
                SolvencyControl(10);
                for (int i = 0; i < Table.Players.Count; i++)
                {
                    Table.Players[i].Hand = CardDeck.GetHand();
                }
                Table.Players[currentPlayer].Money -= Table.Bank.SmallBlind;
                Table.Bank.TableBank += Table.Bank.SmallBlind;
                NextPlayer();
                RateToBank(Table.Players[currentPlayer]);
                NextPlayer();

                
                while (ratePlayers < Table.Players.Count)
                {
                    //TODO: Разобраться с запрос-ответом
                    //RateOfPayer(Table.Players[currentPlayer], answer, raise);       
                }

                if (Table.Players.Count>1)
                {
                    Table.Cards = CardDeck.GetFlop();
                    //Отдать всем карты
                    while (ratePlayers < Table.Players.Count)
                    {
                        //TODO: Разобраться с запрос-ответом
                        //RateOfPayer(Table.Players[currentPlayer], answer, raise);       
                    }
                }

                if (Table.Players.Count > 1)
                {
                    Table.Cards.Add(CardDeck.TopCard());
                    //Отдать всем Table.Cards
                    while (ratePlayers < Table.Players.Count)
                    {
                        //TODO: Разобраться с запрос-ответом
                        //RateOfPayer(Table.Players[currentPlayer], answer, raise);       
                    }
                }

                if (Table.Players.Count > 1)
                {
                    Table.Cards.Add(CardDeck.TopCard());
                    //Отдать всем Table.Cards
                    while (ratePlayers < Table.Players.Count)
                    {
                        //TODO: Разобраться с запрос-ответом
                        //RateOfPayer(Table.Players[currentPlayer], answer, raise);       
                    }
                }

                if (Table.Players.Count > 1)
                {
                    //TODO: Выбор старшей комбинации, перечисление денег, оповещение
                }

                Table.Players.Last().Money += Table.Bank.TableBank;
                Table.Bank.TableBank = 0;
                Table.Players.Clear();
                Table.Players.AddRange(Users);
            }

        }



        //int getCombination(int[] hand, int[] board)
        //{
        //    int[] allCard;
        //    if ((board == null) || (board.length == 0))
        //    {
        //        allCard = new int[hand.length];
        //        System.arraycopy(hand, 0, allCard, 0, hand.length);
        //    }
        //    else 
        //    {
        //        allCard = new int[hand.length + board.length];
        //        System.arraycopy(hand, 0, allCard, 0, hand.length);
        //        System.arraycopy(board, 0, allCard, hand.length,
        //        board.length);
        //    }
        //    int[] card = new int[allCard.length];
        //    int[] suite = new int[allCard.length];
        //    int[] suiteCount = new int[4];
        //    sortHand(allCard, card, suite, suiteCount);
        //    if (isRoyalFlush(card, suite, suiteCount) != -1)
        //    {
        //        return 117;
        //    }
        //    int result = isStraightFlush(card, suite, suiteCount);
        //    if (result != -1)
        //    {
        //        return 104 + result;
        //    }
        //    result = isQuads(card);
        //    if (result != -1)
        //    {
        //        return 91 + result;
        //    }
        //    result = isFullHouse(card);
        //    if (result != -1)
        //    {
        //        return 78 + result;
        //    }
        //    result = isFlush(card, suite, suiteCount);
        //    if (result != -1)
        //    {
        //        return 65 + result;
        //    }
        //    result = isStraight(card);
        //    if (result != -1)
        //    {
        //        return 52 + result;
        //    }
        //    result = isSet(card);
        //    if (result != -1)
        //    {
        //        return 39 + result;
        //    }
        //    result = isTwoPair(card);
        //    if (result != -1)
        //    {
        //        return 26 + result;
        //    }
        //    result = isOnePair(card);
        //    if (result != -1)
        //    {
        //        return 13 + result;
        //    }
        //    return isHighCard(card);
        //}
    }
}
