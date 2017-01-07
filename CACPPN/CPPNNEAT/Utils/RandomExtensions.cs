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
	}
	static class RandomBoolExtensions
	{
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
			return rand.NextDouble() <= MutationChances.GetChanceFor(type);
		}
	}

	static class CPPNRandomExtensions//has usings explicitly from CPPN
	{
		public static float InitialConnectionWeight(this Random rand)
		{
			return (float)(rand.NextRangedDouble(0.0, CPPNParameters.InitialMaxConnectionWeight));
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

	static class NeatRandomExtensions //has arguments explicitly from NEAT
	{
		public static ConnectionGene ConnectionGene(this Random rand, NeatGenome genome)
		{
			return genome.connectionGenes[rand.Next(genome.connectionGenes.Count)];
		}
		public static NodeGene NotInputNodeGene(this Random rand, NeatGenome genome)
		{
			if(genome.nodeGenes.Count <= Neat.parameters.CPPN.InputSize) throw new Exception("not enough range here!");
			return genome.nodeGenes[rand.Next(Neat.parameters.CPPN.InputSize, genome.nodeGenes.Count)];
		}
		public static NodeGene InternalNodeGene(this Random rand, NeatGenome genome)
		{
			var nodes = new List<NodeGene>();
			foreach(var node in genome.nodeGenes)
				if(node.type == NodeType.Hidden)
					nodes.Add(node);
			if(nodes.Count > 0)
				return nodes[rand.Next(0, nodes.Count)];
			else
				return null;
		}
		public static NodeGene NotOutputNodeGene(this Random rand, NeatGenome genome)
		{
			var nodes = new List<NodeGene>();
			foreach(var node in genome.nodeGenes)
				if(node.type != NodeType.Output)
					nodes.Add(node);
			return nodes[rand.Next(0, nodes.Count)];
		}
		public static NodeGene NodeGeneAfterInSequence(this Random rand, NeatGenome genome, NodeGene fromNode)
		{
			var minID = fromNode.nodeID; //TODO this is whaaaa!
			var maxID = 0;
			var toID = rand.Next(minID,maxID);
			return null;
		}
		public static NEATIndividual Representative(this Random rand, List<NEATIndividual> populace)
		{
			return populace[rand.Next(0, populace.Count)];
		}
		public static NEATIndividual Individual(this Random rand, List<NEATIndividual> populace)
		{
			return populace[rand.Next(0, populace.Count)];
		}
	}
}