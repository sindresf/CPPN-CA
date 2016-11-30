using System;
using CPPNNEAT.CPPN;
using CPPNNEAT.NEAT;
using CPPNNEAT.Utils;

namespace CPPNNEAT
{
	struct EAParameters
	{
		public static int PopulationSize = 200;
		public static int MaximumRuns = 12;

		public static float ExcessSimilarityWeight = 1.0f;
		public static float DisjointSimilarityWeight = 1.0f;
		public static float WeightDifferenceSimilarityWeight = 0.4f;
		public static bool IsSeededRun = true;
		public static int RandomSeed = 42;

		public static double SameFloatWithinReason = 0.01f; //should be scaled by how variant the fitness is
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
	//mutation chance of parameters in node functions

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