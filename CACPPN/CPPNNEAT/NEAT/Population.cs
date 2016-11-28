using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPPNNEAT.NEAT
{
	class Population
	{
		public List<Species> species { get; private set; }
		private Dictionary<int,float> SpeciesFitnessMap;

		public Population()
		{
			species = new List<Species>();
			SpeciesFitnessMap = new Dictionary<int, float>();
		}

		public void Initialize(IDCounters IDs)
		{
			species.Add(new Species(IDs.SpeciesID));
			SpeciesFitnessMap.Add(species[0].speciesID, species[0].SpeciesFitness);
			species[0].Initialize(IDs);
		}

		//TODO needs an ADDSpecies method. Should only happen with one individual, so could just be a constructor with a "individual" argument.

		public void Evaluate()
		{
			Parallel.ForEach(species, (Species species) => { species.EvaluatePopulace(); });
			foreach(Species sp in species)
			{
				SpeciesFitnessMap[sp.speciesID] = sp.SpeciesFitness;
			}
		}

		public void MakeNextGeneration(IDCounters IDs)
		{
			Parallel.ForEach(species, (Species species) => { species.MakeNextGeneration(IDs); });
		}

		private void CalculateSpeciesAllowedPopulaceCount()
		{
			Parallel.ForEach(species, (Species species) => {/* some "get species fitness * population count / species scaling. maybe some exponential scaling */});
		}
	}
}