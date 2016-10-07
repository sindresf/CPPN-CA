using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CACPPN.CA.BaseTypes;
using CACPPN.CA.Interfaces;

namespace CACPPN.CA.CellularSpaces
{
    class OneDimensionalCellularSpace : CellularSpace
    {
        private List<Cell> cellSpace;

        public OneDimensionalCellularSpace(int width, IInitialCondition startStates) //not quite like this
        {
            cellSpace = new List<Cell>(width);
            if (startStates != null)
            {

            }
            else
            {
                foreach (Cell cell in cellSpace)
                {
                    //jupp
                }
            }
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
