using System;
using System.Collections.Generic;
using CACPPN.CA.Utils;

namespace CACPPN.CA.BaseTypes
{
    class StateSet
    {
        public int StateSetSize
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

        public List<double> States { get; protected set; }

        protected bool uniformStateSet;

        public StateSet(int initialSize, bool uniformStateSet = true)
        {
            StateSetSize = initialSize;
            this.uniformStateSet = uniformStateSet;
            States = IntervalManager.GetValueEveryStepBetweenZeroAndOne(StateSetSize);
        }

        public void ChangeStateSetSize(int byAmount)
        {
            StateSetSize += byAmount;
        }

        public void setAsNoneUniformStateSet()
        {
            this.uniformStateSet = false;
        }
    }
}
