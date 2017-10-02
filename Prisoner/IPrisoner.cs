using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisoner
{
    public interface IPrisoner
    {
        Move GetFirstMove();
        Move GetMove(Move opponentsPrev);
        string GetName();
    }
}
