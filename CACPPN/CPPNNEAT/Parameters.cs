using CPPNNEAT.CPPN;
using CPPNNEAT.Utils;

namespace CPPNNEAT
{
	struct EAParameters
	{
		public static int PopulationSize = 400;
		public static int MaximumRuns = 150;

		public static float ExcessSimilarityWeight = 1.0f;
		public static float DisjointSimilarityWeight = 1.0f;
		public static float WeightDifferenceSimilarityWeight = 0.4f;
	}

	struct CPPNetworkParameters
	{
		public static int CPPNetworkInputSize = 5;
		public static int CPPNetworkOutputSize = 1; //for CPPN-CA it would always be 1
		public static TupleList<ActivationFunctionType,float> functionChances = new TupleList<ActivationFunctionType, float>
				{
					{ ActivationFunctionType.Sinusodial,    0.2f },
					{ ActivationFunctionType.Gaussian,   0.14f },
					{ ActivationFunctionType.AbsoluteValue,     0.13f },
					{ ActivationFunctionType.PyramidAbsoluteValue, 0.17f},
					{ActivationFunctionType.Modulo, 0.2f },
					{ ActivationFunctionType.Linear,    0.16f }
				};
	}

	struct MutationChances //check these up against standard NEAT settings
	{
		public static float AddNewNode          = 0.001f;
		public static float AddNewConnection    = 0.03f;
		public static float MutateWeight        = 0.75f;
		public static float MutatWeightAmount   = 0.03f;
		public static float ChangeNodeFunction  = 0.0005f;
		//mutation chance of parameters in node functions
	}

	class IDCounters
	{
		private int spID, indID, ngID, cgID;

		public IDCounters()
		{
			SpeciesID = 0;
			IndividualID = 0;
			NodeGeneID = 0;
			ConnectionGeneID = 0;
		}

		public int SpeciesID
		{
			get { return spID++; }
			private set { spID = value; }
		}

		public int IndividualID
		{
			get { return indID++; }
			private set { indID = value; }
		}
		public int NodeGeneID
		{
			get { return ngID++; }
			private set { ngID = value; }
		}

		public int ConnectionGeneID
		{
			get { return cgID++; }
			private set { cgID = value; }
		}
	}
}