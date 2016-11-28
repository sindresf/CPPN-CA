using CPPNNEAT.CPPN;

namespace CPPNNEAT.NEAT
{
	class NodeGene
	{
		public readonly int geneID;
		public readonly int nodeID;
		public readonly NodeType type;
		public readonly ActivationFunction nodeInputFunction;

		public NodeGene(int geneID, int nodeID, NodeType type, ActivationFunctionType nodeInputFunction)
		{
			this.geneID = geneID;
			this.nodeID = nodeID;
			this.type = type;
			if(this.type == NodeType.Hidden || this.type == NodeType.Output)
				this.nodeInputFunction = ActivationFunction.GetRandomInitializedFunction(nodeInputFunction);
			else
				this.nodeInputFunction = null;
		}

		public override bool Equals(object other)
		{
			return ((NodeGene)other).geneID == geneID;
		}

		public static bool operator ==(NodeGene g1, NodeGene g2)
		{
			return g1.geneID == g2.geneID;
		}

		public static bool operator !=(NodeGene g1, NodeGene g2)
		{
			return g1.geneID != g2.geneID;
		}

		public override int GetHashCode()
		{
			return geneID;
		}
	}

	enum NodeType
	{
		Sensor,
		Output,
		Hidden
	}
}