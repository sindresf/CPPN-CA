using System;
using System.Collections.Generic;
using CPPNNEATCA.CA.Base;

namespace CPPNNEATCA.CA.Experiments
{
	class Test1DimCA : BaseOneDimentionalExperimentCA
	{

		public Test1DimCA()
		{ //needs to setup everything that needs to be (and can be) exactly the same per individual
			parameters = new CAParameters(NeighbourHoodSize: 3,
										  CellStateCount: 2,
										  CellWorldWidth: 10);

			seed = new float[] { 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 };
			goal = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };

			if(seed.Length != parameters.CellWorldWidth
			 || goal.Length != parameters.CellWorldWidth)
				throw new FormatException("seed not compatible with experiment parameters");
		}

		public override float RunEvaluation(Func<List<float>, float> TransitionFunction)
		{
			float[] currentValues = seed;
			float[] futureValues = seed;

			return 1.0f;
		}
	}
}