using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CACPPN;
using CPPNNEAT.NEAT;

namespace CPPNNEAT
{
	class SystemRunner
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Creating the CA.");
			//creates CA
			Thread.Sleep(250);
			Console.WriteLine("CA ready to go!");
			Thread.Sleep(50);
			Console.WriteLine("initializing NEAT.");
			EvolutionaryAlgorithm neat = new EvolutionaryAlgorithm(); //takes the CA as argument
			Thread.Sleep(100);
			Console.WriteLine("NEAT ready!");
			//neat.InitializePopulation();
			Thread.Sleep(250);
			Console.WriteLine("Population ready!");
			Console.WriteLine("Population being evaluated for the first time.");
			//neat.EvaluatePopulation();
			Thread.Sleep(550);
			Console.WriteLine("All ready!");
			Thread.Sleep(50);
			Console.WriteLine("Starting evolutionary cycle!");
			Console.WriteLine("Maximum cycles set to {0}", EAParameters.MaximumRuns);
			for(int i = 0; i < EAParameters.MaximumRuns; i++)
			{
				Console.Write(".");
				Thread.Sleep(30);
			}
			Console.Write("\nDone\n");
			Console.WriteLine("finishing up.");
			Thread.Sleep(300);
			Console.WriteLine("Final best fitness individual and intermidiary results can be found in the log file system");
		}
	}
}
