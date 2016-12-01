namespace CPPNNEAT.EA.Base
{
	class SensorNodeGene : Gene
	{
		public readonly int nodeID;
		public readonly NodeType type;

		public SensorNodeGene(int geneID, int nodeID, NodeType type) : base(geneID)
		{
			this.nodeID = nodeID;
			this.type = type;
		}
	}
}