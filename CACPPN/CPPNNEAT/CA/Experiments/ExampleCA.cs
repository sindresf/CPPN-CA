using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPPNNEAT.CA.Experiments
{
	class ExampleCA : BaseNeatCA
	{

		public override float RunEvaluationCA(Func<List<float>, float> TransitionFunction)
		{
			return base.RunEvaluationCA(TransitionFunction);
		}
	}
}