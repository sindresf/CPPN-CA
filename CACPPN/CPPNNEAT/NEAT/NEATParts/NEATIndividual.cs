using CPPNNEATCA.CA;
using CPPNNEATCA.CPPN;
using CPPNNEATCA.CPPN.Parts;
using CPPNNEATCA.EA.Base;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.EA
{
	class NEATIndividual : Individual
	{
		public NeatGenome genome;

		public ICPPNetwork network;

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

		public override void Evaluate(INeatCA ca, int speciesCount)
		{
			network = new CPPNetwork(genome, Neat.parameters.CPPN);
			Fitness *= ((INeatCA)ca).RunEvaluation(network.GetOutput);
			Fitness *= 1.24f / speciesCount;
		}
	}
}