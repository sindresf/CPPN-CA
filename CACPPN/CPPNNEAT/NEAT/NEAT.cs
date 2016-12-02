using System;
using CPPNNEATCA.CA;
using CPPNNEATCA.CPPN.Parts;
using CPPNNEATCA.EA.Base;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.EA
{
	class Neat : EvolutionaryAlgorithm
	{
		public Population population { get; private set; }
		public static INeatCA evaluator;
		public IDCounters IDs;
		public static readonly TupleList<float, ActivationFunctionType> ActivationFunctionChances = CPPNParameters.ActivationFunctionChanceIntervals;
		public static readonly Random random;
		public static Parameters parameters;

		public Neat(INeatCA ca) //to become the wrapper for the CA so NEAT can run exactly the same
		{
			IDs = new IDCounters();
			parameters = new Parameters();
			evaluator = ca;
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
			//don't forget elitism for species with 5+ indies
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