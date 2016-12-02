using System;
using System.Collections.Generic;

namespace CPPNNEAT.CA.Base
{
	abstract class BaseNeatCA : INeatCA
	{
		protected static CAParameters parameters;

		public virtual float RunEvaluation(Func<List<float>, float> TransitionFunction)
		{
			return 1.0f;
		}

		public CAParameters GetParameters()
		{
			return parameters;
		}
	}
}