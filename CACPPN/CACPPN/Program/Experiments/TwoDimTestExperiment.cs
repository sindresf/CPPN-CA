using System.Collections.Generic;
using System.Threading.Tasks;
using CACPPN.CA.Enums.Types;
using CACPPN.CA.DerivedTypes.CellTypes;
using CACPPN.Utils;
using CACPPN.Utils.SeedingMachines;

namespace CACPPN.Program.Experiments
{
    class TwoDimTestExperiment : Experiment
    {
        Cell[,] cellSpace;
        double?[,] futureStates; //expanded into proper storage
        List<double[,]> loggedSpaceStates; //expanded into proper storage

        public TwoDimTestExperiment()
        {
            hyperParams.spaceSize = 27;
            hyperParams.generations = 100;
            cellType = CellType.STEP_VALUED;
            hyperParams.states = 2;
            hyperParams.neighbourhoodWidth = 2;
            hyperParams.timeStep = 140;

            cellSpace = new Cell[hyperParams.spaceSize, hyperParams.spaceSize];
            futureStates = new double?[hyperParams.spaceSize, hyperParams.spaceSize];
            loggedSpaceStates = new List<double[,]>();

            InitialConditionSetup();
            AddCellSpaceToLog();
        }

        private void AddCellSpaceToLog()
        {
            double[,] logSpace = new double[hyperParams.spaceSize, hyperParams.spaceSize];
            Parallel.ForEach(cellSpace.ToEnumerable<Cell>(), cell =>
            {
                logSpace[cell.i, cell.j] = cell.CurrentState;
            });
            loggedSpaceStates.Add(logSpace);
        }

        protected override void InitialConditionSetup()
        {
            InitializeCells();
            SeedTheCA();
            InitializeCellNeighbourhoods();
        }

        protected override void InitializeCells()
        {
            Parallel.For(0, hyperParams.spaceSize, (int i) =>
             {
                 Parallel.For(0, hyperParams.spaceSize, (int j) =>
                 {
                     cellSpace[i, j] = new Cell(i, j, hyperParams.states).SetFirstState(0) as Cell;
                 });
             });
        }

        protected override void InitializeCellNeighbourhoods()
        {
            NeighbourhoodInitializor.InitializeNeighbourhoods2D(cellSpace, hyperParams);
        }

        protected override void SeedTheCA()
        {
            //GameOfLifeSeeds.SpawnCrossOscillator(6, 6, cellSpace, Orientation.VERTICAL);
            //GameOfLifeSeeds.SpawnCrossOscillator(20, 9, cellSpace, Orientation.HORISONTAL);
            // GameOfLifeSeeds.SpawnRPentomino(15, 15, cellSpace, Orientation.VERTICAL);
            //GameOfLifeSeeds.SpawnRandoms(cellSpace, hyperParams.spaceSize, 20);
            GameOfLifeSeeds.RandomFillSpace(cellSpace, 0.5);
        }

        public override void NextState()
        {
            Parallel.ForEach(cellSpace.ToEnumerable<Cell>(), cell =>
            {
                futureStates[cell.i, cell.j] = ruleCheck(cell.NeighbourhoodCurrentState, cell.CurrentState);
            });
            StepSpaceIntoTheFuture();
            AddCellSpaceToLog();
        }

        private void StepSpaceIntoTheFuture()
        {
            Parallel.ForEach(cellSpace.ToEnumerable<Cell>(), cell =>
            {
                cell.FutureState = futureStates[cell.i, cell.j];
            });
        }

        private double? ruleCheck(List<double> neighbourhoodState, double? centreState)
        {
            double total = 0;
            Parallel.ForEach(neighbourhoodState, val =>
            {
                total += val;
            });
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

        public override bool IsSuccessState()
        {
            for (int i = 0; i < hyperParams.spaceSize; i++)
            {
                for (int j = 0; j < hyperParams.spaceSize; j++)
                {
                    if (cellSpace[i, j].CurrentState != loggedSpaceStates[loggedSpaceStates.Count - 2][i, j])
                        return false;
                }
            }
            return true;
        }
        public override string SpaceStateToString()
        {
            string space = "";
            for (int i = 0; i < hyperParams.spaceSize; i++)
            {
                space += "|";
                for (int j = 0; j < hyperParams.spaceSize; j++)
                {
                    space += "" + StateToString(cellSpace[i, j].CurrentState) + ".";
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