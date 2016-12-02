using System;
using CPPNNEAT.CA.Base;
using CPPNNEAT.CA.Base.Interface;
using CPPNNEAT.CA.Utils;

namespace CPPNNEAT.CA.Parts
{
	class OneDimCell : BaseCell
	{
		private readonly float[] cellBoard; //as a reference can the CA change this between checks and it works because There it is not "readonly"?
		private readonly ICell[] cells;

		public OneDimCell(int x, float[] cellBoard, ICell[] cells, int neighbourhoodSize) : base(x, neighbourhoodSize)
		{
			this.cellBoard = cellBoard;
			InitializeNeighbourhood();
		}

		public override float GetState()
		{
			return cellBoard[x];
		}

		protected override void InitializeNeighbourhood()
		{
			NeighbourhoodInitializor.InitializeNeighbourhood1D(this, cells, cellBoard.Length);
		}
	}
}