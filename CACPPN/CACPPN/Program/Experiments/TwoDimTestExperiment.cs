using System;
using System.Collections.Generic;
using CACPPN.CA.DerivedTypes.CellTypes;

namespace CACPPN.Program.Experiments
{
    class TwoDimTestExperiment : Experiment
    {
        FloatCell[,] cellSpace;
        public TwoDimTestExperiment()
        {
            hyperParams.spaceSize = 27;
            hyperParams.generations = 200;
            cellType = CA.TypeEnums.CellType.STEP_VALUED;
            hyperParams.states = 4;
            cellSpace = new FloatCell[hyperParams.spaceSize, hyperParams.spaceSize];
            InitialConditionSetup();
        }

        protected override void InitialConditionSetup()
        {
            for (int i = 0; i < hyperParams.spaceSize; i++)
            {
                for (int j = 0; j < hyperParams.spaceSize; j++)
                {
                    cellSpace[i, j] = new FloatCell(0, hyperParams.states);
                }
            }
            for (int i = 0; i < 12; i++)
            {
                SpawnRandom();
            }
            SpawnOscillator(5);
            SpawnRPentomino(hyperParams.spaceSize - 5);
        }

        private void SpawnOscillator(int midIndex)
        {
            cellSpace[midIndex, midIndex].State = 1;
            cellSpace[midIndex, midIndex].OldState = 1;

            cellSpace[midIndex - 1, midIndex].State = 1;
            cellSpace[midIndex - 1, midIndex].OldState = 1;

            cellSpace[midIndex + 1, midIndex].State = 1;
            cellSpace[midIndex + 1, midIndex].OldState = 1;
        }

        private void SpawnRPentomino(int midIndex)
        {
            cellSpace[midIndex, midIndex].State = 1;
            cellSpace[midIndex, midIndex].OldState = 1;

            cellSpace[midIndex, midIndex - 1].State = 1;
            cellSpace[midIndex, midIndex - 1].OldState = 1;

            cellSpace[midIndex + 1, midIndex].State = 1;
            cellSpace[midIndex + 1, midIndex].OldState = 1;

            cellSpace[midIndex - 1, midIndex].State = 1;
            cellSpace[midIndex - 1, midIndex].OldState = 1;

            cellSpace[midIndex - 1, midIndex + 1].State = 1;
            cellSpace[midIndex - 1, midIndex + 1].OldState = 1;
        }

        private void SpawnRandom()
        {
            Random rand = new Random();
            int indexI = rand.Next(1, hyperParams.spaceSize);
            int indexJ = rand.Next(1, hyperParams.spaceSize);
            cellSpace[indexI, indexJ].State = 1;
            cellSpace[indexI, indexJ].OldState = 1;

        }

        public override bool IsSuccessState()
        {
            double spaceTotal = 0;
            foreach (FloatCell cell in cellSpace)
            {
                spaceTotal += cell.State;
            }
            if (spaceTotal == 0)
                return true;
            return false; //no "success" here because no goal, just run
        }

        public override void NextState()
        {
            for (int i = 1; i < hyperParams.spaceSize - 1; i++)
            {
                for (int j = 1; j < hyperParams.spaceSize - 1; j++)
                {
                    cellSpace[i, j].State = ruleCheck(getOKNeighbourhood(i, j), cellSpace[i, j].OldState);
                }
            }
            //the four sides
            for (int i = 1; i < hyperParams.spaceSize - 1; i++)
            {
                //upper row
                cellSpace[0, i].State = ruleCheck(getUpperNeighbourhood(i), cellSpace[0, i].OldState);
                //lower row
                cellSpace[hyperParams.spaceSize - 1, i].State = ruleCheck(getUpperNeighbourhood(i), cellSpace[hyperParams.spaceSize - 1, i].OldState);
                //left handside
                cellSpace[i, 0].State = ruleCheck(getUpperNeighbourhood(i), cellSpace[i, 0].OldState);
                //right handside
                cellSpace[i, hyperParams.spaceSize - 1].State = ruleCheck(getUpperNeighbourhood(i), cellSpace[i, hyperParams.spaceSize - 1].OldState);
            }
            //the four corners
            cellSpace[0, 0].State = ruleCheck(getUpperLeftNeighbourhood(), cellSpace[0, 0].OldState);
            cellSpace[0, hyperParams.spaceSize - 1].State = ruleCheck(getLowerLeftNeighbourhood(), cellSpace[hyperParams.spaceSize - 1, 0].OldState);
            cellSpace[hyperParams.spaceSize - 1, 0].State = ruleCheck(getUpperRightNeighbourhood(), cellSpace[0, hyperParams.spaceSize - 1].OldState);
            cellSpace[hyperParams.spaceSize - 1, hyperParams.spaceSize - 1].State = ruleCheck(getLowerRightNeighbourhood(), cellSpace[hyperParams.spaceSize - 1, hyperParams.spaceSize - 1].OldState);


            foreach (FloatCell cell in cellSpace)
            {
                cell.OldState = cell.State;
            }
        }

