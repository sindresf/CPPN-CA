using System;
using System.Collections.Generic;
using CPPNNEAT.Utils;
using CPPNNEATCA.CPPN.Parts;
using CPPNNEATCA.EA.Base;
using CPPNNEATCA.NEAT;
using CPPNNEATCA.NEAT.Base;
using CPPNNEATCA.NEAT.Parts;

namespace CPPNNEATCA.Utils
{
	static class Extensions
	{
		public static float ClampWeight(this float val)
		{
			if(val.CompareTo(CPPNParameters.WeightMin) < 0) return CPPNParameters.WeightMin;
			else if(val.CompareTo(CPPNParameters.WeightMax) > 0) return CPPNParameters.WeightMax;
			else return val;
		}
		public static double Clamp(this double val, double min, double max)
		{
			if(val.CompareTo(min) < 0) return min;
			else if(val.CompareTo(max) > 0) return max;
			else return val;
		}
		public static bool SameWithinReason(this float val, float compareTo)
		{
			float error = 0.000001f;
			return val <= compareTo + error
				&& val >= compareTo - error;
		}
		public static bool SameWithinReason(this double val, double compareTo)
		{
			double error = 0.00000000000001f;
			return val <= compareTo + error
				&& val >= compareTo - error;
		}

		public static bool CanReachGoalFromRoot(this Dictionary<int, List<int>> tree, int root, int goalValue)
		{
			if(tree[root].Count == 0) //no outgoing edges = no goal reachable (only output nodes can be the root for this if)
			{
				return false;
			} else if(tree[root].Contains(goalValue)) //goal "reached"
			{
				Console.WriteLine("found it");
				return true;
			} else
			{
				bool could = false;
				foreach(int child in tree[root])
				{
					could = tree.CanReachGoalFromRoot(child, goalValue);
				}
				return could;
			}
			throw new Exception("shouldn't have come here! (cycle check fail)");
		} //TODO THIS NEEDS WORK I THINK
	}
	static class NEATExtensions
	{
		public static float SumFitness(this List<NEATIndividual> populace)
		{
			float sum = 0.0f;
			foreach(NEATIndividual indie in populace)
				sum += indie.Fitness;
			return sum;
		}
		public static float WeightedSum(this TupleList<float, float> inputs)
		{
			float sum = 0.0f;
			foreach(Tuple<float, float> input in inputs)
				sum += input.Item1 * input.Item2;
			return sum;
		}
		public static bool isMoreThanConnectionsIn(this int limit, NEATIndividual indie1, NEATIndividual indie2)
		{
			return indie1.genome.connectionGenes.Count < limit && indie2.genome.connectionGenes.Count < limit;
		}
		public static bool IsMoreConnectedThan(this NeatGenome genom1, NeatGenome genome2)
		{
			return genom1.connectionGenes.Count > genome2.connectionGenes.Count;
		}
		public static float GetWeightDifference(this ConnectionGene gene1, ConnectionGene gene2)
		{
			return Math.Abs(gene1.connectionWeight - gene2.connectionWeight);
		}
		public static float DifferenceTo(this NEATIndividual indie1, NEATIndividual indie2)
		{
			float difference = 0.0f;

			float excessVar = 0.0f;
			float disjointVar = 0.0f;
			float functionVar = 0.0f;
			float weightDiffSum = 0.0f;
			int sameConnGeneCount = 0;

			var shortestNodeSequence = indie1.genome.nodeGenes.Count < indie2.genome.nodeGenes.Count ? indie1.genome.nodeGenes.Count : indie2.genome.nodeGenes.Count;
			var nodes1 = indie1.genome.nodeGenes;
			var nodes2 = indie2.genome.nodeGenes;
			int nodeIndex = 0;
			while(nodeIndex < shortestNodeSequence)
			{
				var node1 = nodes1[nodeIndex];
				var node2 = nodes2[nodeIndex];
				if(node1.geneID == node2.geneID)
				{
					if(node1.type == NodeType.Hidden)
						if(node2.type != NodeType.Hidden) throw new Exception("gene describing totally different things");
						else
							functionVar += ActivationFunction.GetFunctionDifference(((HiddenNodeGene)node1).Function.Type, ((HiddenNodeGene)node2).Function.Type);
				} else
					difference += 0.1f;
				nodeIndex++;
			}

			var longestGeneSequence = NeatGenome.GetMostConnected(indie1.genome, indie2.genome).connectionGenes;
			var shortestGeneSequence = NeatGenome.GetLeastConnected(indie1.genome, indie2.genome).connectionGenes;

			int disjointID = shortestGeneSequence[shortestGeneSequence.Count-1].geneID;


			int shortIndex = 0;
			foreach(ConnectionGene gene in longestGeneSequence)
			{
				if(shortIndex < shortestGeneSequence.Count)
				{
					var shortGene = shortestGeneSequence[shortIndex];
					if(gene.geneID == shortGene.geneID)
					{
						sameConnGeneCount++;
						weightDiffSum += gene.GetWeightDifference(shortGene);
					} else
					{
						if(gene.geneID < disjointID) disjointID++;
						else if(gene.geneID > disjointID) excessVar++;
					}
				} else
				{
					if(gene.geneID < disjointID) disjointID++;
					else if(gene.geneID > disjointID) excessVar++;
					else
					{
						sameConnGeneCount++;
						weightDiffSum += gene.GetWeightDifference(shortestGeneSequence[shortestGeneSequence.Count - 1]);
					}
				}
				shortIndex++;
			}

			int N = 1;
			if(!EAParameters.SetNToOneLimit.isMoreThanConnectionsIn(indie1, indie2))
				N = longestGeneSequence.Count;
			excessVar /= N;
			disjointVar /= N;

			difference += excessVar * EAParameters.ExcessSimilarityWeight;
			difference += disjointVar * EAParameters.DisjointSimilarityWeight;
			difference += functionVar * EAParameters.FunctionSimilarityWeight;
			difference += (weightDiffSum / sameConnGeneCount) * EAParameters.WeightDifferenceSimilarityWeight;
			return difference;
		}
		public static bool Contains(this ConnectionGeneSequence connGenes, int fromID, int toID)
		{
			foreach(var gene in connGenes)
				if(gene.fromNodeID == fromID && gene.toNodeID == toID)
					return true;
			return false;
		}
		public static bool Contains(this NodeGeneSequence nodeGenes, int nodeID)
		{
			foreach(var gene in nodeGenes)
				if(gene.nodeID == nodeID)
					return true;
			return false;
		}
		public static NodeGene Get(this NodeGeneSequence nodeGenes, int nodeID)
		{
			foreach(var node in nodeGenes)
				if(node.nodeID == nodeID)
					return node;
			return null;
		}
		public static bool IsEmpty(this List<NEATIndividual> list)
		{
			return list.Count == 0;
		}
		public static bool ContainsID(this List<CycleCheckGraphNode> nodes, int ID)
		{
			foreach(var node in nodes)
				if(node.nodeID == ID) return true;
			return false;
		}
		public static bool HasInternalNodes(this NeatGenome genome)
		{
			return genome.nodeGenes.Count > Neat.parameters.CPPN.InputSize + Neat.parameters.CPPN.OutputSize;
		}
	}
	static class Extensions1D
	{
		public static int Neighbourhood1DRadius(this int diameter)
		{
			return (diameter - 1) / 2;
		}
		public static string PrintCA(this float[] worldState)
		{
			string output = "|";
			foreach(float state in worldState)
				output += state + "";
			return output + "|";
		}
	}

	static class Extensions2D
	{
		public static int Neighbourhood2DRadius(this int diameter)
		{
			return (diameter - 1) / 4;
		}
		public static string PrintCA(this float[,] worldState)
		{
			string output = "";
			/*
			for(int i = 0; i < worldState.GetLength(0); i++)
			{
				output += "|";
				for(int j = 0; j < worldState.GetLength(1); j++)
				{
					output += worldState[i, j];
				}
				output += "|\n";
			}*/
			for(int i = 0; i < worldState.GetLength(0); i++)
			{
				output += "|";
				for(int j = 0; j < worldState.GetLength(1); j++)
					output += worldState[i, j] > 0.7 ? 'O' : ' ';

				output += "|\n";
			}
			return output;
		}
	}
}