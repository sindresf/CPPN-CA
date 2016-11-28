namespace CPPNNEAT.NEAT
{
	class ConnectionGene // needs a distance function that goes into "toNodeID" to get the activation function params.
	{
		public readonly int geneID;
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