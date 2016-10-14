using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CACPPN.CA.BaseTypes;
using CACPPN.CA.Interfaces;

namespace CACPPN.CA.DerivedTypes.CellTypes
{
    class FloatCell : Cell
    {
        public double State { get; set; }
        public double OldState { get; set; }

        private int states;
        public FloatCell(ICoordinate coords, double initialValue) : base(coords)
        {
            State = initialValue;
            OldState = initialValue;
        }

        public FloatCell(double value, int states) : base(value)
        {
            this.states = states;
            State = value;
            OldState = value;
        }
    }
}
