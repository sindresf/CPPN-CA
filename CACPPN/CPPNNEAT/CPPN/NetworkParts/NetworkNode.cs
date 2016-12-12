using System;
using System.Collections.Generic;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.CPPN.Parts
{
	class NetworkNode : ActivationFunction
	{
		public int nodeID { get; set; }
		private Dictionary<int,Tuple<float,float>> inValues;
		private Dictionary<int,NetworkNode> outConnections;

		public NetworkNode(int nodeID)
		{
			inValues = new Dictionary<int, Tuple<float, float>>();
			outConnections = new Dictionary<int, NetworkNode>();
		}

		public void AddOutConnection(NetworkNode outConnection)
		{
			outConnections.Add(outConnection.nodeID, outConnection);
		}

		public void AddInputConnection(int nodeID, float value, float weight)
		{
			inValues.Add(nodeID, Tuple.Create(value, weight));
		}

		public int GetInputConnectionCount()
		{
			return inValues.Count;
		}

		public float 
	}
}