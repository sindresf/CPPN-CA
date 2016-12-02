using System.Collections.Generic;

namespace CPPNNEAT.CA.Base.Interface
{
	interface ICell
	{
		float GetState();
		List<float> GetNeighbourhoodCurrentState(); //CPPN input list
	}
}