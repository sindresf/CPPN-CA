using System.Collections.Generic;
using CPPNNEATCA.NEAT;

namespace CPPNNEATCA.Utils
{
	class Coefficients : Dictionary<char, Coefficient>
	{
		public Coefficients() : base() { }
	}

	class Coefficient
	{
		public double coValue { get; private set; }
		private double mutateRate, minValue, maxValue;

		public Coefficient(double coefficientStart, double mutateRate, double minValue, double maxValue)
		{
			coValue = coefficientStart;
			this.mutateRate = mutateRate;
			this.minValue = minValue;
			this.maxValue = maxValue;
		}

		public void Mutate()
		{
			coValue = Neat.random.NextRangedDouble(coValue, mutateRate).Clamp(minValue, maxValue);
		}
	}
}