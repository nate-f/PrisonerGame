using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisoner
{
    public class ForgivingBot : IPrisoner
    {
        private bool forgive;

        public Move GetFirstMove()
        {
            forgive = false;
            return Move.COOP;
        }

        public Move GetMove(Move opponentsPrev)
        {
            if (opponentsPrev == Move.COOP)
                return Move.COOP;
            else if (forgive == true)
            {
                forgive = false;
                return Move.COOP;
            }
            else
            {
                forgive = true;
                return Move.DEFECT;
            }
                
        }

        public string GetName()
        {

            return "ForgivingBot";
        }
    }
}
