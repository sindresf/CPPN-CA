using System.Threading.Tasks;
using CACPPN.CA.BaseTypes;

namespace CACPPN.Utils
{
	static class NeighbourhoodInitializor
	{
		public static void InitializeNeighbourhoods1D(AbstractCell[] cellSpace, Hyperparameters hyperParams)
		{
			int width = hyperParams.neighbourhoodWidth;
			int highestIndex = hyperParams.spaceSize;
			Task[] tasks = new Task[2];
			tasks[0] = Task.Run(() => InitializeSafeCellNeigbhourhoods(cellSpace, highestIndex, width));
			tasks[1] = Task.Run(() => InitializeEdgeNeighbourhoods(cellSpace, highestIndex, width));
			Task.WaitAll(tasks);
		}

		private static void InitializeSafeCellNeigbhourhoods(AbstractCell[] cellSpace, int highestIndex, int width)
		{
			Parallel.For(1, highestIndex - 1, i =>
			   {
				   cellSpace[i].Neighbourhood = NeighbourhoodConstructor.getOKNeighbourhood(cellSpace, i, width);
			   });
		}

		private static void InitializeEdgeNeighbourhoods(AbstractCell[] cellSpace, int highestIndex, int width)
		{
			cellSpace[0].Neighbourhood = NeighbourhoodConstructor.getLeftEndNeighbourhood(cellSpace, width);
			cellSpace[highestIndex - 1].Neighbourhood = NeighbourhoodConstructor.getRightEndNeighbourhood(cellSpace, width);
		}

		//TODO make 
		public static void InitializeNeighbourhoods2D(AbstractCell[,] cellSpace, Hyperparameters hyperParams)
		{
			int highestIndex = hyperParams.spaceSize - 1;
			int width = hyperParams.neighbourhoodWidth;
			Task[] tasks = new Task[3];
			tasks[0] = Task.Run(() => InitializeSafeCellNeighbourhoods(cellSpace, highestIndex, width));
			tasks[1] = Task.Run(() => InitializeOutlierCellNeighbourhoods(cellSpace, highestIndex, hyperParams));
			tasks[2] = Task.Run(() => InitializeCornerCellNeighbourhoods(cellSpace, highestIndex, hyperParams));
			Task.WaitAll(tasks);
		}

		private static void InitializeSafeCellNeighbourhoods(AbstractCell[,] cellSpace, int highestIndex, int width)
		{
			//TODO need a "safe distance calculator" for 2D neighbourhood widths
			Parallel.For(1, highestIndex, (int i) =>
			 {
				 Parallel.For(1, highestIndex, (int j) =>
				 {
					 cellSpace[i, j].Neighbourhood = NeighbourhoodConstructor.getOKNeighbourhood(cellSpace, i, j, width);
				 });
			 });
		}
		private static void InitializeOutlierCellNeighbourhoods(AbstractCell[,] cellSpace, int highestIndex, Hyperparameters hyperParams)
		{
			Parallel.For(1, highestIndex, (int i) =>
			  {
				  cellSpace[0, i].Neighbourhood = NeighbourhoodConstructor.getUpperNeighbourhood(cellSpace, i, hyperParams);
				  cellSpace[i, 0].Neighbourhood = NeighbourhoodConstructor.getLeftNeighbourhood(cellSpace, i, hyperParams);
				  cellSpace[highestIndex, i].Neighbourhood = NeighbourhoodConstructor.getLowerNeighbourhood(cellSpace, i, hyperParams);
				  cellSpace[i, highestIndex].Neighbourhood = NeighbourhoodConstructor.getRightNeighbourhood(cellSpace, i, hyperParams);
			  });
		}
		private static void InitializeCornerCellNeighbourhoods(AbstractCell[,] cellSpace, int highestIndex, Hyperparameters hyperParams)
		{
			cellSpace[0, 0].Neighbourhood = NeighbourhoodConstructor.getUpperLeftNeighbourhood(cellSpace, hyperParams);
			cellSpace[0, highestIndex].Neighbourhood = NeighbourhoodConstructor.getUpperRightNeighbourhood(cellSpace, hyperParams);
			cellSpace[highestIndex, 0].Neighbourhood = NeighbourhoodConstructor.getLowerLeftNeighbourhood(cellSpace, hyperParams);
			cellSpace[highestIndex, highestIndex].Neighbourhood = NeighbourhoodConstructor.getLowerRightNeighbourhood(cellSpace, hyperParams);
		}
	}
}