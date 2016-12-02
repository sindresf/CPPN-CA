using CPPNNEAT.CA;
using CPPNNEAT.CPPN;
using CPPNNEAT.EA.Base;
using CPPNNEAT.NEAT.EABase;
using CPPNNEAT.Utils;

namespace CPPNNEAT.EA
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

		public override void Evaluate(IEvaluator ca, int speciesCount)
		{
			network = new CPPNetwork(genome, Neat.parameters.CPPN);
			Fitness *= ((INeatCA)ca).RunEvaluation(network.GetOutput);
			Fitness *= 1.24f / speciesCount;
		}
	}
}