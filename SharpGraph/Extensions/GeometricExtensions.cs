using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SharpGraph.Extensions
{
    public static class GeometricExtensions
    {
        public static Rect CenteredSquare(this Point position, int radius)
        {
            return new Rect(position.X - radius / 2, position.Y - radius / 2, radius, radius);
        }

        public static double Distance(this Point start, Point stop)
        {
            return Math.Sqrt(Math.Pow((start.X - stop.X), 2) + Math.Pow((start.Y - stop.Y), 2));
        }
    }
}
