using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CACPPN.CA.BaseTypes
{
    class BoundaryCondition
    {
        public virtual void HandleBoundaryCellCase(AbstractCell cell)
        {
            throw new Exception("Base boundary case don't do shitt!");
        }
    }
}
