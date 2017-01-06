namespace CPPNNEATCA.EA.Base
{
	abstract class EvolutionaryAlgorithm
	{
		public int CurrentGeneration { get; protected set; }
		public abstract void InitializePopulation();
		public abstract void EvaluatePopulation(int currentGeneration);

		public abstract bool IsDeadRun();
		public abstract bool SolvedIt();
		public abstract void NextGeneration();
		public abstract Individual GetBestIndividual();//placeholder for proper "GetAllTheStatistics" type of func (or class delegating func)
	}
}