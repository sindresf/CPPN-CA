using System;
using CACPPN.CA.BaseTypes;
using CACPPN.CA.Interfaces;
using CACPPN.CA.Utils.Coordinates;

namespace CACPPN.CA.DerivedTypes.NeighbourhoodTypes.Deterministic
{
    class OneDimensionalFromCellOutwardsNeighbourhood : Neighbourhood
    {
        public override void makeNeighbourhoodWithWidth(int stepsFromCell, ICoordinate cellCoordinates, CPPNCoordinateScale coordinateStep)
        {
            base.makeNeighbourhoodWithWidth(stepsFromCell, cellCoordinates, coordinateStep);
            //TODO make this.
            //Has to take into account the meta characteristics of the neighbourhood type in question
        }
    }
}
