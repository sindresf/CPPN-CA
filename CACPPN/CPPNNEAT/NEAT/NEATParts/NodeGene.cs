using System;
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
	}

	class InternalNodeGene : NodeGene
	{
		public InternalNodeGene(int geneID, int nodeID, ActivationFunction function) : base(geneID, nodeID)
		{

		}

		public InternalNodeGene ChangeFunction(ActivationFunctionType newType, int geneID)
		{
			throw new NotImplementedException("last thing on my fucking list!");
		}
	}

	class HiddenNodeGene : InternalNodeGene
	{
		public ActivationFunction function;
		public HiddenNodeGene(int geneID, int nodeID, ActivationFunction function) : base(geneID, nodeID, function)
		{
			type = NodeType.Hidden;
			this.function = function;
		}
	}

	class OutputNodeGene : InternalNodeGene
	{
		public readonly float representedState;
		public ActivationFunction function;
		public OutputNodeGene(int geneID, int nodeID, float representedState, ActivationFunction function) : base(geneID, nodeID, function)
		{
			type = NodeType.Output;
			this.representedState = representedState;
			this.function = function;
		}
	}
}