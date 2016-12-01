using CPPNNEAT.CPPN;

namespace CPPNNEAT.EA
{
	class Individual
	{
		public readonly int individualID;
		public NeatGenome genome;
		public float Fitness { get; private set; }

		public CPPNetwork network;

		public Individual(IDCounters IDs)
		{
			individualID = IDs.IndividualID;
			Fitness = 0.3f;
		}

		public void Initialize(IDCounters IDs)
		{
			genome = new NeatGenome();
			genome.Initialize(IDs);
		}

		public void Evaluate(PlaceHolderCARunner CARunner, int speciesCount)
		{
			//CACase ca = new CACase(); <- static parameter type of "type of ca". doesn't matter here really.
			/*if(network == null)
				network = new CPPNetwork(genome);
			else
			{
				if(genome.hasMutated && !WasGenomeStructuralChange())
					//correct (asexual none structural change (can be checked through gene counting))
					network = network;
				else
					//time for a new network based on the old one
					network = new CPPNetwork(genome); // so this should contain all the information needed for the new network without any loss.
			}
			ca.RunCA(network);
			Fitness *= ca.GetCARunFitnessResult();*/
			Fitness *= 1.24f / speciesCount;
		}
	}
}