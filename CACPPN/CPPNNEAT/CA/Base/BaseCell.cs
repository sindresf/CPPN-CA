using System;
using System.Collections.Generic;
using CPPNNEAT.CA.Base.Interface;

namespace CPPNNEAT.CA.Base
{
	abstract class BaseCell : ICell
	{
		protected List<ICell> _neighbourhood;
		public List<ICell> Neighbourhood
		{
			get { return _neighbourhood; }
			set
			{
				if(_neighbourhood == null)
					_neighbourhood = value;
				else
					throw new MemberAccessException("no resetting of neighbourhood!");
			}
		}
		public readonly int neighbourhoodSize;
		public readonly int x;

		public BaseCell(int x, int neighbourhoodSize)
		{
			this.x = x;
			this.neighbourhoodSize = neighbourhoodSize;
		}

		public List<float> GetNeighbourhoodCurrentState()
		{
			List<float> returnedStates = new List<float>();
			foreach(ICell cell in Neighbourhood)
				returnedStates.Add(cell.GetState());

			return returnedStates;
		}

		public abstract float GetState();
		public abstract void InitializeNeighbourhood();
	}
}