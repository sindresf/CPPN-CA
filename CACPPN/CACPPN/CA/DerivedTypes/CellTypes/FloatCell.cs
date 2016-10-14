using System;
using System.Collections.Generic;
using CACPPN.CA.BaseTypes;

namespace CACPPN.CA.DerivedTypes.CellTypes
{
    class Cell : AbstractCell //any name for this one that's more specific?!?
    {
        private int states;

        public Cell(double value, int states)
        {
            this.states = states;
            _state = value;
        }

        public override double? State
        {
            get { return _state.GetValueOrDefault(); }
            set
            {
                _oldstate = _oldstate == null ? value : _state;
                _state = value;
            }
        }
        public override double? OldState
        {
            get
            {
                return _oldstate ?? _state;
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
    }
}
