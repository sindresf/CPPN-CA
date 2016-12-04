using System;
using System.Collections.Generic;
using CPPNNEAT.CA.Parts;
using CPPNNEATCA;
using CPPNNEATCA.CA.Base;

namespace CPPNNEAT.CA.Experiments
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

			//seed = new float[] { 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 };
			//goal = new float[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };
			seed = new float[,]
									{ { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 },
									  { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 },
									  { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 },
									  { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 },
									  { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 },
									  { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 },
									  { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 },
									  { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 },
									  { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 },
									  { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 }};

			goal = new float[,]
									{ { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 },
									  { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 },
									  { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 },
									  { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 },
									  { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 },
									  { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 },
									  { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 },
									  { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 },
									  { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 },
									  { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 }};

			if(seed.GetLength(1) != parameters.CellWorldWidth
			 || goal.GetLength(0) != parameters.CellWorldWidth)
				throw new FormatException("seed not compatible with experiment parameters" + seed.Length + " " + goal.Length);

			cells = new TwoDimCell[parameters.CellWorldWidth, parameters.CellWorldHeight];
			for(int i = 0; i < parameters.CellWorldWidth; i++)
				for(int j = 0; j < parameters.CellWorldWidth; j++)
					cells[i, j] = new TwoDimCell(i, j, cells, parameters.NeighbourHoodSize);

			foreach(TwoDimCell cell in cells)
				cell.InitializeNeighbourhood();
		}

		public override float RunEvaluation(Func<List<float>, float> TransitionFunction)
		{
			throw new NotImplementedException();
		}
	}
}
