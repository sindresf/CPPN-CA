using System;
using CPPNNEATCA.EA.Base;
using CPPNNEATCA.NEAT.Base;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.NEAT.Parts
{
	class NeatGenome : Genome
	{
		public NodeGeneSequence nodeGenes { get; set; }
		public ConnectionGeneSequence connectionGenes { get; set; }

		public NeatGenome()
		{
			hasMutated = false;
		}

		public NeatGenome(NeatGenome copyFromGenome)
		{
			nodeGenes = new NodeGeneSequence();
			connectionGenes = new ConnectionGeneSequence();
			foreach(NodeGene gene in copyFromGenome.nodeGenes)
			{
				if(gene.type == NodeType.Sensor)
					nodeGenes.Add(new SensorNodeGene(((SensorNodeGene)gene)));
				else if(gene.type == NodeType.Hidden)
					nodeGenes.Add(new HiddenNodeGene(((HiddenNodeGene)gene)));
				else if(gene.type == NodeType.Output)
					nodeGenes.Add(new OutputNodeGene(((OutputNodeGene)gene)));
			}
			foreach(ConnectionGene gene in copyFromGenome.connectionGenes)
				connectionGenes.Add(new ConnectionGene(gene));
			hasMutated = false;
		}

		public override void Initialize(IDCounters IDs)
		{
			InitilizeNodeGenes(IDs);
			if(nodeGenes.Count != 5) throw new TypeInitializationException("not 5 nodes initially", null);
			InitializeConnectionGenes(IDs);
			if(connectionGenes.Count != 6) throw new TypeInitializationException("not 6 connections initially", null);
			if(EAParameters.RandomGeneStart)
				if(Neat.random.NextDouble() <= EAParameters.RandomGeneStartChance)
					Mutator.Mutate(this, IDs);
		}

		private void InitilizeNodeGenes(IDCounters IDs)
		{
			nodeGenes = new NodeGeneSequence();
			int nodeID = 0;
			for(int i = 0; i < Neat.parameters.CPPN.InputSize; i++)
				nodeGenes.Add(new SensorNodeGene(IDs.NodeGeneID, nodeID++));

			int stateCounter = 0;
			for(int i = Neat.parameters.CPPN.InputSize; i < Neat.parameters.CPPN.InputSize + Neat.parameters.CPPN.OutputSize; i++)
				nodeGenes.Add(new OutputNodeGene(IDs.NodeGeneID, nodeID++, stateCounter++));
		}

		private void InitializeConnectionGenes(IDCounters IDs)
		{
			connectionGenes = new ConnectionGeneSequence();
			for(int i = 0; i < Neat.parameters.CPPN.InputSize; i++)
				for(int j = Neat.parameters.CPPN.OutputSize; j > 0; j--)
				{
					if(nodeGenes[i].nodeID > 2) throw new ArgumentException("fromNodeID over 2");
					if(nodeGenes[nodeGenes.Count - j].nodeID > 4) throw new ArgumentException("toNodeID over 4");
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