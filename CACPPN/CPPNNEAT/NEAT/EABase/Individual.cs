using CPPNNEATCA.CA;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.NEAT.Base
{
	abstract class Individual
	{
		public readonly int individualID;
		public float Fitness { get; protected set; }

		public Individual(int individualID)
		{
			this.individualID = individualID;
		}

		public abstract void Initialize(IDCounters IDs);
		public abstract void Evaluate(INeatCA CARunner, int speciesCount);

	}
}