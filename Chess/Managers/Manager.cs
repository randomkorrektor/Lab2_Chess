using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace Managers
{
    public class Manager
    {
        public static Manager Instance = new Manager();

        private Manager()
        {
            timer.Elapsed += Timer_Elapsed;
            timer.Disposed += Timer_Disposed;
        }

        private List<Player> users = new List<Player>();
        private Table table;
        private Timer timer = new Timer(100);
        public event Action<int> TimerTick;
        public event Action StartGame;
        public event Action StartRound;
        public event Action RefreshTable;

        private event Action endOfTimer;






        public static int ratePlayers = 0;
        public static int Button = 0;
        public static int currentPlayer = 1;



        public List<Player> AddUser(string name, Action<Player> refresh)
        {
            users.Add(new Player(name, 10000, refresh));

            if (users.Count == 2)
            {
                timer.Start();
                endOfTimer += GameStart;
            }
            if (users.Count > 4)
            {
                timer.Dispose();
            }
            return users;

        }

        public void GameStart()
        {
            table = new Table(10);
            SolvencyControl();

            while (users.Count > 1)
            {
                SolvencyControl();
                for (int i = 0; i < table.Players.Count; i++)
                {
                    table.Players[i].Hand = table.CardDeck.GetHand();
                }
                table.Players[currentPlayer].Money -= table.Bank.SmallBlind;
                table.Bank.TableBank += table.Bank.SmallBlind;
                NextPlayer();
                RateToBank(table.Players[currentPlayer]);
                NextPlayer();


                while (ratePlayers < table.Players.Count)
                {
                    //TODO: Разобраться с запрос-ответом
                    //RateOfPayer(Table.Players[currentPlayer], answer, raise);       
                }

                if (table.Players.Count > 1)
                {
                    table.Cards = table.CardDeck.GetFlop();
                    //Отдать всем карты
                    while (ratePlayers < table.Players.Count)
                    {
                        //TODO: Разобраться с запрос-ответом
                        //RateOfPayer(Table.Players[currentPlayer], answer, raise);       
                    }
                }

                if (table.Players.Count > 1)
                {
                    table.Cards.Add(table.CardDeck.TopCard());
                    //Отдать всем Table.Cards
                    while (ratePlayers < table.Players.Count)
                    {
                        //TODO: Разобраться с запрос-ответом
                        //RateOfPayer(Table.Players[currentPlayer], answer, raise);       
                    }
                }

                if (table.Players.Count > 1)
                {
                    table.Cards.Add(table.CardDeck.TopCard());
                    //Отдать всем Table.Cards
                    while (ratePlayers < table.Players.Count)
                    {
                        //TODO: Разобраться с запрос-ответом
                        //RateOfPayer(Table.Players[currentPlayer], answer, raise);       
                    }
                }

                if (table.Players.Count > 1)
                {
                    //TODO: Выбор старшей комбинации, перечисление денег, оповещение
                }

                table.Players.Last().Money += table.Bank.TableBank;
                table.Bank.TableBank = 0;
                table.Players.Clear();
                table.Players.AddRange(users);
            }

        }

        public void StartRound(

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimerTick((int)e.SignalTime.Ticks);
            if ((int)e.SignalTime.Ticks >= 1200000)
            {
                timer.Dispose();
            }
        }

        private void Timer_Disposed(object sender, EventArgs e)
        {
            endOfTimer();
        }

        private void SolvencyControl()
        {
            foreach (var user in users)
            {

                if (user.Money < table.Bank.SmallBlind)
                {
                    users.Remove(user);
                }
            }
        }

        public void RateToBank(Player player)
        {
            if (player.Money > table.Bank.Rate)
            {
                player.Money -= table.Bank.Rate;
                table.Bank.TableBank += table.Bank.Rate;
            }
            else
            {
                new Exception("Error! Not enough money.");
            }
        }

        public string RateOfPayer(Player player, string answer, int raise)
        {
            switch (answer)
            {
                case "Call":
                    RateToBank(player);
                    ratePlayers++;
                    NextPlayer();
                    break;
                case "Raise":
                    for (int i = 0; i < table.Players.Count; i++)
                    {
                        if (table.Players[i].Money < table.Bank.Rate + raise)
                        {
                            return "Player" + (i + 1) + "is not enough money!";
                        }
                    }
                    table.Bank.RaiseRate(raise);
                    RateToBank(player);
                    ratePlayers = 1;
                    NextPlayer();
                    break;
                case "Fold":
                    table.Players.Remove(player);
                    break;
                default:
                    return "Invalid command!";
            }
            return answer;
        }

        public void NextPlayer()
        {
            currentPlayer = (currentPlayer + 1) % table.Players.Count;
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
