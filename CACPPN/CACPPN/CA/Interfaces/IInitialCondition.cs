using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CACPPN.CA.Interfaces
{
    interface IInitialCondition
    {
        void GetInitalRandom(ICellularSpace spaceToFill, double fillPercentage);

        void GetSingleCentreCell(ICellularSpace spaceToFill);

        //and then it's up to the spaces to take their respective structure "hardcoded fillers"
    }
}
