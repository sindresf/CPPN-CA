using CPPNNEAT.CA.Base;
using CPPNNEAT.CA.Base.Interface;
using CPPNNEAT.CA.Utils;

namespace CPPNNEAT.CA.Parts
{
	class OneDimCell : BaseCell
	{
		private float[] cellBoard;
		private readonly ICell[] cells;
		private int i;
		private ICell[,] cells1;

		public OneDimCell(int x, ICell[] cells, int neighbourhoodSize) : base(x, neighbourhoodSize)
		{
			this.cells = cells;
		}

		public OneDimCell(int i, ICell[,] cells1, int neighbourHoodSize)
		{
			this.i = i;
			this.cells1 = cells1;
			neighbourhoodSize = neighbourHoodSize;
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