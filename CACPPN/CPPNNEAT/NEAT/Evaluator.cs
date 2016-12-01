using CPPNNEAT.CPPN;

namespace CPPNNEAT.EA
{
	enum EvaluatorStructureType
	{
		OneDimentional,
		TwoDimentional
	}

	enum Experiment
	{
		Simplest1D, //identifies CAs that in turn defines their particulars
		Simplest2D,
		Medium1D,
		Medium2D,
		Difficult1D,
		Difficult2D,
		PushingIt1D,
		PushingIt2D
	}

	class Evaluator
	{
		public static readonly double[,] caBoard; // the "ca"
		static Evaluator()
		{
			caBoard = new double[20, 20];
		}

		public static float EvaluateTransitionFunction(CPPNetwork transitionFunction)
		{
			float fitness = 0.0f;
			//run a static CA. since the transitionFunction is "the difference" this should be fine.

			return fitness;
		}

		public static Evaluator GetEvaluator(EvaluatorStructureType type, Experiment experiment)
		{
			if(type == EvaluatorStructureType.OneDimentional)
				return null;
			else
				return null;
		}
	}
}