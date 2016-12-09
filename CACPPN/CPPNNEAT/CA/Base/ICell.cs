using System;
using System.Collections.Generic;

namespace CPPNNEATCA.CA.Base.Interface
{
	interface ICell
	{
		List<float> GetNeighbourhoodCurrentState(Array cellBoard);
		void InitializeNeighbourhood();
	}
}