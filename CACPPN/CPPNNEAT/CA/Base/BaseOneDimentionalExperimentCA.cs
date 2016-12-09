using CPPNNEATCA.CA.Base.Interface;

namespace CPPNNEATCA.CA.Base
{
	abstract class BaseOneDimentionalExperimentCA : BaseNeatCA
	{
		protected static float[] seed;
		protected static float[] goal;
		protected static ICell[] cells;

		public BaseOneDimentionalExperimentCA()
		{
		}
		protected virtual void PushBack(float[] future, float[] past)
		{
			for(int i = 0; i < parameters.CellWorldWidth; i++)
				past[i] = future[i];
		}
	}
}