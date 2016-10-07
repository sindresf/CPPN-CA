using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CACPPN.CA.TypeEnums;

namespace CACPPN.CA.Interfaces
{
    interface IStoppingConditions
    {
        //TODO this is actally plural?

        // with all stopping conditions such as "problem solved", (invloves the check for the solution
        // times tried (giving up condition)
        // some other restraint on runtime

        //This will be called through the fitness evaluation as the "succeeded stopping condition"

        //Something like this

        ISituation GetSuccessStoppingCondition();

        int GetGiveUpStoppingCondition();

        int GetTimeForCheckingStoppingCondition(); //The "20 steps and you should be at "successStoppingCondition" and such

        void ChangeCountBasedStoppingCondition(StoppingConditionType whichCondition, int amount);
    }

}
