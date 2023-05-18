using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Models
{
    internal class Statistic
    {
        public int CountOfPlayers;
        public List<Player> Players = new List<Player>();


        public Statistic DistributionOfCards(int _countOfCards, Statistic statistic, Random rand, Deck deck, string name)
        {
            Player player1 = new Player(name, _countOfCards);
            statistic.Players.Add(player1);
            for (int j = 0; j < player1.CountOfCards; j++)
            {
                int playerCardCounter = rand.Next(0, deck.Count);// рандомный индекс карты в колоде
                player1.Cards.Add(deck.Cards[playerCardCounter]);
                deck.Cards.Remove(deck.Cards[playerCardCounter]);
                deck.Count--;
            }

            string playerName = null;
            for (int i = 1; i < statistic.CountOfPlayers; i++)
            {//раздача карт игрокам
                playerName = "Bot" + i;
                Player player = new Player(playerName, _countOfCards);

                for (int j = 0; j < player.CountOfCards; j++)
                {
                    int playerCardCounter = rand.Next(0, deck.Count);// рандомный индекс карты в колоде
                    player.Cards.Add(deck.Cards[playerCardCounter]);
                    deck.Cards.Remove(deck.Cards[playerCardCounter]);
                    deck.Count--;
                }

                statistic.Players.Add(player);

            }

            return statistic;
        }

        public int[] MinCardCheck(int repetition, int index, int[] mas)
        {
            if (mas[1] > repetition)
            {
                mas[1] = repetition;
                mas[0] = index;
            }
            return mas;
        }

        public List<Card> TransferCards(int YourCardNumber, Statistic statistic, int countOfFour)
        {//узнаем карты, которые нужно поменять
            List<Card> dropedCards = new List<Card>();//лист карт, которые мы хотим заменить
            int repetition = 0;//количество повторений 1й карты
            dropedCards.Add(statistic.Players.First().Cards[YourCardNumber]);//отсчет в массиве начинается с 0 
            statistic.Players.First().Cards.Remove(statistic.Players.First().Cards[YourCardNumber]);

            int[] mas = new int[2];//mas[0] - индекс  mas[1] - количество повторений
            mas[0] = int.MaxValue;
            mas[1] = int.MaxValue;

            for (int j = 1; j < statistic.Players.Count; j++)
            {
                for (int i = 0; i < statistic.Players[j].Cards.Count; i++)
                {
                    repetition++;
                    if (i == statistic.Players[j].Cards.Count - 1)
                    {
                        if (statistic.Players[j].Cards[i].Name == statistic.Players[j].Cards[i - 1].Name)
                        {
                            repetition++;
                            mas = MinCardCheck(repetition, i, mas);
                            break;
                        }

                        //если ласт карта не повторяется
                        repetition = 1;
                        mas = MinCardCheck(repetition, i, mas);
                        break;
                    }

                    if (statistic.Players[j].Cards[i].Name != statistic.Players[j].Cards[i + 1].Name)
                    {
                        mas = MinCardCheck(repetition, i, mas);
                        repetition = 0;
                    }

                }

                dropedCards.Add(statistic.Players[j].Cards[mas[0]]);
                statistic.Players[j].Cards.Remove(
                    statistic.Players[j].Cards[mas[0]]);
                mas[1] = 5;
                repetition = 0;
                
            }
            return dropedCards;//лист с картами, которые мы поменяем
        }

        public void TransferDroppedCards(List<Card> dropedCards, Statistic statistic)
        {
            for(int i = 0; i < statistic.Players.Count; i++)
            {
                if(i == 0)
                    statistic.Players[0].Cards.Add(dropedCards.Last());//первому последнюю карту

                if(i != 0)
                    statistic.Players[i].Cards.Add(dropedCards[i-1]);
            }
        }
  
        public void CheckWin(Statistic statistic, int countOfFour)
        {
            int repetition = 0;
            int winCount = 0;
            List<int> winList = new List<int>();

            for (int j = 0; j < statistic.CountOfPlayers; j++)
            {
                repetition = 0;
                winCount = 0;
                for (int i = 0; i < statistic.Players[j].Cards.Count; i++)
                {
                    repetition++;
                    if (i == statistic.Players[j].Cards.Count - 1)
                    {
                        if (statistic.Players[j].Cards[i].Name == statistic.Players[j].Cards[i - 1].Name)
                        {
                            //repetition++;
                            winList.Add(repetition);
                            break;
                        }

                        //если ласт карта не повторяется
                        repetition = 1;
                        winList.Add(repetition);
                        break;
                    }

                    if (statistic.Players[j].Cards[i].Name != statistic.Players[j].Cards[i + 1].Name)
                    {
                        winList.Add(repetition);
                        repetition = 0;
                    }
                }

                foreach (var win in winList)
                {
                    if (win == 4)
                        winCount++;

                    if (winCount == countOfFour)
                    {
                        Console.WriteLine();
                        Console.WriteLine("/////////////////////////////////////////////////");
                        Console.WriteLine($"{statistic.Players[j].Name}" + "WINN!!!!!!!");
                        Console.WriteLine("/////////////////////////////////////////////////");
                        Console.WriteLine();
                        foreach (var pl in statistic.Players)
                        {
                            Console.WriteLine("////////////");
                            Console.WriteLine(pl.Name + " cards:");
                            foreach (var card in pl.Cards)
                            {
                                Console.WriteLine(card.Name + " " + card.Suit);
                            }
                        }
                        System.Environment.Exit(0);//стоп программы
                    }
                }

                winList.Clear();
            }
        }

        public Statistic SortByName(Statistic statistic)
        {//сортировка карт по имени
            for (int i = 0; i < statistic.Players.Count; i++)
            {
                statistic.Players[i].Cards = statistic.Players[i].Cards.OrderBy(card => card.Name).ToList();
            }

            return statistic;
        }
    }
}
