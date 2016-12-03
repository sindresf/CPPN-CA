using CPPNNEAT.CA.Base;
using CPPNNEAT.CA.Base.Interface;
using CPPNNEAT.CA.Parts;

namespace CPPNNEAT.CA.Utils
{
	static class NeighbourhoodInitializor
	{
		public static void InitializeNeighbourhood1D(BaseCell cell, ICell[] cells, int cellBoardLength)
		{
			NeighbourhoodInitializer1D.InitializeNeighbourhood(cell, cells, cellBoardLength);
		}
		public static void InitializeNeighbourhood2D(TwoDimCell cell, ICell[,] cells, int cellBoardSize)
		{
			NeighbourhoodInitializor2D.InitializeNeighbourhood2D(cell, cells, cellBoardSize);
		}
	}
}