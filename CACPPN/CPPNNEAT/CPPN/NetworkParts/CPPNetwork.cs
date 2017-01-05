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
					throw new Exception("same nodeID in network!");

			int representedState = 0;
			foreach(NodeGene gene in nodeGenes)
			{
				var nodeID = gene.nodeID;
				switch(gene.type)
				{
				case NodeType.Sensor:
					if(nodeID == 3 || nodeID == 4) Console.WriteLine(nodeID + " just happened");
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
				if(!gene.isEnabled) continue;
				else
				{
					bool contains = hiddenNodes.ContainsKey(gene.toNodeID);
					var toDict = contains ? hiddenNodes : outputNodes;
					if(!toDict.ContainsKey(gene.toNodeID))
					{
						Console.WriteLine("gene {0}:", gene.geneID);
						foreach(var connGene in connectionGenes)
							Console.WriteLine("conn{0}: from:{1} to:{2}", connGene.geneID, connGene.fromNodeID, connGene.toNodeID);
						Console.WriteLine();
					}
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
					} //else is a recurrent thing or something
				foreach(int ID in doneNodesID)
					awaitingNotificationsNodes.Remove(ID);
				whileRuns++;
			}
		}
		private int CheckStateVote()
		{
			int voted = -1;
			float maxActivation = float.MinValue;
			foreach(INetworkNode Inode in outputNodes.Values)
			{
				var node = ((OutputNetworkNode)Inode);
				if(!node.IsFullyNotified)
				{
					/*Console.WriteLine();
					Console.WriteLine("nodeID:{0} state:{1}, shouldHave:{2} had:{3}", node.nodeID, node.representedState, node.shouldHave, node.inValues.Count);
					Console.WriteLine("incoming connections:");*/
					var wKeys = new int[node.inWeights.Keys.Count];
					var vKeys = new int[node.inValues.Keys.Count];
					node.inWeights.Keys.CopyTo(wKeys, 0);
					node.inValues.Keys.CopyTo(vKeys, 0);
					/*Console.Write("connections: ");
					foreach(var w in wKeys)
						Console.Write(w + " ");
					Console.Write("\nvalues received: ");
					foreach(var v in vKeys)
						Console.Write(v + " ");
					Console.WriteLine();*/
					//throw new Exception("The fuck!? not notified output node!");
				}
				var activationLevel = node.Activation;
				if(activationLevel > maxActivation)
				{
					maxActivation = activationLevel;
					voted = node.representedState;
				}
			}
			return voted;
		}
		private void ResetNetwork()
		{
			foreach(InternalNetworkNode node in hiddenNodes.Values)
				node.RemoveOldInput();
			foreach(InternalNetworkNode node in outputNodes.Values)
				node.RemoveOldInput();
		}
	}
}