namespace CACPPN.CA.TypeEnums
{
    enum CellType
    {
        BOOLEAN,
        STEP_VALUED,
        SEMI_CONTINUOUS, //like, double step = 0.001 = 1000 states
        CONTINUOUS // this one only for Actual continuous state space
    }

    enum CellularSpaceType
    {
        ONE_DIMENSIONAL,
        TWO_DIMENSIONAL,
        THREE_DIMENSIONAL,
        OPEN_WORLD,
        SQUARE_GRID,
        HEXAGONAL // and such "whatevers"
    }

    enum NeighbourhoodType
    {
        DETERMINISTIC_WIDTH,
        STOCHASTIC_WIDTH
    }

    enum BoundaryConditionType
    {
        PERIODIC,
        ASSIGNED,
        COPYING,
        REFLECTING,
        ABSORBING
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