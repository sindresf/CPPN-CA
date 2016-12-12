using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CPPNNEATCA.CA.Base;
using CPPNNEATCA.CA.Parts;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.CA.Experiments
{
	class Test1DimCA : BaseOneDimentionalExperimentCA
	{
		private object _lock = new object();
		public Test1DimCA() : base()
		{
			parameters = new CAParameters(NeighbourHoodSize: 3,
										  CellStateCount: 2,
										  CellWorldWidth: 5,
										  MaxGeneration: 4);

			MakeStates(parameters.CellStateCount);

			//seed = new float[] { 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 };
			//goal = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			seed = new float[] { 0, 0, 1, 0, 0 };
			goal = new float[] { 0, 1, 1, 1, 0 };

			if(seed.Length != parameters.CellWorldWidth
			 || goal.Length != parameters.CellWorldWidth)
				throw new FormatException("seed not compatible with experiment parameters");

			cells = new OneDimCell[parameters.CellWorldWidth];
			for(int i = 0; i < parameters.CellWorldWidth; i++)
				cells[i] = new OneDimCell(i, cells, parameters.NeighbourHoodSize);
			foreach(BaseCell cell in cells)
				cell.InitializeNeighbourhood();
		}

		public override float RunEvaluation(Func<Dictionary<int, float>, float> TransitionFunction)
		{
			Console.WriteLine();
			Thread.Sleep(300);
			lock(_lock)
			{
				float[] currentValues = new float[seed.Length];
				seed.CopyTo(currentValues, 0);
				float[] futureValues = new float[seed.Length];
				seed.CopyTo(futureValues, 0);

				float bestStateScore = 2.0f*parameters.CellWorldWidth;
				float currentScore = 0.0f;
				for(int i = 0; i < parameters.MaxGeneration; i++)
				{
					Parallel.ForEach(((BaseCell[])cells), (BaseCell cell) =>
				   {
					   futureValues[cell.x] = FloatToState(TransitionFunction(cell.GetNeighbourhoodCurrentState(currentValues)));
				   });
					currentScore = CurrentVSGoalDifference(futureValues);
					if(IsDeadSpace(futureValues))
						return 1337;
					if(currentScore.SameWithinReason(0.0f))
						break;
					PushBack(futureValues, currentValues);
					Console.WriteLine(currentValues.PrintCA());
					Thread.Sleep(80);
				}
				return bestStateScore;
			}
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
				diff += Math.Abs(goal[i] - current[i]);
			return diff;
		}

		private void PushBack(float[] future, float[] past)
		{
			for(int i = 0; i < parameters.CellWorldWidth; i++)
				past[i] = future[i];
		}
	}
}