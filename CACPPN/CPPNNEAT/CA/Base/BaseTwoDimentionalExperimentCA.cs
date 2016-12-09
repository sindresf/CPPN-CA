using CPPNNEATCA.CA.Base.Interface;

namespace CPPNNEATCA.CA.Base
{
	abstract class BaseTwoDimentionalExperimentCA : BaseNeatCA
	{
		protected static float[][] seed;
		protected static float[][] goal;
		protected static ICell[][] cells;
		public BaseTwoDimentionalExperimentCA() : base()
		{
		}
		protected void PushBack(float[][] future, float[][] past)
		{
			for(int i = 0; i < parameters.CellWorldWidth; i++)
				for(int j = 0; j < parameters.CellWorldHeight; j++)
					past[i][j] = future[i][j];
		}
	}
}