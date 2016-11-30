namespace CPPNNEAT.EA.Base
{
	abstract class EvolutionaryAlgorithm
	{
		public int CurrentGeneration { get; protected set; }

		public abstract void InitializePopulation();
		public abstract void EvaluatePopulation();
		public abstract void NextGeneration();
		public abstract float GetBestFitness();
	}
}