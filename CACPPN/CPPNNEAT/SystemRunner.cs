using System;
using CPPNNEATCA.EA.Base;
using CPPNNEATCA.NEAT;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA
{
	class SystemRunner
	{
		static void Main(string[] args)
		{
			//ProperTestRun();
			float[] xs = new float[] {-1.2f,-1.0f,-0.5f,0.0f,0.5f,1.0f,1.1f};

			foreach(float x in xs)
			{
				float sum = x;
				float xt = 1.0f;
				float y = .0f;
				float mod = 1.0f;

				Console.WriteLine((xt * sum + y) % mod);
			}
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