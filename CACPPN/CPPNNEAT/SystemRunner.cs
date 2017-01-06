using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
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
			//ParaRun();
			SingleRun();
		}

		public static void LambdaExpTest()
		{
			int results = 8;
			int cond1 = 1, cond2 = 1, cond3 = 1;
			for(int i = 0; i < results; i++)
			{
				if(i % 1 == 0) cond1 = cond1 == 1 ? 0 : 1;
				if(i % 2 == 0) cond2 = cond2 == 1 ? 0 : 1;
				if(i % 4 == 0) cond3 = cond3 == 1 ? 0 : 1;
				Console.WriteLine("i = {3} ->rule: {0},{1},{2}", cond1, cond2, cond3, i);
			}
		}

		public static void SingleRun()
		{
			Console.WriteLine("running");
			ProperTestRun();
			Console.WriteLine("run complete");
		}

		public static void ParaRun()
		{
			int runs = 10;
			int threads = 7;
			Console.WriteLine("running " + runs * threads + " NEATs in semi-parallel:");
			var totsTimer = new Stopwatch();
			totsTimer.Start();
			Parallel.For(0, runs, i =>
			{
				ProperTestRun();
				ProperTestRun();
				ProperTestRun();
				ProperTestRun();
				ProperTestRun();
				ProperTestRun();
				ProperTestRun();
			});
			totsTimer.Stop();
			Console.WriteLine("total time: {0} ms", totsTimer.ElapsedMilliseconds);
			Console.WriteLine("avg. para run: {0}", totsTimer.ElapsedMilliseconds / runs);
			Console.WriteLine("avg. Neat run: {0}", totsTimer.ElapsedMilliseconds / (runs * threads));
		}

		public static void ProperTestRun()
		{
			EvolutionaryAlgorithm neat = new Neat();
			neat.InitializePopulation();
			bool solvedit = false;
			for(int i = 0; i < EAParameters.MaximumRuns; i++)
			{
				Console.WriteLine("generation:" + i);
				neat.EvaluatePopulation();
				if(neat.IsDeadRun())
				{
					Console.WriteLine("death");
					break;
				}
				if(neat.SolvedIt())
				{
					Console.WriteLine("obtained maximum fitness");
					solvedit = true;
					break;
				}
				neat.NextGeneration();
			}
			if(solvedit)
				Console.WriteLine("your perfect genome is ready for you in {0}", @"C:/genes");
		}
	}
}
