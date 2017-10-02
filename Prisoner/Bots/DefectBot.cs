using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisoner
{
    public class DefectBot : IPrisoner
    {
        Random rnd = new Random();
        public Move GetFirstMove()
        {
            return Move.DEFECT;
        }

        public Move GetMove(Move opponentsPrev)
        {

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
