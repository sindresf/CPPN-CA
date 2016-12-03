using CPPNNEAT.CA.Base.Interface;

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
	}
}