using CACPPN.CA.Utils.Coordinates;
using System.Collections.Generic;

namespace CACPPN.CA.Interfaces
{
    interface INeighbourhood
    {
        ICollection<ICell> Neighbors { get; set; }

        void makeNeighbourhoodWithWidth(int stepsFromCell, ICoordinate cellCoordinates, CPPNCoordinateScale coordinateStep);
    }
}
