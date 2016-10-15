using System.Collections.Generic;
using CACPPN.CA.BaseTypes;

namespace CACPPN.Utils
{
    static class NeighbourhoodConstructor
    {
        //ONE DIMENSION
        public static List<AbstractCell> getOKNeighbourhood(AbstractCell[] cellSpace, int centreIndex, int width)
        {
            List<AbstractCell> neighbourhood = new List<AbstractCell>();

            for (int i = centreIndex - width; i < centreIndex + width; i++)
            {
                if (i != centreIndex)
                    neighbourhood.Add(cellSpace[i]);
            }

            return neighbourhood;
        }

        public static List<AbstractCell> getLeftEndNeighbourhood(AbstractCell[] cellSpace, int width)
        {
            List<AbstractCell> neighbourhood = new List<AbstractCell>();

            for (int i = width; i > 0; i--)
                neighbourhood.Add(cellSpace[cellSpace.Length - width]);

            for (int i = 1; i <= width; i++)
                neighbourhood.Add(cellSpace[i]);

            return neighbourhood;
        }

        public static List<AbstractCell> getRightEndNeighbourhood(AbstractCell[] cellSpace, int width)
        {
            List<AbstractCell> neighbourhood = new List<AbstractCell>();

            for (int i = width + 1; i > 1; i--)
                neighbourhood.Add(cellSpace[cellSpace.Length - width]);

            for (int i = 0; i < width; i++)
                neighbourhood.Add(cellSpace[i]);

            return neighbourhood;
        }

        //TWO DIMENSIONS
        public static List<AbstractCell> getOKNeighbourhood(AbstractCell[,] cellSpace, int i, int j, int width)
        {
            List<AbstractCell> neighbourhood = new List<AbstractCell>();
            if (width >= 1)
            {
                neighbourhood.Add(cellSpace[i - 1, j]);
                neighbourhood.Add(cellSpace[i, j + 1]);
                neighbourhood.Add(cellSpace[i + 1, j]);
                neighbourhood.Add(cellSpace[i, j - 1]);
            }
            if (width >= 2)
            {
                neighbourhood.Add(cellSpace[i - 1, j - 1]);
                neighbourhood.Add(cellSpace[i - 1, j + 1]);
                neighbourhood.Add(cellSpace[i + 1, j + 1]);
                neighbourhood.Add(cellSpace[i + 1, j - 1]);
            }
            return neighbourhood;
        }

