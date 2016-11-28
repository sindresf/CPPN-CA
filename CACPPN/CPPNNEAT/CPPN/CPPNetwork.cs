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
		private OutputNode outputNode;

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
				switch(genome.nodeGenes[i].type)
				{
				case NodeType.Sensor:
					nodes[i] = genome.nodeGenes[i].nodeInputFunction as SensorNode;
					nodes[i].nodeID = genome.nodeGenes[i].nodeID;
					break;
				case NodeType.Hidden:
					nodes[i] = genome.nodeGenes[i].nodeInputFunction as HiddenNode;
					nodes[i].nodeID = genome.nodeGenes[i].nodeID;
					break;
				case NodeType.Output:
					nodes[i] = genome.nodeGenes[i].nodeInputFunction as OutputNode;
					if(CPPNetworkParameters.CPPNetworkOutputSize == 1)
						outputNode = genome.nodeGenes[i].nodeInputFunction as OutputNode;
					nodes[i].nodeID = genome.nodeGenes[i].nodeID;
					break;
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
		{
			TupleList<float,float> outputs = new TupleList<float, float>();

			//TODO check with the CPPN paper if the way the genes are structured actually takes automatically care of the ordering

			//here go through all the nodes
			//if they're sensor nodes skip (or find away to not store them at all
			//if they are hidden with only connections to sensor nodes they go first
			//and then hidden with sensor
			//and then only hidden
			//and then the output node(s)

			if(outputNode != null)
				return outputNode.GetOutput(outputs);
			return 0.0f;
		}
	}
}