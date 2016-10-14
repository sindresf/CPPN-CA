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

            if (orientation == Orientation.VERTICAL)
            {
                cellSpace[iMid - 1, jMid].State = 1;
                cellSpace[iMid - 1, jMid].OldState = 1;

                cellSpace[iMid, jMid].State = 1;
                cellSpace[iMid, jMid].OldState = 1;

                cellSpace[iMid + 1, jMid].State = 1;
                cellSpace[iMid + 1, jMid].OldState = 1;
            }
            else if (orientation == Orientation.HORISONTAL)
            {

                cellSpace[iMid, jMid - 1].State = 1;
                cellSpace[iMid, jMid - 1].OldState = 1;

                cellSpace[iMid, jMid].State = 1;
                cellSpace[iMid, jMid].OldState = 1;

                cellSpace[iMid, jMid + 1].State = 1;
                cellSpace[iMid, jMid + 1].OldState = 1;
            }
        }
    }
}
