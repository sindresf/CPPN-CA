using System.Collections.Generic;
using System.Threading.Tasks;
using CPPNNEAT.Utils;

namespace CPPNNEAT.EA
{
	class Species
	{
		public readonly int speciesID;
		public List<NEATIndividual> populace { get; private set; }
		public float SpeciesFitness { get; private set; }

		public Species(int speciesID)
		{
			this.speciesID = speciesID;
			SpeciesFitness = 0.0f;
			populace = new List<NEATIndividual>();
		}

		public void Initialize(IDCounters IDs)
		{
			for(int i = 0; i < EAParameters.PopulationSize; i++)
			{
				NEATIndividual indie = new NEATIndividual(IDs);
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