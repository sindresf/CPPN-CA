using System;
using System.Collections.Generic;

namespace CPPNNEAT.Utils
{
	class NEATInfo : IInfo
	{
		public List<GenerationInfo> generationInfos;

		public NEATInfo()
		{
			generationInfos = new List<GenerationInfo>();
		}

		public string GetSaveToFileFormat()
		{
			string s = "";
			foreach(GenerationInfo gInfo in generationInfos)
			{
				s += gInfo.GetSaveToFileFormat() + "\n";
			}
			return s;
		}
	}

	class GenerationInfo : IInfo
	{
		public float bestFitness,avgSpeciesFitness, worstFitness;
		public int generationNumber, speciesCount;
		public List<SpeciesInfo> speciesInfos;

		public GenerationInfo(int generationNumber)
		{
			speciesInfos = new List<SpeciesInfo>();
			this.generationNumber = generationNumber;
		}

		public string GetSaveToFileFormat()
		{
			string s = generationNumber + ": |"
				+speciesCount
				+"|"+bestFitness
				+"|"+avgSpeciesFitness
				+"|"+worstFitness+"|{.|.";
			foreach(SpeciesInfo sInfo in speciesInfos)
			{
				s += sInfo.GetSaveToFileFormat() + ".|.";
			}
			s += "}";
			return s;
		}
	}

	class SpeciesInfo : IInfo
	{
		public int speciesID;
		public float speciesFitness;
		public List<IndividualInfo> indieInfos;

		public SpeciesInfo(int speciesID)
		{
			indieInfos = new List<IndividualInfo>();
			this.speciesID = speciesID;
		}

		public string GetSaveToFileFormat()
		{
			string s = speciesID + ": _" + speciesID + "_";

			return s;
		}
	}

	class IndividualInfo : IInfo
	{
		public float individualFitness;
		public GenomeInfo genomeInfo;

		public IndividualInfo()
		{
			genomeInfo = new GenomeInfo();
		}

		public string GetSaveToFileFormat()
		{
			throw new NotImplementedException();
		}
	}

	class GenomeInfo : IInfo
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

		public string GetSaveToFileFormat()
		{
			throw new NotImplementedException();
		}
	}
}