        public static List<AbstractCell> getUpperNeighbourhood(AbstractCell[,] cellSpace, int i, Hyperparameters hyperParams)
        {
            List<AbstractCell> neighbourhoodState = new List<AbstractCell>();

            if (hyperParams.neighbourhoodWidth >= 1)
            {
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, i]);
                neighbourhoodState.Add(cellSpace[0, i + 1]);
                neighbourhoodState.Add(cellSpace[1, i]);
                neighbourhoodState.Add(cellSpace[0, i - 1]);
            }
            if (hyperParams.neighbourhoodWidth >= 2)
            {
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, i - 1]);
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, i + 1]);
                neighbourhoodState.Add(cellSpace[1, i + 1]);
                neighbourhoodState.Add(cellSpace[1, i - 1]);
            }
            return neighbourhoodState;
        }
        public static List<AbstractCell> getLowerNeighbourhood(AbstractCell[,] cellSpace, int i, Hyperparameters hyperParams)
        {
            List<AbstractCell> neighbourhoodState = new List<AbstractCell>();

            if (hyperParams.neighbourhoodWidth >= 1)
            {
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 2, i]);
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, i + 1]);
                neighbourhoodState.Add(cellSpace[0, i]);
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, i - 1]);
            }
            if (hyperParams.neighbourhoodWidth >= 2)
            {
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 2, i - 1]);
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 2, i + 1]);
                neighbourhoodState.Add(cellSpace[0, i + 1]);
                neighbourhoodState.Add(cellSpace[0, i - 1]);
            }
            return neighbourhoodState;
        }
        public static List<AbstractCell> getRightNeighbourhood(AbstractCell[,] cellSpace, int i, Hyperparameters hyperParams)
        {
            List<AbstractCell> neighbourhoodState = new List<AbstractCell>();

            if (hyperParams.neighbourhoodWidth >= 1)
            {
                neighbourhoodState.Add(cellSpace[i - 1, hyperParams.spaceSize - 1]);
                neighbourhoodState.Add(cellSpace[i, 0]);
                neighbourhoodState.Add(cellSpace[i + 1, hyperParams.spaceSize - 1]);
                neighbourhoodState.Add(cellSpace[i, hyperParams.spaceSize - 2]);
            }
            if (hyperParams.neighbourhoodWidth >= 2)
            {
                neighbourhoodState.Add(cellSpace[i - 1, hyperParams.spaceSize - 2]);
                neighbourhoodState.Add(cellSpace[i - 1, 0]);
                neighbourhoodState.Add(cellSpace[i + 1, 0]);
                neighbourhoodState.Add(cellSpace[i + 1, hyperParams.spaceSize - 2]);
            }
            return neighbourhoodState;
        }
        public static List<AbstractCell> getLeftNeighbourhood(AbstractCell[,] cellSpace, int i, Hyperparameters hyperParams)
        {
            List<AbstractCell> neighbourhoodState = new List<AbstractCell>();

            if (hyperParams.neighbourhoodWidth >= 1)
            {
                neighbourhoodState.Add(cellSpace[i - 1, 0]);
                neighbourhoodState.Add(cellSpace[i, 1]);
                neighbourhoodState.Add(cellSpace[i + 1, 0]);
                neighbourhoodState.Add(cellSpace[i, hyperParams.spaceSize - 1]);
            }
            if (hyperParams.neighbourhoodWidth >= 2)
            {
                neighbourhoodState.Add(cellSpace[i - 1, hyperParams.spaceSize - 1]);
                neighbourhoodState.Add(cellSpace[i - 1, 1]);
                neighbourhoodState.Add(cellSpace[i + 1, hyperParams.spaceSize - 1]);
                neighbourhoodState.Add(cellSpace[i + 1, 1]);
            }
            return neighbourhoodState;
        }

        public static List<AbstractCell> getUpperLeftNeighbourhood(AbstractCell[,] cellSpace, Hyperparameters hyperParams)
        {
            List<AbstractCell> neighbourhoodState = new List<AbstractCell>();

            if (hyperParams.neighbourhoodWidth >= 1)
            {
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, 0]);
                neighbourhoodState.Add(cellSpace[0, 1]);
                neighbourhoodState.Add(cellSpace[1, 0]);
                neighbourhoodState.Add(cellSpace[0, hyperParams.spaceSize - 1]);
            }
            if (hyperParams.neighbourhoodWidth >= 2)
            {
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, hyperParams.spaceSize - 1]);
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, 1]);
                neighbourhoodState.Add(cellSpace[1, 1]);
                neighbourhoodState.Add(cellSpace[1, hyperParams.spaceSize - 1]);
            }
            return neighbourhoodState;
        }

        public static List<AbstractCell> getLowerLeftNeighbourhood(AbstractCell[,] cellSpace, Hyperparameters hyperParams)
        {
            List<AbstractCell> neighbourhoodState = new List<AbstractCell>();

            if (hyperParams.neighbourhoodWidth >= 1)
            {
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 2, 0]);
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, 1]);
                neighbourhoodState.Add(cellSpace[0, 0]);
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, hyperParams.spaceSize - 1]);
            }
            if (hyperParams.neighbourhoodWidth >= 2)
            {
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 2, hyperParams.spaceSize - 1]);
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 2, 1]);
                neighbourhoodState.Add(cellSpace[0, 1]);
                neighbourhoodState.Add(cellSpace[0, hyperParams.spaceSize - 1]);
            }
            return neighbourhoodState;
        }

        public static List<AbstractCell> getUpperRightNeighbourhood(AbstractCell[,] cellSpace, Hyperparameters hyperParams)
        {
            List<AbstractCell> neighbourhoodState = new List<AbstractCell>();

            if (hyperParams.neighbourhoodWidth >= 1)
            {
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, hyperParams.spaceSize - 1]);
                neighbourhoodState.Add(cellSpace[0, 0]);
                neighbourhoodState.Add(cellSpace[1, hyperParams.spaceSize - 1]);
                neighbourhoodState.Add(cellSpace[0, hyperParams.spaceSize - 2]);
            }
            if (hyperParams.neighbourhoodWidth >= 2)
            {
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, hyperParams.spaceSize - 2]);
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, 0]);
                neighbourhoodState.Add(cellSpace[1, 0]);
                neighbourhoodState.Add(cellSpace[1, hyperParams.spaceSize - 2]);
            }
            return neighbourhoodState;
        }

        public static List<AbstractCell> getLowerRightNeighbourhood(AbstractCell[,] cellSpace, Hyperparameters hyperParams)
        {
            List<AbstractCell> neighbourhoodState = new List<AbstractCell>();

            if (hyperParams.neighbourhoodWidth >= 1)
            {
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 2, hyperParams.spaceSize - 1]);
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, 0]);
                neighbourhoodState.Add(cellSpace[0, hyperParams.spaceSize - 1]);
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 1, hyperParams.spaceSize - 2]);

            }
            if (hyperParams.neighbourhoodWidth >= 2)
            {
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 2, hyperParams.spaceSize - 2]);
                neighbourhoodState.Add(cellSpace[hyperParams.spaceSize - 2, 0]);
                neighbourhoodState.Add(cellSpace[0, 0]);
                neighbourhoodState.Add(cellSpace[0, hyperParams.spaceSize - 2]);
            }
            return neighbourhoodState;
        }
    }
}
