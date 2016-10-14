using System.Collections.Generic;
using CACPPN.CA.BaseTypes;

namespace CACPPN.CA.CellularSpaces
{
    class OneDimensionalCellularSpace : CellularSpace
    {
        private List<AbstractCell> cellSpace;

        public OneDimensionalCellularSpace(int width) //not quite like this
        {
            cellSpace = new List<AbstractCell>(width);
        }

        public override void ChangeSpaceBy(int width, int height = 0, int depth = 0)
        {
            List<AbstractCell> newSpace = new List<AbstractCell>(cellSpace.Count + width);
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
