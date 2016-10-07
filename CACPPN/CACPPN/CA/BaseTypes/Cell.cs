using System;
using CACPPN.CA.Interfaces;

namespace CACPPN.CA.BaseTypes
{
    class Cell : ICell //TODO Cell needs some looking into
    {

        protected INeighbourhood _neighbourhood;
        protected ICoordinate _gridPosition;

        public virtual ICoordinate GridPosition
        {
            get { return _gridPosition; }

            protected set
            {
                _gridPosition = value;
            }
        }

        public Cell(ICoordinate coords, double state)
        {
            GridPosition = coords;
            State = state;
        }

        public Cell(ICoordinate coords)
        {
            GridPosition = coords;
        }

        public virtual INeighbourhood Neighbourhood
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
                    throw new ArgumentException("Can't change the neighbourhood!"); //TODO unless random connection or size stuff for laters
            }
        }

        public virtual double State { get; set; } //Don't know what to do with this one yet
    }
}
