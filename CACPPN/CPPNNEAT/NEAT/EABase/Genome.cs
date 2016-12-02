using CPPNNEATCA.Utils;

namespace CPPNNEATCA.EA.Base
{
	abstract class Genome
	{
		public bool hasMutated { get; protected set; }
		public abstract void Initialize(IDCounters IDs);
	}
}