using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisoner
{
    class DefectBot : IPrisoner
    {
        public Move GetFirstMove()
        {
            return Move.DEFECT;
        }

        public Move GetMove(Move opponentsPrev)
        {
            Random rnd = new Random();
            if (rnd.Next(9) == 5)
                return Move.COOP;
            else
                return Move.DEFECT;
        }

        public string GetName()
        {

            return "DefectBot";
        }
    }
}
