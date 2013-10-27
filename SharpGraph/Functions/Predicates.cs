using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraph.Functions
{
    public static class Predicates
    {
        public static bool ReturnTrue<T>(this T weDontCareAboutThis)
        {
            return true;
        }

        public static bool ReturnFalse<T>(this T weDontCareAboutThis)
        {
            return false;
        }
    }
}
