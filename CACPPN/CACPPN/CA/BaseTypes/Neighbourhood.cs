using System;
using System.Collections.Generic;
using CACPPN.CA.Interfaces;
using CACPPN.CA.Utils.Coordinates;

namespace CACPPN.CA.BaseTypes
{
    class Neighbourhood : INeighbourhood
    {
        ICollection<ICell> _neighbors;
        public virtual ICollection<ICell> Neighbors
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual void makeNeighbourhoodWithWidth(int stepsFromCell, ICoordinate cellCoordinates, CPPNCoordinateScale coordinateStep)
        {
            throw new NotImplementedException("Each type of neighbourhood needs its own maker");
        }
    }
}
