namespace CPPNNEAT.NEAT
{
	class EvolutionaryAlgorithm
	{
		public Population population;
		public IDCounters IDs;
		public int generationCount = 0;

		public EvolutionaryAlgorithm(PlaceHolderCA ca) //to become the wrapper for the CA so NEAT can run exactly the same
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
	}
}