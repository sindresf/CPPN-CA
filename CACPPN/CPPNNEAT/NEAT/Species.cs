using System.Collections.Generic;

namespace CPPNNEAT.NEAT
{
    class Species : IComparer<Individual>
    {
        public readonly int speciesID;
        public List<Individual> populace;

        public Species(int speciesID, int populationCount, int currentIndividualID)
        {
            this.speciesID = speciesID;
            populace = new List<Individual>();
            for(int i = 0; i < populationCount; i++)
            {
                populace.Add(new Individual(currentIndividualID++)); // should be a function of its own I think,
                                                                     // for clarity in coding between initial random and such.
            }
        }

        public int Compare(Individual x, Individual y)
        {
            return x.CompareTo(y);
        }
    }
}
