using System.Collections.Generic;

namespace CPPNNEATCA.CPPN
{
	interface ICPPNetwork
	{
		float GetOutput(Dictionary<int, float> input);
	}
}