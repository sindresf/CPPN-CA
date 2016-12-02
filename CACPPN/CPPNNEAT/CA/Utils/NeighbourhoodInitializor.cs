using System.Collections.Generic;
using CPPNNEAT.CA.Base;
using CPPNNEAT.CA.Base.Interface;
using CPPNNEATCA.Utils;

namespace CPPNNEAT.CA.Utils
{
	static class NeighbourhoodInitializor
	{
		public static void InitializeNeighbourhood1D(BaseCell cell, ICell[] cells, int cellBoardLength)
		{
			int neighbourhoodWidth = cell.neighbourhoodSize.NeighbourhoodRadius();

			bool firstCell = cell.x <= 0 + neighbourhoodWidth;
			bool lastCell = cell.x >= cellBoardLength - 1 - neighbourhoodWidth;
			bool safeCell = !firstCell && !lastCell;

			if(safeCell)
				Safe1DInitialize(cell, cells, neighbourhoodWidth);
			else if(firstCell)
				Left1DInitializer(cell, cells, neighbourhoodWidth);
			else if(lastCell)
				Right1DInitializer(cell, cells, neighbourhoodWidth);
		}
		private static void Safe1DInitialize(BaseCell cell, ICell[] cells, int neighbourhoodWidth)
		{
			List<ICell> neighbours = new List<ICell>();
			for(int i = cell.x - neighbourhoodWidth; i <= cell.x + neighbourhoodWidth; i++)
				neighbours.Add(cells[i]);
			cell.Neighbourhood = neighbours;
		}
		private static void Left1DInitializer(BaseCell cell, ICell[] cells, int neighbourhoodWidth)
		{
			List<ICell> neighbours = new List<ICell>();

			cell.Neighbourhood = neighbours;
		}
		private static void Right1DInitializer(BaseCell cell, ICell[] cells, int neighbourhoodWidth)
		{
			List<ICell> neighbours = new List<ICell>();

			cell.Neighbourhood = neighbours;
		}

		public static void InitializeNeighbourhood2D(BaseCell cell, ICell[,] cells, int cellBoardSize)
		{
			int neighbourhoodWidth = cell.neighbourhoodSize.NeighbourhoodRadius();

			bool upperCell = false;
			bool rightCell = false;
			bool lowerCell = false;
			bool leftCell = false;

			bool VertMidCell = !rightCell && !leftCell;
			bool HoriMidCell = !upperCell && !lowerCell;

			bool safeCell = VertMidCell && HoriMidCell;

			if(safeCell)
				Safe2DInitialize(cell, cells, neighbourhoodWidth);
			else if(VertMidCell)
			{
				if(upperCell)
					Upper2DInitialize(cell, cells, neighbourhoodWidth);
				else if(lowerCell)
					Lower2DInitialize(cell, cells, neighbourhoodWidth);
			} else if(HoriMidCell)
			{
				if(leftCell)
					Left2DInitialize(cell, cells, neighbourhoodWidth);
				else if(rightCell)
					Right2DInitialize(cell, cells, neighbourhoodWidth);
			} else
			{
				bool URCell = upperCell && rightCell;
				bool LRCell = rightCell && lowerCell;
				bool LLCell = lowerCell && leftCell;
				bool ULCell = leftCell && upperCell;

				if(URCell) UpperRight2DInitialize(cell, cells, neighbourhoodWidth);
				if(LRCell) LowerRight2DInitialize(cell, cells, neighbourhoodWidth);
				if(LLCell) LowerLeft2DInitialize(cell, cells, neighbourhoodWidth);
				if(ULCell) UpperLeft2DInitialize(cell, cells, neighbourhoodWidth);
			}

		}
		private static void Safe2DInitialize(BaseCell cell, ICell[,] cells, int neighbourhoodWidth)
		{
			List<ICell> neighbours = new List<ICell>();

			cell.Neighbourhood = neighbours;
		}
		private static void Upper2DInitialize(BaseCell cell, ICell[,] cells, int neighbourhoodWidth)
		{
			List<ICell> neighbours = new List<ICell>();

			cell.Neighbourhood = neighbours;
		}
		private static void Right2DInitialize(BaseCell cell, ICell[,] cells, int neighbourhoodWidth)
		{
			List<ICell> neighbours = new List<ICell>();

			cell.Neighbourhood = neighbours;
		}
		private static void Lower2DInitialize(BaseCell cell, ICell[,] cells, int neighbourhoodWidth)
		{
			List<ICell> neighbours = new List<ICell>();

			cell.Neighbourhood = neighbours;
		}
		private static void Left2DInitialize(BaseCell cell, ICell[,] cells, int neighbourhoodWidth)
		{
			List<ICell> neighbours = new List<ICell>();

			cell.Neighbourhood = neighbours;
		}
		private static void UpperRight2DInitialize(BaseCell cell, ICell[,] cells, int neighbourhoodWidth)
		{
			List<ICell> neighbours = new List<ICell>();

			cell.Neighbourhood = neighbours;
		}
		private static void LowerRight2DInitialize(BaseCell cell, ICell[,] cells, int neighbourhoodWidth)
		{
			List<ICell> neighbours = new List<ICell>();

			cell.Neighbourhood = neighbours;
		}
		private static void LowerLeft2DInitialize(BaseCell cell, ICell[,] cells, int neighbourhoodWidth)
		{
			List<ICell> neighbours = new List<ICell>();

			cell.Neighbourhood = neighbours;
		}
		private static void UpperLeft2DInitialize(BaseCell cell, ICell[,] cells, int neighbourhoodWidth)
		{
			List<ICell> neighbours = new List<ICell>();

			cell.Neighbourhood = neighbours;
		}
	}
}