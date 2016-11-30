using System;
using CPPNNEAT.CPPN;
using CPPNNEAT.NEAT;

namespace CPPNNEAT.Utils
{
	class GetRandom
	{
		public static ActivationFunctionType ActivationFunctionType(Random rand)
		{
			return (ActivationFunctionType)rand.Next(Enum.GetValues(typeof(ActivationFunctionType)).Length);
		}

		public static ConnectionGene ConnectionGene(Genome genome, Random rand)
		{
			return genome.connectionGenes[rand.Next(genome.connectionGenes.Count)];
		}

		public static NodeGene NotInputNodeGene(Genome genome, Random rand)
		{
			//skips the input node genes, as nothing ever should change there
			return genome.nodeGenes[rand.Next(CPPNetworkParameters.CPPNetworkInputSize, genome.nodeGenes.Count)];
		}
		public static NodeGene NotOutputNodeGene(Genome genome, Random rand)
		{
			return genome.nodeGenes[rand.Next(0, genome.nodeGenes.Count - CPPNetworkParameters.CPPNetworkOutputSize)];
		}

		public static float InitialConnectionWeight(Random rand)
		{
			return (float)rand.NextDouble() * CPPNetworkParameters.InitialMaxConnectionWeight;
		}
	}
}