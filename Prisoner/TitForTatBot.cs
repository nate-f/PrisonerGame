using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisoner
{
    public class TitForTatBot : IPrisoner
    {
        public Move GetFirstMove()
        {
            return Move.COOP;
        }

        public Move GetMove(Move opponentsPrev)
        {
            if (opponentsPrev == Move.COOP)
                return Move.COOP;
            else
                return Move.DEFECT;
        }

        public string GetName()
        {

            return "TitForTatBot";
        }
    }
}
