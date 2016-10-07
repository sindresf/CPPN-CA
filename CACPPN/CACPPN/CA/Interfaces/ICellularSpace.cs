using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CACPPN.CA.Interfaces
{
    interface ICellularSpace
    {
        ICellularSpace getSpaceNow();

        // The 1D and 2D only cares about their respect dimensions, no problem
        void ChangeSpaceBy(int width, int height = 0, int depth = 0);

        //TODO those the cellular space contain the "how to get neighbours" code?
        // or is that a part of the neighbourhood itself? it knows how to construct itself? probably
    }
}
