using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CACPPN.CA.Interfaces
{
    interface ITransitionRuleSet
    {
        //TODO gotta figure out all the Basic connecting needs

        //this one is kinda weird though since it's where we go from lookup to CPPN

        // so this is where the "getNextState(neighbourhood)" goes? ? 

        //with a self contained private actual rule set?

        //or is this a second argument to the transitioner together with neighhourhood?
    }
}