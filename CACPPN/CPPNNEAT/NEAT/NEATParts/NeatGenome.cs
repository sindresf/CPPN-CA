using CPPNNEATCA.CPPN.Parts;
using CPPNNEATCA.EA.Base;
using CPPNNEATCA.NEAT.Base;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.EA
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
		}

		private void InitilizeNodeGenes(IDCounters IDs)
		{
			nodeGenes = new NodeGeneSequence();

			for(int i = 0; i < Neat.parameters.CPPN.InputSize; i++)
				nodeGenes.Add(new NodeGene(IDs.NodeGeneID, i, NodeType.Sensor, ActivationFunctionType.AbsoluteValue));

			for(int i = Neat.parameters.CPPN.InputSize; i < Neat.parameters.CPPN.InputSize + Neat.parameters.CPPN.OutputSize; i++)
				nodeGenes.Add(new NodeGene(IDs.NodeGeneID,
					i,
					NodeType.Output,
					Neat.random.ActivationFunctionType()));
		}

		private void InitializeConnectionGenes(IDCounters IDs)
		{
			connectionGenes = new ConnectionGeneSequence();
			foreach(NodeGene gene in nodeGenes)
			{
				connectionGenes.Add(new ConnectionGene(IDs.ConnectionGeneID,
					gene.nodeID,
					nodeGenes.Count - 1,
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