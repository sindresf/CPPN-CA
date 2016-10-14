using System;
using System.Collections.Generic;
using CACPPN.CA.DerivedTypes.CellTypes;
using CACPPN.Utils.SeedingMachines;
using CACPPN.CA.Enums.Types;
using CACPPN.CA.Enums;

namespace CACPPN.Program.Experiments
{
    class TwoDimTestExperiment : Experiment
    {
        FloatCell[,] cellSpace;
        double[,] lastSpaceState;
        public TwoDimTestExperiment()
        {
            hyperParams.spaceSize = 27;
            hyperParams.generations = 100;
            cellType = CellType.STEP_VALUED;
            hyperParams.states = 4;
            cellSpace = new FloatCell[hyperParams.spaceSize, hyperParams.spaceSize];
            lastSpaceState = new double[hyperParams.spaceSize, hyperParams.spaceSize];
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
            /* for (int i = 0; i < 12; i++)
             {
                 SpawnRandom();
             }
             SpawnRPentomino(hyperParams.spaceSize - 5);
             SpawnRPentomino(5);

             SpawnRPentomino(18);*/

            GameOfLifeSeeds.SpawnCrossOscillator(12, 12, cellSpace, Orientation.VERTICAL);
            GameOfLifeSeeds.SpawnCrossOscillator(13, 14, cellSpace, Orientation.VERTICAL);
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
            for (int i = 0; i < hyperParams.spaceSize; i++)
            {
                for (int j = 0; j < hyperParams.spaceSize; j++)
                {
                    if (cellSpace[i, j].State != lastSpaceState[i, j])
                        return false;
                }
            }
            return true;
        }

        public override void NextState()
        {
            double nextState;
            for (int i = 1; i < hyperParams.spaceSize - 1; i++)
            {
                for (int j = 1; j < hyperParams.spaceSize - 1; j++)
                {
                    nextState = ruleCheck(getOKNeighbourhood(i, j), cellSpace[i, j].OldState);
                    lastSpaceState[i, j] = cellSpace[i, j].State;
                    cellSpace[i, j].State = nextState;
                }
            }
            //the four sides
            for (int i = 1; i < hyperParams.spaceSize - 1; i++)
            {
                //upper row
                nextState = ruleCheck(getUpperNeighbourhood(i), cellSpace[0, i].OldState);
                lastSpaceState[0, i] = cellSpace[0, i].State;
                cellSpace[0, i].State = nextState;
                //lower row
                nextState = ruleCheck(getUpperNeighbourhood(i), cellSpace[hyperParams.spaceSize - 1, i].OldState);
                lastSpaceState[hyperParams.spaceSize - 1, i] = cellSpace[hyperParams.spaceSize - 1, i].State;
                cellSpace[hyperParams.spaceSize - 1, i].State = nextState;
                //left handside
                nextState = ruleCheck(getUpperNeighbourhood(i), cellSpace[i, 0].OldState);
                lastSpaceState[i, 0] = cellSpace[i, 0].State;
                cellSpace[i, 0].State = nextState;
                //right handside
                nextState = ruleCheck(getUpperNeighbourhood(i), cellSpace[i, hyperParams.spaceSize - 1].OldState);
                lastSpaceState[i, hyperParams.spaceSize - 1] = cellSpace[i, hyperParams.spaceSize - 1].State;
                cellSpace[i, hyperParams.spaceSize - 1].State = nextState;
            }
            //the four corners
            nextState = ruleCheck(getUpperLeftNeighbourhood(), cellSpace[0, 0].OldState);
            lastSpaceState[0, 0] = cellSpace[0, 0].State;
            cellSpace[0, 0].State = nextState;

            nextState = ruleCheck(getLowerLeftNeighbourhood(), cellSpace[hyperParams.spaceSize - 1, 0].OldState);
            cellSpace[0, hyperParams.spaceSize - 1].State = nextState;
            lastSpaceState[0, hyperParams.spaceSize - 1] = nextState;

            nextState = ruleCheck(getUpperRightNeighbourhood(), cellSpace[0, hyperParams.spaceSize - 1].OldState);
            lastSpaceState[hyperParams.spaceSize - 1, 0] = cellSpace[hyperParams.spaceSize - 1, 0].State;
            cellSpace[hyperParams.spaceSize - 1, 0].State = nextState;

            nextState = ruleCheck(getLowerRightNeighbourhood(), cellSpace[hyperParams.spaceSize - 1, hyperParams.spaceSize - 1].OldState);
            lastSpaceState[hyperParams.spaceSize - 1, hyperParams.spaceSize - 1] = cellSpace[hyperParams.spaceSize - 1, hyperParams.spaceSize - 1].State;
            cellSpace[hyperParams.spaceSize - 1, hyperParams.spaceSize - 1].State = nextState;

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