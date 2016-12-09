using System;
using CPPNNEAT.CA.Base;
using CPPNNEAT.CA.Base.Interface;
using CPPNNEAT.CA.Utils;

namespace CPPNNEAT.CA.Parts
{
	class OneDimCell : BaseCell
	{
		private readonly ICell[] cells;

		public OneDimCell(int x, ICell[] cells, int neighbourhoodSize) : base(x, neighbourhoodSize)
		{
			this.cells = cells;
		}

		protected override float GetState(Array cellBoard)
		{
			if(cellBoard.Rank != 1) throw new ArgumentException("has to be a float[] Array!");
			return ((float[])cellBoard)[x];
		}

		public override void InitializeNeighbourhood()
		{
			NeighbourhoodInitializor.InitializeNeighbourhood1D(this, cells);
		}
	}
}