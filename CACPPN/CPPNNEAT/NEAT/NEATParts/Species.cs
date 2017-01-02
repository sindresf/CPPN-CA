using System.Collections.Generic;
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
		private NEATIndividual representative;

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
			var genome = new NeatGenome();
			genome.Initialize(IDs);
			for(int i = 0; i < EAParameters.PopulationSize; i++)
			{
				NEATIndividual indie = new NEATIndividual(genome, IDs);
				populace.Add(indie);
				genome = new NeatGenome(genome);
			}
		}

		public void EvaluatePopulace()
		{
			foreach(NEATIndividual indie in populace) indie.Evaluate(populace.Count);
			//Parallel.ForEach(populace, indie => { indie.Evaluate(populace.Count); });
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

		public NEATIndividual NewSpeciesRepresentative()
		{
			representative = Neat.random.Representative(populace);
			return representative;
		}

		public List<NEATIndividual> MakeNextGeneration(int AllowedPopulaceSize, IDCounters IDs)
		{
			var missFits = new List<NEATIndividual>();

			var _populace = new List<NEATIndividual>();
			if(populace.Count >= EAParameters.LowerChampionSpeciesCount)
				_populace.Add(getBest());

			/*foreach(NEATIndividual indie in populace)
			{ //TODO this is not proper
				if(Neat.random.NextDouble() <= EAParameters.ASexualReproductionQuota)
					indie.genome = Mutator.Mutate(indie.genome, IDs);
				if(BelongsInSpecies(indie))
					_populace.Add(indie); //can't actually do this because of species size things
				else
					missFits.Add(indie);
			}*/
			while(_populace.Count < AllowedPopulaceSize) //aka can have more indies next generation
			{
				var indie = Neat.random.Individual(populace);
				if(Neat.random.NextDouble() <= EAParameters.ASexualReproductionQuota)
					indie.genome = Mutator.Mutate(indie.genome, IDs);
				if(BelongsInSpecies(indie))
					_populace.Add(indie); //can't actually do this because of species size things
				else
					missFits.Add(indie);
				//crossover 
			}

			populace = new List<NEATIndividual>(_populace);

			return missFits;
		}

		public bool BelongsInSpecies(NEATIndividual indie)
		{
			bool yes = false;
			if(indie.DifferenceTo(representative) <= EAParameters.SpeciesInclusionRadius)
				yes = true;
			return yes;
		}

		public void AddIndividual(NEATIndividual indie)
		{
			populace.Add(indie);
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