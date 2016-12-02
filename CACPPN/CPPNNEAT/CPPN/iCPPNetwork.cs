using System.Collections.Generic;

namespace CPPNNEAT.CPPN
{
	interface ICPPNetwork
	{
		float GetOutput(List<float> input);
	}
}