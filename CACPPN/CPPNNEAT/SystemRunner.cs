using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			var timer = new Stopwatch();
			timer.Start();
			for(int i = 0; i < 50; i++)
				ProperTestRun();
			//NetworkTest();
			timer.Stop();
			Console.WriteLine("that took {0} ms", timer.ElapsedMilliseconds);
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
			Console.WriteLine();
			CPPNParameters parames = new CPPNParameters();
			CPPNetwork network = new CPPNetwork(genome,parames);
			var lol = network.inputNodes;
			var lmao = network.hiddenNodes;
			var rofl = network.outputNodes;
			Console.Write("inputCount: {0}\tnodeIDs: ", lol.Count);
			foreach(var sta in lol.Values)
			{
				Console.Write(sta.nodeID + " ");
			}
			Console.Write("\nhiddenCount: {0}\tnodeIDs: ", lmao.Count);
			foreach(var sta in lmao.Values)
			{
				Console.Write(((InternalNetworkNode)sta).nodeID + " ");
			}
			Console.Write("\noutputCount: {0}\tnodeIDs: ", rofl.Count);
			foreach(var sta in rofl.Values)
			{
				Console.Write(((OutputNetworkNode)sta).nodeID + " ");
			}
			Console.WriteLine("\n");
			Console.WriteLine("\n\tRunning Through The CPPNetwork!\n");
			int nextState = network.GetNextState(new List<float>() {0.2f,0.4f,0.99f});
			Console.WriteLine("Got state: {0}", nextState);
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