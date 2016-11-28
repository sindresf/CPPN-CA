using System.Collections.Generic;

namespace CPPNNEAT.NEAT
{
	class Genome
	{
		public List<NodeGene> nodeGenes;
		public List<ConnectionGene> connectionGenes;

		// functions for adding new genes which disables older ones and such
		public void Initialize()
		{
			nodeGenes = new List<NodeGene>();
			connectionGenes = new List<ConnectionGene>();
		}
	}
}