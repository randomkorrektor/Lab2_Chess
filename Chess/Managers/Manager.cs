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
        public static CardDeck CardDeck = new CardDeck();
        public static Table Table;

        TimerCallback timeCB = new TimerCallback(GameStart);


        public List<Player> AddPlayer(string name)
        {
            Players.Add(new Player(name, 10000));
            if(Players.Count==1)
            {
                Timer timer = new Timer(GameStart, null, 120000, Timeout.Infinite);
            }
            return Players;
        }

        


        public static void GameStart(object state)
        {
            Table = new Table(Players.ToArray(), 10);
        }


    }
}
