namespace CPPNNEATCA.EA.Base
{
	abstract class Individual
	{
		public readonly int individualID;
		public float Fitness { get; protected set; }

		public Individual(int indieID)
		{
			individualID = indieID;
		}

		public abstract void Evaluate(int speciesCount);
	}
}