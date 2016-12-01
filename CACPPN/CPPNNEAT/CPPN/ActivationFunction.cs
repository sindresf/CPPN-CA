using System;
using CPPNNEAT.Utils;

namespace CPPNNEAT.CPPN
{
	class ActivationFunction
	{
		public virtual float GetOutput(TupleList<float, float> inputs)
		{
			throw new NotImplementedException("base case is not an actual function!");
		}

		protected float SumWeightedInputs(TupleList<float, float> inputs)
		{
			float sum = 0.0f;
			foreach(Tuple<float, float> input in inputs)
				sum += input.Item1 * input.Item2;
			return sum;
		}

		public static ActivationFunction GetRandomInitializedFunction(ActivationFunctionType type)
		{
			switch(type) //all these constructors is for when "everything" is done and I add function parameter mutation
			{
			case ActivationFunctionType.Sinusodial:
				return new SinusFunction();
			case ActivationFunctionType.Gaussian:
				return new GaussianFunction();
			case ActivationFunctionType.AbsoluteValue:
				return new AbsoluteValueFunction();
			case ActivationFunctionType.PyramidAbsoluteValue:
				return new PyramidAbsoluteValueFunction();
			case ActivationFunctionType.Modulo:
				return new ModuloFunction();
			case ActivationFunctionType.Linear:
				return new LinearFunction();
			default:
				return null;
			}
		}
	}

	enum ActivationFunctionType
	{
		Sinusodial,
		Gaussian,
		AbsoluteValue,
		PyramidAbsoluteValue,
		Modulo,
		Linear
	}

	class SinusFunction : ActivationFunction
	{
		public SinusFunction() { }

		public override float GetOutput(TupleList<float, float> inputs)
		{
			float sum = SumWeightedInputs(inputs);
			return (float)Math.Sin(sum);
		}
	}

	class GaussianFunction : ActivationFunction
	{
		private float mean = 0.0f;
		private float variance = 1.0f;
		public GaussianFunction() { }

		public override float GetOutput(TupleList<float, float> inputs)
		{
			float sum = SumWeightedInputs(inputs);
			double exponent = - (Math.Pow((sum - mean),2) / (2*variance));
			double divide = 1.0 / Math.Sqrt(2*Math.PI*variance);

			return (float)(divide * Math.Pow(Math.E, exponent)); //parameterized point here
		}
	}
	class AbsoluteValueFunction : ActivationFunction
	{
		public AbsoluteValueFunction() { }

		public override float GetOutput(TupleList<float, float> inputs)
		{
			float sum = SumWeightedInputs(inputs);
			return Math.Abs(sum);
		}
	}

	class PyramidAbsoluteValueFunction : ActivationFunction
	{
		public PyramidAbsoluteValueFunction() { }

		public override float GetOutput(TupleList<float, float> inputs)
		{
			float sum = SumWeightedInputs(inputs);
			return Math.Abs(1.0f - (sum % 1.0f));
		}
	}

	class ModuloFunction : ActivationFunction
	{
		public ModuloFunction() { }

		public override float GetOutput(TupleList<float, float> inputs)
		{
			float sum = SumWeightedInputs(inputs);
			return sum % 1.0f;
		}
	}

	class LinearFunction : ActivationFunction
	{
		public LinearFunction() { }

		public override float GetOutput(TupleList<float, float> inputs)
		{
			return SumWeightedInputs(inputs);
		}
	}

	class OtherFunction : ActivationFunction
	{
		public OtherFunction() { }

		public override float GetOutput(TupleList<float, float> inputs)
		{
			float sum = SumWeightedInputs(inputs);

			return (float)Math.Sqrt(sum);
		}
	}
}