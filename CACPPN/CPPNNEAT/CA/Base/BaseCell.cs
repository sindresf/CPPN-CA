using System;
using System.Collections.Generic;
using CPPNNEAT.CA.Base.Interface;

namespace CPPNNEAT.CA.Base
{
	abstract class BaseCell : ICell
	{
		public readonly int neighbourhoodSize;
		public readonly int x;

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

		public BaseCell(int x, int neighbourhoodSize)
		{
			this.x = x;
			this.neighbourhoodSize = neighbourhoodSize;
		}

		protected abstract float GetState(Array cellBoard);
		public List<float> GetNeighbourhoodCurrentState(Array cellBoard)
		{
			List<float> returnedStates = new List<float>();
			foreach(BaseCell cell in Neighbourhood)
				returnedStates.Add(cell.GetState(cellBoard));

			return returnedStates;
		}
		public abstract void InitializeNeighbourhood();
	}
}