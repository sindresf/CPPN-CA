using System.Collections.Generic;

namespace CPPNNEAT.NEAT
{
    class Population
    {
        List<Species> species;
        public readonly int populationCount;
        private int newSpeciesID = 0;
        private int currentIndividualID = 0;

        public Population(int populationCount)
        {
            this.populationCount = populationCount;
            species = new List<Species>();
            species.Add(new Species(newSpeciesID++, populationCount, currentIndividualID));
            currentIndividualID += populationCount;
        }
    }
}
