using System;
using CPPNNEATCA.EA;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.CPPN.Parts
{
	abstract class ActivationFunction
	{
		protected Coefficients coefficients;

		public ActivationFunction()
		{
			coefficients = new Coefficients();
		}

		public virtual float GetOutput(TupleList<float, float> inputs)
		{
			throw new NotImplementedException("base case is not an actual function!");
		}

		public virtual void MutateCoefficient()
		{
			Neat.random.Coefficient(coefficients).Mutate();
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
			case ActivationFunctionType.Sensor:
				return null;
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
		Linear,
		Sensor
	}

	class SinusFunction : ActivationFunction
	{
		public SinusFunction() : base()
		{ //entirely up to user to keep track off and make no "divide by 0" stuff occure
			coefficients.Add('a', new Coefficient(1.0, 0.1, 0.0, 3.0)); //also, this would be a long ass list of parameters to tune
			coefficients.Add('b', new Coefficient(1.0, 0.1, -3.0, 3.0));
			coefficients.Add('c', new Coefficient(1.0, 0.1, 0.01, 3.0));
			coefficients.Add('d', new Coefficient(1.0, 0.1, 0.5, 1.7));
		}

		public override float GetOutput(TupleList<float, float> inputs)
		{
			float sum = SumWeightedInputs(inputs);
			double a = coefficients['a'].coValue;
			double b = coefficients['b'].coValue;
			double c = coefficients['c'].coValue;
			double d = coefficients['d'].coValue;
			return (float)(a * Math.Sin(Math.Pow(c * sum, d)) + b);
		}
	}

	class GaussianFunction : ActivationFunction
	{
		public GaussianFunction() : base()
		{
			coefficients.Add('m', new Coefficient(.0, .1, -3.0, 3.0));
			coefficients.Add('v', new Coefficient(1.0, .05, -.7, 2.0));
			coefficients.Add('x', new Coefficient(2.0, .02, 1.01, 2.5));
			coefficients.Add('2', new Coefficient(2.0, .01, 1.9, 2.1));
		}

		public override float GetOutput(TupleList<float, float> inputs)
		{
			float sum = SumWeightedInputs(inputs);

			double mean = coefficients['m'].coValue;
			double variance = coefficients['v'].coValue;
			double power = coefficients['x'].coValue;
			double two = coefficients['2'].coValue;

			double exponent = - (Math.Pow((sum - mean),power) / (two*variance));
			double divide = 1.0 / Math.Sqrt(two*Math.PI*variance);

			return (float)(divide * Math.Pow(Math.E, exponent));
		}
	}

	class AbsoluteValueFunction : ActivationFunction
	{
		public AbsoluteValueFunction() : base()
		{

		}

		public override float GetOutput(TupleList<float, float> inputs)
		{
			float sum = SumWeightedInputs(inputs);
			float x = 1.0f;
			float y = .0f;
			return Math.Abs(sum * x) + y;
		}
	}

	class PyramidAbsoluteValueFunction : ActivationFunction
	{
		public PyramidAbsoluteValueFunction() : base()
		{

		}

		public override float GetOutput(TupleList<float, float> inputs)
		{
			float sum = SumWeightedInputs(inputs);
			float x = 1.0f;
			float y = .0f;
			float z = 1.0f;
			float w = 1.0f;
			return Math.Abs((w - (sum % z)) * x) + y;
		}
	}

	class ModuloFunction : ActivationFunction
	{
		public ModuloFunction() : base()
		{

		}

		public override float GetOutput(TupleList<float, float> inputs)
		{
			float sum = SumWeightedInputs(inputs);
			float x = 1.0f;
			float y = .0f;
			float mod = 1.0f;
			return (x * sum + y) % mod;
		}
	}

	class LinearFunction : ActivationFunction
	{
		public LinearFunction() : base()
		{
			coefficients.Add('a', new Coefficient(1.0, 0.1, -3.0, 3.0));
			coefficients.Add('b', new Coefficient(0.0, 0.1, -3.0, 3.0));
		}

		public override float GetOutput(TupleList<float, float> inputs)
		{
			double a = coefficients['a'].coValue;
			double b = coefficients['b'].coValue;
			return (float)(SumWeightedInputs(inputs) * a + b);
		}
	}

	class OtherFunction : ActivationFunction
	{
		public OtherFunction() : base()
		{
			coefficients.Add('x', new Coefficient(1.0, 0.0, 0.0, 1.0));
		}

		public override float GetOutput(TupleList<float, float> inputs)
		{
			float sum = SumWeightedInputs(inputs);
			double x = coefficients['x'].coValue;

			return (float)Math.Sqrt(sum * x);
		}
	}
}