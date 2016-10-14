using System;
using System.Collections.Generic;
using CACPPN.CA.BaseTypes;

namespace CACPPN.CA.DerivedTypes.CellTypes
{
    class FloatCell : Cell
    {
        public override double State { get; set; }
        public override double OldState { get; set; }

        public override List<Cell> Neighbourhood
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

        private int states;

        public FloatCell(double value, int states) : base(value)
        {
            this.states = states;
            State = value;
            OldState = value;
        }
    }
}
