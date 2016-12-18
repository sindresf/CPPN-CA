using CPPNNEATCA.CPPN.Parts;
using CPPNNEATCA.EA.Base;

namespace CPPNNEATCA.NEAT.Parts
{
	enum NodeType
	{
		Sensor,
		Output,
		Hidden
	}
	class NodeGene : Gene
	{
		public readonly int nodeID;
		public NodeType type { get; protected set; }

		public NodeGene(int geneID, int nodeID) : base(geneID)
		{
			this.nodeID = nodeID;
		}

		public NodeGene(NodeGene gene) : base(gene.geneID)
		{
			nodeID = gene.nodeID;
			type = gene.type;
		}
	}

	class SensorNodeGene : NodeGene
	{
		public SensorNodeGene(int geneID, int nodeID) : base(geneID, nodeID)
		{
			type = NodeType.Sensor;
		}
		public SensorNodeGene(SensorNodeGene gene) : base(gene)
		{
			type = NodeType.Sensor;
		}
	}

	class InternalNodeGene : NodeGene
	{
		public ActivationFunction Function;
		public InternalNodeGene(int geneID, int nodeID, ActivationFunctionType type) : base(geneID, nodeID)
		{
			Function = ActivationFunction.GetRandomInitializedFunction(type);
		}
		public InternalNodeGene(int geneID, int nodeID, ActivationFunction function) : base(geneID, nodeID)
		{
			Function = function;
		}
		public InternalNodeGene(InternalNodeGene gene) : base(gene.geneID, gene.nodeID)
		{
			Function = gene.Function;
		}

		public InternalNodeGene ChangeFunction(ActivationFunctionType newType, int geneID)
		{
			ActivationFunction function = ActivationFunction.GetRandomInitializedFunction(newType);
			InternalNodeGene gene = new InternalNodeGene(geneID,nodeID,function);
			return gene;
		}
	}

	class HiddenNodeGene : InternalNodeGene
	{
		public HiddenNodeGene(int geneID, int nodeID, ActivationFunctionType function) : base(geneID, nodeID, function)
		{
			type = NodeType.Hidden;
		}
		public HiddenNodeGene(int geneID, int nodeID, ActivationFunction function) : base(geneID, nodeID, function)
		{
			type = NodeType.Hidden;
		}
		public HiddenNodeGene(HiddenNodeGene gene) : base(gene)
		{
			type = NodeType.Hidden;
		}
	}

	class OutputNodeGene : InternalNodeGene
	{
		public readonly float representedState;
		public OutputNodeGene(int geneID, int nodeID, float representedState, ActivationFunctionType function) : base(geneID, nodeID, function)
		{
			type = NodeType.Output;
			this.representedState = representedState;
		}
		public OutputNodeGene(int geneID, int nodeID, float representedState, ActivationFunction function) : base(geneID, nodeID, function)
		{
			type = NodeType.Output;
			this.representedState = representedState;
		}
		public OutputNodeGene(OutputNodeGene gene) : base(gene)
		{
			type = NodeType.Output;
			representedState = gene.representedState;
		}
	}
}