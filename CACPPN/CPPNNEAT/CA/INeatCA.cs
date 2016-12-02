using System;
using System.Collections.Generic;
using CPPNNEAT.NEAT.EABase;

namespace CPPNNEAT.CA
{
	interface INeatCA : IEvaluator
	{
		//Runs the entire CA based on nothing on a CPPNs return from a list of input states
		CAParameters GetParameters();
		float RunEvaluation(Func<List<float>, float> TransitionFunction);
	}
}