using CPPNNEATCA.CA.Base;
using CPPNNEATCA.CA.Base.Interface;
using CPPNNEATCA.CA.Parts;

namespace CPPNNEAT.CA.Utils
{
	static class NeighbourhoodInitializor
	{
		public static void InitializeNeighbourhood1D(BaseCell cell, ICell[] cells)
		{
			NeighbourhoodInitializer1D.InitializeNeighbourhood(cell, cells, cells.Length);
		}
		public static void InitializeNeighbourhood2D(TwoDimCell cell, ICell[][] cells)
		{
			NeighbourhoodInitializor2D.InitializeNeighbourhood2D(cell, cells, cells.GetLength(0));
		}
	}
}