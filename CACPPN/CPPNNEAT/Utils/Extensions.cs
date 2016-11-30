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

		public static bool SameWithinReason(this float val, float compareTo)
		{
			return val <= compareTo + EAParameters.SameFloatWithinReason
				&& val >= compareTo - EAParameters.SameFloatWithinReason;
		}
	}
}