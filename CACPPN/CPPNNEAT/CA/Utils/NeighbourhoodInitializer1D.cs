using System.Collections.Generic;
using CPPNNEAT.CA.Base;
using CPPNNEAT.CA.Base.Interface;
using CPPNNEATCA.Utils;

namespace CPPNNEAT.CA.Utils
{
	class NeighbourhoodInitializer1D
	{
		public static void InitializeNeighbourhood(BaseCell cell, ICell[] cells, int cellBoardLength)
		{
			int neighbourhoodWidth = cell.neighbourhoodSize.Neighbourhood1DRadius();

			bool firstCell = cell.x < 0 + neighbourhoodWidth;
			bool lastCell = cell.x > cellBoardLength - 1 - neighbourhoodWidth;
			bool safeCell = !firstCell && !lastCell;

			if(safeCell)
				Safe1DInitialize(cell, cells, neighbourhoodWidth);
			else if(firstCell)
				Left1DInitializer(cell, cells, neighbourhoodWidth, cellBoardLength);
			else if(lastCell)
				Right1DInitializer(cell, cells, neighbourhoodWidth, cellBoardLength);
		}
		private static void Safe1DInitialize(BaseCell cell, ICell[] cells, int neighbourhoodWidth)
		{
			List<ICell> neighbours = new List<ICell>();
			for(int i = cell.x - neighbourhoodWidth; i <= cell.x + neighbourhoodWidth; i++)
				neighbours.Add(cells[i]);
			cell.Neighbourhood = neighbours;
		}
		private static void Left1DInitializer(BaseCell cell, ICell[] cells, int neighbourhoodWidth, int cellBoardLength)
		{
			List<ICell> neighbourhood = new List<ICell>();
			for(int i = neighbourhoodWidth; i > 0; i--)
				neighbourhood.Add(cells[cellBoardLength - i]);
			neighbourhood.Add(cells[0]);
			for(int i = 1; i < neighbourhoodWidth + 1; i++)
				neighbourhood.Add(cells[i]);
			cell.Neighbourhood = neighbourhood;
		}
		private static void Right1DInitializer(BaseCell cell, ICell[] cells, int neighbourhoodWidth, int cellBoardLength)
		{
			List<ICell> neighbourhood = new List<ICell>();
			for(int i = neighbourhoodWidth + 1; i > 1; i--)
				neighbourhood.Add(cells[cellBoardLength - i]);
			neighbourhood.Add(cells[cellBoardLength - 1]);
			for(int i = 0; i < neighbourhoodWidth; i++)
				neighbourhood.Add(cells[i]);
			cell.Neighbourhood = neighbourhood;
		}
	}
}
