using System;
using System.Collections.Generic;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.CPPN.Parts
{
	class InternalNetworkNode : InputNetworkNode
	{
		private Dictionary<int, float> inValues, inWeights;
		private ActivationFunction activationFunction;

		public InternalNetworkNode(int nodeID, ActivationFunction function) : base(nodeID)
		{
			activationFunction = function;
			inValues = new Dictionary<int, float>();
			inWeights = new Dictionary<int, float>();
		}

		public void AddInputConnection(int inputNodeID, float inputWeight)
		{
			inWeights[inputNodeID] = inputWeight;
		}

		public void Notify(int inputNodeID, float value)
		{
			inValues[inputNodeID] = value;
		}

		public bool IsFullyNotified(int shouldHave)
		{
			return inValues.Count == shouldHave;
		}

		public void PropagateOutput()
		{
			var nodeInput = new TupleList<float,float>();
			foreach(int inputNodeID in inValues.Keys)
				nodeInput.Add(Tuple.Create(inValues[inputNodeID], inWeights[inputNodeID]));

			float nodeOutput = activationFunction.GetOutput(nodeInput);
			foreach(InternalNetworkNode node in outConnections)
				node.Notify(nodeID, nodeOutput);
		}
	}

	class InputNetworkNode
	{
		public readonly int nodeID;
		protected List<InternalNetworkNode> outConnections;

		public InputNetworkNode(int nodeID)
		{
			this.nodeID = nodeID;
			outConnections = new List<InternalNetworkNode>();
		}

		public void AddOutConnection(InternalNetworkNode outConnection)
		{
			outConnections.Add(outConnection);
		}

		public void PropagateOutput(float input)
		{
			foreach(InternalNetworkNode node in outConnections)
				node.Notify(nodeID, input);
		}
	}

	class OutputNetworkNode
	{
		public readonly int nodeID;
		public readonly float representedState;
		private Dictionary<int, float> inValues, inWeights;
		private ActivationFunction activationFunction;

		private int shouldHave;
		public OutputNetworkNode(int nodeID, float representedState, ActivationFunction function)
		{
			this.nodeID = nodeID;
			this.representedState = representedState;
			activationFunction = function;
			inValues = new Dictionary<int, float>();
			inWeights = new Dictionary<int, float>();
		}

		public void SetupDone()
		{
			shouldHave = inWeights.Count;
		}
		public void AddInputConnection(int inputNodeID, float inputWeight)
		{
			inWeights[inputNodeID] = inputWeight;
		}
		public void Notify(int inputNodeID, float value)
		{
			inValues[inputNodeID] = value;
		}
		public bool IsFullyNotified()
		{
			return shouldHave == inValues.Count;
		}
	}
}