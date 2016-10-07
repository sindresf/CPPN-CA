using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CACPPN.CA.Interfaces
{
    interface IStateSet
    {
        void setAsNoneUniformStateSet(); //should again make a call to "StateSetSize" stochastic
        // also forces the need for the Cell to be of a type with a statesetsize member

        int StateSetSize { get; set; }

        void ChangeStateSetSize(int byAmount); //should initiate a call to a 0-1 "splitt by statesetsize" function (util func?)
    }
}
