using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace Managers
{
    class GameManager
    {
        public static GameManager Instance = new GameManager();

        public Table table = new Table(10);

        private GameManager() { }




        public void StartGame()
        {
            table.GameNumber++;
            UserManager.Instance.SolvencyControl(table.Bank.BigBlind);
            if (UserManager.Instance.Users.Count <= 1)
            {
                IOManager.Instance.PlayerWin(UserManager.Instance.Users[0]);
                return;
            }
            IOManager.Instance.StartingGame();

            for (int i = 0; i < table.Players.Count; i++)
            {
                table.Players[i].Hand = table.CardDeck.GetHand();
            }

            table.Players[table.CurrentPlayer].Money -= table.Bank.SmallBlind;

            table.Bank.TableBank += table.Bank.SmallBlind;

            NextPlayer();
            RateToBank(table.CurrentPlayer);
            NextPlayer();

            IOManager.Instance.RefreshingPlayers();
            IOManager.Instance.RefreshingTable(table);

            StartRound();
        }



        public void StartRound()
        {
            if (UserManager.Instance.Users.Count <= 1)
            {
                IOManager.Instance.PlayerWin(UserManager.Instance.Users[0]);
                return;
            }
            IOManager.Instance.StartingRound();
            if (table.RoundType == RoundType.Flop)
            {
                table.Cards = table.CardDeck.GetFlop();
            }
            else
            {
                table.Cards.Add(table.CardDeck.TopCard());
            }
            IOManager.Instance.RefreshingTable(table);
            IOManager.Instance.RefreshingPlayers();
            table.RoundType = (RoundType)(((int)table.RoundType + 1) % 3);
        }

        public void EndofGame()
        {
            table.Players.Last().Money += table.Bank.TableBank;
            table.Bank.TableBank = 0;
            table.Players.Clear();
            table.Players.AddRange(UserManager.Instance.Users);
        }


        public void RateToBank(int playerNumber)
        {
            if (table.Players[playerNumber].Money > table.Bank.Rate)
            {
                table.Players[playerNumber].Money -= table.Bank.Rate;
                table.Bank.TableBank += table.Bank.Rate;
            }
            else
            {
                new Exception("Error! Not enough money.");
            }
        }

        public string RateOfPayer(int playerNumber, PlayersCommand command, int raise)
        {
            switch (command)
            {
                case PlayersCommand.call:
                    RateToBank(playerNumber);
                    table.RatePlayers++;
                    NextPlayer();
                    break;
                case PlayersCommand.raise:
                    for (int i = 0; i < table.Players.Count; i++)
                    {
                        if (table.Players[i].Money < table.Bank.Rate + raise)
                        {
                            return "Player" + (i + 1) + "is not enough money!";
                        }
                    }
                    table.Bank.RaiseRate(raise);
                    RateToBank(playerNumber);
                    table.RatePlayers = 1;
                    NextPlayer();
                    break;
                case PlayersCommand.fold:
                    table.Players.RemoveAt(playerNumber);
                    break;
                default:
                    return "Invalid command!";
            }
            return command.ToString();
        }

        public void NextPlayer()
        {
            table.Players[table.CurrentPlayer].IsCurrent = false;
            table.CurrentPlayer = (table.CurrentPlayer + 1) % table.Players.Count;
            table.Players[table.CurrentPlayer].IsCurrent = true;

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
