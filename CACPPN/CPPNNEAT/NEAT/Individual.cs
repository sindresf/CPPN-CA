using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPPNNEAT.NEAT
{
    class Individual : IComparable<Individual>
    {
        public readonly int individualID;
        public Genome genome;

        public Individual(int individualID)
        {
            this.individualID = individualID;
        }

        public int CompareTo(Individual other)
        {
            //needs to be an implementation of the function in the paper.
            return other.genome.connectionGenes.Count - genome.connectionGenes.Count;
        }
    }
}
