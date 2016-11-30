namespace CPPNNEAT.Utils
{
	static class Extensions
	{
		public static float ClampWeight(this float val)
		{
			if(val.CompareTo(CPPNetworkParameters.WeightMin) < 0) return CPPNetworkParameters.WeightMin;
			else if(val.CompareTo(CPPNetworkParameters.WeightMax) > 0) return CPPNetworkParameters.WeightMax;
			else return val;
		}
	}
}