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
		private Dictionary<int,int> nodeInputCounts;

		private Dictionary<int, bool> nodeHasPropagated;
		private CPPNParameters parameters;

		public CPPNetwork(NeatGenome genome, CPPNParameters parameters)
		{
			this.parameters = parameters;

			inputNodes = new Dictionary<int, InputNetworkNode>();
			hiddenNodes = new Dictionary<int, InternalNetworkNode>();
			outputNodes = new Dictionary<int, InternalNetworkNode>();
			nodeHasPropagated = new Dictionary<int, bool>();
			nodeInputCounts = new Dictionary<int, int>();

			SetupNodeList(genome.nodeGenes);
			SetupConnections(genome.connectionGenes);
		}

		private void SetupNodeList(GeneSequence<NodeGene> nodeGenes) //makes this a lot easier to just have "no sensor nodes" a list of hidden and funnel to output
		{
			for(int i = 0; i < nodeGenes.Count; i++)
			{
				var nodeID = nodeGenes[i].nodeID;
				nodeInputCounts[nodeID] = 0;
				switch(nodeGenes[i].type)
				{
				case NodeType.Sensor:
					inputNodes.Add(nodeID, new InputNetworkNode(nodeID));
					break;
				case NodeType.Hidden:
					nodeHasPropagated[nodeID] = false;
					hiddenNodes.Add(nodeID, new InternalNetworkNode(nodeID, nodeGenes[i].nodeInputFunction));
					break;
				case NodeType.Output:
					outputNodes.Add(nodeID, new InternalNetworkNode(nodeID, nodeGenes[i].nodeInputFunction));
					break;
				}
			}
		}

		private void SetupConnections(GeneSequence<ConnectionGene> connectionGenes)
		{
			foreach(ConnectionGene gene in connectionGenes)
			{
				nodeInputCounts[gene.toNodeID] = nodeInputCounts[gene.toNodeID] + 1;
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

		public float GetOutput(List<float> input)
		{
			var ret = (float)Neat.random.NextRangedDouble(0.5, 0.49);
			foreach(InputNetworkNode node in inputNodes.Values)
				node.PropagateOutput(input[node.nodeID]);

			while(!hasAllPropagated())
			{
				foreach(InternalNetworkNode node in hiddenNodes.Values)
				{
					if(node.IsFullyNotified(nodeInputCounts[node.nodeID]))
					{
						nodeHasPropagated[node.nodeID] = true;
						node.PropagateOutput();
					}
				}
			}
			return ret;
		}
		private bool hasAllPropagated()
		{
			bool hasAll = true;
			foreach(bool has in nodeHasPropagated.Values)
				if(!has)
					hasAll = false;
			return hasAll;
		}
	}
}