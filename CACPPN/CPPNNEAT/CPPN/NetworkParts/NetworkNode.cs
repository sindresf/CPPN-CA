using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using CPPNNEATCA.Utils;

namespace CPPNNEATCA.CPPN.Parts
{

	interface INetworkNode
	{
		void SetupDone();
		void Notify(int inputNodeID, float value);
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
			foreach(INetworkNode node in outConnections)
				node.Notify(nodeID, input);
		}
	}

	class InternalNetworkNode : InputNetworkNode, INetworkNode
	{
		protected ConcurrentDictionary<int, float> inValues, inWeights;
		protected ActivationFunction Function;
		protected int shouldHave;

		public InternalNetworkNode(int nodeID, ActivationFunction function) : base(nodeID)
		{
			Function = function;
			inValues = new ConcurrentDictionary<int, float>();
			inWeights = new ConcurrentDictionary<int, float>();
		}

		public virtual void AddInputConnection(int inputNodeID, float inputWeight)
		{
			inWeights[inputNodeID] = inputWeight;
		}

		public virtual void Notify(int inputNodeID, float value)
		{
			inValues[inputNodeID] = value;
		}

		public void PropagateOutput()
		{
			var nodeInput = new TupleList<float,float>();
			foreach(int inputNodeID in inValues.Keys)
				nodeInput.Add(Tuple.Create(inValues[inputNodeID], inWeights[inputNodeID]));

			float nodeOutput = Function.GetOutput(nodeInput);
			foreach(InternalNetworkNode node in outConnections)
				node.Notify(nodeID, nodeOutput);
		}

		public virtual void SetupDone()
		{
			shouldHave = inWeights.Count;
		}

		public virtual bool IsFullyNotified
		{
			get
			{
				return shouldHave == inValues.Count;
			}
		}
		public virtual void RemoveOldInput()
		{
			inValues = new ConcurrentDictionary<int, float>();
		}
	}

	class OutputNetworkNode : InternalNetworkNode, INetworkNode
	{
		public readonly int representedState;

		public OutputNetworkNode(int nodeID, int representedState, ActivationFunction function) : base(nodeID, function)
		{
			this.representedState = representedState;
		}

		public OutputNetworkNode(int nodeID, ActivationFunction function) : base(nodeID, function)
		{
			representedState = 0;
		}
		public float Activation
		{
			get
			{
				var nodeInput = new TupleList<float,float>();
				foreach(int inputNodeID in inValues.Keys)
					nodeInput.Add(Tuple.Create(inValues[inputNodeID], inWeights[inputNodeID]));

				return Function.GetOutput(nodeInput);
			}
		}
	}
}