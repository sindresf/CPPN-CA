using System;
using CPPNNEAT.CPPN;
using CPPNNEAT.NEAT;

namespace CPPNNEAT.Utils
{
	static class GetRandom //extention methods for the random class
	{
		public static ActivationFunctionType ActivationFunctionType(this Random rand)
		{
			return (ActivationFunctionType)rand.Next(Enum.GetValues(typeof(ActivationFunctionType)).Length);
		}

		public static ConnectionGene ConnectionGene(this Random rand, Genome genome)
		{
			return genome.connectionGenes[rand.Next(genome.connectionGenes.Count)];
		}

		public static NodeGene NotInputNodeGene(this Random rand, Genome genome)
		{
			//skips the input node genes, as nothing ever should change there
			return genome.nodeGenes[rand.Next(CPPNetworkParameters.CPPNetworkInputSize, genome.nodeGenes.Count)];
		}
		public static NodeGene NotOutputNodeGene(this Random rand, Genome genome)
		{
			return genome.nodeGenes[rand.Next(0, genome.nodeGenes.Count - CPPNetworkParameters.CPPNetworkOutputSize)];
		}

		public static float InitialConnectionWeight(this Random rand)
		{
			return (float)rand.NextDouble() * CPPNetworkParameters.InitialMaxConnectionWeight;
		}
	}
}