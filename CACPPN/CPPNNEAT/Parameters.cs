using System;
using CPPNNEATCA.CA;
using CPPNNEATCA.CA.Experiments;
using CPPNNEATCA.CPPN.Parts;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA
{
	class Parameters
	{
		public readonly EAParameters EA;
		public readonly CPPNParameters CPPN;
		public readonly CAParameters CA;
		public INeatCA experiment;

		public Parameters()
		{
			experiment = new LambdaExperiment(); // THE ONLY LINE NEEDING CHANGING IN THE GENERAL CASE
			CA = experiment.GetParameters();
			EA = new EAParameters(experiment);
			CPPN = new CPPNParameters(CA);
		}
	}

	struct EAParameters
	{
		public static INeatCA CAExperiment;
		public EAParameters(INeatCA experimentCA)
		{
			CAExperiment = experimentCA;
		}

		public const int PopulationSize = 100;
		public const int MaximumRuns = 80;
		public const int SpeciesImprovementTriesBeforeDeath = 15;
		public const int SetNToOneLimit = 12;
		public const int LowerChampionSpeciesCount = 5;

		public const float ExcessSimilarityWeight = 1.0f;
		public const float DisjointSimilarityWeight = 1.0f;
		public const float FunctionSimilarityWeight = 1.0f;
		public const float WeightDifferenceSimilarityWeight = 1f;
		public const float SpeciesInclusionRadius = 3.0f;

		public static float ASexualReproductionQuota = 0.25f;
		public static float SexualReproductionStillMutateChance = 0.06f;

		public static bool RandomGeneStart = true;
		public static float RandomGeneStartChance = 0.06f;

		public static bool IsSeededRun = false;
		public static int RandomSeed = 42;
	}

	struct CAParameters
	{
		public int NeighbourHoodSize;
		public int CellStateCount;
		public int CellWorldWidth;
		public int CellWorldHeight;
		public int MaxGeneration;
		public float MaxFitnessPossible;

		public CAParameters(int NeighbourHoodSize, int CellStateCount, int CellWorldWidth, int MaxGeneration, float MaxFitness = float.MinValue)
		{
			this.NeighbourHoodSize = NeighbourHoodSize;
			this.CellStateCount = CellStateCount;
			this.CellWorldWidth = CellWorldWidth;
			CellWorldHeight = CellWorldWidth;
			this.MaxGeneration = MaxGeneration;
			MaxFitnessPossible = MaxFitness;
		}
	}

	struct CPPNParameters
	{
		public const float InitialMaxConnectionWeight = 0.5f;
		public const float WeightMin = -1.0f, WeightMax = 1.0f;

		public int InputSize;
		public int OutputSize;

		public CPPNParameters(CAParameters caParams)
		{
			InputSize = caParams.NeighbourHoodSize;
			OutputSize = caParams.CellStateCount;
		}

		private static TupleList<ActivationFunctionType,float> FunctionChances = new TupleList<ActivationFunctionType, float>
				{	// make sure it all sums to 1.0 (100%)
					{ ActivationFunctionType.Sinusodial,    0.2f },
					{ ActivationFunctionType.Gaussian,   0.14f },
					{ ActivationFunctionType.AbsoluteValue,     0.13f },
					{ ActivationFunctionType.PyramidAbsoluteValue, 0.17f},
					{ActivationFunctionType.Modulo, 0.2f },
					{ ActivationFunctionType.Linear,    0.16f }
				};

		public static float[,] ActivationFunctionSimilarity = new float[,] {
			{    1,0.12f,0.2f, 0.0f,0.32f,0.3f },//sin
			{0.12f,    1,0.1f,0.56f,0.15f,0.1f },//gauss
			{ 0.2f, 0.1f,   1, 0.0f, 0.2f,0.4f },//abs
			{ 0.0f,0.56f,0.0f,    1,0.05f,0.0f },//Pyramid
			{0.32f,0.15f,0.2f,0.05f,    1,0.32f },//Modulo
			{ 0.3f, 0.1f,0.4f, 0.0f, 0.32f,   1 } }; //linear

		public static TupleList<float, ActivationFunctionType> ActivationFunctionChanceIntervals
		{
			get
			{
				var intervals = new TupleList<float, ActivationFunctionType>();
				float compoundedValue = 0.0f;
				foreach(Tuple<ActivationFunctionType, float> tuple in FunctionChances)
				{
					compoundedValue += tuple.Item2;
					intervals.Add(compoundedValue, tuple.Item1);
				}
				return intervals;
			}
		}
	}
}