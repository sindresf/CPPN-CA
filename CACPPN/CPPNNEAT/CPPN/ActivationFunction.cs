using System;
using System.Collections.Generic;

namespace CPPNNEAT.CPPN
{
	class ActivationFunction
	{
		// all CCPN really is is a collection of functions instead of
		// a single activation function

		// get randomly initialized activation function based on type function

		public virtual float GetOutput(List<float> input) //delegate or inherited function
		{
			return 0.0f;
		}

		public static ActivationFunction GetRandomInitializedFunction(ActivationFunctionType type)
		{
			switch(type)
			{
			case ActivationFunctionType.Sinusodial:
				return new SinusFunction();
			case ActivationFunctionType.thisAndThat:
				return null;
			case ActivationFunctionType.andOfSuch:
				return null;
			default:
				return null;
			}
		}
	}

	class SinusFunction : ActivationFunction
	{
		public SinusFunction() { }

		public override float GetOutput(List<float> input)
		{
			float sum = 0.0f;
			foreach(float inp in input)
				sum += inp;

			return (float)Math.Sin(sum); //parameterized point here
		}
	}

	class OtherFunction : ActivationFunction
	{
		public OtherFunction() { }

		public override float GetOutput(List<float> input)
		{
			float sum = 0.0f;
			foreach(float inp in input)
				sum *= inp + 2.3f;

			return (float)Math.Sqrt(sum); //parameterized point here
		}
	}

	enum ActivationFunctionType
	{
		Sinusodial,
		thisAndThat,
		andOfSuch
	}
}