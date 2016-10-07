using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CACPPN.CA.Interfaces
{
	interface IStateTranslator
	{
        // Takes the double value (or faked boolean value) and
        // translates it into whatever you want, be it colours or
        // grayscale or integer values or "ready for storage"
        
        T Translate<T>(double cellState);
	}
}
