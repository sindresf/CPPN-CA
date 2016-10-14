using System.Collections.Generic;
using CACPPN.CA.BaseTypes;

namespace CACPPN.CA.CellularSpaces
{
    class OneDimensionalCellularSpace : CellularSpace
    {
        private List<Cell> cellSpace;

        public OneDimensionalCellularSpace(int width) //not quite like this
        {
            cellSpace = new List<Cell>(width);
        }

        public override void ChangeSpaceBy(int width, int height = 0, int depth = 0)
        {
            List<Cell> newSpace = new List<Cell>(cellSpace.Count + width);
            if (width < 0)
            {
                //TODO only take out the center of the last space
            }
            else if (width > 0)
            {
                //find the starting point of the old space centered in the new one
            }
        }
    }
}
