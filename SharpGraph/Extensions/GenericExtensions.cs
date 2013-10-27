using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraph.Extensions
{
    public static class GenericExtensions
    {
        public static T ThrowIfNull<T>(this T value, string variableName) where T : class
        {
            if (value == null)
            {
                throw new NullReferenceException(string.Format("Value is Null: {0}", variableName));
            }

            return value;
        }

        public static T ThrowIfNull<T>(this T value) where T : class
        {
            return value.ThrowIfNull(string.Empty);
        }
    }
}
