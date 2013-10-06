using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SharpGraph
{
    public enum RadiusStyles
    {
        Raw,
        SquareRoot,
        Log
    };

    public enum StopStyles
    {
        None,
        Circles,
        Squares
    };

    public enum PathStyles
    {
        None,
        SimpleLine
    };

    public static class DrawStyles
    {
        public delegate int RadiusSize(int value, double factor);
        public delegate void DrawPath(WriteableBitmap output, Color color, Point start, Point stop);
        public delegate void DrawStop(WriteableBitmap output, Color color, Point position, int radius);

        public static DrawPath Delegate(this PathStyles pathStyle)
        {
            switch (pathStyle)
            {
                case PathStyles.None:
                    return delegate(WriteableBitmap output, Color color, Point start, Point stop)
                    { };
                case PathStyles.SimpleLine:
                    return delegate(WriteableBitmap output, Color color, Point start, Point stop)
                    {
                        output.DrawLineAa(start.X.Round(), start.Y.Round(), stop.X.Round(), stop.Y.Round(), color);
                    };
                default:
                    return null;
            }
        }

        public static DrawStop Delegate(this StopStyles stopStyle)
        {
            switch (stopStyle)
            {
                case StopStyles.None:
                    return delegate(WriteableBitmap output, Color color, Point position, int radius)
                    { };
                case StopStyles.Circles:
                    return delegate(WriteableBitmap output, Color color, Point position, int radius)
                    {
                        output.FillEllipseCentered(position.X.Round(), position.Y.Round(), radius, radius, color);
                    };
                case StopStyles.Squares:
                    return delegate(WriteableBitmap output, Color color, Point position, int radius)
                    {
                        var centeredSquare = position.CenteredSquare(radius);
                        output.FillRectangle(centeredSquare.TopLeft.X.Round(), centeredSquare.TopLeft.Y.Round(), centeredSquare.BottomRight.X.Round(), centeredSquare.BottomRight.Y.Round(), color);
                    };
                default:
                    return null;
            }
        }

        public static RadiusSize Delegate(this RadiusStyles radiusStyle)
        {
            switch (radiusStyle)
            {
                case RadiusStyles.Raw:
                    return delegate(int value, double factor)
                    {
                        return (value * factor).Round();
                    };
                case RadiusStyles.SquareRoot:
                    return delegate(int value, double factor)
                    {
                        return (Math.Sqrt(value) * factor).Round();
                    };
                case RadiusStyles.Log:
                    return delegate(int value, double factor)
                    {
                        return (Math.Log(value) * factor).Round();
                    };
                default:
                    return null;
            }
        }
    }
}
