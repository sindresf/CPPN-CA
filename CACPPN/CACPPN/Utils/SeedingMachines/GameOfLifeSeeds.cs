using System;
using System.Threading.Tasks;
using CACPPN.CA.BaseTypes;
using CACPPN.CA.DerivedTypes.CellTypes;
using CACPPN.CA.Enums;

namespace CACPPN.Utils.SeedingMachines
{
    static class GameOfLifeSeeds
    {
        public static void SpawnCrossOscillator(int iMid, int jMid, AbstractCell[,] cellSpace, Orientation orientation)
        {
            //in case of future orientation possibilities
            if (orientation != Orientation.HORISONTAL && orientation != Orientation.VERTICAL)
                throw new ArgumentException("that orientation not possible for this sees");
            //from midIndex out
            int seedHeight = 1;
            int seedWidth = 1;
            string midpointTooCloseToEdgeErrorMessage = "needs more space around midpoint for that seed";

            //check that there's space enough for the seed where you want it placed
            if (!(iMid >= seedHeight
                && jMid >= seedWidth))
                throw new ArgumentOutOfRangeException(midpointTooCloseToEdgeErrorMessage);
            if (!(iMid <= cellSpace.GetLength(0) - 1 - seedHeight
                && jMid <= cellSpace.GetLength(1) - 1 - seedWidth))
                throw new ArgumentOutOfRangeException(midpointTooCloseToEdgeErrorMessage);

            cellSpace[iMid, jMid].SetFirstState(1);

            int iFirst = iMid;
            int jFirst = jMid;
            int iLast = iMid;
            int jLast = jMid;

            switch (orientation)
            {
                case Orientation.HORISONTAL:
                    jFirst -= 1;
                    jLast += 1;
                    break;
                case Orientation.VERTICAL:
                    iFirst -= 1;
                    iLast += 1;
                    break;
            }
            cellSpace[iFirst, jFirst].SetFirstState(1);
            cellSpace[iLast, jLast].SetFirstState(1);
        }

        //TODO these are not fixed up
        public static void SpawnRPentomino(int iMid, int jMid, Cell[,] cellSpace, Orientation orientation)
        {
            //in case of future orientation possibilities
            if (orientation != Orientation.HORISONTAL && orientation != Orientation.VERTICAL)
                throw new ArgumentException("that orientation not possible for this sees");

            //from midIndex out
            int seedHeight = 1;
            int seedWidth = 1;
            string midpointTooCloseToEdgeErrorMessage = "needs more space around midpoint for that seed";

            //check that there's space enough for the seed where you want it placed
            if (!(iMid >= seedHeight
                && jMid >= seedWidth))
                throw new ArgumentOutOfRangeException(midpointTooCloseToEdgeErrorMessage);
            if (!(iMid <= cellSpace.GetLength(0) - 1 - seedHeight
                && jMid <= cellSpace.GetLength(1) - 1 - seedWidth))
                throw new ArgumentOutOfRangeException(midpointTooCloseToEdgeErrorMessage);

            cellSpace[iMid, jMid].SetFirstState(1);
            cellSpace[iMid, jMid - 1].SetFirstState(1);
            cellSpace[iMid + 1, jMid].SetFirstState(1);
            cellSpace[iMid - 1, jMid].SetFirstState(1);
            cellSpace[iMid - 1, jMid + 1].SetFirstState(1);
        }

        public static void RandomFillSpace(AbstractCell[,] cellSpace, double fillAmount)
        {
            Random rand = new Random();
            Parallel.ForEach<AbstractCell>(cellSpace.ToEnumerable<AbstractCell>(), cell =>
            {
                if (rand.NextDouble() < fillAmount)
                    cell.SetFirstState(1);
            });
        }

        public static void SpawnRandoms(AbstractCell[,] cellSpace, int randMax, int count)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnRandom(cellSpace, randMax);
            }
        }

        public static void SpawnRandom(AbstractCell[,] cellSpace, int randMax)
        {
            Random rand = new Random();
            int maxTries = (int)((randMax * randMax) * 0.45);
            int indexI = rand.Next(randMax);
            int indexJ = rand.Next(randMax);
            int tryCount = 0;
            while (cellSpace[indexI, indexJ].CurrentState == 1 && tryCount++ < maxTries)
            {
                indexI = rand.Next(randMax);
                indexJ = rand.Next(randMax);
            }
            cellSpace[indexI, indexJ].SetFirstState(1);
        }
    }
}