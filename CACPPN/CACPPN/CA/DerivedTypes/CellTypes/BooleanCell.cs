using System;
using System.Collections.Generic;
using CACPPN.CA.BaseTypes;
namespace CACPPN.CA.DerivedTypes.CellTypes
{
    class BooleanCell : AbstractCell
    {
        public BooleanCell(int i) : base(i)
        {
        }
        public override double CurrentState
        {
            get
            {
                return _currentState;
            }
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
                if (_neighbourhood == null)
                    _neighbourhood = value;
            }
        }

        public override List<double> NeighbourhoodCurrentState
        {
            get
            {
                List<double> neighState = new List<double>();
                foreach (AbstractCell cell in _neighbourhood)
                {
                    neighState.Add(cell.CurrentState);
                }
                return neighState;
            }
        }

        public override AbstractCell SetFirstState(double value)
        {
            _currentState = value;
            _futureState = value;
            return this;
        }
    }
}
