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
		private Dictionary<int, INetworkNode> hiddenNodes, outputNodes;
		private Dictionary<int, bool> nodeHasPropagated;
		private CPPNParameters parameters;

		public CPPNetwork(NeatGenome genome, CPPNParameters parameters)
		{
			this.parameters = parameters;

			inputNodes = new Dictionary<int, InputNetworkNode>();
			hiddenNodes = new Dictionary<int, INetworkNode>();
			outputNodes = new Dictionary<int, INetworkNode>();
			nodeHasPropagated = new Dictionary<int, bool>();

			SetupNodeList(genome.nodeGenes);
			SetupConnections(genome.connectionGenes);
		}

		private void SetupNodeList(GeneSequence<NodeGene> nodeGenes)
		{
			for(int i = 0; i < nodeGenes.Count; i++)
			{
				var nodeID = nodeGenes[i].nodeID;
				switch(nodeGenes[i].type)
				{
				case NodeType.Sensor:
					inputNodes.Add(nodeID, new InputNetworkNode(nodeID));
					break;
				case NodeType.Hidden:
					nodeHasPropagated[nodeID] = false;
					hiddenNodes.Add(nodeID, new InternalNetworkNode(nodeID, ((InternalNodeGene)nodeGenes[i]).Function));
					break;
				case NodeType.Output:
					outputNodes.Add(nodeID, new OutputNetworkNode(nodeID, 0.0f, ((InternalNodeGene)nodeGenes[i]).Function) as INetworkNode);
					break;
				}
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
					((InternalNetworkNode)hiddenNodes[gene.fromNodeID]).AddOutConnection(toDict[gene.toNodeID]);
			}
			if(hiddenNodes.Count > 0) foreach(INetworkNode node in hiddenNodes.Values) node.SetupDone();
			foreach(INetworkNode node in outputNodes.Values) node.SetupDone();
		}
		private Dictionary<int, INetworkNode> GetToNodeContainingDict(int tokey)
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
			if(hiddenNodes.Count > 0)
			{
				foreach(InputNetworkNode node in inputNodes.Values)
					node.PropagateOutput(input[node.nodeID]);

				while(!hasAllPropagated())
				{
					foreach(InternalNetworkNode node in hiddenNodes.Values)
					{
						if(node.IsFullyNotified())
						{
							nodeHasPropagated[node.nodeID] = true;
							node.PropagateOutput();
						}
					}
				}
				return ret;
			} else //this means there's just input-output, meaning "direct state voting"
			{

				return CheckStateVote();
			}
		}
		private bool hasAllPropagated()
		{
			bool hasAll = true;
			foreach(bool has in nodeHasPropagated.Values)
				if(!has)
					hasAll = false;
			return hasAll;
		}
		private float CheckStateVote()
		{
			float voted = -1;
			foreach(InternalNetworkNode node in outputNodes.Values)
			{
				node.IsFullyNotified();
			}
			return 1.0f;
		}
	}
}