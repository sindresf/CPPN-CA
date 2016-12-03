using CPPNNEAT.CA.Base;
using CPPNNEAT.CA.Base.Interface;
using CPPNNEAT.CA.Parts;

namespace CPPNNEAT.CA.Utils
{
	static class NeighbourhoodInitializor
	{
		public static void InitializeNeighbourhood1D(BaseCell cell, ICell[] cells)	
		{
			NeighbourhoodInitializer1D.InitializeNeighbourhood(cell, cells, cells.Length);
		}
		public static void InitializeNeighbourhood2D(TwoDimCell cell, ICell[,] cells)
		{
			NeighbourhoodInitializor2D.InitializeNeighbourhood2D(cell, cells, cells.Length);
		}
	}
}