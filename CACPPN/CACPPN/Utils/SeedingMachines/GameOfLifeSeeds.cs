using System;
using CACPPN.CA.BaseTypes;
using CACPPN.CA.Enums;

namespace CACPPN.Utils.SeedingMachines
{
    static class GameOfLifeSeeds
    {
        public static void lol()
        {

        }
        public static void SpawnCrossOscillator(int iMid, int jMid, Cell[,] cellSpace, Orientation orientation)
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

            cellSpace[iMid, jMid].State = 1;
            cellSpace[iMid, jMid].OldState = 1;

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

            cellSpace[iFirst, jFirst].State = 1;
            cellSpace[iFirst, jFirst].OldState = 1;

            cellSpace[iLast, jLast].State = 1;
            cellSpace[iLast, jLast].OldState = 1;
        }
    }
}
