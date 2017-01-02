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

		public override bool SolvedIt()
		{
			if(parameters.CA.MaxFitnessPossible != float.MinValue)
			{
				var ind = GetBestIndividual();
				var fit = ind.Fitness;
				var maxFit = parameters.CA.MaxFitnessPossible;
				bool isSame = fit.SameWithinReason(maxFit);
				return isSame;
				//return GetBestIndividual().Fitness.SameWithinReason(parameters.CA.MaxFitnessPossible);
			}
			return false; //makes it just search for "the best"
		}

		public override Individual GetBestIndividual()
		{
			var best = float.MinValue;
			NEATIndividual bestIndie = null;
			Console.Write(population.species.Count);
			foreach(Species sp in population.species)
			{
				Console.WriteLine(" " + sp.populace.Count);
				foreach(NEATIndividual indie in sp.populace)
					if(indie.Fitness > best)
					{
						best = indie.Fitness;
						bestIndie = indie;
					}
			}
			Console.WriteLine(bestIndie.Fitness);
			return bestIndie;
		}
	}
}