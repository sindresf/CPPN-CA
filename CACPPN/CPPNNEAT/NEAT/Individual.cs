using System;
using CPPNNEAT.CPPN;

namespace CPPNNEAT.NEAT
{
	class Individual : IComparable<Individual>
	{
		public readonly int individualID;
		public Genome genome;
		public float Fitness { get; private set; }

		private CPPNetwork network; //only a "new" genome -> new network when there's a structural change

		public Individual(IDCounters IDs)
		{
			individualID = IDs.IndividualID;
			Fitness = 0.0f;
		}

		public void Initialize(IDCounters IDs)
		{
			genome = new Genome();
			genome.Initialize(IDs);
		}

		public void Evaluate()
		{
			if(network == null)
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


			// Here is where I need a STATIC CA already initialized for the whole run
			// that's run and then a fitness function call is made
			// to check the final CA state (if that is all that matters)
			Fitness = 0.01f; // <- the result of the CA call.
		}

		private bool WasGenomeStructuralChange()
		{
			//yes? this makes sense right?
			bool isStructurallyChanged = false;
			isStructurallyChanged = genome.nodeGenes.Count != network.nodes.Length;
			isStructurallyChanged &= genome.connectionGenes.Count != network.connections.Length;
			return isStructurallyChanged;
		}

		public int CompareTo(Individual other)
		{
			//needs to be an implementation of the function in the paper.
			return other.genome.connectionGenes.Count - genome.connectionGenes.Count;
		}
	}
}