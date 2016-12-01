using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPPNNEAT.EA
{
	class Population
	{
		public List<Species> species { get; private set; }
		private Dictionary<int,float> SpeciesFitnessMap;

		public PlaceHolderCARunner ca;

		public Population(PlaceHolderCARunner ca)
		{
			this.ca = ca;
			species = new List<Species>();
			SpeciesFitnessMap = new Dictionary<int, float>();
		}

		public void Initialize(IDCounters IDs)
		{
			species.Add(new Species(IDs.SpeciesID));
			SpeciesFitnessMap.Add(species[0].speciesID, species[0].SpeciesFitness);
			species[0].Initialize(IDs);
		}

		public void Evaluate()
		{
			Parallel.ForEach(species, (Species species) => { species.EvaluatePopulace(ca); });
			foreach(Species sp in species)
				SpeciesFitnessMap[sp.speciesID] = sp.SpeciesFitness;
		}

		public void MakeNextGeneration(IDCounters IDs)
		{
			Parallel.ForEach(species, (Species species) => { species.MakeNextGeneration(1, IDs); }); //TODO this needs to be a proper "1"
		}

		private void CalculateSpeciesAllowedPopulaceCount()
		{
			Parallel.ForEach(species, (Species species) => { });
		}
	}
}