using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CACPPN.CA.BaseTypes;
using CACPPN.CA.Interfaces;
namespace CACPPN.CA.DerivedTypes.CellTypes
{
    //The whole boolean concept is actually abstracted away,
    // interacting with the rest of the program through doubles as everything else.
    class BooleanCell : Cell
    {
        private bool _state;

        public BooleanCell(ICoordinate coords, double initialValue) : base(coords)
        {
            State = initialValue;
        }

        public override double State
        {
            get
            {
                return _state ? 1 : 0;
            }
            set
            {   //TODO decide if I should enforce some kind of "determinism" on the system by defining 0 as < 0.2 and 1 as > 0.8 ? or whatever
                if (value > 0.00 && value >= 0.500001)
                    _state = true;
                else if (value > 0.00 && value <= 0.5000009)
                    _state = false;
                else
                {
                    throw new ArgumentException("negative double value unacceptable!");
                }
            }
        }

    }
}
