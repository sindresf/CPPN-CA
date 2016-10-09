using CACPPN.CA.Utils;
using CACPPN.CA.TypeEnums;
using CACPPN.CA.DerivedTypes.CellTypes;
using System;

namespace CACPPN.Program.Experiments
{

    abstract class Experiment
    {
        public Hyperparameters hyperParams;
        public CellType cellType = CellType.BOOLEAN;
        public CellularSpaceType spaceType = CellularSpaceType.ONE_DIMENSIONAL;
        public NeighbourhoodType neighbourhoodType = NeighbourhoodType.DETERMINISTIC_WIDTH;
        public BoundaryConditionType boundaryConditionType = BoundaryConditionType.PERIODIC;
        public TimeStepType timeStepType = TimeStepType.UNIFORM;
        public TranstionRuleSetRepresentationType ruleRepresentationType = TranstionRuleSetRepresentationType.LOOKUP_TABLE;

        public Experiment()
        {
            //TODO to hold all the defaults, with experiments only overriding what makes them "special"
            hyperParams = new Hyperparameters();
            hyperParams.generations = 20;
            hyperParams.spaceSize = 25;
            hyperParams.states = 2;
            hyperParams.neighbourhoodWidth = 1;
            hyperParams.acceptableFitness = 1;
        }

        protected abstract void InitialConditionSetup();

        public abstract void NextState();

        public abstract bool IsSuccessState(); //for "not looking for a specific space state" this just returns false

        public abstract string SpaceStateToString();
    }

    class BasicExperiment : Experiment
    {
        private BooleanCell[] cellSpace;

        public BasicExperiment() : base()
        {
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
        }

        public override bool IsSuccessState()
        {
            //an "all is true" state
            foreach (BooleanCell cell in cellSpace)
            {
                if (cell.State == 0)
                    return false;
            }
            return true;
        }

        public override void NextState()
        {
            throw new NotImplementedException();
        }

        public override string SpaceStateToString()
        {
            return "this is the state now: ";
        }
    }
}
