using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CPPNNEATCA.CA.Base;
using CPPNNEATCA.CA.Parts;

namespace CPPNNEATCA.CA.Experiments
{
	class Test2DimCA : BaseTwoDimentionalExperimentCA
	{
		public Test2DimCA() : base()
		{
			parameters = new CAParameters(NeighbourHoodSize: 5,
											 CellStateCount: 2,
											 CellWorldWidth: 10,
											 MaxGeneration: 4);

			MakeStates(parameters.CellStateCount);

			seed = new float[10][];
			seed[0] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			seed[1] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			seed[2] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			seed[3] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			seed[4] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			seed[5] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			seed[6] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			seed[7] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			seed[8] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			seed[9] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };

			goal = new float[10][];
			goal[0] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			goal[1] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			goal[2] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			goal[3] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			goal[4] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			goal[5] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			goal[6] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			goal[7] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			goal[8] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			goal[9] = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };


			if(seed.Length != parameters.CellWorldWidth
			 || goal.Length != parameters.CellWorldWidth)
				throw new FormatException("seed not compatible with experiment parameters" + seed.Length + " " + goal.Length);

			cells = new TwoDimCell[parameters.CellWorldWidth][];
			for(int i = 0; i < parameters.CellWorldWidth; i++)
			{
				cells[i] = new TwoDimCell[parameters.CellWorldHeight];
				for(int j = 0; j < parameters.CellWorldWidth; j++)
					cells[i][j] = new TwoDimCell(i, j, cells, parameters.NeighbourHoodSize);
			}

			foreach(TwoDimCell[] cellRow in cells)
				foreach(TwoDimCell cell in cellRow)
					cell.InitializeNeighbourhood();
		}

		public override float RunEvaluation(Func<List<float>, int> TransitionFunction)
		{
			float[][] currentValues = new float[seed.GetLength(0)][];
			float[][] futureValues = new float[seed.GetLength(0)][];

			for(int i = 0; i < seed.Length; i++)
			{
				currentValues[i] = new float[seed[i].Length];
				seed[i].CopyTo(currentValues[i], 0);
				futureValues[i] = new float[seed[i].Length];
				seed[i].CopyTo(futureValues[i], 0);
			}

			float bestStateScore = 2.0f*parameters.CellWorldWidth;
			float currentScore = 0.0f;

			for(int i = 0; i < parameters.MaxGeneration; i++)
			{
				for(int x = 0; x < parameters.CellWorldWidth; x++)
					for(int y = 0; y < parameters.CellWorldHeight; y++)
						futureValues[x][y] = FloatToState(TransitionFunction(cells[x][y].GetNeighbourhoodCurrentState(currentValues)));
				/*
				Parallel.For(0, parameters.CellWorldHeight, x =>
				 {
					 Parallel.For(0, parameters.CellWorldWidth, y =>
					 {
						 futureValues[x][y] = FloatToState(TransitionFunction(cells[x][y].GetNeighbourhoodCurrentState(currentValues)));
					 });
				 });*/
				/*currentScore = CurrentVSGoalDifference(futureValues);
				if(IsDeadSpace(futureValues))
					return 1337;
				if(currentScore.SameWithinReason(0.0f))
					break;*/
				PushBack(futureValues, currentValues);
			}
			return bestStateScore;
		}
	}
}