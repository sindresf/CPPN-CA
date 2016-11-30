﻿using System;
using System.Collections.Generic;
using CPPNNEAT.Utils;

namespace CPPNNEAT.EA
{
	class Genome
	{
		public List<NodeGene> nodeGenes { get; set; }
		public List<ConnectionGene> connectionGenes { get; set; }

		public bool hasMutated { get; private set; }

		public Genome()
		{
			hasMutated = false;
		}

		public Genome(Genome copyFromGenome)
		{
			nodeGenes = new List<NodeGene>();
			connectionGenes = new List<ConnectionGene>();
			foreach(NodeGene gene in copyFromGenome.nodeGenes)
				nodeGenes.Add(new NodeGene(gene.geneID, gene.nodeID, gene.type, gene.functionType));
			foreach(ConnectionGene gene in copyFromGenome.connectionGenes)
				connectionGenes.Add(new ConnectionGene(gene.geneID, gene.fromNodeID, gene.toNodeID, gene.isEnabled, gene.connectionWeight));
			hasMutated = false;
		}

		// functions for adding new genes which disables older ones and such
		public void Initialize(IDCounters IDs)
		{
			InitilizeNodeGenes(IDs);
			InitializeConnectionGenes(IDs);
		}

		private void InitilizeNodeGenes(IDCounters IDs)
		{
			nodeGenes = new List<NodeGene>();
			//add the input nodes and the output node   <- always minimal start in NEAT
			int nodeCount = CPPNetworkParameters.CPPNetworkInputSize + CPPNetworkParameters.CPPNetworkOutputSize;
			for(int i = 0; i < nodeCount; i++)
				if(i == nodeCount - 1)
					nodeGenes.Add(new NodeGene(IDs.NodeGeneID,
						i,
						NodeType.Sensor,
						CPPN.ActivationFunctionType.Input)); //actually choose this one at random
				else
					nodeGenes.Add(new NodeGene(IDs.NodeGeneID,
						i,
						NodeType.Sensor,
						CPPN.ActivationFunctionType.Input));
		}

		private void InitializeConnectionGenes(IDCounters IDs)
		{
			connectionGenes = new List<ConnectionGene>();
			foreach(NodeGene gene in nodeGenes)
			{
				connectionGenes.Add(new ConnectionGene(IDs.ConnectionGeneID,
					gene.nodeID,
					nodeGenes.Count - 1,
					true,
					NEAT.random.InitialConnectionWeight())); //need a static EARandom
			}
		}
	}
}