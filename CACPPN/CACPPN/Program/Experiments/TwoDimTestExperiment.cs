using System.Collections.Generic;
using CACPPN.CA.DerivedTypes.CellTypes;
using CACPPN.CA.Enums;
using CACPPN.CA.Enums.Types;
using CACPPN.Utils;
using CACPPN.Utils.SeedingMachines;

namespace CACPPN.Program.Experiments
{
    class TwoDimTestExperiment : Experiment
    {
        Cell[,] cellSpace;
        double?[,] lastSpaceState; //expanded into proper storage

        public TwoDimTestExperiment()
        {
            hyperParams.spaceSize = 27;
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
            InitializeCellNeighbourhoods();
            SeedTheCA();
        }

        protected override void InitializeCells()
        {
            for (int i = 0; i < hyperParams.spaceSize; i++)
            {
                for (int j = 0; j < hyperParams.spaceSize; j++)
                {
                    cellSpace[i, j] = new Cell(0, i, j, hyperParams.states);
                }
            }
        }
        protected override void InitializeCellNeighbourhoods()
        {
            NeighbourhoodInitializor.InitializeNeighbourhoods2D(cellSpace, hyperParams);
        }

        protected override void SeedTheCA()
        {
            GameOfLifeSeeds.SpawnCrossOscillator(12, 12, cellSpace, Orientation.VERTICAL);
            GameOfLifeSeeds.SpawnCrossOscillator(14, 19, cellSpace, Orientation.HORISONTAL);
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
                nextState = ruleCheck(cell.NeighbourhoodOldState, cell.OldState);
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