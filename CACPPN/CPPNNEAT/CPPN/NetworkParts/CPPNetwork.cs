using System;
using System.Collections.Generic;
using CPPNNEATCA.EA.Base;
using CPPNNEATCA.NEAT;
using CPPNNEATCA.NEAT.Parts;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.CPPN.Parts
{
	class CPPNetwork : ICPPNetwork
	{
		private Dictionary<int, InputNetworkNode> inputNodes;
		private Dictionary<int, InternalNetworkNode> hiddenNodes, outputNodes;
		private CPPNParameters parameters;

		public CPPNetwork(NeatGenome genome, CPPNParameters parameters)
		{
			this.parameters = parameters;
			inputNodes = new Dictionary<int, InputNetworkNode>();
			hiddenNodes = new Dictionary<int, InternalNetworkNode>();
			outputNodes = new Dictionary<int, InternalNetworkNode>();
			SetupNodeList(genome.nodeGenes);
			SetupConnections(genome.connectionGenes);
		}

		private void SetupNodeList(GeneSequence<NodeGene> nodeGenes) //makes this a lot easier to just have "no sensor nodes" a list of hidden and funnel to output
		{
			for(int i = 0; i < nodeGenes.Count; i++)
				switch(nodeGenes[i].type)
				{
				case NodeType.Sensor:
					inputNodes.Add(nodeGenes[i].nodeID, new InputNetworkNode(nodeGenes[i].nodeID));
					break;
				case NodeType.Hidden:
					hiddenNodes.Add(nodeGenes[i].nodeID, new InternalNetworkNode(nodeGenes[i].nodeID, nodeGenes[i].nodeInputFunction));
					break;
				case NodeType.Output:
					outputNodes.Add(nodeGenes[i].nodeID, new InternalNetworkNode(nodeGenes[i].nodeID, nodeGenes[i].nodeInputFunction));
					break;
				}
		}

		private void SetupConnections(GeneSequence<ConnectionGene> connectionGenes)
		{
			foreach(ConnectionGene gene in connectionGenes)
			{
				var toDict = GetToNodeContainingDict(gene.toNodeID);
				toDict[gene.toNodeID].AddInputConnection(gene.fromNodeID, gene.connectionWeight);

				if(inputNodes.ContainsKey(gene.fromNodeID))
					inputNodes[gene.fromNodeID].AddOutConnection(toDict[gene.toNodeID]);

				else if(hiddenNodes.ContainsKey(gene.fromNodeID))
					hiddenNodes[gene.fromNodeID].AddOutConnection(toDict[gene.toNodeID]);
			}
		}
		private Dictionary<int, InternalNetworkNode> GetToNodeContainingDict(int tokey)
		{
			if(hiddenNodes.ContainsKey(tokey))
				return hiddenNodes;
			if(outputNodes.ContainsKey(tokey))
				return outputNodes;
			throw new ArgumentException("The fuck!? that nodeGeneID:" + tokey + " is in No of the Network nodes that can be To nodes!");
		}

		public float GetOutput(Dictionary<int, float> input)
		{
			var ret = (float)Neat.random.NextRangedDouble(0.5, 0.49);
			foreach(InputNetworkNode node in inputNodes.Values)
				node.PropagateOutput(input[node.nodeID]);

			return ret;
		}
	}
}