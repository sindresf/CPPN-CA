using System.Collections.Generic;
using CPPNNEAT.EA;

namespace CPPNNEAT.Utils
{
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
			coValue = Neat.random.NextGaussianRandom(coValue, mutateRate).Clamp(minValue, maxValue);
		}
	}

	class Coefficients : Dictionary<char, Coefficient>
	{
		public Coefficients() : base()
		{

		}
	}
}
