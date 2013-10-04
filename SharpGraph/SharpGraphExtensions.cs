using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SharpGraph
{
    public static class SharpGraphExtensions
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

        public static bool Between(this double value, double lowerValue, double upperValue, bool inclusive)
        {
            return inclusive
                ? (lowerValue <= value) && (value <= upperValue)
                : (lowerValue < value) && (value < upperValue);
        }

        public static double ThrowIfNotPercentage(this double value)
        {
            if (!value.Between(0, 1, true))
            {
                throw new Exception("Value is not a percentage");
            }
            return value;
        }

        public static byte ValueAtPercentWayToEnd(this byte startValue, double percent, byte endValue)
        {
            return (byte)(startValue + percent.ThrowIfNotPercentage() * (endValue - startValue));
        }

        public static int Round(this double value)
        {
            return (int)Math.Round(value);
        }

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
