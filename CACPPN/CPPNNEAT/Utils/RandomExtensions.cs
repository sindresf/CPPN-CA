using System;
using System.Collections.Generic;
using CPPNNEATCA.CPPN.Parts;
using CPPNNEATCA.NEAT;
using CPPNNEATCA.NEAT.Parts;

namespace CPPNNEATCA.Utils
{
	static class RandomExtensions //extension methods for the random class
	{
		public static double NextRangedDouble(this Random rand, double mid, double minMax)
		{
			return mid + ((rand.NextDouble() * 2.0 * minMax) - minMax);
		}

		public static float NextFloat(this Random rand)
		{
			return (float)rand.NextDouble();
		}
		public static float InitialConnectionWeight(this Random rand)
		{
			return (float)(rand.NextRangedDouble(0.0, CPPNParameters.InitialMaxConnectionWeight));
		}

		public static bool NextBoolean(this Random rand)
		{
			return rand.NextDouble() > 0.5000;
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
			foreach(Tuple<float, ActivationFunctionType> tuple in Neat.ActivationFunctionChances)
			{
				if(selectorValue >= lastInterval && selectorValue < tuple.Item1)
					return tuple.Item2;
				lastInterval = tuple.Item1;
			}
			return CPPN.Parts.ActivationFunctionType.Linear;
		}
		public static ConnectionGene ConnectionGene(this Random rand, NeatGenome genome)
		{
			return genome.connectionGenes[rand.Next(genome.connectionGenes.Count)];
		}
		public static NodeGene NotInputNodeGene(this Random rand, NeatGenome genome)
		{
			return genome.nodeGenes[rand.Next(Neat.parameters.CPPN.InputSize, genome.nodeGenes.Count)];
		}
		public static NodeGene NotOutputNodeGene(this Random rand, NeatGenome genome)
		{
			int outputNodeStart = 0; //this needs work. maybe switching out functionality.
			int outputNodeEnd = outputNodeStart + Neat.parameters.CPPN.OutputSize;
			return genome.nodeGenes[rand.Next(0, genome.nodeGenes.Count - Neat.parameters.CPPN.InputSize)];
		}
		public static Coefficient Coefficient(this Random rand, Dictionary<char, Coefficient> dict)
		{
			//a bit of a bias way to go about it but not even supposed to be used so... can look at it properly later
			ICollection<char> keys = dict.Keys;
			double chance = 1.0/keys.Count;
			foreach(char key in keys)
			{
				if(rand.NextBoolean(chance))
					return dict[key];
			}
			throw new DllNotFoundException("silly random select failed");
		}
	}
}