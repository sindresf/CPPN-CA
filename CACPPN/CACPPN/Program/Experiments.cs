using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CACPPN.CA.Interfaces;
using CACPPN.CA.TypeEnums;

namespace CACPPN.Program
{
    class Experiments
    {

        //TODO so this has that delegation dictionary with the enum names as keys and 
        // experiment functions as values

        public static Experiment GetExperiment(ExperimentNames experimentName)
        {
            //TODO and looks up that dictionary here and returns the setup.
            return null;
        }
    }

    class Experiment
    {
        //TODO this should be a very easily switch delegate sort of thing
        // where you just "name the experiment" and they're all set up the same with a list of all
        // paramaters and versions of the same interfaces needed to run Any CA experiment

        //TODO use properties because some might be only set and only get and whatever

        //TODO figure out what the Experiment needs, and what can be Through the parts
        public IBoundaryCondition BoundaryCondition { get; set; }
        public ICell Cell { get; set; }
        public ICellularSpace CellularSpace { get; set; }
        public IInitialCondition Seed { get; set; }
        public INeighbourhood NeighbourHood { get; set; }
        public IStateSet StateSet { get; set; }
        public IStateTranslator StateTranslator { get; set; }
        public IStoppingConditions StoppingCondition { get; set; } //Condition S ?
        public ITimeVariable TimeVariable { get; set; } //Default should just be "gogogo" aka no time, just asap
        public ITransitioner Transitioner { get; set; }
        public ITransitionRuleSet TransitionRuleSet { get; set; }

        //Here goes setup functions for every experiment, or how delegation works
    }
}
