using System;
using CPPNNEAT.CPPN;
using CPPNNEAT.EA.Base;
using CPPNNEAT.Utils;

namespace CPPNNEAT.EA
{
	class Neat : EvolutionaryAlgorithm
	{
		public Population population { get; private set; }
		public static Evaluator evaluator = new Evaluator(EAParameters.neatCA);
		public IDCounters IDs;
		public static readonly TupleList<float, ActivationFunctionType> ActivationFunctionChances = CPPNetworkParameters.ActivationFunctionChanceIntervals;
		public static readonly Random random;

		public Neat(Evaluator ca) //to become the wrapper for the CA so NEAT can run exactly the same
		{
			IDs = new IDCounters();
			population = new Population(ca, IDs);
		}

		static Neat()
		{
			if(EAParameters.IsSeededRun)
				random = new Random(EAParameters.RandomSeed);
			else
				random = new Random();
		}

		public override void InitializePopulation()
		{
			population.Initialize();
		}

		public override void EvaluatePopulation()
		{
			population.Evaluate();
		}

		public override void NextGeneration()
		{
			CurrentGeneration++;
			population.MakeNextGeneration();
		}

		public override float GetBestFitness()
		{
			float best = 0.0f;
			foreach(Species sp in population.species)
			{
				foreach(NEATIndividual indie in sp.populace)
				{
					if(indie.Fitness > best)
						best = indie.Fitness;
				}
			}
			return best;
		}
	}
}