using System;
using CPPNNEATCA.EA.Base;
using CPPNNEATCA.NEAT;

namespace CPPNNEATCA
{
	class SystemRunner
	{
		static void Main(string[] args)
		{
			//ProperTestRun();
			int stateCount = 5;
			var states = new float[stateCount];
			states[0] = 0.0f;
			states[stateCount - 1] = 1.0f;
			double interval = 1.0 / (stateCount - 1);
			for(int i = 1; i < stateCount - 1; i++)
				states[i] = (float)Math.Round(states[i - 1] + interval, 2);
			foreach(float state in states)
				Console.Write(state + " ");

			float shortestDistance = 1.0f;
			float value = 0.4652345913123f;
			float resultingState = 0.0f;
			foreach(float candidateState in states)
			{
				float dist = Math.Abs(candidateState-value);
				if(shortestDistance > dist)
				{
					shortestDistance = dist;
					resultingState = candidateState;
				}
			}
			Console.WriteLine("\nfloatToState({0}) = {1}", value, resultingState);
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