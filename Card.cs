using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_21
{
    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public enum Rank
    {
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public struct Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }

        public int Value
        {
            get
            {
                if (Rank == Rank.Ace)
                    return 11;
                else if (Rank == Rank.King)
                    return 4;
                else if (Rank == Rank.Queen)
                    return 3;
                else if (Rank == Rank.Jack)
                    return 2;
                else
                    return (int)Rank;
            }
        }
    }
}
