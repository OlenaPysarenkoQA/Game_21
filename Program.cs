using System;
using System.Collections.Generic;
using System.Linq;

namespace Game_21
{
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
            Console.ReadLine();
            
            Console.WriteLine("\nPress enter to start the game!");
            Console.ReadLine();

            Console.Clear();

            Game game = new Game();
            game.Run();
        }    
    }
        
}

