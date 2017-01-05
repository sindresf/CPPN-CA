using System;
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
			SpeciesFitness = float.MinValue;
			BestFitnessAchieved = float.MinValue;
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
			SpeciesFitness = (float)(Math.Pow(populace.SumFitness(), 2) / populace.Count);
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

		public List<NEATIndividual> MakeNextGeneration(int AllowedPopulaceSize, IDCounters IDs, List<int> newNodeGenes, List<int> newConnectionGenes)
		{
			var missFits = new List<NEATIndividual>();

			var _populace = new List<NEATIndividual>();
			if(populace.Count >= EAParameters.LowerChampionSpeciesCount)
				_populace.Add(GetBest());

			var ASexCount = (int)(AllowedPopulaceSize*EAParameters.ASexualReproductionQuota);
			var ASexPopulace = new List<NEATIndividual>(populace);

			_populace.AddRange(ASexualPart(ASexCount, ASexPopulace, missFits, IDs));
			_populace.AddRange(SexualPart(AllowedPopulaceSize, IDs, missFits));

			populace = new List<NEATIndividual>(_populace);

			return missFits;
		}

		private List<NEATIndividual> ASexualPart(int ASexCount, List<NEATIndividual> ASexPopulace, List<NEATIndividual> missFits, IDCounters IDs)
		{
			var _populace = new List<NEATIndividual>();
			while(_populace.Count < ASexCount && !ASexPopulace.IsEmpty())
			{
				var origIndie = Neat.random.Individual(ASexPopulace);
				origIndie.genome = Mutator.Mutate(origIndie.genome, IDs);
				var mutatedIndie = new NEATIndividual(origIndie);

				if(BelongsInSpecies(mutatedIndie))
					_populace.Add(mutatedIndie);
				else
					missFits.Add(mutatedIndie);
				ASexPopulace.Remove(origIndie);
			}
			return _populace;
		}
		private List<NEATIndividual> SexualPart(int AllowedPopulaceSize, IDCounters IDs, List<NEATIndividual> missFits)
		{
			var _populace = new List<NEATIndividual>();
			while(_populace.Count < AllowedPopulaceSize)
			{
				var dad = Neat.random.Individual(populace);
				var mum = Neat.random.Individual(populace);
				var child = new NEATIndividual(Mutator.Crossover(dad,mum),IDs);
				if(Neat.random.NextBoolean(EAParameters.SexualReproductionStillMutateChance))
					child.genome = Mutator.Mutate(child.genome, IDs);
				if(BelongsInSpecies(child))
					_populace.Add(child);
				else
					missFits.Add(child);
			}
			return _populace;
		}
		public bool BelongsInSpecies(NEATIndividual indie)
		{
			return (indie.DifferenceTo(representative) <= EAParameters.SpeciesInclusionRadius);
		}

		public void AddIndividual(NEATIndividual indie)
		{
			populace.Add(indie);
		}

		private NEATIndividual GetBest()
		{
			float bestFitness = 0.0f;
			List<NEATIndividual> bestIndies = new List<NEATIndividual>();
			bestFitness = float.MinValue;
			foreach(var indie in populace)
			{
				if(indie.Fitness > bestFitness)
				{
					bestIndies = new List<NEATIndividual>();
					bestIndies.Add(indie);
				} else if(indie.Fitness == bestFitness)
					bestIndies.Add(indie);
			}
			if(bestIndies.Count == 1)
				return bestIndies[0];
			else
				return Neat.random.Individual(bestIndies);
		}
	}
}