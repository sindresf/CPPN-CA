using System;
using System.Collections.Generic;
using CACPPN.CA.BaseTypes;

namespace CACPPN.CA.DerivedTypes.CellTypes
{
    class TreeCell : AbstractCell
    {
        public override double CurrentState
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override double? FutureState
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

        public override List<AbstractCell> Neighbourhood
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

        public override List<double> NeighbourhoodCurrentState
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override AbstractCell SetFirstState(double value)
        {
            throw new NotImplementedException();
        }
    }
}
