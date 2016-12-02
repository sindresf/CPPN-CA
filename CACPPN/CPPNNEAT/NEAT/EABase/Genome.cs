using CPPNNEATCA.Utils;

namespace CPPNNEATCA.NEAT.Base
{
	abstract class Genome
	{
		public bool hasMutated { get; protected set; }
		public abstract void Initialize(IDCounters IDs);
	}
}