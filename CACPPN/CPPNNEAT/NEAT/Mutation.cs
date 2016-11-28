using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPPNNEAT.NEAT
{
	class Mutation
	{
		public static void AddNode()
		{

		}

		public static void AddConnection()
		{

		}

		public static void ChangeWeight()
		{

		}

		public static void ChangeFunction()
		{

		}

		public static Genome Crossover(Individual indie1, Individual indie2)
		{
			Genome genome1 = indie1.genome;
			Genome genome2 = indie2.genome;

			return null;
		}
	}

	enum MutationType
	{
		AddNode,
		AddConnection,
		ChangeWeight,
		ChangeFunction
	}
}
