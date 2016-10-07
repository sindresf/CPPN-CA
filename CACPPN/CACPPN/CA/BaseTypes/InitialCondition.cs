using CACPPN.CA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CACPPN.CA.BaseTypes
{
    class InitialCondition : IInitialCondition
    {
        public void GetInitalRandom(ICellularSpace spaceToFill, double fillPercentage)
        {
            throw new NotImplementedException();
        }

        public void GetSingleCentreCell(ICellularSpace spaceToFill)
        {
            throw new NotImplementedException();
        }
    }
}
