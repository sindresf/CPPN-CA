using System;
using CPPNNEATCA.CA;
using CPPNNEATCA.CPPN.Parts;
using CPPNNEATCA.EA.Base;
using CPPNNEATCA.NEAT.Parts;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.NEAT
{
	class Neat : EvolutionaryAlgorithm
	{
		public Population population { get; private set; }
		public static Parameters parameters = new Parameters();
		public static INeatCA evaluator;
		public IDCounters IDs;
		public static readonly TupleList<float, ActivationFunctionType> ActivationFunctionChances = CPPNParameters.ActivationFunctionChanceIntervals;
		public static readonly Random random;

		public Neat() //to become the wrapper for the CA so NEAT can run exactly the same
		{
			IDs = new IDCounters();
			evaluator = parameters.experiment;
			population = new Population(evaluator, IDs);
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

		public override bool IsDeadRun()
		{
			//means the last species killed itself off due to no improvement
			return population.species.Count == 0;
		}

		public override void NextGeneration()
		{
			CurrentGeneration++;
			//don't forget elitism for species with 5+ indies
			population.MakeNextGeneration();
		}

		public override float GetBestFitness()
		{
			float best = 0.0f;
			int bestID = 0;
			foreach(Species sp in population.species)
			{
				foreach(NEATIndividual indie in sp.populace)
				{
					if(indie.Fitness > best)
					{
						best = indie.Fitness;
						bestID = indie.individualID;
					}
				}
			}
			return best;
		}
	}
}