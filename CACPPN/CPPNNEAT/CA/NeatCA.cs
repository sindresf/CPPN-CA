using System;
using System.Collections.Generic;

namespace CPPNNEAT.CA
{
	class BaseNeatCA : INeatCA
	{
		protected static CAParameters parameters;

		public BaseNeatCA()
		{
			parameters = new CAParameters();

		}
		public virtual float RunEvaluationCA(Func<List<float>, float> TransitionFunction)
		{
			return 1.0f;
		}
	}
}