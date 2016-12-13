using System;
using System.Collections.Generic;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.CPPN.Parts
{

	interface INetworkNode
	{
		void SetupDone();
		void Notify(int inputNodeID, float value);
		bool IsFullyNotified();
		void AddInputConnection(int inputNodeID, float inputWeight);
	}


	class InputNetworkNode
	{
		public readonly int nodeID;

		protected List<INetworkNode> outConnections;

		public InputNetworkNode(int nodeID)
		{
			this.nodeID = nodeID;
			outConnections = new List<INetworkNode>();
		}

		public void AddOutConnection(INetworkNode outConnection)
		{
			outConnections.Add(outConnection);
		}

		public void PropagateOutput(float input)
		{
			foreach(InternalNetworkNode node in outConnections)
				node.Notify(nodeID, input);
		}
	}

	class InternalNetworkNode : InputNetworkNode, INetworkNode
	{
		private Dictionary<int, float> inValues, inWeights;
		private ActivationFunction activationFunction;
		private int shouldHave;

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

		public void PropagateOutput()
		{
			var nodeInput = new TupleList<float,float>();
			foreach(int inputNodeID in inValues.Keys)
				nodeInput.Add(Tuple.Create(inValues[inputNodeID], inWeights[inputNodeID]));

			float nodeOutput = activationFunction.GetOutput(nodeInput);
			foreach(InternalNetworkNode node in outConnections)
				node.Notify(nodeID, nodeOutput);
		}

		public void SetupDone()
		{
			shouldHave = inWeights.Count;
		}

		public bool IsFullyNotified()
		{
			return shouldHave == inValues.Count;
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