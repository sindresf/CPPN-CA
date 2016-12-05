using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.NEAT.Parts
{
	class Species
	{
		public readonly int speciesID;
		public List<NEATIndividual> populace { get; private set; }
		public float SpeciesFitness { get; private set; }

		public bool isDead { get; private set; }
		private float BestFitnessAchieved;
		private int improvementCount;

		public Species(int speciesID)
		{
			this.speciesID = speciesID;
			Setup();
		}

		public Species(NEATIndividual indie, int speciesID)
		{
			this.speciesID = speciesID;
			Setup();
			populace.Add(indie);
		}

		private void Setup()
		{
			SpeciesFitness = 0.0f;
			BestFitnessAchieved = 0.0f;
			populace = new List<NEATIndividual>();
			isDead = false;
			improvementCount = 0;
		}

		public void Initialize(IDCounters IDs)
		{
			for(int i = 0; i < EAParameters.PopulationSize; i++)
			{
				NEATIndividual indie = new NEATIndividual(IDs);
				indie.Initialize(IDs);
				populace.Add(indie);
			}
		}

		public void EvaluatePopulace()
		{
			Parallel.ForEach(populace, indie => { indie.Evaluate(populace.Count); });
			SpeciesFitness = populace.SumFitness();
			DiedOffCheck();
		}

		private void DiedOffCheck()
		{
			if(SpeciesFitness > BestFitnessAchieved)
				BestFitnessAchieved = SpeciesFitness;
			else
				isDead = ++improvementCount > EAParameters.SpeciesImprovementTriesBeforeDeath;
		}

		public void MakeNextGeneration(int AllowedPopulaceSize, IDCounters IDs)
		{
			//this is where the compare should come into play for fitness selection.
			//and "foreach" by the allowed amount
			var _populace = new List<NEATIndividual>();
			if(populace.Count >= EAParameters.LowerChampionSpeciesCount)
				_populace.Add(getBest());
			foreach(NEATIndividual indie in populace)
			{ //TODO this is not proper
				indie.genome = Mutator.Mutate(indie.genome, IDs);
				_populace.Add(indie);
			}
			populace = new List<NEATIndividual>(_populace);
		}

		private NEATIndividual getBest()
		{
			float bestFitness = 0.0f;
			NEATIndividual bestIndie = null;
			foreach(NEATIndividual indie in populace)
				if(indie.Fitness > bestFitness)
				{
					bestFitness = indie.Fitness;
					bestIndie = indie;
				}
			return bestIndie ?? populace[0];
		}
	}
}