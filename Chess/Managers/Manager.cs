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
            currentPlayer = currentPlayer + 1 % Table.Players.Count;
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

                int counter = 0;
                while (ratePlayers < Table.Players.Count)
                {
                    counter %= Table.Players.Count;
                    //TODO: Разобраться с запрос-ответом
                    //RateOfPayer(Table.Players[counter], answer, raise);       
                    counter++;
                }
                if(Table.Players.Count>1)
                {

                }
            }
        }
    }
}
