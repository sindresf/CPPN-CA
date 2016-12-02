using System;
using CPPNNEAT.CA;
using CPPNNEAT.CPPN;
using CPPNNEAT.EA;
using CPPNNEAT.Utils;

namespace CPPNNEAT
{
	struct EAParameters
	{
		public const int PopulationSize = 200;
		public const int MaximumRuns = 12;
		public const int SpeciesImprovementTriesBeforeDeath = 15;
		public static INeatCA neatCA = new BaseNeatCA(); //needs subclasses that "are the experiment" like FloweringCA <- is a growth setup CA

		public const float ExcessSimilarityWeight = 1.0f;
		public const float DisjointSimilarityWeight = 1.0f;
		public const float WeightDifferenceSimilarityWeight = 0.4f;
		public const int SetNToOneLimit = 12;

		public const bool IsSeededRun = true;
		public const int RandomSeed = 42;

		public const double SameFloatWithinReason = 0.01f; //should be scaled by how variant the fitness is
	}

	struct CPPNetworkParameters
	{
		public static float InitialMaxConnectionWeight = 0.13f;
		public static float WeightMin = -2.0f, WeightMax = 2.0f;

		public static int CPPNetworkInputSize = 3; //start with 1D
		public static int CPPNetworkOutputSize = 1;

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

struct MutationChances //check these up against standard NEAT settings
{
	private const double AddNewNode          = 0.001f;
	private const double AddNewConnection    = 0.03f;
	private const double ChangeWeight        = 0.75f;
	public const float MutatWeightAmount   = 0.1f;
	private const double ChangeNodeFunction  = 0.0005f;
	public const double CreationMutationChance = 0.05; // <- completely out of my ass, like most of these

	public static double GetMutationChance(MutationType type)
	{
		switch(type)
		{
		case MutationType.AddNode:
			return AddNewNode;
		case MutationType.AddConnection:
			return AddNewConnection;
		case MutationType.ChangeWeight:
			return MutatWeightAmount;
		case MutationType.ChangeFunction:
			return ChangeNodeFunction;
		default:
			return 0.0;
		}
	}
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