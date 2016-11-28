namespace CPPNNEAT.NEAT
{
	class EvolutionaryAlgorithm
	{
		public Population population;
		public IDCounters IDs;
		public int generationCount = 0;
		//public CA ca, can be a Parameter so I can switch it out there

		public EvolutionaryAlgorithm()
		{
			IDs = new IDCounters();
		}

		public void InitializePopulation()
		{
			population = new Population();
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