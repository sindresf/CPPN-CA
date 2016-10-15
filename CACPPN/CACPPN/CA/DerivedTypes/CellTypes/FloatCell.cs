using System.Collections.Generic;
using CACPPN.CA.BaseTypes;

namespace CACPPN.CA.DerivedTypes.CellTypes
{
    class Cell : AbstractCell //any name for this one that's more specific?!?
    {
        private int states;

        public readonly int i, j;

        public Cell(double value, int states)
        {
            _state = value;
            this.states = states;
        }

        public Cell(double value, int i, int j, int states)
        {
            _state = value;
            this.i = i;
            this.j = j;
            this.states = states;
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
                return _neighbourhood;
            }

            set
            {
                _neighbourhood = value;
            }
        }

        public List<double?> NeighbourhoodOldState
        {
            get
            {
                List<double?> neighState = new List<double?>();
                foreach (Cell cell in _neighbourhood)
                {
                    neighState.Add(cell.OldState);
                }
                return neighState;
            }
        }
    }
}
