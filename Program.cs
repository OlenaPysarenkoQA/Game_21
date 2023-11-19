using System;
using System.Collections.Generic;
using System.Linq;

public struct Card
{
    public string Suit { get; set; }
    public string Rank { get; set; }
    public int Value { get; set; }
}

internal class Deck
{
    private List<Card> cards;
    private int currentPlayerIndex;

    public List<Card> Cards { get { return cards; } }
    public int CurrentPlayerIndex { get { return currentPlayerIndex; } }

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

        string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
        string[] ranks = { "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

        foreach (string suit in suits)
        {
            foreach (string rank in ranks)
            {
                int value;

                if (rank == "Ace")
                    value = 11;
                else if (rank == "King")
                    value = 4;
                else if (rank == "Queen")
                    value = 3;
                else if (rank == "Jack")
                    value = 2;
                else
                    value = int.Parse(rank);

                newDeck.Add(new Card { Suit = suit, Rank = rank, Value = value });
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
        Dictionary<string, int> rankOrder = new Dictionary<string, int>
    {
        {"6", 0}, {"7", 1}, {"8", 2}, {"9", 3}, {"10", 4},
        {"Jack", 5}, {"Queen", 6}, {"King", 7}, {"Ace", 8}
    };

        cards = cards.OrderBy(card => rankOrder[card.Rank])
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
            if (cards[i].Rank == "Ace")
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
        List<Card> spadesCards = cards.Where(card => card.Suit == "Spades").ToList();
        cards.RemoveAll(card => card.Suit == "Spades");
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

internal class Player
{
    public List<Card> Hand { get; set; }
    public int GamesWon { get; set; }

    public Player()
    {
        Hand = new List<Card>();
        GamesWon = 0;
    }

    public int CalculateScore()
    {
        int score = Hand.Sum(card => card.Value);
        int numAces = Hand.Count(card => card.Rank == "Ace");
        return score;
    }

    public void ClearHand()
    {
        Hand.Clear();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Deck deck = new Deck();
        Player player = new Player();
        Player computer = new Player();
        bool playAgain = true;

        Console.WriteLine("Task 1. Ordered Deck by value: ");
        deck.GenerateOrderedDeck();
        deck.PrintDeck();

        Console.WriteLine("\nTask 2. Shuffle Deck: ");
        deck.ShuffleDeck();
        deck.PrintDeck();

        Console.WriteLine("\nTask 3. Find Positions of Aces: ");
        deck.FindAllAcesPositions();

        Console.WriteLine("\nTask 4. Move Spades to Beginning: ");
        deck.MoveSpadesToBeginning();
        deck.PrintDeck();

        Console.WriteLine("\nTask 5. Sortered Deck: ");
        deck.SortDeck();
        deck.PrintDeck();
        Console.WriteLine("\nPress enter to start the game!");
        Console.ReadLine();

        deck.ShuffleDeck();

        Console.Clear();

        while (playAgain)
        {
            Console.Clear();
            player.ClearHand();
            computer.ClearHand();

            Console.Write("Who gets the initial cards? (1 - You, 2 - Computer): ");
            int initialPlayerChoice;
            while (!int.TryParse(Console.ReadLine(), out initialPlayerChoice) || (initialPlayerChoice != 1 && initialPlayerChoice != 2))
            {
                Console.WriteLine("Invalid input. Please enter 1 or 2.");
            }

            deck.DealCards(computer, 2, ref playAgain);
            deck.DealCards(player, 2, ref playAgain);

            while (true)
            {
                Console.WriteLine($"Your hand:");
                foreach (Card card in player.Hand)
                {
                    Console.WriteLine($"{card.Rank} of {card.Suit}");
                }
                Console.WriteLine($"Total Score: {player.CalculateScore()}");

                if (player.CalculateScore() == 21 || player.Hand.Count(card => card.Rank == "Ace") == 2)
                {
                    Console.WriteLine("\nYou win with 21 points or 2 Aces!");
                    break;
                }

                if (player.CalculateScore() > 21)
                {
                    break;
                }

                if (computer.CalculateScore() == 21 || computer.Hand.Count(card => card.Rank == "Ace") == 2)
                {
                    Console.WriteLine("\nComputer wins with 21 points or 2 Aces!");
                    break;
                }

                if (computer.CalculateScore() > 21)
                {
                    break;
                }

                Console.WriteLine("\nInput 1 for get another card or 2 to stop receiving cards:");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
                {
                    Console.WriteLine("Invalid input. Please enter 1 or 2.");
                }

                if (choice == 1)
                {
                    deck.DealCards(player, 1, ref playAgain);
                    if (!playAgain)
                    {
                        break;
                    }
                }
                else if (choice == 2)
                {
                    break;
                }

                if (computer.CalculateScore() < 19)
                {
                    deck.DealCards(computer, 1, ref playAgain);
                    if (!playAgain)
                    {
                        break;
                    }
                }
            }

            Console.WriteLine($"\nYour hand:");
            foreach (Card card in player.Hand)
            {
                Console.WriteLine($"{card.Rank} of {card.Suit}");
            }
            Console.WriteLine($"Total Score: {player.CalculateScore()}");

            Console.WriteLine($"\nComputer's hand:");
            foreach (Card card in computer.Hand)
            {
                Console.WriteLine($"{card.Rank} of {card.Suit}");
            }
            Console.WriteLine($"Total Score: {computer.CalculateScore()}");

            if ((player.CalculateScore() <= 21 && player.CalculateScore() > computer.CalculateScore()) ||
                (computer.CalculateScore() > 21 && player.CalculateScore() <= 21) ||
                (player.CalculateScore() > 21 && computer.CalculateScore() > 21 && player.CalculateScore() < computer.CalculateScore()))
            {
                Console.WriteLine($"\nYou win!");
                player.GamesWon++;
            }
            else if ((computer.CalculateScore() <= 21 && computer.CalculateScore() > player.CalculateScore()) ||
                     (player.CalculateScore() > 21 && computer.CalculateScore() <= 21) ||
                     (computer.CalculateScore() > 21 && player.CalculateScore() > 21 && computer.CalculateScore() < player.CalculateScore()))
            {
                Console.WriteLine($"\nComputer wins!");
                computer.GamesWon++;
            }
            else if (player.CalculateScore() <= 21 && computer.CalculateScore() <= 21 && player.CalculateScore() < computer.CalculateScore())
            {
                Console.WriteLine($"\nYou win!");
                player.GamesWon++;
            }
            else if (player.CalculateScore() <= 21 && computer.CalculateScore() <= 21 && player.CalculateScore() > computer.CalculateScore())
            {
                Console.WriteLine($"\nComputer wins!");
                computer.GamesWon++;
            }
            else if (player.CalculateScore() == computer.CalculateScore())
            {
                Console.WriteLine("\nIt's a draw!");
            }

            Console.WriteLine("\nDo you want to play again? (y/n): ");
            string playAgainInput = Console.ReadLine().ToLower();
            playAgain = (playAgainInput == "y");
        }

        Console.WriteLine("\nGame Statistics:");
        Console.WriteLine($"Your Games Won: {player.GamesWon}");
        Console.WriteLine($"Computer's Games Won: {computer.GamesWon}");

        Console.ReadLine();
    }
}
