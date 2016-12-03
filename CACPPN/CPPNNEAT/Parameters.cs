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
			experiment = new Test1DimCA(); // THE ONLY LINE NEEDING CHANGING IN THE GENERAL CASE
			CA = experiment.GetParameters();
			EA = new EAParameters(experiment);
			CPPN = new CPPNParameters(CA);
		}
	}

	struct EAParameters
	{
		public const int PopulationSize = 120;
		public const int MaximumRuns = 100;
		public const int SpeciesImprovementTriesBeforeDeath = 15;
		public static INeatCA CAExperiment;

		public EAParameters(INeatCA experimentCA)
		{
			CAExperiment = experimentCA;
		}

		public const float ExcessSimilarityWeight = 1.0f;
		public const float DisjointSimilarityWeight = 1.0f;
		public const float WeightDifferenceSimilarityWeight = 0.4f;
		public const float SpeciesInclusionRadius = 3.0f;
		public const int LowerChampionSpeciesCount = 5;
		public const int SetNToOneLimit = 12;

		public const float ASexualReproductionQuota = 0.25f;

		public const bool IsSeededRun = true;
		public const int RandomSeed = 42;

		public const double SameFloatWithinReason = 0.0001f;
	}

	struct CAParameters
	{
		public int NeighbourHoodSize;
		public int CellStateCount;
		public int CellWorldWidth;
		public int CellWorldHeight;
		public int MaxGeneration;

		public CAParameters(int NeighbourHoodSize, int CellStateCount, int CellWorldWidth, int MaxGeneration)
		{
			this.NeighbourHoodSize = NeighbourHoodSize;
			this.CellStateCount = CellStateCount;
			this.CellWorldWidth = CellWorldWidth;
			CellWorldHeight = CellWorldWidth;
			this.MaxGeneration = MaxGeneration;
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