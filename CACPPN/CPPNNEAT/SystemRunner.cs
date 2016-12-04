using System;
using CPPNNEATCA.EA.Base;
using CPPNNEATCA.NEAT;

namespace CPPNNEATCA
{
	class SystemRunner
	{
		static void Main1(string[] args)
		{
			ProperTestRun();
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
				neat.NextGeneration(); // <- this can't possibly work just yet
			}
			Console.Write("\n\nDone\n");
		}
	}
}