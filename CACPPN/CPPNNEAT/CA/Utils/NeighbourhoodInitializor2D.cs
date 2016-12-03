using System.Collections.Generic;
using CPPNNEAT.CA.Base.Interface;
using CPPNNEAT.CA.Parts;
using CPPNNEATCA.Utils;

namespace CPPNNEAT.CA.Utils
{
	static class NeighbourhoodInitializor2D
	{
		public static void InitializeNeighbourhood2D(TwoDimCell cell, ICell[,] cells, int cellBoardSize)
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
					Upper2DInitialize(cell, cells, neighbourhoodWidth, cellBoardSize);
				else if(lowerCell)
					Lower2DInitialize(cell, cells, neighbourhoodWidth, cellBoardSize);
			} else if(HoriMidCell)
			{
				if(leftCell)
					Left2DInitialize(cell, cells, neighbourhoodWidth, cellBoardSize);
				else if(rightCell)
					Right2DInitialize(cell, cells, neighbourhoodWidth, cellBoardSize);
			} else
			{
				bool URCell = upperCell && rightCell;
				bool LRCell = rightCell && lowerCell;
				bool LLCell = lowerCell && leftCell;
				bool ULCell = leftCell && upperCell;

				if(URCell) UpperRight2DInitialize(cell, cells, neighbourhoodWidth, cellBoardSize);
				if(LRCell) LowerRight2DInitialize(cell, cells, neighbourhoodWidth, cellBoardSize);
				if(LLCell) LowerLeft2DInitialize(cell, cells, neighbourhoodWidth, cellBoardSize);
				if(ULCell) UpperLeft2DInitialize(cell, cells, neighbourhoodWidth, cellBoardSize);
			}

		}
		private static void Safe2DInitialize(TwoDimCell cell, ICell[,] cells, int neighbourhoodWidth)
		{
			List<ICell> neighbourhood = new List<ICell>();
			int i = cell.x;
			int j = cell.y;
			//width == 1
			neighbourhood.Add(cells[i - 1, j]);
			neighbourhood.Add(cells[i, j + 1]);
			neighbourhood.Add(cells[i, j]);
			neighbourhood.Add(cells[i + 1, j]);
			neighbourhood.Add(cells[i, j - 1]);
			//width == 2
			if(neighbourhoodWidth == 2)
			{
				neighbourhood.Add(cells[i - 1, j - 1]);
				neighbourhood.Add(cells[i - 1, j + 1]);
				neighbourhood.Add(cells[i + 1, j + 1]);
				neighbourhood.Add(cells[i + 1, j - 1]);
			}
			cell.Neighbourhood = neighbourhood;
		}
		private static void Upper2DInitialize(TwoDimCell cell, ICell[,] cells, int neighbourhoodWidth, int cellBoardSize)
		{
			List<ICell> neighbourhood = new List<ICell>();
			int i = cell.x;
			int j = cell.y;
			//width == 1
			neighbourhood.Add(cells[cellBoardSize - 1, i]);
			neighbourhood.Add(cells[0, i + 1]); //remember this! :O
			neighbourhood.Add(cells[1, i]);
			neighbourhood.Add(cells[0, i - 1]);
			//width == 2
			if(neighbourhoodWidth >= 2)
			{
				neighbourhood.Add(cells[cellBoardSize - 1, i - 1]);
				neighbourhood.Add(cells[cellBoardSize - 1, i + 1]);
				neighbourhood.Add(cells[1, i + 1]);
				neighbourhood.Add(cells[1, i - 1]);
			}
			cell.Neighbourhood = neighbourhood;
		}
		private static void Right2DInitialize(TwoDimCell cell, ICell[,] cells, int neighbourhoodWidth, int cellBoardSize)
		{
			List<ICell> neighbourhood = new List<ICell>();
			int i = cell.x;
			int j = cell.y;
			//width == 1
			neighbourhood.Add(cells[i - 1, cellBoardSize - 1]);
			neighbourhood.Add(cells[i, 0]);
			neighbourhood.Add(cells[i, j]);
			neighbourhood.Add(cells[i + 1, cellBoardSize - 1]);
			neighbourhood.Add(cells[i, cellBoardSize - 2]);
			//width == 2
			if(cellBoardSize >= 2)
			{
				neighbourhood.Add(cells[i - 1, cellBoardSize - 2]);
				neighbourhood.Add(cells[i - 1, 0]);
				neighbourhood.Add(cells[i + 1, 0]);
				neighbourhood.Add(cells[i + 1, cellBoardSize - 2]);
			}
			cell.Neighbourhood = neighbourhood;
		}
		private static void Lower2DInitialize(TwoDimCell cell, ICell[,] cells, int neighbourhoodWidth, int cellBoardSize)
		{
			List<ICell> neighbourhood = new List<ICell>();
			int i = cell.x;
			int j = cell.y;
			//width == 1

			//width == 2

			cell.Neighbourhood = neighbourhood;
		}
		private static void Left2DInitialize(TwoDimCell cell, ICell[,] cells, int neighbourhoodWidth, int cellBoardSize)
		{
			List<ICell> neighbourhood = new List<ICell>();
			int i = cell.x;
			int j = cell.y;
			//width == 1

			//width == 2

			cell.Neighbourhood = neighbourhood;
		}
		private static void UpperRight2DInitialize(TwoDimCell cell, ICell[,] cells, int neighbourhoodWidth, int cellBoardSize)
		{
			List<ICell> neighbourhood = new List<ICell>();
			int i = cell.x;
			int j = cell.y;
			//width == 1

			//width == 2

			cell.Neighbourhood = neighbourhood;
		}
		private static void LowerRight2DInitialize(TwoDimCell cell, ICell[,] cells, int neighbourhoodWidth, int cellBoardSize)
		{
			List<ICell> neighbourhood = new List<ICell>();
			int i = cell.x;
			int j = cell.y;
			//width == 1

			//width == 2

			cell.Neighbourhood = neighbourhood;
		}
		private static void LowerLeft2DInitialize(TwoDimCell cell, ICell[,] cells, int neighbourhoodWidth, int cellBoardSize)
		{
			List<ICell> neighbourhood = new List<ICell>();
			int i = cell.x;
			int j = cell.y;
			//width == 1

			//width == 2

			cell.Neighbourhood = neighbourhood;
		}
		private static void UpperLeft2DInitialize(TwoDimCell cell, ICell[,] cells, int neighbourhoodWidth, int cellBoardSize)
		{
			List<ICell> neighbourhood = new List<ICell>();
			int i = cell.x;
			int j = cell.y;
			//width == 1

			//width == 2

			cell.Neighbourhood = neighbourhood;
		}
	}
}