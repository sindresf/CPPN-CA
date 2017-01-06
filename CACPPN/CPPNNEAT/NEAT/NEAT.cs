using System;
using System.Collections.Generic;
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

		public NEATIndividual bestAchieved;
		public int generationOfBest = 0;

		public Neat()
		{
			evaluator = parameters.experiment;
			population = new Population(evaluator);
			IDs = new IDCounters();
			population.IDs = IDs;
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
			bestAchieved = population.GetBestIndividual();
		}

		public override void EvaluatePopulation(int currentGeneration)
		{
			population.Evaluate();
			var challenger = population.GetBestIndividual();
			if(challenger != null && challenger.Fitness > bestAchieved.Fitness)
			{
				bestAchieved = challenger;
				generationOfBest = currentGeneration;
			}
		}

		public override bool IsDeadRun()
		{
			return population.species.Count == 0;
		}

		public override void NextGeneration()
		{
			CurrentGeneration++;
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
			}
			return false;
		}

		public override Individual GetBestIndividual()
		{
			var bestIndie = population.GetBestIndividual();
			return bestIndie;
		}
	}
}