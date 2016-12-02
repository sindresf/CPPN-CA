namespace CACPPN.CA.Enums.Types
{
	enum CellType
	{
		BOOLEAN,
		STEP_VALUED
	}

	enum CellularSpaceType
	{
		ONE_DIMENSIONAL,
		TWO_DIMENSIONAL
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
		CPPN,
		UNIFORM,
		NONE_UNIFORM
	}
}