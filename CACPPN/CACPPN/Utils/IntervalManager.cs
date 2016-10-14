using System;
using System.Collections.Generic;

namespace CACPPN.Utils
{
    class IntervalManager
    {
        //for statesets
        public static List<double> GetValueEveryStepBetweenZeroAndOne(int steps)
        {
            List<double> Values = new List<double>();
            double stepSize = 1.0 / steps;
            for (double step = 0; step < 1.0; step += stepSize)
                Values.Add(step);
            if (Values.Count != steps)
                throw new Exception("the for loop for stepping resulted in " + (Values.Count - steps) + "different steps than wanted!");
            return Values;
        }

        //for coordinates
        // get values from "min" to "max" version of above, since it's usualle -1 to 1, but other than that the exact same.
        //have a more generic one for different lengths in 2 dimensions? later, much later.


    }
}
