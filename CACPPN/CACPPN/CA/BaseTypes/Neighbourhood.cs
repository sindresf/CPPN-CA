using System;
using System.Collections.Generic;

namespace CACPPN.CA.BaseTypes
{
    class Neighbourhood
    {

        public virtual void makeNeighbourhoodWithWidth(int stepsFromCell)
        {
            throw new NotImplementedException("Each type of neighbourhood needs its own maker");
        }
    }
}
