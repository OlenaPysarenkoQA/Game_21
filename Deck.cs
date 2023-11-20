using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_21
{
    public class Deck
    {
        private List<Card> cards;
        private int currentPlayerIndex;

        public List<Card> Cards => cards;
        public int CurrentPlayerIndex => currentPlayerIndex;

        public Deck()
        {
            ResetDeck();
            currentPlayerIndex = 0;
            ShuffleDeck();
        }

        private void ResetDeck()
        {
            cards = GenerateDeck();
            ShuffleDeck();
        }

        private List<Card> GenerateDeck()
        {
            List<Card> newDeck = new List<Card>();

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    newDeck.Add(new Card { Suit = suit, Rank = rank });
                }
            }

            return newDeck;
        }

        public void ShuffleDeck()
        {
            Random random = new Random();
            cards = cards.OrderBy(card => random.Next()).ToList();
        }

        public void SortDeck()
        {
            cards = cards.OrderBy(card => card.Rank)
                         .ThenBy(card => card.Suit)
                         .ToList();
        }

        public void OrderDeck()
        {
            cards = cards.OrderBy(card => card.Value).ToList();
        }

        public void GenerateOrderedDeck()
        {
            cards = GenerateDeck();
            OrderDeck();
        }

        public void PrintDeck()
        {
            foreach (Card card in cards)
            {
                Console.WriteLine($"{card.Rank} of {card.Suit}");
            }
        }

        public List<int> FindAllAcesPositions()
        {
            List<int> acePositions = new List<int>();

            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].Rank == Rank.Ace)
                {
                    acePositions.Add(i);
                }
            }

            Console.WriteLine("Positions of Aces:");
            foreach (int position in acePositions)
            {
                Console.Write($"{position} ");
            }
            Console.WriteLine();

            return acePositions;
        }

        public void MoveSpadesToBeginning()
        {
            List<Card> spadesCards = cards.Where(card => card.Suit == Suit.Spades).ToList();
            cards.RemoveAll(card => card.Suit == Suit.Spades);
            cards.InsertRange(0, spadesCards);
        }

        public void DealCards(Player player, int numCards, ref bool playAgain)
        {
            for (int i = 0; i < numCards; i++)
            {
                if (cards.Any())
                {
                    player.Hand.Add(cards.First());
                    cards.RemoveAt(0);
                }
                else
                {
                    Console.WriteLine("The deck is empty. Cannot deal more cards.");
                    Console.WriteLine("\nDo you want to play again? (y/n): ");
                    string playAgainInput = Console.ReadLine().ToLower();
                    playAgain = (playAgainInput == "y");
                    if (playAgain)
                        ResetDeck();
                    return;
                }
            }
        }

        public void SwitchPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % 2;
        }
    }
}
