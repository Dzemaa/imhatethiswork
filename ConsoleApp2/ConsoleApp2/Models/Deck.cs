
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Models
{
    internal class Deck
    {
        public int Count = 52;
        public List<Card> Cards = new List<Card>();///

        public void CreateDesc()
        {
            string[] test = { "spades", "diamonds", "clubs", "hearts" };
            string[] cardss = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            for (int i = 0; i < cardss.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Card card = new Card()
                    {
                        Name = cardss[i],
                        Suit = test[j]
                    };

                    Cards.Add(card);
                };

            }
        }
    }
}
