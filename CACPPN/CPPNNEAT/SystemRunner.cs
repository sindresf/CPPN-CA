using System;
using System.Threading;
using CPPNNEATCA.CA;
using CPPNNEATCA.CA.Experiments;
using CPPNNEATCA.EA.Base;
using CPPNNEATCA.NEAT;

namespace CPPNNEATCA
{
	class SystemRunner
	{
		static void Main(string[] args)
		{
			MockUpRun();
			//ProperTestRun();
		}

		public static void MockUpRun()
		{
			Console.WriteLine("initializing NEAT.");
			EvolutionaryAlgorithm neat = new Neat(); //takes the CA as argument
			Thread.Sleep(100);
			Console.WriteLine("NEAT ready!");
			neat.InitializePopulation();
			Thread.Sleep(250);
			Console.WriteLine("Population ready!");
			Console.WriteLine("Population being evaluated for the first time.");
			neat.EvaluatePopulation();
			Thread.Sleep(550);
			Console.WriteLine("All ready!");
			Thread.Sleep(50);
			Console.WriteLine("Starting evolutionary cycle!");
			Console.WriteLine("Maximum cycles set to {0}", EAParameters.MaximumRuns);
			for(int i = 0; i < EAParameters.MaximumRuns; i++)
			{
				Console.Write("."); //some form of interrupt could be nice if it is to run "all day"
				neat.EvaluatePopulation();
				//what's MISSING more than the rest is the MakingLoveMachine
				Thread.Sleep(30);
			}
			Console.Write("\nDone\n");
			Console.Write("best fitness: {0}\n", neat.GetBestFitness()); //not exaclty how i want it to work but..
			Console.WriteLine("finishing up.");
			Thread.Sleep(300);
			Console.WriteLine("Final best fitness individual and intermidiary results can be found in the log file system");
		}

		public static void ProperTestRun()
		{

		}
	}
}