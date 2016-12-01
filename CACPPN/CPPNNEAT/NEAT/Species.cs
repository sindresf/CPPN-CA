using System.Collections.Generic;
using System.Threading.Tasks;
using CPPNNEAT.Utils;

namespace CPPNNEAT.EA
{
	class Species
	{
		public readonly int speciesID;
		public List<Individual> populace { get; private set; }
		public float SpeciesFitness { get; private set; }

		public Species(int speciesID)
		{
			this.speciesID = speciesID;
			SpeciesFitness = 0.0f;
			populace = new List<Individual>();
		}

		public void Initialize(IDCounters IDs)
		{
			for(int i = 0; i < EAParameters.PopulationSize; i++)
			{
				Individual indie = new Individual(IDs);
				indie.Initialize(IDs);
				populace.Add(indie);
			}
		}

		public void EvaluatePopulace(PlaceHolderCARunner ca)
		{
			Parallel.ForEach(populace, indie => { indie.Evaluate(ca, populace.Count); });
			SpeciesFitness = populace.SumFitness();
		}

		public void MakeNextGeneration(int AllowedPopulaceSize, IDCounters IDs)
		{
			//this is where the compare should come into play for fitness selection.
			//and "foreach" by the allowed amount
		}
	}
}