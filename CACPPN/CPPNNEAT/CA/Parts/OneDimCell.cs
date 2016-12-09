using System;
using CPPNNEAT.CA.Utils;
using CPPNNEATCA.CA.Base;
using CPPNNEATCA.CA.Base.Interface;

namespace CPPNNEATCA.CA.Parts
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
			if(cellBoard is float[]) return ((float[])cellBoard)[x];
			else throw new ArgumentException("has to be float[] Array!");
		}

		public override void InitializeNeighbourhood()
		{
			NeighbourhoodInitializor.InitializeNeighbourhood1D(this, cells);
		}
	}
}