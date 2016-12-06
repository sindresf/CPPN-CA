using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CPPNNEAT.CA.Base;
using CPPNNEAT.CA.Parts;
using CPPNNEATCA;
using CPPNNEATCA.CA;
using CPPNNEATCA.CA.Base;
using CPPNNEATCA.Utils;

namespace CPPNNEAT
{
	class LookUpCA : BaseOneDimentionalExperimentCA
	{
		static void Main1(string[] args)
		{
			Console.WriteLine("CA TEST RUN\n");
			INeatCA ca = new LookUpCA();

			var lookupTable = setupMapping();

			Func<List<float>, float> transitionfunction = (List<float> input) =>
			{
				foreach(List<int> rule in lookupTable)
				{
					bool ruleMatched = true;
					for(int i = 0; i < rule.Count - 1; i++)
					{
						if(rule[i] != (int)input[i])
							ruleMatched = false;
					}
					if(ruleMatched)
						return rule.Last();
				}
				Console.Write("whaa");
				return 0;
			};

			ca.RunEvaluation(transitionfunction);
		}

		private static List<List<int>> setupMapping()
		{
			var mapping = new List<List<int>>();
			mapping.Add(new List<int> { 0, 0, 0,
										   0 });

			mapping.Add(new List<int> { 0, 0, 1,
										   1 });

			mapping.Add(new List<int> { 0, 1, 0,
										   1 });

			mapping.Add(new List<int> { 0, 1, 1,
										   1 });

			mapping.Add(new List<int> { 1, 0, 0,
										   1 });

			mapping.Add(new List<int> { 1, 0, 1,
										   0 });

			mapping.Add(new List<int> { 1, 1, 0,
										   0 });

			mapping.Add(new List<int> { 1, 1, 1,
										   0 });
			return mapping;
		}

		public LookUpCA() : base()
		{
			parameters = new CAParameters(NeighbourHoodSize: 3, //above three is just "almost" supported currently
										  CellStateCount: 2,
										  CellWorldWidth: 25,
										  MaxGeneration: 25);

			MakeStates(parameters.CellStateCount);

			seed = new float[] { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

			if(seed.Length != parameters.CellWorldWidth)
				throw new FormatException("seed not compatible with experiment parameters");

			cells = new OneDimCell[parameters.CellWorldWidth];
			for(int i = 0; i < parameters.CellWorldWidth; i++)
				cells[i] = new OneDimCell(i, cells, parameters.NeighbourHoodSize);
			foreach(BaseCell cell in cells)
				cell.InitializeNeighbourhood();
		}

		public override float RunEvaluation(Func<List<float>, float> TransitionFunction)
		{
			Console.WriteLine("Running CA...");
			float[] currentValues = new float[seed.Length];
			seed.CopyTo(currentValues, 0);
			float[] futureValues = new float[seed.Length];
			seed.CopyTo(futureValues, 0);

			Console.WriteLine(currentValues.PrintCA());
			foreach(OneDimCell cell in cells)
				cell.ReferenceCurrentCellStates(currentValues);

			float bestStateScore = 2.0f*parameters.CellWorldWidth;
			//float currentScore = 0.0f;
			for(int i = 0; i < parameters.MaxGeneration; i++)
			{
				Parallel.ForEach(((BaseCell[])cells), (BaseCell cell) =>
				{
					futureValues[cell.x] = TransitionFunction(cell.GetNeighbourhoodCurrentState());
				});
				/*currentScore = CurrentVSGoalDifference(futureValues);
				if(IsDeadSpace(futureValues))
					return 1337;
				if(currentScore.SameWithinReason(0.0f))
					break;*/
				PushBack(futureValues, currentValues);
				Console.WriteLine(currentValues.PrintCA());
				Thread.Sleep(200);
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