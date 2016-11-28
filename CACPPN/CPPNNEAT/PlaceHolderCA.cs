using System;
using System.Collections.Generic;
using System.Threading;
using CPPNNEAT.CPPN;

namespace CPPNNEAT
{
	class PlaceHolderCA
	{
		public PlaceHolderCA()
		{

		}

		public void RunCA(CPPNetwork network)
		{
			SeedCA();
			Console.Write("_");
			Thread.Sleep(10);
			network.GetOutput(new List<float>() { 0, 0, 0 });
			Console.Write("_");
			Thread.Sleep(10);
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