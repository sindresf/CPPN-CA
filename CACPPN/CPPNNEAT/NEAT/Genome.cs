using System.Collections.Generic;

namespace CPPNNEAT.NEAT
{
	class Genome
	{
		public List<NodeGene> nodeGenes { get; set; }
		public List<ConnectionGene> connectionGenes { get; set; }

		public bool hasMutated { get; private set; }

		public Genome()
		{
			hasMutated = false;
		}

		// functions for adding new genes which disables older ones and such
		public void Initialize(IDCounters IDs)
		{
			InitilizeNodeGenes(IDs);
			InitializeConnectionGenes(IDs);
		}

		private void InitilizeNodeGenes(IDCounters IDs)
		{
			nodeGenes = new List<NodeGene>();
			//add the input nodes and the output node   <- always minimal start in NEAT
			int nodeCount = CPPNetworkParameters.CPPNetworkInputSize + CPPNetworkParameters.CPPNetworkOutputSize;
			for(int i = 0; i < nodeCount; i++)
				if(i == nodeCount - 1)
					nodeGenes.Add(new NodeGene(IDs.NodeGeneID,
						i,
						NodeType.Sensor,
						CPPN.ActivationFunctionType.Input)); //actually choose this one at random
				else
					nodeGenes.Add(new NodeGene(IDs.NodeGeneID,
						i,
						NodeType.Sensor,
						CPPN.ActivationFunctionType.Input));
		}

		private void InitializeConnectionGenes(IDCounters IDs)
		{
			connectionGenes = new List<ConnectionGene>();
			foreach(NodeGene gene in nodeGenes)
			{
				connectionGenes.Add(new ConnectionGene(IDs.ConnectionGeneID,
					gene.nodeID,
					nodeGenes.Count - 1,
					true,
					CPPNetworkParameters.InitialMaxConnectionWeight)); //needs random init around param
			}
		}

		public Genome GetMutatedGenome(Genome genome, MutationType mutationType)
		{
			switch(mutationType)
			{
			case MutationType.AddConnection:
				return genome;
			case MutationType.AddNode:
				return genome;
			case MutationType.ChangeFunction:
				return genome;
			case MutationType.ChangeWeight:
				return genome;
			default:
				return genome;
			}
		}
	}
}