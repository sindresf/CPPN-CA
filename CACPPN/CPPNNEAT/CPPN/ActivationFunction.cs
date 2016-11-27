using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPPNNEAT.CPPN
{
    class ActivationFunction
    {
        // all CCPN really is is a collection of functions instead of
        // a single activation function

        // get randomly initialized activation function based on type function

        public static ActivationFunction GetRandomInitializedFunction(ActivationFunctionType type)
        {
            switch (type)
            {
                case ActivationFunctionType.Sinusodial:
                    return null;
                case ActivationFunctionType.thisAndThat:
                    return null;
                case ActivationFunctionType.andOfSuch:
                    return null;
                default:
                    return null;
            }
        }
    }

    enum ActivationFunctionType
    {
        Sinusodial,
        thisAndThat,
        andOfSuch
    }
}