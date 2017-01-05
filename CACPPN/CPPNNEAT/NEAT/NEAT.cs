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
			}
			return false;
		}

		public override Individual GetBestIndividual()
		{
			Console.WriteLine("\nspecies present:" + population.species.Count);

			NEATIndividual bestIndie = population.GetBestIndividual();
			Console.WriteLine("best:{0}\n", bestIndie.Fitness);
			return bestIndie;
		}
	}
}