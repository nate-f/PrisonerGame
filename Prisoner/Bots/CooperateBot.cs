using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisoner
{
    public class CooperateBot : IPrisoner
    {
        Random rnd = new Random();
        public Move GetFirstMove()
        {
            return Move.COOP;
        }

        public Move GetMove(Move opponentsPrev)
        {
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
