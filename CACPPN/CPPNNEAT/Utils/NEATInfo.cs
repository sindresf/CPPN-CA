using System.Collections.Generic;

namespace CPPNNEAT.Utils
{
	class NEATInfo
	{
		public List<GenerationInfo> generationInfos;

		public NEATInfo()
		{
			generationInfos = new List<GenerationInfo>();
		}
	}

	class GenerationInfo
	{
		public float bestFitness,avgSpeciesFitness, worstFitness;
		public int speciesCount;
		public List<SpeciesInfo> speciesInfos;

		public GenerationInfo()
		{
			speciesInfos = new List<SpeciesInfo>();
		}
	}

	class SpeciesInfo
	{
		public float speciesFitness;
		public List<IndividualInfo> indieInfos;

		public SpeciesInfo()
		{
			indieInfos = new List<IndividualInfo>();
		}
	}

	class IndividualInfo
	{
		public float individualFitness;
		public GenomeInfo genomeInfo;

		public IndividualInfo()
		{
			genomeInfo = new GenomeInfo();
		}
	}

	class GenomeInfo
	{
		public int nodeGeneCount, connectionGeneCount;

		public GenomeInfo(int nodeGeneCount, int connectionGeneCount)
		{
			this.nodeGeneCount = nodeGeneCount;
			this.connectionGeneCount = connectionGeneCount;
		}
		public GenomeInfo()
		{

		}
	}
}