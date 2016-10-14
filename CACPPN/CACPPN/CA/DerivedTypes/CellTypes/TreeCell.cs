using System;
using System.Collections.Generic;
using CACPPN.CA.BaseTypes;

namespace CACPPN.CA.DerivedTypes.CellTypes
{
    class TreeCell : AbstractCell
    {
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

        public override double? OldState
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override double? State
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
    }
}
