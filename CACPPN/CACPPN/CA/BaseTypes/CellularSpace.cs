using System;

namespace CACPPN.CA.BaseTypes
{
    class CellularSpace
    {
        public virtual void ChangeSpaceBy(int width, int height = 0, int depth = 0)
        {
            throw new Exception("Depends on the specific construct how this works!");
        }
    }
}
