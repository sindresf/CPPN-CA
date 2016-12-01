using CPPNNEAT.CPPN;
using CPPNNEAT.EA.Base;
using CPPNNEAT.Utils;

namespace CPPNNEAT.EA
{
	class NEATIndividual : Individual
	{
		public NeatGenome genome;

		public CPPNetwork network;

		public NEATIndividual(IDCounters IDs) : base(IDs.IndividualID)
		{
			Fitness = 0.3f;
		}

		public override void Initialize(IDCounters IDs)
		{
			genome = new NeatGenome();
			genome.Initialize(IDs);
			if(Neat.random.NextBoolean(MutationChances.CreationMutationChance))
				Mutator.Mutate(genome, IDs);
		}

		public override void Evaluate(PlaceHolderCARunner CARunner, int speciesCount)
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