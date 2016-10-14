using System.Collections.Generic;

namespace CACPPN.CA.BaseTypes
{
    abstract class AbstractCell
    {
        protected List<AbstractCell> _neighbourhood;

        public abstract List<AbstractCell> Neighbourhood { get; set; }

        protected double? _state, _oldstate;

        public abstract double? State { get; set; }
        public abstract double? OldState { get; }

    }
}
