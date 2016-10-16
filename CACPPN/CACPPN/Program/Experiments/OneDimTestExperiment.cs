using System;
using System.Collections.Generic;
using System.Linq;
using CACPPN.CA.DerivedTypes.CellTypes;

namespace CACPPN.Program.Experiments
{
    class OneDimTestExperiment : Experiment
    {
        BooleanCell[] cellSpace;
        List<List<double>> mapping;

        public OneDimTestExperiment() : base()
        {
            hyperParams.spaceSize = 110;
            hyperParams.generations = 500;
            cellSpace = new BooleanCell[hyperParams.spaceSize];
            InitialConditionSetup();
        }

        protected override void InitialConditionSetup()
        {
            int mid = hyperParams.spaceSize / 2;
            for (int i = 0; i < hyperParams.spaceSize; i++)
            {
                if (i == mid)
                    cellSpace[i] = new BooleanCell(1);
                else
                    cellSpace[i] = new BooleanCell(0);
            }
            Random rand = new Random();
            int index = rand.Next(hyperParams.spaceSize);
            cellSpace[index].SetFirstState(1);

            index = rand.Next(hyperParams.spaceSize);
            cellSpace[index].SetFirstState(1);
            setupMapping();
        }

        private void setupMapping()
        {
            mapping = new List<List<double>>(); //This is why lookup sucks!
            mapping.Add(new List<double> { 0, 0, 0, 0 });
            mapping.Add(new List<double> { 0, 0, 1, 1 });
            mapping.Add(new List<double> { 0, 1, 0, 0 });
            mapping.Add(new List<double> { 1, 0, 0, 1 });
            mapping.Add(new List<double> { 0, 1, 1, 1 });
            mapping.Add(new List<double> { 1, 1, 0, 0 });
            mapping.Add(new List<double> { 1, 0, 1, 0 });
            mapping.Add(new List<double> { 1, 1, 1, 0 });

        }

        public override bool IsSuccessState()
        {
            return false; //no "success" here because no goal, just run
        }

        public override void NextState()
        {
            for (int i = 0; i < hyperParams.spaceSize; i++)
            {
                List<double?> neighbourhoodState = new List<double?>();
                if (i == 0)
                {
                    neighbourhoodState.Add(cellSpace.Last().FutureState);
                    neighbourhoodState.Add(cellSpace[i].FutureState);
                    neighbourhoodState.Add(cellSpace[i + 1].FutureState);
                }
                else if (i == hyperParams.spaceSize - 1)
                {
                    neighbourhoodState.Add(cellSpace[i - 1].FutureState);
                    neighbourhoodState.Add(cellSpace[i].FutureState);
                    neighbourhoodState.Add(cellSpace.First().FutureState);
                }
                else
                {
                    neighbourhoodState.Add(cellSpace[i - 1].FutureState);
                    neighbourhoodState.Add(cellSpace[i].FutureState);
                    neighbourhoodState.Add(cellSpace[i + 1].FutureState);
                }

                cellSpace[i].FutureState = ruleCheck(neighbourhoodState);
            }
        }

        private double? ruleCheck(List<double?> neighbourhoodState)
        {
            foreach (List<double> rule in mapping)
            {
                bool ruleMatched = true;
                for (int i = 0; i < rule.Count - 1; i++)
                {
                    if (rule[i] != neighbourhoodState[i])
                    {
                        ruleMatched = false;
                    }
                }
                if (ruleMatched)
                {
                    //Console.WriteLine("rule and neighbourhood: " + string.Join(",", rule.ToArray()) + "  ..  " + string.Join(",", neighbourhoodState.ToArray()));
                    return rule.Last();
                }
            }
            return 0;
        }

        public override string SpaceStateToString()
        {
            string space = "|";
            foreach (BooleanCell cell in cellSpace)
            {
                space += "" + StateToString(cell.CurrentState);
            }
            space = space.Substring(0, space.Length - 1) + "|";
            return space;
        }
        private char StateToString(double? state)
        {
            if (state == 0)
            {
                return ' ';
            }
            else if (state == 1)
            {
                return 'O';
            }
            return 'X';
        }

        protected override void InitializeCells()
        {
            throw new NotImplementedException();
        }

        protected override void InitializeCellNeighbourhoods()
        {
            throw new NotImplementedException();
        }

        protected override void SeedTheCA()
        {
            throw new NotImplementedException();
        }
    }
}
