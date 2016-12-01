using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CACPPN.CA.DerivedTypes.CellTypes;
using CACPPN.Utils;

namespace CACPPN.Program.Experiments
{
	class OneDimTestExperiment : Experiment
	{
		BooleanCell[] cellSpace;
		List<List<double>> mapping;
		double?[] futureStates; //expanded into proper storage

		public OneDimTestExperiment() : base()
		{
			hyperParams.spaceSize = 60;
			hyperParams.generations = 100;
			hyperParams.neighbourhoodWidth = 1;
			hyperParams.timeStep = 100;
			cellSpace = new BooleanCell[hyperParams.spaceSize];
			futureStates = new double?[hyperParams.spaceSize];
			InitialConditionSetup();
		}

		protected override void InitialConditionSetup()
		{
			InitializeCells();
			InitializeCellNeighbourhoods();
			SeedTheCA();
			setupMapping();
		}

		private void setupMapping()
		{
			mapping = new List<List<double>>(); //This is why lookup sucks!
			mapping.Add(new List<double> { 0, 0, 0, 0 });
			mapping.Add(new List<double> { 0, 0, 1, 1 });
			mapping.Add(new List<double> { 0, 1, 0, 1 });
			mapping.Add(new List<double> { 0, 1, 1, 1 });
			mapping.Add(new List<double> { 1, 0, 0, 1 });
			mapping.Add(new List<double> { 1, 0, 1, 0 });
			mapping.Add(new List<double> { 1, 1, 0, 0 });
			mapping.Add(new List<double> { 1, 1, 1, 0 });
		}

		public override bool IsSuccessState()
		{
			return false; //no "success" here because no goal, just run
		}

		public override void NextState()
		{
			Parallel.For(0, hyperParams.spaceSize, (int i) =>
			{
				futureStates[i] = ruleCheck(cellSpace[i].NeighbourhoodCurrentState);
			});
			Parallel.For(0, hyperParams.spaceSize, (int i) =>
			 {
				 cellSpace[i].FutureState = futureStates[i];
			 });
		}

		private double? ruleCheck(List<double> neighbourhoodState)
		{
			foreach(List<double> rule in mapping)
			{
				bool ruleMatched = true;
				for(int i = 0; i < rule.Count - 1; i++)
				{
					if(rule[i] != neighbourhoodState[i])
						ruleMatched = false;
				}
				if(ruleMatched)
					return rule.Last();
			}
			Console.Write("whaa");
			return 0;
		}

		public override string SpaceStateToString()
		{
			string space = "|";
			foreach(BooleanCell cell in cellSpace)
			{
				space += "" + StateToString(cell.CurrentState);
			}
			space = space.Substring(0, space.Length - 1) + "|";
			return space;
		}
		private char StateToString(double? state)
		{
			if(state == 0)
			{
				return ' ';
			} else if(state == 1)
			{
				return 'O';
			}
			return 'X';
		}

		protected override void InitializeCells()
		{
			Parallel.For(0, hyperParams.spaceSize, (int i) =>
			  {
				  cellSpace[i] = new BooleanCell(i).SetFirstState(0) as BooleanCell;
			  });
		}

		protected override void InitializeCellNeighbourhoods()
		{
			NeighbourhoodInitializor.InitializeNeighbourhoods1D(cellSpace, hyperParams);
		}

		protected override void SeedTheCA()
		{
			Random rand = new Random();
			int index = rand.Next(hyperParams.spaceSize);
			cellSpace[index].SetFirstState(1);

			index = rand.Next(hyperParams.spaceSize);
			cellSpace[index].SetFirstState(1);
		}
	}
}