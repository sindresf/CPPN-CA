using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CACPPN.CA.Interfaces;

namespace CACPPN.CA.BaseTypes
{
    class Cell : ICell
    {
        protected INeighbourhood _neighbourhood;
        public INeighbourhood neighbourhood
        {
            get
            {
                return _neighbourhood;
            }

            set
            {
                if (_neighbourhood == null)
                    neighbourhood = value;
            }
        }

        public virtual double State
        {
            get;
            set;
        }
    }
}
