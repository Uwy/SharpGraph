using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraph.Extensions
{
    public static class CollectionsExtensions
    {
        public static IEnumerable<T> ThrowIfEmpty<T>(this IEnumerable<T> items, string variableName)
        {
            if (items.ThrowIfNull().Count() == 0)
            {
                throw new Exception(string.Format("Enumerable is empty: {0}", variableName));
            }
            return items;
        }

        public static IEnumerable<T> ThrowIfEmpty<T>(this IEnumerable<T> items)
        {
            return items.ThrowIfEmpty(string.Empty);
        }

    }
}
