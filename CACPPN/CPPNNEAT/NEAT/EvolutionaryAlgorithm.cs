using System;
using CPPNNEAT.CPPN;
using CPPNNEAT.Utils;

namespace CPPNNEAT.NEAT
{
	class NEAT
	{
		public Population population;
		public IDCounters IDs;
		public int generationCount = 0;
		public static readonly TupleList<float, ActivationFunctionType> ActivationFunctionChances = CPPNetworkParameters.ActivationFunctionChanceIntervals;
		public static readonly Random random;// = new Random(); //preprocessor directive for being seeded or no?

		public NEAT(PlaceHolderCARunner ca) //to become the wrapper for the CA so NEAT can run exactly the same
		{
			IDs = new IDCounters();
			population = new Population(ca);
		}

		static NEAT()
		{
			if(EAParameters.IsSeededRun)
				random = new Random(EAParameters.RandomSeed);
			else
				random = new Random();
		}

		public void InitializePopulation()
		{
			population.Initialize(IDs);
		}

		public void EvaluatePopulation()
		{
			population.Evaluate();
		}

		public void MakeNextGeneration()
		{
			generationCount++;
			population.MakeNextGeneration(IDs);
		}

		public float GetBestFitness() //placeholder for proper "GetAllTheStatistics" type of func (or class delegating func)
		{
			float best = 0.0f;
			foreach(Species sp in population.species)
			{
				foreach(Individual indie in sp.populace)
				{
					if(indie.Fitness > best)
						best = indie.Fitness;
				}
			}
			return best;
		}
	}
}