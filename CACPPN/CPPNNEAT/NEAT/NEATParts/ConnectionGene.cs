using System;
using CPPNNEATCA.EA.Base;
using CPPNNEATCA.Utils;

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

		public ConnectionGene(ConnectionGene gene, bool newWeight = false) : base(gene.geneID)
		{
			fromNodeID = gene.fromNodeID;
			toNodeID = gene.toNodeID;
			isEnabled = gene.isEnabled;
			if(newWeight)
				connectionWeight = Neat.random.InitialConnectionWeight();
			else
				connectionWeight = gene.connectionWeight;
		}
	}
}