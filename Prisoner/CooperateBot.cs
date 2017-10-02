using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisoner
{
    class CooperateBot : IPrisoner
    {
        public Move GetFirstMove()
        {
            return Move.COOP;
        }

        public Move GetMove(Move opponentsPrev)
        {
            Random rnd = new Random();
            if (rnd.Next(9) == 5)
                return Move.DEFECT;
            else
                return Move.COOP;
        }

        public string GetName()
        {

            return "CooperateBot";
        }
    }
}
