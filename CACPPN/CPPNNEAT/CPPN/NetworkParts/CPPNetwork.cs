using System;
using System.Collections.Generic;
using CPPNNEATCA.NEAT;
using CPPNNEATCA.NEAT.Parts;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.CPPN.Parts
{
	class CPPNetwork : ICPPNetwork
	{
		private NetworkNode[] hiddenNodes;
		private float[][] connections;
		private NetworkNode outputNode;

		private Dictionary<int, float> nodeOutput;

		private CPPNParameters parameters;

		public CPPNetwork(NeatGenome genome, CPPNParameters parameters)
		{
			this.parameters = parameters;
			/*
			nodeOutput = new Dictionary<int, float>();
			SetupNodeList(genome);
			SetupConnectionMatrix(genome);*/
		}

		private void SetupNodeList(NeatGenome genome) //makes this a lot easier to just have "no sensor nodes" a list of hidden and funnel to output
		{
			int hiddenNodes = (genome.nodeGenes.Count - parameters.InputSize - parameters.OutputSize);
			if(hiddenNodes >= 1)
			{
				this.hiddenNodes = new NetworkNode[genome.nodeGenes.Count]; //just - the input and output count to get hidden? yeaa...

				for(int i = 0; i < genome.nodeGenes.Count; i++)
				{
					switch(genome.nodeGenes[i].type)
					{
					case NodeType.Hidden:
						this.hiddenNodes[i] = genome.nodeGenes[i].nodeInputFunction as NetworkNode;
						this.hiddenNodes[i].nodeID = genome.nodeGenes[i].nodeID;
						break;
					case NodeType.Output:
						if(parameters.OutputSize == 1)
						{
							outputNode = genome.nodeGenes[i].nodeInputFunction as NetworkNode;
							outputNode.nodeID = genome.nodeGenes[i].nodeID;
						}
						break;
					}
				}
			} else
			{ // so this is how it is in the beginning
				this.hiddenNodes = new NetworkNode[0];

				Console.WriteLine(genome.nodeGenes[parameters.InputSize].nodeInputFunction);// SENSOR NODE TYPE RETURNS NULL!!!! :O :'(
				outputNode = genome.nodeGenes[parameters.InputSize].nodeInputFunction as NetworkNode;
				//outputNode.nodeID = genome.nodeGenes[parameters.InputSize].nodeID;
			}
		}

		private void SetupConnectionMatrix(NeatGenome genome)
		{
			connections = new float[hiddenNodes.Length + 1][]; // +1 for the output node

			for(int i = 0; i < connections.Length; i++)
			{
				int connectionInCount = 0;
				foreach(ConnectionGene gene in genome.connectionGenes)
					if(gene.toNodeID == hiddenNodes[i].nodeID || gene.toNodeID == outputNode.nodeID) // not how it works though
						connectionInCount++;
				connections[i] = new float[connectionInCount];
			}
			//connections established
			//now make them be weights
		}

		public float GetOutput(List<float> input) // represents the entirety of the input nodes
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