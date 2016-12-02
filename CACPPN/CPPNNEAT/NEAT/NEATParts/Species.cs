using System.Collections.Generic;
using System.Threading.Tasks;
using CPPNNEATCA.CA;
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

		public void EvaluatePopulace(INeatCA ca)
		{
			Parallel.ForEach(populace, indie => { indie.Evaluate(ca, populace.Count); });
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
		}
	}
}