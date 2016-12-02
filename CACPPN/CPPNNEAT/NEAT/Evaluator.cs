using System;
using System.Collections.Generic;
using CPPNNEAT.CA;
using CPPNNEAT.CPPN;

namespace CPPNNEAT.EA
{

	class Evaluator
	{
		private INeatCA ca;

		public Evaluator(INeatCA ca)
		{
			this.ca = ca;
		}

		public float EvaluateTransitionFunction(CPPNetwork transitionFunction)
		{
			Func<List<float>, float> transFunc = transitionFunction.GetOutput;
			return ca.RunEvaluationCA(transFunc);
		}
	}
}