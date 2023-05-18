using ConsoleApp2.Models;
using System.Drawing;
using static ConsoleApp2.Program;
using static System.Reflection.Metadata.BlobBuilder;

namespace ConsoleApp2
{
    internal class Program
    {

        public static int GetCountOfCardsDependsOfCountOfPeople(Deck deck, int countOfPlayers)
        {
            if (deck.Count % countOfPlayers != 0)
            {
                deck.Count--;
                GetCountOfCardsDependsOfCountOfPeople(deck, countOfPlayers);
            }

            return deck.Count;
        }

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter count of players:");

                    Statistic statistic = new Statistic()
                    {
                        CountOfPlayers = Convert.ToInt32(Console.ReadLine())
                    };                    

                    if (statistic.CountOfPlayers > 13 || statistic.CountOfPlayers < 2)
                        throw new Exception("DESC IS SMALL FOR YOUR COUNT OF PLAYERS");

                    Deck deck = new Deck();
                    deck.CreateDesc();
                    Random rand = new Random();

                    deck.Count = GetCountOfCardsDependsOfCountOfPeople(deck, statistic.CountOfPlayers);//определяем количество карт исходя из количества игроков

                    if (deck.Count != 52)
                    {
                        deck.Cards.RemoveAt(deck.Count - 1);//удаляем ненужные карты
                    }

                    Console.WriteLine("Enter your name");
                    string name = Console.ReadLine();

                    int _countOfCards = deck.Count / statistic.CountOfPlayers;//количество карт у игрока
                    statistic = statistic.DistributionOfCards(_countOfCards, statistic, rand, deck, name);//раздача карт

                    int countOfFour = 0;
                    while (true)
                    {//вот тебе выбор 4к

                        Console.WriteLine("Do you want to choose count of four for the win?");
                        Console.WriteLine("(0)Yes (1)No");
                        int ifer = Convert.ToInt32(Console.ReadLine());

                        if (ifer != 1 && ifer != 0)                          
                            Console.WriteLine("ERROR: Enter rirgt number");

                        if(ifer == 1)
                        {
                            countOfFour = _countOfCards / 4;//количнство 4к для выйгрыша
                            break;
                        }
                           
                        if(ifer == 0)
                        {
                            Console.WriteLine("Enter count of four:");;                            
                            countOfFour = Convert.ToInt32(Console.ReadLine());

                            if(countOfFour <= _countOfCards / 4)
                                break;

                            Console.WriteLine("ERROR: We can't collect so many identical cards");
                            Console.WriteLine("Enter right number");
                        }

                    }
                    
                    while (true)
                    {
                        statistic = statistic.SortByName(statistic);

                        Console.WriteLine("Your desc:");
                        for (int i = 0; i < statistic.Players.First().Cards.Count; i++)
                        {
                            Console.WriteLine($"({i}) {statistic.Players.First().Cards[i].Name}" +
                                $" {statistic.Players.First().Cards[i].Suit}");
                        }

                        /*for (int j = 1; j < statistic.Players.Count; j++)  
                        {
                            Console.WriteLine($"{statistic.Players[j].Name} cards:");
                            Console.WriteLine("/////////////////////////////");
                                
                            for (int i = 0; i < statistic.Players[j].Cards.Count; i++)
                            {
                                Console.WriteLine($"({i}) {statistic.Players[j].Cards[i].Name}" +
                                    $" {statistic.Players[j].Cards[i].Suit}");
                            }
                            Console.WriteLine("/////////////////////////////");
                        }*/

                        statistic.CheckWin(statistic, countOfFour);

                        Console.WriteLine("Choose number of the card:");
                        int cardNumber = Convert.ToInt32(Console.ReadLine());

                        if (cardNumber >= statistic.Players.First().Cards.Count || cardNumber < 0)
                            throw new Exception("FACKING DUMP WRONG NUMBER OF THE CARD");


                        List<Card> dropedCards = statistic.TransferCards(cardNumber, statistic, countOfFour);//скинутые карты

                        statistic.TransferDroppedCards(dropedCards, statistic);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("You are DUMP, just play, do not break my game. Check error:" + ex.ToString());
                }
            }
        }
    }
}
