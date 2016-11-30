using System;
using System.Collections.Generic;
using System.Threading;
using CPPNNEAT.CPPN;

namespace CPPNNEAT
{
	class PlaceHolderCARunner
	{
		public PlaceHolderCARunner()
		{

		}

		private static readonly object _lock = new object();

		public void RunCA(CPPNetwork network)
		{
			//so this needs to "remake" a simple CA "case" to run per individual
			lock(_lock)
			{
				SeedCA();
				Console.Write("_");
				Thread.Sleep(10);
				network.GetOutput(new List<float>() { 0, 0, 0 });
				Console.Write("_");
				Thread.Sleep(10);
			}
		}

		public float GetCARunFitnessResult() //this is where all the different tests comes in, with parameter changes *only* NEAT side
		{
			return 1.1f;
		}

		private void SeedCA()
		{
			// "resets it" so every individual starts out the same
		}
	}
}