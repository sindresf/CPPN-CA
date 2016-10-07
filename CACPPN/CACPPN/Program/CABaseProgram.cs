using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CACPPN.CA.TypeEnums;
using CACPPN.CA.Interfaces;

namespace CACPPN.Program
{
    class CABaseProgram //Remember, the BASE CA is not evolutionary design, but ALL hardcoded
    {
        /*The while loop running the evolution and evaluation.
		  Stores the internal per-run statistics, or passes it up through a 
		  statistics object for the wrapping statisticsProgram to work with
		*/
        static void Main(string[] args)
        {
            //This is just the main while under development of basic CA components
            //with no logging or statistics beyond console checking and results.
            //Seudo code
            //SETUP = Experiment(type.basic)
            //for(up till giveup count)
            //unless success condition met along the way, if have it
            //save last space state
            // update every cell's last state to new state

            Experiment setup = Experiments.GetExperiment(ExperimentNames.BASIC);
            int maxGenerations = setup.StoppingCondition.GetGiveUpStoppingCondition();
            ISituation goalSpaceSituation = setup.StoppingCondition.GetSuccessStoppingCondition();

            for (int generation = 0; generation < maxGenerations; generation++)
            {

            }
        }
    }
}
