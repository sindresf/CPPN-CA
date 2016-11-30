using CPPNNEAT.CPPN;
using CPPNNEAT.NEAT.Base;

namespace CPPNNEAT.EA
{
	class NodeGene : Gene
	{
		public readonly int nodeID;
		public readonly NodeType type;
		public readonly ActivationFunction nodeInputFunction;
		public readonly ActivationFunctionType functionType;

		public NodeGene(int geneID, int nodeID, NodeType type, ActivationFunctionType nodeInputFunction)
		{
			this.geneID = geneID;
			this.nodeID = nodeID;
			this.type = type;
			functionType = nodeInputFunction;
			if(this.type == NodeType.Hidden || this.type == NodeType.Output)
				this.nodeInputFunction = ActivationFunction.GetRandomInitializedFunction(functionType);
			else
				this.nodeInputFunction = null; // Sensor nodes arent really "a thing"
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