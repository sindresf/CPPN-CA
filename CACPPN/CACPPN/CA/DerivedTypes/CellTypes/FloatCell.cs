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
            _currentState = value;
            this.states = states;
        }

        public Cell(int i, int j, int states)
        {
            this.i = i;
            this.j = j;
            this.states = states;
        }

        public override AbstractCell SetFirstState(double value)
        {
            _currentState = value;
            _futureState = value;
            return this;
        }

        public override double CurrentState
        {
            get { return _currentState; }
        }
        public override double? FutureState
        {
            get
            {
                return _futureState;
            }

            set
            {
                _currentState = value.GetValueOrDefault();
                _futureState = null;
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

        public override List<double> NeighbourhoodCurrentState
        {
            get
            {
                List<double> neighState = new List<double>();
                foreach (Cell cell in _neighbourhood)
                {
                    neighState.Add(cell.CurrentState);
                }
                return neighState;
            }
        }
    }
}
