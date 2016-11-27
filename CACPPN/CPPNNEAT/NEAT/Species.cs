using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPPNNEAT.NEAT
{
    class Species : IComparer<Individual>
    {
        public readonly int speciesID;
        public List<Individual> populace { get; private set; }

        public Species(int speciesID)
        {
            this.speciesID = speciesID;
            populace = new List<Individual>();
        }

        public void Initialize(IDCounters IDs)
        {
            for(int i = 0; i < EAParameters.PopulationSize; i++)
            {
                Individual indie = new Individual(IDs);
                indie.Initialize();
            }
        }

        public void EvaluatePopulace()
        {
            Parallel.ForEach(populace, indie => { indie.Evaluate(); });
        }

        public int Compare(Individual x, Individual y)
        {
            return x.CompareTo(y);
        }
    }
}
