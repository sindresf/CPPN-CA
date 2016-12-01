using CPPNNEAT.EA.Base;
using CPPNNEAT.Utils;

namespace CPPNNEAT.EA
{
	class NeatGenome : Genome
	{
		public GeneSequence<InternalNodeGene> nodeGenes { get; set; }
		public GeneSequence<ConnectionGene> connectionGenes { get; set; }

		public NeatGenome()
		{
			hasMutated = false;
		}

		public NeatGenome(NeatGenome copyFromGenome)
		{
			nodeGenes = new NodeGeneSequence();
			connectionGenes = new ConnectionGeneSequence();
			foreach(InternalNodeGene gene in copyFromGenome.nodeGenes)
				nodeGenes.Add(new InternalNodeGene(gene));
			foreach(ConnectionGene gene in copyFromGenome.connectionGenes)
				connectionGenes.Add(new ConnectionGene(gene));
			hasMutated = false;
		}

		public void Initialize(IDCounters IDs)
		{
			InitilizeNodeGenes(IDs);
			InitializeConnectionGenes(IDs);
		}

		private void InitilizeNodeGenes(IDCounters IDs)
		{
			nodeGenes = new NodeGeneSequence();
			//add the input nodes and the output node   <- always minimal start in NEAT
			int nodeCount = CPPNetworkParameters.CPPNetworkInputSize + CPPNetworkParameters.CPPNetworkOutputSize;
			for(int i = 0; i < nodeCount; i++)
				if(i == nodeCount - 1)
					nodeGenes.Add(new InternalNodeGene(IDs.NodeGeneID,
						i,
						NodeType.Sensor,
						NEAT.random.ActivationFunctionType()));
				else
					nodeGenes.Add(new InternalNodeGene(IDs.NodeGeneID,
						i,
						NodeType.Sensor,
						NEAT.random.ActivationFunctionType())); //need input "nodes" to be empty geneID-nodeID shells
		}

		private void InitializeConnectionGenes(IDCounters IDs)
		{
			connectionGenes = new ConnectionGeneSequence();
			foreach(InternalNodeGene gene in nodeGenes)
			{
				connectionGenes.Add(new ConnectionGene(IDs.ConnectionGeneID,
					gene.nodeID,
					nodeGenes.Count - 1,
					true,
					NEAT.random.InitialConnectionWeight()));
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