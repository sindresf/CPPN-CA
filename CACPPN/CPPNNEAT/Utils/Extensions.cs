using System;
using System.Collections.Generic;
using CPPNNEAT.EA;
using CPPNNEAT.EA.Base;

namespace CPPNNEAT.Utils
{
	static class Extensions
	{
		public static float ClampWeight(this float val)
		{
			if(val.CompareTo(CPPNetworkParameters.WeightMin) < 0) return CPPNetworkParameters.WeightMin;
			else if(val.CompareTo(CPPNetworkParameters.WeightMax) > 0) return CPPNetworkParameters.WeightMax;
			else return val;
		}

		public static bool SameWithinReason(this float val, float compareTo)
		{
			return val <= compareTo + EAParameters.SameFloatWithinReason
				&& val >= compareTo - EAParameters.SameFloatWithinReason;
		}

		public static float SumFitness(this List<Individual> populace)
		{
			float sum = 0.0f;
			foreach(Individual indie in populace)
				sum += indie.Fitness;
			return sum;
		}

		public static bool IsLowerThanLimit(this int limit, Individual indie1, Individual indie2)
		{
			return indie1.genome.connectionGenes.Count < limit && indie2.genome.connectionGenes.Count < limit;
		}

		public static bool IsLongerThan(this Genome genom1, Genome genome2)
		{
			return genom1.connectionGenes.Count > genome2.connectionGenes.Count;
		}

		public static int GetHighestConnectionGeneID(this Genome genome)
		{
			int max = 0;
			foreach(Gene gene in genome.connectionGenes)
			{
				if(gene.geneID > max)
					max = gene.geneID;
			}
			return max;
		}

		public static float GetWeightDifference(this ConnectionGene gene1, ConnectionGene gene2)
		{
			return Math.Abs(gene1.connectionWeight - gene2.connectionWeight);
		}

		public static float SimilarityTo(this Individual indie1, Individual indie2)
		{
			float similarity = 0.0f;

			float excessVar = 0.0f;
			float disjointVar = 0.0f;
			float weightVar = 0.0f;

			int longestGenome = Genome.GetLonger(indie1.genome, indie2.genome).connectionGenes.Count;
			int shortestGenome = Genome.GetShorter(indie1.genome, indie2.genome).connectionGenes.Count;

			int disjointPoint =Math.Min(indie1.genome.GetHighestConnectionGeneID(),
										indie2.genome.GetHighestConnectionGeneID());

			excessVar = longestGenome - disjointPoint; //TODO fix that there is a disparaty between talking about nodeID and geneID

			for(int i = 0; i < shortestGenome; i++)
			{
				if(indie1.genome.connectionGenes[i].geneID != indie2.genome.connectionGenes[i].geneID)
					disjointVar += 1;
				else
					weightVar += indie1.genome.connectionGenes[i].GetWeightDifference(indie1.genome.connectionGenes[i]);

			}

			int N = 1;
			if(!EAParameters.SetNToOneLimit.IsLowerThanLimit(indie1, indie2))
				N = longestGenome;
			excessVar /= N;
			disjointVar /= N;

			similarity += excessVar * EAParameters.ExcessSimilarityWeight;
			similarity += disjointVar * EAParameters.DisjointSimilarityWeight;
			similarity += weightVar * EAParameters.WeightDifferenceSimilarityWeight;
			return similarity;
		}
	}
}