using System;
using System.Collections.Generic;

namespace CPPNNEAT.CA
{
	interface INeatCA
	{
		//Runs the entire CA based on nothing on a CPPNs return from a list of input states
		float RunEvaluationCA(Func<List<float>, float> TransitionFunction);
	}
}