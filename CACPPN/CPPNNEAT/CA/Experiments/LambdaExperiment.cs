using System;
using System.Collections.Generic;
using CPPNNEATCA.CA.Base;

namespace CPPNNEATCA.CA.Experiments
{
	class LambdaExperiment : BaseNeatCA
	{
		public LambdaExperiment() : base()
		{

			parameters = new CAParameters(NeighbourHoodSize: 3,
										  CellStateCount: 2,
										  CellWorldWidth: 15,
										  MaxGeneration: 40);

			MakeStates(parameters.CellStateCount);


		}

		public override float RunEvaluation(Func<List<float>, int> TransitionFunction)
		{
			throw new NotImplementedException();
		}
	}

	class RuleSet : List<Rule>
	{
		public RuleSet(int[] results) : base()
		{
			float cond1 = 0, cond2 = 0, cond3 = 0;
			for(int i = 0; i < results.Length; i++)
			{
				if(i % 1 == 0) cond1 = 1;
				if(i % 2 == 0) cond2 = 1;
				if(i % 3 == 0) cond3 = 1;
				Add(new Rule(cond1, cond2, cond3, results[i]));
			}
		}
	}

	class Rule : List<float>
	{
		public int Result { get; private set; }
		public Rule(float condition1, float condition2, float condition3, int result) : base()
		{
			Add(condition1);
			Add(condition2);
			Add(condition3);
			Result = result;
		}
	}
}
