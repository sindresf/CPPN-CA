using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CPPNNEAT.CA.Base;
using CPPNNEAT.CA.Parts;
using CPPNNEATCA.CA.Base;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.CA.Experiments
{
	class Test1DimCA : BaseOneDimentionalExperimentCA
	{

		public Test1DimCA() : base()
		{ //needs to setup everything that needs to be (and can be) exactly the same per individual
			parameters = new CAParameters(NeighbourHoodSize: 3,
										  CellStateCount: 2,
										  CellWorldWidth: 10,
										  MaxGeneration: 15);

			MakeStates(parameters.CellStateCount);

			seed = new float[] { 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 };
			goal = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };

			if(seed.Length != parameters.CellWorldWidth
			 || goal.Length != parameters.CellWorldWidth)
				throw new FormatException("seed not compatible with experiment parameters");

			cells = new OneDimCell[parameters.CellWorldWidth];
			for(int i = 0; i < parameters.CellWorldWidth; i++)
				cells[i] = new OneDimCell(i, cells, parameters.NeighbourHoodSize);
			foreach(BaseCell cell in cells)
				cell.InitializeNeighbourhood();
		}

		public override float RunEvaluation(Func<List<float>, float> TransitionFunction)
		{
			float[] currentValues = seed;
			float[] futureValues = seed;

			foreach(OneDimCell cell in cells)
				cell.ReferenceCurrentCellStates(currentValues);

			Console.WriteLine("Running CA generation:");
			float bestStateScore = 2.0f*parameters.CellWorldWidth;
			float currentScore = 0.0f;
			for(int i = 0; i < parameters.MaxGeneration; i++)
			{
				Console.WriteLine(i);
				Parallel.ForEach(((BaseCell[])cells), (BaseCell cell) =>
			   {
				   futureValues[cell.x] = FloatToState(TransitionFunction(cell.GetNeighbourhoodCurrentState()));
			   });
				currentScore = CurrentVSGoalDifference(futureValues);
				if(IsDeadSpace(futureValues))
				{
					Console.WriteLine("dead space.");
					break;
				}
				if(currentScore.SameWithinReason(0.0f))
				{
					Console.WriteLine("Success!");
					break;
				}
				PushBack(futureValues, currentValues);
			}
			return bestStateScore;
		}
		private bool IsDeadSpace(float[] cellStates)
		{
			foreach(float state in cellStates)
				if(state.SameWithinReason(0.0f))
					return false;
			return true;
		}

		private float CurrentVSGoalDifference(float[] current)
		{
			float diff = 0.0f;
			for(int i = 0; i < parameters.CellWorldWidth; i++)
				diff += goal[i] - current[i];
			return diff;
		}

		private void PushBack(float[] future, float[] past)
		{
			for(int i = 0; i < parameters.CellWorldWidth; i++)
				past[i] = future[i];
		}
	}
}