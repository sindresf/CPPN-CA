using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CACPPN.CA.Interfaces;

namespace CACPPN.CA.BaseTypes
{
    class StoppingConditions : IStoppingConditions
    {
        protected int GiveUpCount, CheckingTimeCount;

        protected ISituation SuccessState { get; set; }

        public StoppingConditions(int giveUp, int checkingTime, ISituation SuccessState)
        {
            GiveUpCount = giveUp;
            CheckingTimeCount = checkingTime;
            this.SuccessState = SuccessState;
        }

        public void ChangeCountBasedStoppingCondition(StoppingConditionType whichCondition, int amount)
        {
            switch (whichCondition)
            {
                case StoppingConditionType.CHECKINGTIME:
                    CheckingTimeCount += amount;
                    break;
                case StoppingConditionType.GIVEUP:
                    GiveUpCount += amount;
                    break;
            }
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
