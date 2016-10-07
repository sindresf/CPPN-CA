using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CACPPN.CA.Interfaces
{
    interface ICell
    {
        ICoordinate GridPosition { get; set; }
        INeighbourhood Neighbourhood { get; set; }
        double State { get; set; }
    }
}
