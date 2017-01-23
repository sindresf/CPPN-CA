using System;
using System.Collections.Generic;
using CPPNNEATCA.EA.Base;
using CPPNNEATCA.NEAT.Base;
using CPPNNEATCA.NEAT.Parts;

namespace CPPNNEATCA.CPPN.Parts
{
	class CPPNetwork : ICPPNetwork
	{
		public Dictionary<int, InputNetworkNode> inputNodes;
		public Dictionary<int, INetworkNode> hiddenNodes, outputNodes;
		public Dictionary<int, INetworkNode> awaitingNotificationsNodes;
		public CPPNParameters parameters;


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
			var seenBefore = new List<int>();
			foreach(var node in nodeGenes)
				if(!seenBefore.Contains(node.nodeID))
					seenBefore.Add(node.nodeID);
				else
				{
					Console.WriteLine();
					foreach(var n in nodeGenes)
					{
						Console.Write("nID:{0} ", n.nodeID);
					}
					Console.WriteLine();
					throw new Exception("network same nodeID in network!");
				}

			int representedState = 0;
			foreach(NodeGene gene in nodeGenes)
			{
				var nodeID = gene.nodeID;
				switch(gene.type)
				{
				case NodeType.Sensor:
					inputNodes.Add(nodeID, new InputNetworkNode(nodeID));
					break;
				case NodeType.Hidden:
					var hnode =  new InternalNetworkNode(nodeID, ((InternalNodeGene)gene).Function) as INetworkNode;
					awaitingNotificationsNodes[nodeID] = hnode;
					hiddenNodes.Add(nodeID, hnode);
					break;
				case NodeType.Output:
					var onode = new OutputNetworkNode(nodeID, representedState++, ((InternalNodeGene)gene).Function) as INetworkNode;
					outputNodes.Add(nodeID, onode);
					break;
				}
			}
		}
		private void SetupConnections(ConnectionGeneSequence connectionGenes)
		{
			foreach(ConnectionGene gene in connectionGenes)
			{
				if(gene.isEnabled)
				{
					bool contains = hiddenNodes.ContainsKey(gene.toNodeID);
					var toDict = contains ? hiddenNodes : outputNodes;
					var toNode = toDict[gene.toNodeID];
					toNode.AddInputConnection(gene.fromNodeID, gene.connectionWeight);

					if(inputNodes.ContainsKey(gene.fromNodeID))
						inputNodes[gene.fromNodeID].AddOutConnection(toDict[gene.toNodeID]);

					else if(hiddenNodes.ContainsKey(gene.fromNodeID))
						((InternalNetworkNode)hiddenNodes[gene.fromNodeID]).AddOutConnection(toDict[gene.toNodeID]);
				}
			}
		}

		public int GetNextState(List<float> input)
		{
			PropagateInput(input);
			if(hiddenNodes.Count > 0)
				PropagateInternal();
			int state;
			state = CheckStateVote();
			return state;
		}
		public void ActivateOutputNodes(List<float> input)
		{
			PropagateInput(input);
			if(hiddenNodes.Count > 0)
				PropagateInternal();
		}
		private void PropagateInput(List<float> input)
		{
			foreach(InputNetworkNode node in inputNodes.Values)
				node.PropagateOutput(input[node.nodeID]);
		}
		private void PropagateInternal()
		{
			var whileRuns = 1;
			while(awaitingNotificationsNodes.Count > 0)
			{
				if(whileRuns > 500)
					break;
				var doneNodesID = new List<int>();
				foreach(InternalNetworkNode node in awaitingNotificationsNodes.Values)
					if(node.IsFullyNotified)
					{
						node.PropagateOutput();
						doneNodesID.Add(node.nodeID);
					}
				foreach(int ID in doneNodesID)
					awaitingNotificationsNodes.Remove(ID);
				whileRuns++;
			}
		}
		private int CheckStateVote()
		{
			int voted = -1;
			float MostActivated = float.MinValue;
			foreach(INetworkNode Inode in outputNodes.Values)
			{
				var node = ((OutputNetworkNode)Inode);
				if(!node.IsFullyNotified)
				{
					//throw new Exception("The fuck!? not notified output node!");
				}
				var activationLevel = node.Activation;
				if(activationLevel > MostActivated)
				{
					MostActivated = activationLevel;
					voted = node.representedState;
				}
			}
			return voted;
		}
		public void ResetNetwork()
		{
			foreach(InternalNetworkNode node in hiddenNodes.Values)
				node.RemoveOldInput();
			foreach(InternalNetworkNode node in outputNodes.Values)
				node.RemoveOldInput();
		}
	}
}