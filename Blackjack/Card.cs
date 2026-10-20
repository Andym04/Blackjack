using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public class Card
    {
        public string Suit { get; set; }   // "Clubs", "Diamonds", "Hearts", "Spades"
        public string Rank { get; set; }   // "Ace", "2", "3", ..., "Jack", "Queen", "King"
        public int Value { get; set; }     // Base Blackjack value
        public string Name => $"{Rank} of {Suit}";

        public Card(string suit, string rank, int value)
        {
            Suit = suit;
            Rank = rank;
            Value = value;
        }

        public override string ToString() => Name;
    }
}
