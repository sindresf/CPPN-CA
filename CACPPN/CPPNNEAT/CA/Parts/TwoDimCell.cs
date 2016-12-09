using System;
using CPPNNEAT.CA.Base;
using CPPNNEAT.CA.Base.Interface;
using CPPNNEAT.CA.Utils;

namespace CPPNNEAT.CA.Parts
{
	class TwoDimCell : BaseCell
	{
		public readonly int y;
		private readonly ICell[][] cells;

		public TwoDimCell(int x, int y, ICell[][] cells, int neighbourhoodSize) : base(x, neighbourhoodSize)
		{
			this.y = y;
			this.cells = cells;
		}

		protected override float GetState(Array cellBoard)
		{
			if(cellBoard.Rank != 2) throw new ArgumentException("has to be float[][] Array!");
			return ((float[][])cellBoard)[x][y];
		}

		public override void InitializeNeighbourhood()
		{
			NeighbourhoodInitializor.InitializeNeighbourhood2D(this, cells);
		}
	}
}