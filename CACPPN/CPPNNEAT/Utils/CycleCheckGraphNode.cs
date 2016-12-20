using System.Collections.Generic;

namespace CPPNNEAT.Utils
{
	class CycleCheckGraphNode
	{
		public List<CycleCheckGraphNode> children;
		public readonly int nodeID;

		public CycleCheckGraphNode(int nodeID)
		{
			children = new List<CycleCheckGraphNode>();
			this.nodeID = nodeID;
		}
	}
}