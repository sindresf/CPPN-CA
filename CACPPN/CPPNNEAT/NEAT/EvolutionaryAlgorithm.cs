namespace CPPNNEAT.NEAT
{
	class EvolutionaryAlgorithm
	{
		public Population population;
		public IDCounters IDs;
		public int generationCount = 0;

		public EvolutionaryAlgorithm(PlaceHolderCARunner ca) //to become the wrapper for the CA so NEAT can run exactly the same
		{
			IDs = new IDCounters();
			population = new Population(ca);
		}

		public void InitializePopulation()
		{
			population.Initialize(IDs);
		}

		public void EvaluatePopulation()
		{
			population.Evaluate();
		}

		public void MakeNextGeneration()
		{
			generationCount++;
			population.MakeNextGeneration(IDs);
		}

		public float GetBestFitness()
		{
			float best = 0.0f;
			foreach(Species sp in population.species)
			{
				foreach(Individual indie in sp.populace)
				{
					if(indie.Fitness > best)
						best = indie.Fitness;
				}
			}
			return best;
		}
	}
}