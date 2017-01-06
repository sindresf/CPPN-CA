using CPPNNEATCA.NEAT.Parts;

namespace CPPNNEATCA.EA.Base
{
	abstract class Genome
	{
		public bool hasMutated { get; set; }
		public abstract void Initialize(Population population);
	}
}