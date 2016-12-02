using System;
using System.Collections.Generic;

namespace CPPNNEATCA.CA.Base
{
	abstract class BaseNeatCA : INeatCA
	{
		protected static CAParameters parameters;

		public abstract float RunEvaluation(Func<List<float>, float> TransitionFunction);

		public CAParameters GetParameters()
		{
			return parameters;
		}
	}
}