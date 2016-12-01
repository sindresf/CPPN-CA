using CPPNNEAT.CPPN;
using CPPNNEAT.EA.Base;

namespace CPPNNEAT.EA
{
	class InternalNodeGene : SensorNodeGene
	{
		public readonly ActivationFunction nodeInputFunction;
		public readonly ActivationFunctionType functionType;

		public InternalNodeGene(int geneID, int nodeID, NodeType type, ActivationFunctionType nodeInputFunction) : base(geneID, nodeID, type)
		{
			functionType = nodeInputFunction;
			if(this.type == NodeType.Hidden || this.type == NodeType.Output)
				this.nodeInputFunction = ActivationFunction.GetRandomInitializedFunction(functionType);
			else
				this.nodeInputFunction = null; // Sensor nodes arent really "a thing"
		}

		public InternalNodeGene(InternalNodeGene gene) : base(gene.geneID, gene.nodeID, gene.type)
		{
			functionType = gene.functionType;
			nodeInputFunction = gene.nodeInputFunction;
		}

		public override bool Equals(object other)
		{
			return ((InternalNodeGene)other).geneID == geneID;
		}

		public static bool operator ==(InternalNodeGene g1, InternalNodeGene g2)
		{
			return g1.geneID == g2.geneID;
		}

		public static bool operator !=(InternalNodeGene g1, InternalNodeGene g2)
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