using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraph.Extensions
{
    public static class NumericExtensions
    {
        public static bool Between(this double value, double lowerValue, double upperValue, bool inclusive)
        {
            return inclusive
                ? (lowerValue <= value) && (value <= upperValue)
                : (lowerValue < value) && (value < upperValue);
        }

        public static bool Between(this int value, int lowerValue, int upperValue, bool inclusive)
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

        public static int ValueAtPercentWayToEnd(this int startValue, double percent, int endValue)
        {
            return (int)(startValue + percent.ThrowIfNotPercentage() * (endValue - startValue));
        }

        public static int Round(this double value)
        {
            return (int)Math.Round(value);
        }
    }
}
