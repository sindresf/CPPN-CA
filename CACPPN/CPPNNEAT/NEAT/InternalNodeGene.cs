using CPPNNEAT.CPPN;
using CPPNNEAT.EA.Base;

namespace CPPNNEAT.EA
{
	class NodeGene : Gene
	{
		public readonly int nodeID;
		public readonly NodeType type;
		public readonly ActivationFunctionType functionType;
		public readonly ActivationFunction nodeInputFunction;

		public NodeGene(int geneID, int nodeID, NodeType type, ActivationFunctionType nodeInputFunction) : base(geneID)
		{
			this.nodeInputFunction = ActivationFunction.GetRandomInitializedFunction(functionType);
		}

		public NodeGene(NodeGene gene) : base(gene.geneID)
		{
			nodeInputFunction = gene.nodeInputFunction;
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