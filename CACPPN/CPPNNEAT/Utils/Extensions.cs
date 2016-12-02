using System;
using System.Collections.Generic;
using CPPNNEATCA.EA;
using CPPNNEATCA.EA.Base;

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
			return val <= compareTo + EAParameters.SameFloatWithinReason
				&& val >= compareTo - EAParameters.SameFloatWithinReason;
		}

		public static float SumFitness(this List<NEATIndividual> populace)
		{
			float sum = 0.0f;
			foreach(NEATIndividual indie in populace)
				sum += indie.Fitness;
			return sum;
		}

		public static bool IsLowerThanLimit(this int limit, NEATIndividual indie1, NEATIndividual indie2)
		{
			return indie1.genome.connectionGenes.Count < limit && indie2.genome.connectionGenes.Count < limit;
		}

		public static bool IsLongerThan(this NeatGenome genom1, NeatGenome genome2)
		{
			return genom1.connectionGenes.Count > genome2.connectionGenes.Count;
		}

		public static int GetHighestConnectionGeneID(this NeatGenome genome)
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

		public static float SimilarityTo(this NEATIndividual indie1, NEATIndividual indie2)
		{
			float similarity = 0.0f;

			float excessVar = 0.0f;
			float disjointVar = 0.0f;
			float weightDiffSum = 0.0f;
			int sameGeneCount = 0;

			List<ConnectionGene> longestGeneSequence = NeatGenome.GetLonger(indie1.genome, indie2.genome).connectionGenes;
			List<ConnectionGene> shortestGeneSequence = NeatGenome.GetShorter(indie1.genome, indie2.genome).connectionGenes;

			int disjointID = shortestGeneSequence[shortestGeneSequence.Count-1].geneID;


			int shortIndex = 0;
			foreach(ConnectionGene gene in longestGeneSequence)
			{ // juuust might be able to restructure this for readability O:)
				if(shortIndex < shortestGeneSequence.Count)
				{
					ConnectionGene shortGene = shortestGeneSequence[shortIndex];
					if(gene.geneID == shortGene.geneID)
					{
						sameGeneCount++;
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
						sameGeneCount++;
						weightDiffSum += gene.GetWeightDifference(shortestGeneSequence[shortestGeneSequence.Count - 1]);
					}
				}
				shortIndex++;
			}

			int N = 1;
			if(!EAParameters.SetNToOneLimit.IsLowerThanLimit(indie1, indie2))
				N = longestGeneSequence.Count;
			excessVar /= N;
			disjointVar /= N;

			similarity += excessVar * EAParameters.ExcessSimilarityWeight;
			similarity += disjointVar * EAParameters.DisjointSimilarityWeight;
			similarity += (weightDiffSum / sameGeneCount) * EAParameters.WeightDifferenceSimilarityWeight;
			return similarity;
		}
	}
}