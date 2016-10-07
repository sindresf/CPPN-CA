using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CACPPN.CA.Interfaces
{
    interface ISituation
    {
        //The general description of the state space, if such a thing is explicitely a thing

        ICellularSpace Situation { get; set; } //something like so?
    }
}
