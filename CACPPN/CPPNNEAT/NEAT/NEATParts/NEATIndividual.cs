﻿using CPPNNEATCA.CA.Experiments;
using CPPNNEATCA.CPPN;
using CPPNNEATCA.CPPN.Parts;
using CPPNNEATCA.EA.Base;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.NEAT.Parts
{
	class NEATIndividual : Individual
	{
		public NeatGenome genome;
		public ICPPNetwork network;

		public NEATIndividual(NeatGenome genome, IDCounters IDs, bool init = false) : base(IDs.IndividualID)
		{
			Fitness = 0.0f;
			if(init)
				this.genome = new NeatGenome(genome, init: true);
			else
				this.genome = new NeatGenome(genome);
		}

		public NEATIndividual(NEATIndividual indie) : base(indie.individualID)
		{
			Fitness = 0.0f;
			genome = new NeatGenome(indie.genome);
		}

		public override void Evaluate(int speciesCount)
		{
			network = new CPPNetwork(genome, Neat.parameters.CPPN);
			//Fitness = Neat.evaluator.RunEvaluation(network.GetNextState);
			Fitness = ((LambdaExperiment)(Neat.evaluator)).RunNetworkFeedbackEvaluation(network); //trial thing. might have it Inside the RunEvaluation call
			if(Fitness >= Neat.parameters.CA.MaxFitnessPossible)
				Fitness = Fitness / speciesCount;
			else
				Fitness = Fitness;
		}
	}
}