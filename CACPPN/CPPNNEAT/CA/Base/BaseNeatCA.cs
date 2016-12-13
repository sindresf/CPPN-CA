using System;
using System.Collections.Generic;

namespace CPPNNEATCA.CA.Base
{
	abstract class BaseNeatCA : INeatCA
	{
		protected static CAParameters parameters;
		protected float[] states;

		protected void MakeStates(int stateCount)
		{
			states = new float[stateCount];
			states[0] = 0.0f;
			states[stateCount - 1] = 1.0f;
			double interval = 1.0 / (stateCount - 1);
			for(int i = 1; i < stateCount - 1; i++)
				states[i] = (float)Math.Round(states[i - 1] + interval, 2);
		}

		protected float FloatToState(int stateIndex)
		{
			return states[stateIndex];
		}

		public abstract float RunEvaluation(Func<List<float>, int> TransitionFunction);

		public CAParameters GetParameters()
		{
			return parameters;
		}
	}
}