using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPPNNEAT.NEAT;

namespace CPPNNEAT.CPPN
{
    class NetworkBuilder
    {
        // takes the genome
        // and creates a proper network
        // based on the ActivationFunctions
        // only exposed function is the "GetNetwork(Genome genes)"
        // and that is only so that NEAT can work the individual
        // mutation for individuals who survive and/or mutation only on the weights.
        //any new structure change should result in a call to this

        public static CPPNetwork GetNetwork(Genome genome)
        {
            return null;
        }
    }
}
