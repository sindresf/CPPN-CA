using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CACPPN.CA.Interfaces
{
    interface ITimeVariable
    {
        //This weirdo!
        //jesus
        // just have it log the timestep it takes?
        // and then way down the line if the master is like Done in december
        // this could be usefull for trying async updates...?

        TimeSpan getTimeStep(); //return "0" in default (every case) mode
    }
}
