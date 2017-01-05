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

			var ASexCount = AllowedPopulaceSize;//(int)(AllowedPopulaceSize*EAParameters.ASexualReproductionQuota);

			var ASexPopulace = new List<NEATIndividual>(populace);
			while(_populace.Count < ASexCount && !ASexPopulace.IsEmpty()) //this should instead "fill up the ASexual reproduction" and then fill up the rest with crossover
			{
				var origIndie = Neat.random.Individual(ASexPopulace);
				origIndie.genome = Mutator.Mutate(origIndie.genome, IDs);
				var mutatedIndie = new NEATIndividual(origIndie);

				if(BelongsInSpecies(mutatedIndie))
					_populace.Add(mutatedIndie); //can't actually do this because of species size things
				else
					missFits.Add(mutatedIndie);
				ASexPopulace.Remove(origIndie);
			}
			var SexualPopulace = new List<NEATIndividual>(populace);
			while(_populace.Count < AllowedPopulaceSize && !SexualPopulace.IsEmpty())
			{
				//crossover THIS IS LIKE... NEEDED!
				var dad = Neat.random.Individual(SexualPopulace); //goes by fitnessi
				var mum = Neat.random.Individual(SexualPopulace); //same

				var child = new NEATIndividual(Mutator.Crossover(dad,mum),IDs);
				if(Neat.random.NextBoolean(EAParameters.SexualReproductionStillMutateChance))
					child.genome = Mutator.Mutate(child.genome, IDs);
				if(BelongsInSpecies(child))
					_populace.Add(child); //can't actually do this because of species size things
				else
					missFits.Add(child);
				SexualPopulace.Remove(dad);
				SexualPopulace.Remove(mum);
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