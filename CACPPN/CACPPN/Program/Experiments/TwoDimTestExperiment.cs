using System.Collections.Generic;
using CACPPN.CA.DerivedTypes.CellTypes;
using CACPPN.Utils.SeedingMachines;
using CACPPN.CA.Enums.Types;
using CACPPN.CA.Enums;
using CACPPN.Utils;

namespace CACPPN.Program.Experiments
{
    class TwoDimTestExperiment : Experiment
    {
        Cell[,] cellSpace;
        double?[,] lastSpaceState; //expanded into proper storage
        private readonly int highestIndex;
        public TwoDimTestExperiment()
        {
            hyperParams.spaceSize = 27;
            highestIndex = hyperParams.spaceSize - 1;
            hyperParams.generations = 100;
            cellType = CellType.STEP_VALUED;
            hyperParams.states = 2;
            hyperParams.neighbourhoodWidth = 2;

            cellSpace = new Cell[hyperParams.spaceSize, hyperParams.spaceSize];
            lastSpaceState = new double?[hyperParams.spaceSize, hyperParams.spaceSize];

            InitialConditionSetup();
        }

        protected override void InitialConditionSetup()
        {
            InitialiseCells();
            InitialiseNeighbourhoods();
            SeedTheCA();
        }

        private void InitialiseCells()
        {
            for (int i = 0; i < hyperParams.spaceSize; i++)
            {
                for (int j = 0; j < hyperParams.spaceSize; j++)
                {
                    cellSpace[i, j] = new Cell(0, hyperParams.states);
                }
            }
        }
        private void InitialiseNeighbourhoods()
        {
            //TODO need a "safe distance calculator" for 2D neighbourhood widths
            //"inside the space" cells
            for (int i = 1; i < highestIndex; i++)
            {
                for (int j = 1; j < highestIndex; j++)
                {
                    cellSpace[i, j].Neighbourhood = NeighbourhoodConstructor.getOKNeighbourhood(cellSpace, i, j, hyperParams.neighbourhoodWidth);
                }
            }
            //the four sides
            for (int i = 1; i < highestIndex; i++)
            {
                cellSpace[0, i].Neighbourhood = NeighbourhoodConstructor.getUpperNeighbourhood(cellSpace, i, hyperParams);
                cellSpace[i, 0].Neighbourhood = NeighbourhoodConstructor.getLeftNeighbourhood(cellSpace, i, hyperParams);
                cellSpace[highestIndex, i].Neighbourhood = NeighbourhoodConstructor.getLowerNeighbourhood(cellSpace, i, hyperParams);
                cellSpace[i, highestIndex].Neighbourhood = NeighbourhoodConstructor.getRightNeighbourhood(cellSpace, i, hyperParams);
            }
            //the four corners
            cellSpace[0, 0].Neighbourhood = NeighbourhoodConstructor.getUpperLeftNeighbourhood(cellSpace, hyperParams);
            cellSpace[0, highestIndex].Neighbourhood = NeighbourhoodConstructor.getUpperRightNeighbourhood(cellSpace, hyperParams);
            cellSpace[highestIndex, 0].Neighbourhood = NeighbourhoodConstructor.getLowerLeftNeighbourhood(cellSpace, hyperParams);
            cellSpace[highestIndex, highestIndex].Neighbourhood = NeighbourhoodConstructor.getLowerRightNeighbourhood(cellSpace, hyperParams);
        }

        private void SeedTheCA()
        {
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
            for (int i = 1; i < highestIndex; i++)
            {
                for (int j = 1; j < highestIndex; j++)
                {
                    cell = cellSpace[i, j];
                    nextState = ruleCheck(null, FetchCellState(cell));
                    lastSpaceState[i, j] = cell.State;
                    cell.State = nextState;
                }
            }
            //the four sides
            for (int i = 1; i < highestIndex; i++)
            {
                //upper row
                cell = cellSpace[0, i];
                nextState = ruleCheck(getUpperNeighbourhood(i), FetchCellState(cell));
                lastSpaceState[0, i] = cell.State;
                cell.State = nextState;
                //lower row
                cell = cellSpace[highestIndex, i];
                nextState = ruleCheck(getUpperNeighbourhood(i), FetchCellState(cell));
                lastSpaceState[highestIndex, i] = cell.State;
                cell.State = nextState;
                //left handside
                cell = cellSpace[i, 0];
                nextState = ruleCheck(getUpperNeighbourhood(i), FetchCellState(cell));
                lastSpaceState[i, 0] = cell.State;
                cell.State = nextState;
                //right handside
                cell = cellSpace[i, highestIndex];
                nextState = ruleCheck(getUpperNeighbourhood(i), FetchCellState(cell));
                lastSpaceState[i, highestIndex] = cell.State;
                cell.State = nextState;
            }
            //the four corners
            cell = cellSpace[0, 0];
            nextState = ruleCheck(getUpperLeftNeighbourhood(), FetchCellState(cell));
            lastSpaceState[0, 0] = cell.State;
            cell.State = nextState;

            cell = cellSpace[highestIndex, 0];
            nextState = ruleCheck(getLowerLeftNeighbourhood(), FetchCellState(cell));
            lastSpaceState[highestIndex, 0] = nextState;
            cell.State = nextState;

            cell = cellSpace[0, highestIndex];
            nextState = ruleCheck(getUpperRightNeighbourhood(), FetchCellState(cell));
            lastSpaceState[highestIndex, 0] = cell.State;
            cell.State = nextState;

            cell = cellSpace[highestIndex, highestIndex];
            nextState = ruleCheck(getLowerRightNeighbourhood(), FetchCellState(cell));
            lastSpaceState[highestIndex, highestIndex] = cell.State;
            cell.State = nextState;

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