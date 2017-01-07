using System;
using System.Collections.Generic;
using CPPNNEATCA.CA;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.NEAT.Parts
{
	class Population
	{
		public List<Species> species { get; private set; }
		public List<int> newNodeGenesThisGeneration, SplittConnectionGeneIDsThisGeneration;
		public Dictionary<Tuple<int,int>, int> addedConnectionsThisGeneration;
		public Dictionary<int,Tuple<int,int,int>> newConnectionGenesThisGenerationFromConnectionSplit;
		private Dictionary<int,float> SpeciesFitnessMap;
		public Dictionary<int,int> allowedPopulaceSize;

		public INeatCA ca;
		public IDCounters IDs;

		private float avgSpeciesFitness;
		private float SpeciesFitnessSD;

		public Population(INeatCA ca)
		{
			this.ca = ca;
			species = new List<Species>();
			SpeciesFitnessMap = new Dictionary<int, float>();
			avgSpeciesFitness = 0.0f;
			SpeciesFitnessSD = 0.0f;
			//HERE HERE HERE BULLSHITT!
			//var x = PieChart.ProcessFile(File.Open("lol", FileMode.Open));
		}

		public void Initialize()
		{
			allowedPopulaceSize = new Dictionary<int, int>();
			species.Add(new Species(IDs.SpeciesID));
			SpeciesFitnessMap.Add(species[0].speciesID, species[0].SpeciesFitness);
			species[0].Initialize(this);
		}

		public Species AddSpecies(NEATIndividual indie)
		{
			int id = IDs.SpeciesID;
			Species sp = new Species(indie, id);
			species.Add(sp);
			SpeciesFitnessMap.Add(id, sp.SpeciesFitness);
			return sp;
		}

		public void KillSpecies(List<Species> deadSpecies)
		{
			foreach(Species sp in deadSpecies)
			{
				species.Remove(sp);
				SpeciesFitnessMap.Remove(sp.speciesID);
			}
		}

		public void Evaluate()
		{
			foreach(Species sp in species) sp.EvaluatePopulace();
			//Parallel.ForEach(species, (Species species) => { species.EvaluatePopulace(); });
			CheckForDeadSpecies();
			CalcGenerationAvgFitness();
			CalcGenerationFitnessSD();
		}
		private void CalcGenerationAvgFitness()
		{
			avgSpeciesFitness = 0.0f;
			foreach(Species sp in species)
			{
				SpeciesFitnessMap[sp.speciesID] = sp.SpeciesFitness;
				avgSpeciesFitness += sp.SpeciesFitness;
			}
			avgSpeciesFitness /= species.Count;
		}
		private void CalcGenerationFitnessSD()
		{
			var subMeanSquares = new List<double>();
			foreach(var sp in species)
				subMeanSquares.Add(Math.Pow(sp.SpeciesFitness - avgSpeciesFitness, 2.0));
			double MeanMean = 0.0f;
			foreach(var mean in subMeanSquares)
				MeanMean += mean;
			SpeciesFitnessSD = (float)(Math.Sqrt(MeanMean / subMeanSquares.Count));
		}

		private void CheckForDeadSpecies()
		{
			List<Species> deadSpecies = new List<Species>();
			foreach(Species sp in species)
				if(sp.isDead) deadSpecies.Add(sp);
			KillSpecies(deadSpecies);
		}

		public void MakeNextGeneration()
		{
			newNodeGenesThisGeneration = new List<int>();
			SplittConnectionGeneIDsThisGeneration = new List<int>();
			addedConnectionsThisGeneration = new Dictionary<Tuple<int, int>, int>();
			newConnectionGenesThisGenerationFromConnectionSplit = new Dictionary<int, Tuple<int, int, int>>();

			allowedPopulaceSize = SpeciesSizes();
			var speciesRepresentatives = GetSpeciesRepresentatives();
			var missfits = GetMissFitsFromSpeciesNextGeneration();
			FindMissfitsHomeInPopulation(missfits);
		}

		private Dictionary<int, int> SpeciesSizes()
		{
			var allowedPopulaceSizes = new Dictionary<int, int>();
			int averageSpots = EAParameters.PopulationSize / species.Count;
			foreach(Species sp in species)
				allowedPopulaceSizes[sp.speciesID] = CalculateSpeciesAllowedPopulaceCount(sp, averageSpots);
			allowedPopulaceSizes = EnforcePopulationSize(allowedPopulaceSizes);
			return SizesForSurvivors(allowedPopulaceSizes);
		}
		private Dictionary<int, int> EnforcePopulationSize(Dictionary<int, int> allowedPopulaceSizes)
		{
			var enforcedPopulation = new Dictionary<int,int>(allowedPopulaceSizes);
			int sumSizes = 0;
			foreach(var val in enforcedPopulation.Values)
				sumSizes += val;
			int missing = EAParameters.PopulationSize - sumSizes;
			int count = 1;
			if(missing < 0)
			{
				missing *= -1;
				count = -1;
				for(int i = 0; i < missing; i++)
				{
					var key = 0;
					var max = int.MinValue;
					foreach(var pair in enforcedPopulation)
						if(pair.Value > max)
						{
							key = pair.Key;
							max = pair.Value;
						}
					enforcedPopulation[key] += count;
				}
			} else
				for(int i = 0; i < missing; i++)
				{
					var key = 0;
					var min = int.MaxValue;
					foreach(var pair in enforcedPopulation)
						if(pair.Value < min)
						{
							key = pair.Key;
							min = pair.Value;
						}
					enforcedPopulation[key] += count;
				}
			return enforcedPopulation;
		}
		private Dictionary<int, int> SizesForSurvivors(Dictionary<int, int> dict)
		{
			var sizes = new Dictionary<int,int>(dict);
			var phasedOutSpeciesIDs = new List<int>();
			foreach(var pair in dict)
				if(pair.Value == 0)
					phasedOutSpeciesIDs.Add(pair.Key);
			var phasedOutSpecies = new List<Species>();
			foreach(var sp in species)
			{
				if(phasedOutSpeciesIDs.Contains(sp.speciesID))
					phasedOutSpecies.Add(sp);
			}
			foreach(var ID in phasedOutSpeciesIDs)
			{
				sizes.Remove(ID);
			}
			KillSpecies(phasedOutSpecies);
			return sizes;
		}
		private int CalculateSpeciesAllowedPopulaceCount(Species sp, int avgSpots)
		{
			int SDCount = 0;
			float goalFitness = sp.SpeciesFitness;
			float growingFitness = avgSpeciesFitness;
			bool goingUp = growingFitness < goalFitness;
			if(goingUp)
			{
				while(growingFitness < goalFitness)
				{
					SDCount++;
					growingFitness += SpeciesFitnessSD * 0.7f;
				}
			} else
			{
				while(growingFitness > goalFitness)
				{
					SDCount--;
					growingFitness += SpeciesFitnessSD * -0.7f;
				}
			}
			return (int)(avgSpots + SDCount * 2.0).Clamp(0, EAParameters.PopulationSize);
		}

		private List<NEATIndividual> GetSpeciesRepresentatives()
		{
			var representatives = new List<NEATIndividual>();
			foreach(Species sp in species)
				representatives.Add(sp.NewSpeciesRepresentative());
			return representatives;
		}

		private List<NEATIndividual> GetMissFitsFromSpeciesNextGeneration()
		{
			var missFits = new List<NEATIndividual>();
			foreach(Species sp in species)
				missFits.AddRange(sp.MakeNextGeneration(allowedPopulaceSize[sp.speciesID],
					this,
					newNodeGenesThisGeneration,
					SplittConnectionGeneIDsThisGeneration));
			return missFits;
		}

		private void FindMissfitsHomeInPopulation(List<NEATIndividual> missFits)
		{
			var actualMissfits = MissFitsInAllSpecies(missFits);
			var missFitMatches = new Dictionary<int,List<NEATIndividual>>();
			var seenMFBefore = new List<int>();
			foreach(var mf1 in actualMissfits)
			{
				foreach(var mf2 in actualMissfits)
					if(!seenMFBefore.Contains(mf2.individualID))
						if(mf1.individualID != mf2.individualID)
							if(mf1.DifferenceTo(mf2) <= EAParameters.SpeciesInclusionRadius)
								if(missFitMatches.ContainsKey(mf1.individualID))
									missFitMatches[mf1.individualID].Add(mf2);
								else
								{
									missFitMatches.Add(mf1.individualID, new List<NEATIndividual>());
									missFitMatches[mf1.individualID].Add(mf1);
									missFitMatches[mf1.individualID].Add(mf2);
								}
				seenMFBefore.Add(mf1.individualID);
			}
			if(missFitMatches.Keys.Count != 0)
				foreach(var val in missFitMatches.Values)
				{
					var sp = AddSpecies(val[0]);
					foreach(var indie in val)
						if(indie.individualID != val[0].individualID)
							sp.AddIndividual(indie);
				}

			for(int i = 0; i < actualMissfits.Count; i++)
				foreach(var id in missFitMatches.Keys)
					if(id == actualMissfits[i].individualID) actualMissfits.Remove(actualMissfits[i]);


			foreach(var mf in actualMissfits)
				AddSpecies(mf);
		}

		private List<NEATIndividual> MissFitsInAllSpecies(List<NEATIndividual> missFits)
		{
			var actualMissfits = new List<NEATIndividual>();
			bool noHome = true;
			foreach(var missfit in missFits)
			{
				foreach(var sp in species)
				{
					if(sp.BelongsInSpecies(missfit))
					{
						noHome = false;
						sp.AddIndividual(missfit);
						break;
					}
				}
				if(noHome)
					actualMissfits.Add(missfit);
				noHome = true;
			}
			return actualMissfits;
		}

		public NEATIndividual GetBestIndividual()
		{
			float bestFitness = float.MinValue;
			NEATIndividual bestIndie = null;
			foreach(var sp in species)
			{
				var spBest = sp.GetBest();
				if(spBest.Fitness > bestFitness)
				{
					bestFitness = spBest.Fitness;
					bestIndie = spBest;
				}
			}
			return bestIndie;
		}
		public NEATIndividual GetBiggestIndividual()
		{
			int mostStuff = 0;
			NEATIndividual biggestIndie = null;
			foreach(Species sp in species)
			{
				foreach(var indie in sp.populace)
				{
					int indieStuff = indie.genome.nodeGenes.Count + indie.genome.connectionGenes.Count;
					if(indieStuff > mostStuff)
					{
						mostStuff = indieStuff;
						biggestIndie = indie;
					}
				}
			}
			return biggestIndie;
		}
	}
}