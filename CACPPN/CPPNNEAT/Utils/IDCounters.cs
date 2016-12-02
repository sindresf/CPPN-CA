namespace CPPNNEAT.Utils
{
	class IDCounters
	{
		private int spID, indID, ngID, cgID;

		public IDCounters()
		{
			SpeciesID = 0;
			IndividualID = 0;
			NodeGeneID = 0;
			ConnectionGeneID = 0;
		}

		public int SpeciesID
		{
			get { return spID++; }
			private set { spID = value; }
		}

		public int IndividualID
		{
			get { return indID++; }
			private set { indID = value; }
		}
		public int NodeGeneID
		{
			get { return ngID++; }
			private set { ngID = value; }
		}

		public int ConnectionGeneID
		{
			get { return cgID++; }
			private set { cgID = value; }
		}
	}
}