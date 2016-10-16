using System.Collections.Generic;

namespace CACPPN.CA.BaseTypes
{
    abstract class AbstractCell
    {
        protected List<AbstractCell> _neighbourhood;

        public abstract List<AbstractCell> Neighbourhood { get; set; }

        protected double _currentState;
        protected double? _futureState;

        public abstract double CurrentState { get; }
        public abstract double? FutureState { get; set; }

        public abstract List<double> NeighbourhoodCurrentState { get; }

        public abstract AbstractCell SetFirstState(double value);
    }
}
