namespace CPPNNEATCA.EA.Base
{
	abstract class Gene
	{
		public readonly int geneID;

		public Gene(int geneID)
		{
			this.geneID = geneID;
		}

		public override bool Equals(object other)
		{
			return ((Gene)other).geneID == geneID;
		}

		public static bool operator ==(Gene g1, Gene g2)
		{
			return g1.geneID == g2.geneID;
		}

		public static bool operator !=(Gene g1, Gene g2)
		{
			return g1.geneID != g2.geneID;
		}

		public override int GetHashCode()
		{
			return geneID;
		}
	}
}