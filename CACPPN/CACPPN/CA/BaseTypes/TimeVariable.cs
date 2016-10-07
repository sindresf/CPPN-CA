using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CACPPN.CA.Interfaces;

namespace CACPPN.CA.BaseTypes
{
    class TimeVariable : ITimeVariable
    {
        protected static bool uniformTimeStep = true;

        public virtual TimeSpan getTimeStep()
        {
            return TimeSpan.MinValue; //should make it run as fast as possible
        }
    }
}
