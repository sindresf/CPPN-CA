using System;
using CPPNNEATCA.CPPN.Parts;
using CPPNNEATCA.EA.Base;
using CPPNNEATCA.NEAT;
using CPPNNEATCA.NEAT.Parts;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA
{
	class SystemRunner
	{
		static void Main(string[] args)
		{
			//ProperTestRun();
			NetworkTest();
		}

		public static void NetworkTest()
		{
			IDCounters IDs = new IDCounters();
			NeatGenome genome = new NeatGenome();
			genome.Initialize(IDs);
			Console.WriteLine("nodeGeneCount: " + genome.nodeGenes.Count);
			Console.WriteLine("connectionGeneGount: " + genome.connectionGenes.Count);
			Console.WriteLine("IDCounter for nodeGenes equals "
				+ genome.nodeGenes.Count
				+ ": " + (IDs.NodeGeneID == genome.nodeGenes.Count));
			Console.WriteLine("IDCounter for connectionGenes equals "
				+ genome.connectionGenes.Count
				+ ": " + (IDs.NodeGeneID == genome.connectionGenes.Count));
			Console.WriteLine("all other counters are 0: "
				+ (IDs.IndividualID == 0 && IDs.SpeciesID == 0));

			CPPNParameters parames = new CPPNParameters();
			CPPNetwork network = new CPPNetwork(genome,parames);

		}

		public static void ProperTestRun()
		{
			Console.Write("\nStarting up\n");
			EvolutionaryAlgorithm neat = new Neat();
			neat.InitializePopulation();

			Console.Write("\nRunning.");
			for(int i = 0; i < EAParameters.MaximumRuns; i++)
			{
				Console.Write(".");
				neat.EvaluatePopulation();
				neat.NextGeneration();
			}
			Console.Write("\n\nDone\n");
		}
	}
}