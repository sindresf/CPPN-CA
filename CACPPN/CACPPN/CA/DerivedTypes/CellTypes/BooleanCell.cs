using System;
using System.Collections.Generic;
using CACPPN.CA.BaseTypes;
namespace CACPPN.CA.DerivedTypes.CellTypes
{
    class BooleanCell : AbstractCell
    {
        private bool _state;
        private bool _oldState;

        public BooleanCell(double value)
        {
            State = value;
        }

        public override double? State
        {
            get
            {
                return _state ? 1 : 0;
            }
            set
            {   //TODO decide if I should enforce some kind of "determinism" on the system by defining 0 as < 0.2 and 1 as > 0.8 ? or whatever
                if (value <= 1.00 && value >= 0.500001)
                    _state = true;
                else if (value >= 0.00 && value <= 0.5000009)
                    _state = false;
                else
                {
                    throw new ArgumentException("negative double value unacceptable!");
                }
            }
        }

        public override double? OldState
        {
            get
            {
                return _oldState ? 1 : 0;
            }
            /*set TODO to be part of State set
            {   //TODO decide if I should enforce some kind of "determinism" on the system by defining 0 as < 0.2 and 1 as > 0.8 ? or whatever
                if (value <= 1.00 && value >= 0.500001)
                    _oldState = true;
                else if (value >= 0.00 && value <= 0.5000009)
                    _oldState = false;
                else
                {
                    throw new ArgumentException("negative double value unacceptable!");
                }
            }*/
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
    }
}