        private List<double> getOKNeighbourhood(int i, int j)
        {
            List<double> neighbourhoodState = new List<double>();
            neighbourhoodState.Add(cellSpace[i - 1, j - 1].OldState);
            neighbourhoodState.Add(cellSpace[i - 1, j].OldState);
            neighbourhoodState.Add(cellSpace[i - 1, j + 1].OldState);

            neighbourhoodState.Add(cellSpace[i, j - 1].OldState);
            neighbourhoodState.Add(cellSpace[i, j + 1].OldState);

            neighbourhoodState.Add(cellSpace[i + 1, j - 1].OldState);
            neighbourhoodState.Add(cellSpace[i + 1, j].OldState);
            neighbourhoodState.Add(cellSpace[i + 1, j + 1].OldState);
            return neighbourhoodState;
        }
        private List<double> getUpperNeighbourhood(int i)
        {
            List<double> neighbourhoodState = new List<double>();

            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, i].OldState);
            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, i - 1].OldState);
            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, i + 1].OldState);

            neighbourhoodState.Add(cellSpace[0, i - 1].OldState);
            neighbourhoodState.Add(cellSpace[0, i + 1].OldState);

            neighbourhoodState.Add(cellSpace[1, i - 1].OldState);
            neighbourhoodState.Add(cellSpace[1, i].OldState);
            neighbourhoodState.Add(cellSpace[1, i + 1].OldState);
            return neighbourhoodState;
        }
        private List<double> getLowerNeighbourhood(int i)
        {
            List<double> neighbourhoodState = new List<double>();

            neighbourhoodState.Add(cellSpace[0, i].OldState);
            neighbourhoodState.Add(cellSpace[0, i - 1].OldState);
            neighbourhoodState.Add(cellSpace[0, i + 1].OldState);

            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, i - 1].OldState);
            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, i + 1].OldState);

            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 2, i - 1].OldState);
            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 2, i].OldState);
            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 2, i + 1].OldState);
            return neighbourhoodState;
        }
        private List<double> getRightNeighbourhood(int i)
        {
            List<double> neighbourhoodState = new List<double>();

            neighbourhoodState.Add(cellSpace[i - 1, hyperParams.spaceSize - 2].OldState);
            neighbourhoodState.Add(cellSpace[i - 1, hyperParams.spaceSize - 1].OldState);
            neighbourhoodState.Add(cellSpace[i - 1, 0].OldState);

            neighbourhoodState.Add(cellSpace[i, hyperParams.spaceSize - 2].OldState);
            neighbourhoodState.Add(cellSpace[i, 0].OldState);

            neighbourhoodState.Add(cellSpace[i + 1, hyperParams.spaceSize - 2].OldState);
            neighbourhoodState.Add(cellSpace[i + 1, hyperParams.spaceSize - 1].OldState);
            neighbourhoodState.Add(cellSpace[i + 1, 0].OldState);
            return neighbourhoodState;
        }
        private List<double> getLeftNeighbourhood(int i)
        {
            List<double> neighbourhoodState = new List<double>();

            neighbourhoodState.Add(cellSpace[i - 1, hyperParams.spaceSize - 1].OldState);
            neighbourhoodState.Add(cellSpace[i - 1, 0].OldState);
            neighbourhoodState.Add(cellSpace[i - 1, 1].OldState);

            neighbourhoodState.Add(cellSpace[i, hyperParams.spaceSize - 1].OldState);
            neighbourhoodState.Add(cellSpace[i, 1].OldState);

            neighbourhoodState.Add(cellSpace[i + 1, hyperParams.spaceSize - 1].OldState);
            neighbourhoodState.Add(cellSpace[i + 1, 0].OldState);
            neighbourhoodState.Add(cellSpace[i + 1, 1].OldState);
            return neighbourhoodState;
        }

        private List<double> getUpperLeftNeighbourhood()
        {
            List<double> neighbourhoodState = new List<double>();

            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, hyperParams.spaceSize - 1].OldState);
            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, 0].OldState);
            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, 1].OldState);

            neighbourhoodState.Add(cellSpace[0, hyperParams.spaceSize - 1].OldState);
            neighbourhoodState.Add(cellSpace[0, 1].OldState);

            neighbourhoodState.Add(cellSpace[1, hyperParams.spaceSize - 1].OldState);
            neighbourhoodState.Add(cellSpace[1, 0].OldState);
            neighbourhoodState.Add(cellSpace[1, 1].OldState);
            return neighbourhoodState;
        }

        private List<double> getLowerLeftNeighbourhood()
        {
            List<double> neighbourhoodState = new List<double>();

            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 2, hyperParams.spaceSize - 1].OldState);
            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 2, 0].OldState);
            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 2, 1].OldState);

            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, hyperParams.spaceSize - 1].OldState);
            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, 1].OldState);

            neighbourhoodState.Add(cellSpace[0, hyperParams.spaceSize - 1].OldState);
            neighbourhoodState.Add(cellSpace[0, 0].OldState);
            neighbourhoodState.Add(cellSpace[0, 1].OldState);
            return neighbourhoodState;
        }

        private List<double> getUpperRightNeighbourhood()
        {
            List<double> neighbourhoodState = new List<double>();

            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, hyperParams.spaceSize - 2].OldState);
            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, hyperParams.spaceSize - 1].OldState);
            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, 0].OldState);

            neighbourhoodState.Add(cellSpace[0, hyperParams.spaceSize - 2].OldState);
            neighbourhoodState.Add(cellSpace[0, 0].OldState);

            neighbourhoodState.Add(cellSpace[1, hyperParams.spaceSize - 2].OldState);
            neighbourhoodState.Add(cellSpace[1, hyperParams.spaceSize - 1].OldState);
            neighbourhoodState.Add(cellSpace[1, 0].OldState);
            return neighbourhoodState;
        }

        private List<double> getLowerRightNeighbourhood()
        {
            List<double> neighbourhoodState = new List<double>();

            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 2, hyperParams.spaceSize - 2].OldState);
            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 2, hyperParams.spaceSize - 1].OldState);
            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 2, 0].OldState);

            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, hyperParams.spaceSize - 2].OldState);
            neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, 0].OldState);

            neighbourhoodState.Add(cellSpace[0, hyperParams.spaceSize - 2].OldState);
            neighbourhoodState.Add(cellSpace[0, hyperParams.spaceSize - 1].OldState);
            neighbourhoodState.Add(cellSpace[0, 0].OldState);
            return neighbourhoodState;
        }

        private double ruleCheck(List<double> neighbourhoodState, double centreState)
        {
            double total = 0;
            foreach (double state in neighbourhoodState)
            {
                total += state;
            }
            if (centreState > 0)
            {
                if (total < 2)
                    return 0;
                if (total < 4)
                    return 1;
                return 0;
            }
            else
            {
                if (total == 3)
                    return 1;
                return 0;
            }
        }

        public override string SpaceStateToString()
        {
            string space = "";
            for (int i = 0; i < hyperParams.spaceSize; i++)
            {
                space += "|";
                for (int j = 0; j < hyperParams.spaceSize; j++)
                {
                    space += "" + StateToString(cellSpace[i, j].State) + ".";
                }
                space += "|\n";
            }
            return space;
        }
        private char StateToString(double state)
        {
            if (state == 0)
                return ' ';
            if (state == 0.25)
                return '.';
            if (state == 0.5)
                return 'o';
            if (state == 1)
                return 'O';
            else
                return 'X';
        }
    }
}