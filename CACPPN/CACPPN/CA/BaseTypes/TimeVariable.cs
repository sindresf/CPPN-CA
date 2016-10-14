using System;
namespace CACPPN.CA.BaseTypes
{
    class TimeVariable
    {
        protected static bool uniformTimeStep = true;

        public virtual TimeSpan getTimeStep()
        {
            return TimeSpan.MinValue; //should make it run as fast as possible
        }
    }
}
