using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CACPPN.CA.Interfaces;

namespace CACPPN.CA.BaseTypes
{
    class BoundaryCondition : IBoundaryCondition
    {
        public virtual void HandleBoundaryCellCase(ICell cell)
        {
            throw new Exception("Base boundary case don't do shitt!");
        }
    }
}
