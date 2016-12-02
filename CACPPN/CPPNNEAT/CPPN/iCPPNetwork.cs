using System.Collections.Generic;

namespace CPPNNEATCA.CPPN
{
	interface ICPPNetwork
	{
		float GetOutput(List<float> input);
	}
}