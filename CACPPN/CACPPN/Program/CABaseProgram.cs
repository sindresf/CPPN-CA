using System;
using System.Threading;
using CACPPN.Program.Experiments;

namespace CACPPN.Program
{
	class CABaseProgram //Remember, the BASE CA is not evolutionary design, but ALL hardcoded
	{
		static void Main(string[] args)
		{
			CABaseProgram program = new CABaseProgram();
			program.Run();
		}

		public void Run()
		{
			Experiment experiment = new OneDimTestExperiment();
			Console.WriteLine(experiment.SpaceStateToString());
			bool successFinish = false;
			for(int i = 0; i < experiment.hyperParams.generations; i++)
			{
				experiment.NextState();
				Thread.Sleep(experiment.hyperParams.timeStep);
				if(experiment.spaceType != CA.Enums.Types.CellularSpaceType.ONE_DIMENSIONAL)
					Console.Clear();
				if(experiment.IsSuccessState())
				{
					successFinish = true;
					Console.WriteLine(experiment.SpaceStateToString());
					Console.WriteLine("\ndone through success");
					break;
				}
				Console.WriteLine(experiment.SpaceStateToString());
			}
			if(!successFinish)
				Console.WriteLine("\nall generations done.");
		}
	}
}