using System;
using CPPNNEAT.CA.Utils;
using CPPNNEATCA.CA.Base;
using CPPNNEATCA.CA.Base.Interface;

namespace CPPNNEATCA.CA.Parts
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
			if(cellBoard is float[][]) return ((float[][])cellBoard)[x][y];
			else throw new ArgumentException("has to be float[][] Array!");

		}

		public override void InitializeNeighbourhood()
		{
			NeighbourhoodInitializor.InitializeNeighbourhood2D(this, cells);
		}
	}
}