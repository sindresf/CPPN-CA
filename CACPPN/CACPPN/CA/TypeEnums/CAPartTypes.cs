using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CACPPN.CA.TypeEnums
{
    enum CellType
    {
        BOOLEAN,
        STEP_VALUED,
        SEMI_CONTINUOUS, //like, double step = 0.001 = 1000 states
        CONTINUOUS // this one only for Actual continuous state space
    }

    enum CellularSpaceTypes
    {
        ONE_DIMENSIONAL,
        TWO_DIMENSIONAL,
        THREE_DIMENSIONAL,
        OPEN_WORLD,
        SQUARE_GRID,
        HEXAGONAL // and such "whatevers"
    }

    enum NeighbourhoodTypes
    {
        VAN_NEUMANN, //TODO find out what the base ones were called
        WOLFRAM,
        RADIOUS_DEFINED,
        STOCHASTIC_CONNECTION,
        UNIFORM_COUNT,
        STOCHASTIC_COUNT
    }

    enum BoundaryConditionType
    {
        PERIODIC,
        ASSIGNED,
        COPYING,
        REFLECTING,
        ABSORBING
    }

    enum StoppingConditionType
    {
        SUCCESS,
        GIVE_UP,
        CHECKING_TIME,
        PROBLEM
    }

    enum TimeStepType
    {
        UNIFORM,
        NONE_UNIFORM,
        PER_CELL_CONTINUOUS
    }
    enum TranstionRuleSetRepresentationType
    {
        LOOKUP_TABLE,
        RULES,
        CPPN,
        UNIFORM,
        NONE_UNIFORM
    }
}
