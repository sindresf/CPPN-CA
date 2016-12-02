using System;
using System.Collections.Generic;

namespace CPPNNEATCA.Utils
{
	class TupleList<T1, T2> : List<Tuple<T1, T2>>
	{
		public void Add(T1 item1, T2 item2)
		{
			Add(new Tuple<T1, T2>(item1, item2));
		}

		public List<T1> Item1List()
		{
			List<T1> item1List = new List<T1>();
			foreach(Tuple<T1, T2> tuple in this)
			{
				item1List.Add(tuple.Item1);
			}
			return item1List;
		}

		public List<T2> Item2List()
		{
			List<T2> item2List = new List<T2>();
			foreach(Tuple<T1, T2> tuple in this)
			{
				item2List.Add(tuple.Item2);
			}
			return item2List;
		}
	}
}