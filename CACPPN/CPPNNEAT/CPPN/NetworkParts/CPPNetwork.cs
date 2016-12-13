using System;
using System.Collections.Generic;
using CPPNNEATCA.EA.Base;
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
					outputNodes.Add(nodeID, new OutputNetworkNode(nodeID, 0, ((InternalNodeGene)nodeGene).Function) as INetworkNode);
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
			PropagateInput(input);
			if(hiddenNodes.Count > 0)
				PropagateInternal();

			return CheckStateVote();
		}
		private void PropagateInput(List<float> input)
		{
			foreach(InputNetworkNode node in inputNodes.Values)
				node.PropagateOutput(input[node.nodeID]);
		}
		private void PropagateInternal()
		{
			while(awaitingNotificationsNodes.Count > 0)
			{
				var doneNodesID = new List<int>();
				foreach(InternalNetworkNode node in awaitingNotificationsNodes.Values)
					if(node.IsFullyNotified)
					{
						node.PropagateOutput();
						doneNodesID.Add(node.nodeID);
					}
				foreach(int ID in doneNodesID)
					awaitingNotificationsNodes.Remove(ID);
			}
		}
		private int CheckStateVote()
		{
			int voted = -1;
			float maxActivation = float.MinValue;
			foreach(INetworkNode Inode in outputNodes.Values)
			{
				var node = ((OutputNetworkNode)Inode);
				var activationLevel = node.Activation;
				if(activationLevel > maxActivation)
				{
					maxActivation = activationLevel;
					voted = node.representedState;
				}
			}
			return voted;
		}
	}
}