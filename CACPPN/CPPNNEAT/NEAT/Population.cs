using System.Collections.Generic;
using System.Threading.Tasks;
using CPPNNEATCA.CA;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.EA
{
	class Population
	{
		public List<Species> species { get; private set; }
		private Dictionary<int,float> SpeciesFitnessMap;

		public INeatCA ca;
		public IDCounters IDs;

		private float avgSpeciesFitness;

		public Population(INeatCA ca, IDCounters IDs)
		{
			this.ca = ca;
			this.IDs = IDs;
			species = new List<Species>();
			SpeciesFitnessMap = new Dictionary<int, float>();
			avgSpeciesFitness = 0.0f;
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
			Parallel.ForEach(species, (Species species) => { species.EvaluatePopulace(ca); });
			CheckForDeadSpecies();
			avgSpeciesFitness = 0.0f;
			foreach(Species sp in species)
			{
				SpeciesFitnessMap[sp.speciesID] = sp.SpeciesFitness;
				avgSpeciesFitness += sp.SpeciesFitness;
			}
			avgSpeciesFitness /= species.Count;
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
			/*int indieSpotsLeft = EAParameters.PopulationSize;
			foreach(Species sp in species)
			{
				int allowedSpaceForSpecies = CalculateSpeciesAllowedPopulaceCount(species);
			}*/
			Parallel.ForEach(species, (Species species) =>
			{
				species.MakeNextGeneration(CalculateSpeciesAllowedPopulaceCount(species), IDs);
			});
		}

		private int CalculateSpeciesAllowedPopulaceCount(Species sp)
		{
			//int allowedSize = 0;
			//float spStatus = sp.SpeciesFitness - avgSpeciesFitness; //is this positive it is better than avg and vice versa

			return EAParameters.PopulationSize / species.Count;
		}
	}
}