using System.Collections.Generic;

namespace CPPNNEATCA.CPPN
{
	interface ICPPNetwork
	{
		int GetNextState(List<float> input);
	}
}