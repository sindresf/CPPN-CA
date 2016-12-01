using System;
using CPPNNEAT.CPPN;
using CPPNNEAT.EA;

namespace CPPNNEAT.Utils
{
	static class RandomExtensions //extension methods for the random class
	{

		public static float NextFloat(this Random rand)
		{
			return (float)rand.NextDouble();
		}

		public static bool NextBoolean(this Random rand)
		{
			return rand.NextDouble() > 0.5;
		}

		public static bool NextBoolean(this Random rand, double probability)
		{
			return rand.NextDouble() < probability;
		}

		public static bool DoMutation(this Random rand, MutationType type)
		{
			return rand.NextDouble() <= MutationChances.GetMutationChance(type);
		}

		public static ActivationFunctionType ActivationFunctionType(this Random rand)
		{
			float selectorValue = rand.NextFloat();
			float lastInterval = 0.0f;
			foreach(Tuple<float, ActivationFunctionType> tuple in EA.NEAT.ActivationFunctionChances)
			{
				if(selectorValue >= lastInterval && selectorValue < tuple.Item1)
					return tuple.Item2;
				lastInterval = tuple.Item1;
			}
			return CPPN.ActivationFunctionType.Linear; //this should never occur by the laws of random within 0->1 so doesn't skew the distribution
		}


		public static ConnectionGene ConnectionGene(this Random rand, NeatGenome genome)
		{
			return genome.connectionGenes[rand.Next(genome.connectionGenes.Count)];
		}

		public static InternalNodeGene NotInputNodeGene(this Random rand, NeatGenome genome)
		{
			//skips the input node genes, as nothing ever should change there
			return genome.nodeGenes[rand.Next(CPPNetworkParameters.CPPNetworkInputSize, genome.nodeGenes.Count)];
		}
		public static InternalNodeGene NotOutputNodeGene(this Random rand, NeatGenome genome)
		{
			return genome.nodeGenes[rand.Next(0, genome.nodeGenes.Count - CPPNetworkParameters.CPPNetworkOutputSize)];
		}

		public static float InitialConnectionWeight(this Random rand)
		{
			return rand.NextFloat() * CPPNetworkParameters.InitialMaxConnectionWeight;
		}
	}
}