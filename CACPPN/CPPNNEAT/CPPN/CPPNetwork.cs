using System.Collections.Generic;

namespace CPPNNEAT.CPPN
{
	class CPPNetwork
	{
		List<NetworkNode> nodes; // to store the activation functions
		float[][] connections;    // to store all the weights

		public CPPNetwork(int nodeCount)
		{
			nodes = new List<NetworkNode>();
			connections = new float[nodeCount][];
		}

		public float GetOutput(List<float> input)
		{
			return 0.0f;
		}
	}

	class NetworkNode
	{
		private readonly ActivationFunction function;

		public NetworkNode(ActivationFunction function)
		{
			this.function = function;
		}

		public float GetOutput(List<float> input)
		{
			return function.GetOutput(input);
		}
	}
}