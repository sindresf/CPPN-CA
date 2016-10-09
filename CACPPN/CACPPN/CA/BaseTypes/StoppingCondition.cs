using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CACPPN.CA.Interfaces;

namespace CACPPN.CA.BaseTypes
{
    class StoppingConditions
    {
        protected int GiveUpCount, CheckingTimeCount;

        protected ISituation SuccessState { get; set; }

        public StoppingConditions(int giveUp, int checkingTime, ISituation SuccessState)
        {
            GiveUpCount = giveUp;
            CheckingTimeCount = checkingTime;
            this.SuccessState = SuccessState;
        }

        public int GetGiveUpStoppingCondition()
        {
            return GiveUpCount;
        }

        public ISituation GetSuccessStoppingCondition()
        {
            return SuccessState;
        }

        public int GetTimeForCheckingStoppingCondition()
        {
            return CheckingTimeCount;
        }
    }
}
