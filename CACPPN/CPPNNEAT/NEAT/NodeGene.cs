using CPPNNEAT.CPPN;
using CPPNNEAT.EA.Base;
using CPPNNEAT.Utils;
using System;

namespace CPPNNEAT.EA
{
	class NodeGene : Gene
	{
		public readonly int nodeID;
		public readonly NodeType type;
		public readonly ActivationFunctionType functionType;
		public ActivationFunction nodeInputFunction { get; private set; }

		public NodeGene(int geneID, int nodeID, NodeType type, ActivationFunctionType nodeInputFunctionType) : base(geneID)
		{
			this.nodeID = nodeID;
			this.type = type;
			functionType = nodeInputFunctionType;
			nodeInputFunction = ActivationFunction.GetRandomInitializedFunction(functionType);
		}

		public NodeGene(NodeGene gene) : base(gene.geneID)
		{
			nodeID = gene.nodeID;
			type = gene.type;
			functionType = gene.functionType;
			nodeInputFunction = gene.nodeInputFunction;
		}

		public NodeGene ChangeFunction(ActivationFunctionType newFunctionType, int newGeneID)
		{
			if(type != NodeType.Sensor)
			{
				return new NodeGene(newGeneID, nodeID, type, newFunctionType);
			} else
				throw new ArgumentException("Sensor nodes not supposed to have functions!");
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