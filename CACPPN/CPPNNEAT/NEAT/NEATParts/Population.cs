using System;
using System.Collections.Generic;
using CPPNNEATCA.CA;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.NEAT.Parts
{
	class Population
	{
		public List<Species> species { get; private set; }
		private List<int> newNodeGenesThisGeneration, newConnectionGenesThisGeneration;
		private Dictionary<int,float> SpeciesFitnessMap;

		public INeatCA ca;
		public IDCounters IDs;

		private float avgSpeciesFitness;
		private float SpeciesFitnessSD;

		public Population(INeatCA ca, IDCounters IDs)
		{
			this.ca = ca;
			this.IDs = IDs;
			species = new List<Species>();
			SpeciesFitnessMap = new Dictionary<int, float>();
			avgSpeciesFitness = 0.0f;
			SpeciesFitnessSD = 0.0f;
			//HERE HERE HERE BULLSHITT!
			//var x = PieChart.ProcessFile(File.Open("lol", FileMode.Open));
		}

		public void Initialize()
		{
			species.Add(new Species(IDs.SpeciesID));
			SpeciesFitnessMap.Add(species[0].speciesID, species[0].SpeciesFitness);
			species[0].Initialize(IDs);
		}

		public void AddSpecies(NEATIndividual indie)
		{
			int id = IDs.SpeciesID;
			Species sp = new Species(indie, id);
			species.Add(sp);
			SpeciesFitnessMap.Add(id, sp.SpeciesFitness);
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
			newConnectionGenesThisGeneration = new List<int>();

			var allowedPopulaceSize = SpeciesSizes();
			var speciesRepresentatives = GetSpeciesRepresentatives();
			var missfits = GetMissFitsFromSpeciesNextGeneration(allowedPopulaceSize);
			FindMissfitsHomeInPopulation(missfits);
		}

		private Dictionary<int, int> SpeciesSizes()
		{
			var allowedPopulaceSize = new Dictionary<int, int>();
			int averageSpots = EAParameters.PopulationSize / species.Count;
			foreach(Species sp in species)
			{
				int spots = CalculateSpeciesAllowedPopulaceCount(sp,averageSpots); //normalize these?
				allowedPopulaceSize[sp.speciesID] = spots;
			}
			return allowedPopulaceSize;
		}
		private int CalculateSpeciesAllowedPopulaceCount(Species sp, int avgSpots)
		{
			int SDCount = 0;
			int skewDir = 1;
			float higherGoal = sp.SpeciesFitness;
			float lowerGoal = sp.SpeciesFitness;
			float growingFitness = avgSpeciesFitness;
			if(growingFitness > sp.SpeciesFitness) skewDir *= -1;

			while(growingFitness > lowerGoal && growingFitness < higherGoal)
			{
				SDCount += skewDir;
				growingFitness += SpeciesFitnessSD * skewDir;
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

		private List<NEATIndividual> GetMissFitsFromSpeciesNextGeneration(Dictionary<int, int> allowedPopulaceSize)
		{
			var missFits = new List<NEATIndividual>();
			foreach(Species sp in species)
				missFits.AddRange(sp.MakeNextGeneration(allowedPopulaceSize[sp.speciesID],
					IDs,
					newNodeGenesThisGeneration,
					newConnectionGenesThisGeneration));
			return missFits;
		}

		private void FindMissfitsHomeInPopulation(List<NEATIndividual> missFits)
		{
			//if does not comply with any representatives.
			var actualMissfits = MissFitsInAllSpecies(missFits);
			Console.WriteLine("AMF:" + actualMissfits.Count);
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
	}
}