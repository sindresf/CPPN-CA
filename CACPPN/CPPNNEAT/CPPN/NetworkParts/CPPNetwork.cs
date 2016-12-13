using System;
using System.Collections.Generic;
using CPPNNEATCA.EA.Base;
using CPPNNEATCA.NEAT;
using CPPNNEATCA.NEAT.Parts;

namespace CPPNNEATCA.CPPN.Parts
{
	class CPPNetwork : ICPPNetwork
	{
		private Dictionary<int, InputNetworkNode> inputNodes;
		private Dictionary<int, INetworkNode> hiddenNodes, outputNodes;
		private Dictionary<int, INetworkNode> awaitingNotificationsNodes;
		private CPPNParameters parameters;

		public CPPNetwork(NeatGenome genome, CPPNParameters parameters)
		{
			this.parameters = parameters;

			inputNodes = new Dictionary<int, InputNetworkNode>();
			hiddenNodes = new Dictionary<int, INetworkNode>();
			outputNodes = new Dictionary<int, INetworkNode>();
			awaitingNotificationsNodes = new Dictionary<int, INetworkNode>();

			SetupNodeList(genome.nodeGenes);
			SetupConnections(genome.connectionGenes);
		}

		private void SetupNodeList(GeneSequence<NodeGene> nodeGenes)
		{
			for(int i = 0; i < nodeGenes.Count; i++)
			{
				var nodeGene = nodeGenes[i];
				var nodeID = nodeGene.nodeID;
				switch(nodeGene.type)
				{
				case NodeType.Sensor:
					inputNodes.Add(nodeID, new InputNetworkNode(nodeID));
					break;
				case NodeType.Hidden:
					var node =  new InternalNetworkNode(nodeID, ((InternalNodeGene)nodeGene).Function) as INetworkNode;
					awaitingNotificationsNodes[nodeID] = node;
					hiddenNodes.Add(nodeID, node);
					break;
				case NodeType.Output:
					outputNodes.Add(nodeID, new OutputNetworkNode(nodeID, 0.0f, ((InternalNodeGene)nodeGene).Function) as INetworkNode);
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

		public int GetNextState(List<float> input)
		{
			var ret = Neat.random.Next(Neat.parameters.CA.CellStateCount);
			if(hiddenNodes.Count > 0)
			{
				foreach(InputNetworkNode node in inputNodes.Values)
					node.PropagateOutput(input[node.nodeID]);

				while(awaitingNotificationsNodes.Count > 0)
				{
					foreach(InternalNetworkNode node in awaitingNotificationsNodes.Values)
					{
						if(node.IsFullyNotified())
						{
							node.PropagateOutput();
							awaitingNotificationsNodes.Remove(node.nodeID);
						}
					}
				}
				//so here every hidden node should've had their say
				//and so the politicianNodes are checked for state
				int state = CheckStateVote();
				return ret;
			} else //this means there's just input-output, meaning "direct state voting"
			{

				return CheckStateVote();
			}
		}
		private int CheckStateVote()
		{
			float voted = -1;
			foreach(InternalNetworkNode node in outputNodes.Values)
			{
				node.IsFullyNotified();
			}
			return 1;
		}
	}
}