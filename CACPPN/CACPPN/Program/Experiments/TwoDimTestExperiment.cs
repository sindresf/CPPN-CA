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
        Cell[,] cellSpace;
        double?[,] lastSpaceState;
        private bool firstGeneration = true;
        public TwoDimTestExperiment()
        {
            hyperParams.spaceSize = 27;
            hyperParams.generations = 100;
            cellType = CellType.STEP_VALUED;
            hyperParams.states = 2;
            cellSpace = new Cell[hyperParams.spaceSize, hyperParams.spaceSize];
            lastSpaceState = new double?[hyperParams.spaceSize, hyperParams.spaceSize];
            InitialConditionSetup();
        }

        protected override void InitialConditionSetup()
        {
            for (int i = 0; i < hyperParams.spaceSize; i++)
            {
                for (int j = 0; j < hyperParams.spaceSize; j++)
                {
                    cellSpace[i, j] = new Cell(0, hyperParams.states);
                }
            }

            GameOfLifeSeeds.SpawnCrossOscillator(12, 12, cellSpace, Orientation.VERTICAL);
            GameOfLifeSeeds.SpawnCrossOscillator(16, 16, cellSpace, Orientation.HORISONTAL);
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
            double? nextState;
            Cell cell;
            for (int i = 1; i < hyperParams.spaceSize - 1; i++)
            {
                for (int j = 1; j < hyperParams.spaceSize - 1; j++)
                {
                    cell = cellSpace[i, j];
                    nextState = ruleCheck(getOKNeighbourhood(i, j), FetchCellState(cell));
                    lastSpaceState[i, j] = cell.State;
                    cell.State = nextState;
                }
            }
            //the four sides
            for (int i = 1; i < hyperParams.spaceSize - 1; i++)
            {
                //upper row
                cell = cellSpace[0, i];
                nextState = ruleCheck(getUpperNeighbourhood(i), FetchCellState(cell));
                lastSpaceState[0, i] = cell.State;
                cell.State = nextState;
                //lower row
                cell = cellSpace[hyperParams.spaceSize - 1, i];
                nextState = ruleCheck(getUpperNeighbourhood(i), FetchCellState(cell));
                lastSpaceState[hyperParams.spaceSize - 1, i] = cell.State;
                cell.State = nextState;
                //left handside
                cell = cellSpace[i, 0];
                nextState = ruleCheck(getUpperNeighbourhood(i), FetchCellState(cell));
                lastSpaceState[i, 0] = cell.State;
                cell.State = nextState;
                //right handside
                cell = cellSpace[i, hyperParams.spaceSize - 1];
                nextState = ruleCheck(getUpperNeighbourhood(i), FetchCellState(cell));
                lastSpaceState[i, hyperParams.spaceSize - 1] = cell.State;
                cell.State = nextState;
            }
            //the four corners
            cell = cellSpace[0, 0];
            nextState = ruleCheck(getUpperLeftNeighbourhood(), FetchCellState(cell));
            lastSpaceState[0, 0] = cell.State;
            cell.State = nextState;

            cell = cellSpace[hyperParams.spaceSize - 1, 0];
            nextState = ruleCheck(getLowerLeftNeighbourhood(), FetchCellState(cell));
            lastSpaceState[hyperParams.spaceSize - 1, 0] = nextState;
            cell.State = nextState;

            cell = cellSpace[0, hyperParams.spaceSize - 1];
            nextState = ruleCheck(getUpperRightNeighbourhood(), FetchCellState(cell));
            lastSpaceState[hyperParams.spaceSize - 1, 0] = cell.State;
            cell.State = nextState;

            cell = cellSpace[hyperParams.spaceSize - 1, hyperParams.spaceSize - 1];
            nextState = ruleCheck(getLowerRightNeighbourhood(), FetchCellState(cell));
            lastSpaceState[hyperParams.spaceSize - 1, hyperParams.spaceSize - 1] = cell.State;
            cell.State = nextState;

            firstGeneration = false;
        }

        private double? FetchCellState(Cell cell)
        {
            return firstGeneration ? cell.State : cell.OldState;
        }

        private List<double?> getOKNeighbourhood(int i, int j)
        {
            List<double?> neighbourhoodState = new List<double?>();
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
        private List<double?> getUpperNeighbourhood(int i)
        {
            List<double?> neighbourhoodState = new List<double?>();

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
        private List<double?> getLowerNeighbourhood(int i)
        {
            List<double?> neighbourhoodState = new List<double?>();

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
        private List<double?> getRightNeighbourhood(int i)
        {
            List<double?> neighbourhoodState = new List<double?>();

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
        private List<double?> getLeftNeighbourhood(int i)
        {
            List<double?> neighbourhoodState = new List<double?>();

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

        private List<double?> getUpperLeftNeighbourhood()
        {
            List<double?> neighbourhoodState = new List<double?>();

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

        private List<double?> getLowerLeftNeighbourhood()
        {
            List<double?> neighbourhoodState = new List<double?>();

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

        private List<double?> getUpperRightNeighbourhood()
        {
            List<double?> neighbourhoodState = new List<double?>();

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

        private List<double?> getLowerRightNeighbourhood()
        {
            List<double?> neighbourhoodState = new List<double?>();

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

        private double? ruleCheck(List<double?> neighbourhoodState, double? centreState)
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
        private char StateToString(double? state)
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