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
            InitializeCells();
            InitializeNeighbourhoods();
            SeedTheCA();
        }

        private void InitializeCells()
        {
            for (int i = 0; i < hyperParams.spaceSize; i++)
            {
                for (int j = 0; j < hyperParams.spaceSize; j++)
                {
                    cellSpace[i, j] = new Cell(0, i, j, hyperParams.states);
                }
            }
        }
        private void InitializeNeighbourhoods()
        {
            InitializeSafeCellNeighbourhoods();
            InitializeOutlierCellNeighbourhoods();
            InitializeCornerCellNeighbourhoods();
        }
        private void InitializeSafeCellNeighbourhoods()
        {
            //TODO need a "safe distance calculator" for 2D neighbourhood widths
            for (int i = 1; i < highestIndex; i++)
            {
                for (int j = 1; j < highestIndex; j++)
                {
                    cellSpace[i, j].Neighbourhood = NeighbourhoodConstructor.getOKNeighbourhood(cellSpace, i, j, hyperParams.neighbourhoodWidth);
                }
            }
        }
        private void InitializeOutlierCellNeighbourhoods()
        {
            for (int i = 1; i < highestIndex; i++)
            {
                cellSpace[0, i].Neighbourhood = NeighbourhoodConstructor.getUpperNeighbourhood(cellSpace, i, hyperParams);
                cellSpace[i, 0].Neighbourhood = NeighbourhoodConstructor.getLeftNeighbourhood(cellSpace, i, hyperParams);
                cellSpace[highestIndex, i].Neighbourhood = NeighbourhoodConstructor.getLowerNeighbourhood(cellSpace, i, hyperParams);
                cellSpace[i, highestIndex].Neighbourhood = NeighbourhoodConstructor.getRightNeighbourhood(cellSpace, i, hyperParams);
            }
        }
        private void InitializeCornerCellNeighbourhoods()
        {
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

            foreach (Cell cell in cellSpace)
            {
                nextState = ruleCheck(cell.NeighbourhoodState, cell.OldState);
                lastSpaceState[cell.i, cell.j] = cell.OldState;
                cell.State = nextState;
            }
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