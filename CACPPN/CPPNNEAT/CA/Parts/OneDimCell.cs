using CPPNNEAT.CA.Base;
using CPPNNEAT.CA.Base.Interface;
using CPPNNEAT.CA.Utils;

namespace CPPNNEAT.CA.Parts
{
	class OneDimCell : BaseCell
	{
		private float[] cellBoard;
		private readonly ICell[] cells;

		public OneDimCell(int x, ICell[] cells, int neighbourhoodSize) : base(x, neighbourhoodSize)
		{
			this.cells = cells;
		}

		public override float GetState()
		{
			return cellBoard[x];
		}

		public void ReferenceCurrentCellStates(float[] currentCellStates)
		{
			cellBoard = currentCellStates;
		}

		public override void InitializeNeighbourhood()
		{
			NeighbourhoodInitializor.InitializeNeighbourhood1D(this, cells);
		}
	}
}