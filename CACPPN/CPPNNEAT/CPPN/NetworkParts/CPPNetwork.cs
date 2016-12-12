using System.Collections.Generic;
using CPPNNEATCA.EA.Base;
using CPPNNEATCA.NEAT;
using CPPNNEATCA.NEAT.Parts;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.CPPN.Parts
{
	class CPPNetwork : ICPPNetwork
	{
		private List<NetworkNode> inputNodes, hiddenNodes, outputNodes;
		private CPPNParameters parameters;

		public CPPNetwork(NeatGenome genome, CPPNParameters parameters)
		{
			this.parameters = parameters;
			inputNodes = new List<NetworkNode>();
			hiddenNodes = new List<NetworkNode>();
			outputNodes = new List<NetworkNode>();
			SetupNodeList(genome.nodeGenes);
			SetupConnectionMatrix(genome.connectionGenes);
		}

		private void SetupNodeList(GeneSequence<NodeGene> nodeGenes) //makes this a lot easier to just have "no sensor nodes" a list of hidden and funnel to output
		{
			for(int i = 0; i < nodeGenes.Count; i++)
			{
				switch(nodeGenes[i].type)
				{
				case NodeType.Sensor:
					inputNodes.Add(new NetworkNode(nodeGenes[i].nodeID, nodeGenes[i].nodeInputFunction));
					break;
				case NodeType.Hidden:
					hiddenNodes.Add(new NetworkNode(nodeGenes[i].nodeID, nodeGenes[i].nodeInputFunction));
					break;
				case NodeType.Output:
					outputNodes.Add(new NetworkNode(nodeGenes[i].nodeID, nodeGenes[i].nodeInputFunction));
					break;
				}
			}
		}

		private void SetupConnectionMatrix(GeneSequence<ConnectionGene> connectionGenes)
		{
			for(int i = 0; i < connectionGenes.Count; i++)
			{
				int connectionInCount = 0;
				foreach(ConnectionGene gene in connectionGenes)
					if(gene.toNodeID == hiddenNodes[i].nodeID || gene.toNodeID == outputNode.nodeID) // not how it works though
						connectionInCount++;
			}
			//connections established
			//now make them be weights
		}

		public float GetOutput(List<float> input)
		{
			return (float)Neat.random.NextRangedDouble(0.5, 0.49);/*
			if(hiddenNodes.Length == 0)
			{// means straight up input to output
			 //for each input get the weight to make the tupleList
				return outputNode.GetOutput(null);// input);
			} else
			{
				TupleList<float,float> outputs = new TupleList<float, float>();

				//TODO check with the CPPN paper if the way the genes are structured actually takes automatically care of the ordering

				//here go through all the nodes
				//if they're sensor nodes skip (or find away to not store them at all
				//if they are hidden with only connections to sensor nodes they go first
				//and then hidden with sensor
				//and then only hidden
				//and then the output node(s)

				//recursive memoized call from the output node?
				//if sensorNode return input

				if(outputNode != null)
					return outputNode.GetOutput(outputs); //this is the ActivationFunction GetOutput function!
			}
			return 0.0f;*/
		}
	}
}