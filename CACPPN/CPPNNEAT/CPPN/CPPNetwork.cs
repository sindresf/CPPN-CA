using System.Collections.Generic;
using CPPNNEAT.NEAT;
using CPPNNEAT.Utils;

namespace CPPNNEAT.CPPN
{
	class CPPNetwork //no recurrent connections as a feature, because of its "once fed forward" nature (until physics might enter)s
	{
		//doesn't it say in the paper that the bias is ALWAYS one? so can just add that to any output OH YES, THE WEIGHT FOR IT

		public NetworkNode[] nodes; // to store the activation functions 
		public float[][] connections;   // to store all the weights
																		// TODO MAKE THIS A DICTIONARY ON NODE-IDs INFACT MAKE ALL THINGS DICTIONARIES ON SUCH IDs
		private NetworkNode outputNode;

		public CPPNetwork(Genome genome)
		{
			SetupNodeList(genome);
			SetupConnectionMatrix(genome);
		}

		private void SetupNodeList(Genome genome)
		{
			nodes = new NetworkNode[genome.nodeGenes.Count];

			for(int i = 0; i < genome.nodeGenes.Count; i++)
			{
				nodes[i] = genome.nodeGenes[i].nodeInputFunction as NetworkNode;
				nodes[i].nodeID = genome.nodeGenes[i].nodeID;

				if(genome.nodeGenes[i].type == NodeType.Output)
				{
					outputNode = genome.nodeGenes[i].nodeInputFunction as NetworkNode;
					outputNode.nodeID = genome.nodeGenes[i].nodeID;
				}
			}
		}

		private void SetupConnectionMatrix(Genome genome)
		{
			connections = new float[genome.nodeGenes.Count][];

			for(int i = 0; i < genome.nodeGenes.Count; i++)
			{
				int connectionCount = 0;

				foreach(ConnectionGene gene in genome.connectionGenes)
					if(gene.fromNodeID == genome.nodeGenes[i].nodeID)
						connectionCount++;

				connections[i] = new float[connectionCount];
			}
			foreach(ConnectionGene gene in genome.connectionGenes)
			{
			}
		}

		public float GetOutput(List<float> input) // represents the entirety of the input nodes
		{                                         // some optimalization here about remove the actual nodes and use this directly as input to hidden
			TupleList<float,float> outputs = new TupleList<float, float>();



			return outputNode.GetOutput(outputs);
		}
	}

	class NetworkNode : ActivationFunction
	{
		public int nodeID { get; set; }
	}
}