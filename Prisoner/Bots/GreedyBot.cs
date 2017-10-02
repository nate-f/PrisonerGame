using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisoner
{
    class GreedyBot : IPrisoner
    {
        Random rnd = new Random();
        public Move GetFirstMove()
        {
            return Move.COOP;
        }

        public Move GetMove(Move opponentsPrev)
        {
            
            if (rnd.Next(12) == 5)
                return Move.DEFECT;
            else if (opponentsPrev == Move.COOP)
                return Move.COOP;
            else
                return Move.DEFECT;
        }

        public string GetName()
        {

            return "GreedyBot";
        }
    }
}
