using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CACPPN.CA.Interfaces
{
    interface ICoordinate
    {
        ICoordinate GetCoordinates();
        void SetCoordinates(double x, double y = 0, double z = 0);
    }
}
