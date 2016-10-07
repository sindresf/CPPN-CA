using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CACPPN.CA.Interfaces;

namespace CACPPN.CA.BaseTypes
{
    class CellularSpace : ICellularSpace
    {
        public virtual void ChangeSpaceBy(int width, int height = 0, int depth = 0)
        {
            throw new Exception("Depends on the specific construct how this works!");
        }

        public virtual ICellularSpace getSpaceNow()
        {
            throw new Exception("a derived specific construct!");
        }
    }
}
