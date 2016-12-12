using System;
using System.Collections.Generic;

namespace CPPNNEATCA.CA
{
	interface INeatCA
	{
		//Runs the entire CA based on nothing on a CPPNs return from a list of input states
		CAParameters GetParameters();
		float RunEvaluation(Func<Dictionary<int, float>, float> TransitionFunction);
	}
}