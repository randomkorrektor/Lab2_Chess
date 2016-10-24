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

        public static List<Player> Players = new List<Player>();
        public static List<Player> Users = new List<Player>();
        public static CardDeck CardDeck = new CardDeck();
        public static Table Table;
        public Timer timer;
        public List<SystemMessage> SystemMessageList = new List<SystemMessage>();
        public static int ratePlayers = 0;
        public static int Button = 0;
        public static int currentPlayer = 1;

        TimerCallback timeCB = new TimerCallback(GameStart);


        public List<Player> AddPlayer(string name)
        {
            Players.Add(new Player(name, 10000));
            Users.Add(Players.Last());
            if (Players.Count == 1)
            {
                timer = new Timer(GameStart, null, 120000, Timeout.Infinite);
            }
            if (Players.Count > 4)
            {
                timer.Dispose();
                GameStart(true);
            }
            return Players;

        }

        //Удалить в случае ненадобности
        //public void SolvencyControl()
        //{
        //    for (int i = 0; i < Players.Count; i++)
        //    {
        //        if (Players[i].Money < Table.Bank.BigBlind)
        //        {
        //            SystemMessageList.Add(new SystemMessage("Not enough money to continue the game.", i));
        //            Players.RemoveAt(i);
        //        }
        //    }
        //}

        public static bool RateToBank(Player player)
        {
            player.Money -= Table.Bank.Rate;
            Table.Bank.TableBank += Table.Bank.Rate;
            return true;
        }

        public string RateOfPayer(Player player,string answer,int raise)
        {
            switch (answer)
            {
                case "Call":
                    RateToBank(player);
                    break;
                case "Raise":
                    for (int i = 0; i < Players.Count; i++)
                    {
                        if (Players[i].Money < Table.Bank.Rate + raise)
                        {
                            return "Player" + i + 1 + "is not enough money!";
                        }
                    }
                    Table.Bank.RaiseRate(raise);
                    RateToBank(player);
                    break;
                case "Fold":
                    
                    break;
            default:
                    return "Invalid command!";
            }
            return answer;
        }

        public static void NextPlayer()
        {
            currentPlayer = currentPlayer + 1 % Players.Count;
        }

        public static void GameStart(object state)
        {
            Table = new Table(Players.ToArray(), 10);
            while(Players.Count>1)
            {
                for (int i = 0; i < Players.Count; i++)
                {
                    if (Players[i].Money < Table.Bank.BigBlind)
                    {
                        Players[i].Hand = CardDeck.GetHand();
                    }
                }
                Players[currentPlayer].Money -= Table.Bank.SmallBlind;
                Table.Bank.TableBank += Table.Bank.SmallBlind;
                NextPlayer();
                RateToBank(Players[currentPlayer]);
                NextPlayer();

                while (ratePlayers < Players.Count)
                {
                    //Ожидание ответа от игрока
                    
                }
            }
        }
    }
}
