using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_21
{
    public class Game
    {
        private Deck deck;
        private Player player;
        private Player computer;
        private bool playAgain;
        
        public Game()
        {
            deck = new Deck();
            player = new Player();
            computer = new Player();
            playAgain = true;
        }

        public void Run()
        {
            deck.ShuffleDeck();

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

                    if (player.CalculateScore() == 21 || player.Hand.Count(card => card.Rank == Rank.Ace) == 2)
                    {
                        Console.WriteLine("\nYou win with 21 points or 2 Aces!");
                        break;
                    }

                    if (player.CalculateScore() > 21)
                    {
                        break;
                    }

                    if (computer.CalculateScore() == 21 || computer.Hand.Count(card => card.Rank == Rank.Ace) == 2)
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

                DetermineWinner();

                Console.WriteLine("\nDo you want to play again? (y/n): ");
                string playAgainInput = Console.ReadLine().ToLower();
                playAgain = (playAgainInput == "y");
            }

            GameStatistics();
            Console.ReadLine();
        }

        private void DetermineWinner()
        {
            int playerScore = player.CalculateScore();
            int computerScore = computer.CalculateScore();

            if (playerScore <= 21 && (playerScore > computerScore || computerScore > 21))
            {
                Console.WriteLine($"\nYou win!");
                player.GamesWon++;
            }
            else if (computerScore <= 21 && (computerScore > playerScore || playerScore > 21))
            {
                Console.WriteLine($"\nComputer wins!");
                computer.GamesWon++;
            }
            else if (playerScore <= 21 && computerScore <= 21 && playerScore == computerScore)
            {
                Console.WriteLine("\nIt's a draw!");
            }
        }

        private void GameStatistics()
        {
            Console.WriteLine("\nGame Statistics:");
            Console.WriteLine($"Your Games Won: {player.GamesWon}");
            Console.WriteLine($"Computer's Games Won: {computer.GamesWon}");
        }
    }
}
