using System;
using CPPNNEATCA.EA.Base;
using CPPNNEATCA.NEAT.Base;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.NEAT.Parts
{
	class NeatGenome : Genome
	{
		public GeneSequence<NodeGene> nodeGenes { get; set; }
		public GeneSequence<ConnectionGene> connectionGenes { get; set; }

		public NeatGenome()
		{
			hasMutated = false;
		}

		public NeatGenome(NeatGenome copyFromGenome)
		{
			nodeGenes = new NodeGeneSequence();
			connectionGenes = new ConnectionGeneSequence();
			foreach(NodeGene gene in copyFromGenome.nodeGenes)
				nodeGenes.Add(new NodeGene(gene));
			foreach(ConnectionGene gene in copyFromGenome.connectionGenes)
				connectionGenes.Add(new ConnectionGene(gene));
			hasMutated = false;
		}

		public override void Initialize(IDCounters IDs)
		{
			InitilizeNodeGenes(IDs);
			InitializeConnectionGenes(IDs);
			if(nodeGenes.Count != 5) throw new TypeInitializationException("not 5 nodes initially", null);
			if(connectionGenes.Count != 6) throw new TypeInitializationException("not 6 connections initially", null);
		}

		private void InitilizeNodeGenes(IDCounters IDs)
		{
			nodeGenes = new NodeGeneSequence();

			for(int i = 0; i < Neat.parameters.CPPN.InputSize; i++)
				nodeGenes.Add(new SensorNodeGene(IDs.NodeGeneID, i) as NodeGene);

			int stateCounter = 0;
			for(int i = Neat.parameters.CPPN.InputSize; i < Neat.parameters.CPPN.InputSize + Neat.parameters.CPPN.OutputSize; i++)
				nodeGenes.Add(new OutputNodeGene(IDs.NodeGeneID, i, stateCounter++, Neat.random.ActivationFunctionType()) as NodeGene);
		}

		private void InitializeConnectionGenes(IDCounters IDs)
		{
			connectionGenes = new ConnectionGeneSequence();
			for(int i = 0; i < Neat.parameters.CA.NeighbourHoodSize; i++)
				for(int j = Neat.parameters.CA.CellStateCount; j > 0; j--)
				{
					connectionGenes.Add(new ConnectionGene(IDs.ConnectionGeneID,
						nodeGenes[i].nodeID,
						nodeGenes[nodeGenes.Count - j].nodeID,
						true,
						Neat.random.InitialConnectionWeight()));
				}
		}

		public static NeatGenome GetLonger(NeatGenome genome1, NeatGenome genome2)
		{
			return genome1.IsLongerThan(genome2) ? genome1 : genome2;
		}

		public static NeatGenome GetShorter(NeatGenome genome1, NeatGenome genome2)
		{
			return genome1.IsLongerThan(genome2) ? genome2 : genome1;
		}
	}
}