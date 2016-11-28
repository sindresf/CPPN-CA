using System;
using CPPNNEAT.CPPN;

namespace CPPNNEAT.NEAT
{
	class Individual : IComparable<Individual>
	{
		public readonly int individualID;
		public Genome genome;
		public float Fitness { get; private set; }

		public Individual(IDCounters IDs)
		{
			individualID = IDs.IndividualID;
			Fitness = 0.0f;
		}

		public void Initialize()
		{
			genome = new Genome();
			genome.Initialize();
		}

		public void Evaluate()
		{
			CPPNetwork network = NetworkBuilder.GetNetwork(genome);

			// Here is where I need a STATIC CA already initialized for the whole run
			// that's run and then a fitness function call is made
			// to check the final CA state (if that is all that matters)
			Fitness = 0.01f; // <- the result of the CA call.
		}

		public int CompareTo(Individual other)
		{
			//needs to be an implementation of the function in the paper.
			return other.genome.connectionGenes.Count - genome.connectionGenes.Count;
		}
	}
}