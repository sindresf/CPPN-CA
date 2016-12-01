using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPPNNEAT.EA
{
	class Population
	{
		public List<Species> species { get; private set; }
		private Dictionary<int,float> SpeciesFitnessMap;

		public PlaceHolderCARunner ca;

		public IDCounters IDs;

		public Population(PlaceHolderCARunner ca, IDCounters IDs)
		{
			this.ca = ca;
			this.IDs = IDs;
			species = new List<Species>();
			SpeciesFitnessMap = new Dictionary<int, float>();
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

		public void KillSpecies(Species sp)
		{
			species.Remove(sp);
			SpeciesFitnessMap.Remove(sp.speciesID);
		}

		public void Evaluate()
		{
			Parallel.ForEach(species, (Species species) => { species.EvaluatePopulace(ca); });
			CheckForDeadSpecies();
			foreach(Species sp in species)
				SpeciesFitnessMap[sp.speciesID] = sp.SpeciesFitness;
		}

		private void CheckForDeadSpecies()
		{
			Parallel.ForEach(species, (Species species) =>
			{
				if(species.isDead) KillSpecies(species);
			});
		}

		public void MakeNextGeneration()
		{
			Parallel.ForEach(species, (Species species) => { species.MakeNextGeneration(1, IDs); }); //TODO this needs to be a proper "1"
		}

		private void CalculateSpeciesAllowedPopulaceCount()
		{
			Parallel.ForEach(species, (Species species) => { });
		}
	}
}