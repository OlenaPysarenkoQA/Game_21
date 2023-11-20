using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_21
{
    public class Player
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
            return score;
        }
        public void ClearHand()
        {
            Hand.Clear();
        }
    }
}
