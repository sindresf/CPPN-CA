using CPPNNEAT.CA.Base;
using CPPNNEAT.CA.Base.Interface;
using CPPNNEAT.CA.Utils;

namespace CPPNNEAT.CA.Parts
{
	class TwoDimCell : BaseCell
	{
		public readonly int y;
		private float[,] cellBoard;
		private readonly ICell[,] cells;

		public TwoDimCell(int x, int y, ICell[,] cells, int neighbourhoodSize) : base(x, neighbourhoodSize)
		{
			this.y = y;
			this.cells = cells;
		}

		public override float GetState()
		{
			return cellBoard[x, y];
		}

		public void ReferenceCurrentCellStates(float[,] currentCellStates)
		{
			cellBoard = currentCellStates;
		}

		public override void InitializeNeighbourhood()
		{
			NeighbourhoodInitializor.InitializeNeighbourhood2D(this, cells);
		}
	}
}