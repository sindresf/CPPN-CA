using System;
using CPPNNEAT.CPPN;
using CPPNNEAT.EA.Base;
using CPPNNEAT.Utils;

namespace CPPNNEAT.EA
{
	class Neat : EvolutionaryAlgorithm
	{
		public Population population;
		public IDCounters IDs;
		public static readonly TupleList<float, ActivationFunctionType> ActivationFunctionChances = CPPNetworkParameters.ActivationFunctionChanceIntervals;
		public static readonly Random random;

		public Neat(PlaceHolderCARunner ca) //to become the wrapper for the CA so NEAT can run exactly the same
		{
			IDs = new IDCounters();
			population = new Population(ca);
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
			population.Initialize(IDs);
		}

		public override void EvaluatePopulation()
		{
			population.Evaluate();
		}

		public override void NextGeneration()
		{
			CurrentGeneration++;
			population.MakeNextGeneration(IDs);
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