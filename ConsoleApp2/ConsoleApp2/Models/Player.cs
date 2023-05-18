using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Models
{
    internal class Player
    {
        public string Name;
        public List<Card> Cards = new List<Card>();//
        public int CountOfCards;

        public Player(string name, int _countOfCards)
        {
            Name = name;
            CountOfCards = _countOfCards;
        }
    }
}
