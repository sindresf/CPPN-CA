using System;
using CPPNNEAT.CA.Base;
using CPPNNEAT.CA.Base.Interface;
using CPPNNEAT.CA.Utils;

namespace CPPNNEAT.CA.Parts
{
	class TwoDimCell : BaseCell
	{
		public readonly int y;
		private readonly float[,] cellBoard;
		private readonly ICell[,] cells;

		public TwoDimCell(int x, int y, float[,] cellBoard, ICell[,] cells, int neighbourhoodSize) : base(x, neighbourhoodSize)
		{
			this.y = y;
			this.cellBoard = cellBoard;
			this.cells = cells;
			InitializeNeighbourhood();
		}

		public override float GetState()
		{
			return cellBoard[x, y];
		}

		protected override void InitializeNeighbourhood()
		{
			NeighbourhoodInitializor.InitializeNeighbourhood2D(this, cells, cellBoard.Length);
		}
	}
}