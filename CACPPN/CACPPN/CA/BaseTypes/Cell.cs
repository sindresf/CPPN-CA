using System;
using CACPPN.CA.Interfaces;

namespace CACPPN.CA.BaseTypes
{
    class Cell : ICell
    {

        protected INeighbourhood _neighbourhood;
        protected ICoordinate _gridPosition;

        public ICoordinate GridPosition
        {
            get { return _gridPosition; }

            private set
            {
                _gridPosition = value;
            }
        }

        public Cell()
        {
            //TODO the private set sets the position here
        }

        public INeighbourhood Neighbourhood
        {
            get
            {
                return _neighbourhood;
            }

            set
            {
                if (_neighbourhood == null)
                    _neighbourhood = value;
                else
                    throw new ArgumentException("Can't change the neighbourhood!");
            }
        }

        public virtual double State { get; set; } //Don't know what to do with this one yet
    }
}
