using CACPPN.CA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CACPPN.CA.Utils.Coordinates
{
    //Doubles because on CPPN level the convention is from -1 to 1
    //so needs a "CoordinateScaler" as well

    public struct CPPNCoordinateScale
    {
        public double scale;
    }
    struct OneDimensionCoordinate : ICoordinate
    {
        double x;
        public ICoordinate GetCoordinates()
        {
            return this;
        }

        public void SetCoordinates(double xCoord, double yCoord = 0, double zCoord = 0)
        {
            x = xCoord;
        }
    }
    struct TwoDimensionCoordinate : ICoordinate
    {
        double x, y;
        public ICoordinate GetCoordinates()
        {
            return this;
        }

        public void SetCoordinates(double xCoord, double yCoord = 0, double zCoord = 0)
        {
            x = xCoord;
            y = yCoord;
        }
    }
    struct ThreeDimensionCoordinate : ICoordinate
    {
        double x, y, z;
        public ICoordinate GetCoordinates()
        {
            return this;
        }

        public void SetCoordinates(double xCoord, double yCoord = 0, double zCoord = 0)
        {
            x = xCoord;
            y = yCoord;
            z = zCoord;
        }
    }
}
