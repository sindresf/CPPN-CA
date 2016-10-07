using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CACPPN.CA.Interfaces
{
    interface IBoundaryCondition
    {
        void HandleBoundaryCellCase(ICell cell); //needs some argument no? maybe each boundary rule type can figure it out
    }
}
