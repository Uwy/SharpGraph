using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraph.Extensions
{
    public static class TupleExtensions
    {
        public static Tuple<T, U, V, X> AssignToCopy<T, U, V, X>(this Tuple<T, U, V, X> tuple, T value)
            where T : struct
            where U : struct
            where V : struct
            where X : struct
        {
            return Tuple.Create(value, tuple.Item2, tuple.Item3, tuple.Item4);
        }

        public static Tuple<T, U, V, X> AssignToCopy<T, U, V, X>(this Tuple<T, U, V, X> tuple, U value)
            where T : struct
            where U : struct
            where V : struct
            where X : struct
        {
            return Tuple.Create(tuple.Item1, value, tuple.Item3, tuple.Item4);
        }

        public static Tuple<T, U, V, X> AssignToCopy<T, U, V, X>(this Tuple<T, U, V, X> tuple, V value)
            where T : struct
            where U : struct
            where V : struct
            where X : struct
        {
            return Tuple.Create(tuple.Item1, tuple.Item2, value, tuple.Item4);
        }


        public static Tuple<T, U, V, X> AssignToCopy<T, U, V, X>(this Tuple<T, U, V, X> tuple, X value)
            where T : struct
            where U : struct
            where V : struct
            where X : struct
        {
            return Tuple.Create(tuple.Item1, tuple.Item2, tuple.Item3, value);
        }
    }
}
