using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraph.Extensions
{
    public static class ObjectExtensions
    {
        public static object GetPropValue(this object obj, string name)
        {
            foreach (var part in name.Split('.'))
            {
                if (obj == null)
                {
                    return null;
                }
                var info = obj.GetType().GetProperty(part);
                if (info == null)
                {
                    return null;
                }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static T GetPropValue<T>(this object obj, string name)
        {
            var retval = GetPropValue(obj, name);
            if (retval == null)
            {
                return default(T);
            }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }

        public static bool TryGetPropValue<T>(this object obj, string name, out T value)
        {
            try
            {
                value = (T)obj.GetPropValue(name); // throws InvalidCastException if types are incompatible
            }
            catch (InvalidCastException) 
            {
                value = default(T);
                return false;
            }
            if (value == null)
            {
                return false;
            }
            return true;
        }
    }
}
