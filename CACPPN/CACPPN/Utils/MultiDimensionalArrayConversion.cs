using System;
using System.Collections.Generic;

namespace CACPPN.Utils
{
	public static class MultiDimensionalArrayConversion
	{
		public static IEnumerable<T> ToEnumerable<T>(this Array target)
		{
			foreach(var item in target)
				yield return (T)item;
		}
	}
}