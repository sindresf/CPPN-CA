using System;
using CPPNNEAT.EA.Base;

namespace CPPNNEAT.EA
{
	class ConnectionGene : Gene
	{
		public readonly int fromNodeID, toNodeID;
		public bool isEnabled;
		public float connectionWeight { get; set; }

		public ConnectionGene(int geneID, int fromNodeID, int toNodeID, bool isEnabled, float connectionWeight)
		{
			this.geneID = geneID;
			this.fromNodeID = fromNodeID;
			this.toNodeID = toNodeID;
			this.isEnabled = isEnabled;
			this.connectionWeight = connectionWeight;
		}

		public override bool Equals(object other)
		{
			return ((ConnectionGene)other).geneID == geneID;
		}

		public static bool operator ==(ConnectionGene g1, ConnectionGene g2)
		{
			return g1.geneID == g2.geneID;
		}

		public static bool operator !=(ConnectionGene g1, ConnectionGene g2)
		{
			return g1.geneID != g2.geneID;
		}

		public override int GetHashCode()
		{
			return geneID;
		}
	}
}