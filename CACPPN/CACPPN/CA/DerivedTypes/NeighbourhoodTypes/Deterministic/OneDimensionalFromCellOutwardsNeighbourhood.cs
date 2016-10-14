using System;
using CACPPN.CA.BaseTypes;

namespace CACPPN.CA.DerivedTypes.NeighbourhoodTypes.Deterministic
{
    class OneDimensionalFromCellOutwardsNeighbourhood : Neighbourhood
    {
        public override void makeNeighbourhoodWithWidth(int stepsFromCell)
        {
            base.makeNeighbourhoodWithWidth(stepsFromCell);
            //TODO make this.
            //Has to take into account the meta characteristics of the neighbourhood type in question
        }
    }
}
