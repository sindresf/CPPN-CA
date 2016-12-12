using System;
using System.Collections.Generic;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.CPPN.Parts
{
	class NetworkNode
	{
		public int nodeID { get; set; }
		private Dictionary<float, float> inValues;
		private List<NetworkNode> outConnections;
		private ActivationFunction activationFunction;

		public NetworkNode(int nodeID, ActivationFunction function)
		{
			activationFunction = function;
			inValues = new Dictionary<float, float>();
			outConnections = new List<NetworkNode>();
		}

		public void AddOutConnection(NetworkNode outConnection)
		{
			outConnections.Add(outConnection);
		}

		public void AddInputConnection(int nodeID, float value, float weight) //this needs working
		{
			inValues[weight] = value;
		}

		public void PropagateOutput()
		{
			var nodeInput = new TupleList<float,float>();
			foreach(KeyValuePair<float, float> pair in inValues)
				nodeInput.Add(Tuple.Create(pair.Key, pair.Value));

			float nodeOutput = activationFunction.GetOutput(nodeInput);
			foreach(NetworkNode node in outConnections)
				node.AddInputConnection(nodeID, nodeOutput, -1);
		}

	}
}