using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPPNNEAT.NEAT
{
	class Population
	{
		public List<Species> species { get; private set; }

		public Population()
		{
			species = new List<Species>();
		}

		public void Initialize(IDCounters IDs)
		{
			species.Add(new Species(IDs.SpeciesID));
			species[0].Initialize(IDs);
		}

		public void Evaluate()
		{
			Parallel.ForEach(species, (Species species) => { species.EvaluatePopulace(); });
		}
	}
}