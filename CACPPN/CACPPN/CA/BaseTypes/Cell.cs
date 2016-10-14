using System;
using System.Collections.Generic;

namespace CACPPN.CA.BaseTypes
{
    abstract class Cell
    {
        protected List<Cell> _neighbourhood;

        public Cell(double value)
        {
            State = value;
        }

        public Cell()
        {

        }

        public abstract List<Cell> Neighbourhood { get; set; }

        public abstract double State { get; set; }
        public abstract double OldState { get; set; }

    }
}
