using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CACPPN.CA.BaseTypes;

namespace CACPPN.CA.DerivedTypes.CellTypes
{
    class BooleanCell : Cell
    {
        private bool _state;
        public override double State
        {
            get
            {
                return _state ? 1 : 0;
            }
            set
            {
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
