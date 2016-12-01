using System.Collections.Generic;

namespace CACPPN.CA.BaseTypes
{
	abstract class AbstractCell
	{
		protected List<AbstractCell> _neighbourhood;

		public abstract List<AbstractCell> Neighbourhood { get; set; }

		protected double _currentState;
		protected double? _futureState;

		public readonly int i, j, z;

		public AbstractCell(int i, int j, int z)
		{
			this.i = i;
			this.j = j;
			this.z = z;
		}

		public AbstractCell(int i, int j)
		{
			this.i = i;
			this.j = j;
		}

		public AbstractCell(int i)
		{
			this.i = i;
		}

		public abstract double CurrentState { get; }
		public abstract double? FutureState { get; set; }

		public abstract List<double> NeighbourhoodCurrentState { get; }

		public abstract AbstractCell SetFirstState(double value);
	}
}
