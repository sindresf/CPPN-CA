using CPPNNEATCA.EA.Base;

namespace CPPNNEATCA.NEAT.Parts
{
	class ConnectionGene : Gene
	{
		public readonly int fromNodeID, toNodeID;
		public bool isEnabled;
		public float connectionWeight { get; set; }

		public ConnectionGene(int geneID, int fromNodeID, int toNodeID, bool isEnabled, float connectionWeight) : base(geneID)
		{
			this.fromNodeID = fromNodeID;
			this.toNodeID = toNodeID;
			this.isEnabled = isEnabled;
			this.connectionWeight = connectionWeight;
		}

		public ConnectionGene(ConnectionGene gene) : base(gene.geneID)
		{
			fromNodeID = gene.fromNodeID;
			toNodeID = gene.toNodeID;
			isEnabled = gene.isEnabled;
			connectionWeight = gene.connectionWeight;
		}
	}
}