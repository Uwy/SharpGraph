using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SharpGraph.Extensions;
using System.Collections.ObjectModel;


namespace SharpGraph.Models
{
    public enum ColorStyle
    {
        Raw,
        Continuous
    };

    public enum RadiusStyle
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
        public delegate double ColorPick(Point start, Point stop, double oldPercent, double quantum);
        public delegate void DrawPath(WriteableBitmap output, Color color, Point start, Point stop);
        public delegate void DrawStop(WriteableBitmap output, Color color, Point position, int radius);

        private static readonly ReadOnlyDictionary<PathStyles, string> PathStylesNames = new ReadOnlyDictionary<PathStyles, string>
        (new Dictionary<PathStyles, string>()
        {
            {PathStyles.None, "None"},
            {PathStyles.SimpleLine, "Simple line"}
        });

        private static readonly ReadOnlyDictionary<ColorStyle, string> ColorStylesNames = new ReadOnlyDictionary<ColorStyle, string>
        (new Dictionary<ColorStyle, string>()
        {
            {ColorStyle.Raw, "Raw"},
            {ColorStyle.Continuous, "Continuous"},

        });

        private static readonly ReadOnlyDictionary<RadiusStyle, string> RadiusStylesNames = new ReadOnlyDictionary<RadiusStyle, string>
        (new Dictionary<RadiusStyle, string>()
        {
            {RadiusStyle.Raw, "Raw"},
            {RadiusStyle.Log, "Logarithm"},
            {RadiusStyle.SquareRoot, "Square root"},

        });

        private static readonly ReadOnlyDictionary<StopStyles, string> StopStylesNames = new ReadOnlyDictionary<StopStyles, string>
        (new Dictionary<StopStyles, string>()
        {
            {StopStyles.None, "None"},
            {StopStyles.Circles, "Circles"},
            {StopStyles.Squares, "Squares"}
        });

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

        public static RadiusSize Delegate(this RadiusStyle radiusStyle)
        {
            switch (radiusStyle)
            {
                case RadiusStyle.Raw:
                    return delegate(int value, double factor)
                    {
                        return (value * factor).Round();
                    };
                case RadiusStyle.SquareRoot:
                    return delegate(int value, double factor)
                    {
                        return (Math.Sqrt(value) * factor).Round();
                    };
                case RadiusStyle.Log:
                    return delegate(int value, double factor)
                    {
                        return (Math.Log(value) * factor).Round();
                    };
                default:
                    return null;
            }
        }

        public static ColorPick Delegate(this ColorStyle colorStyle)
        {
            switch (colorStyle)
            {
                case ColorStyle.Raw:
                    return delegate(Point start, Point stop, double oldPercent, double quantum)
                    {
                        var distance = start.Distance(stop);
                        MouseTracker.MaxDistance = distance;
                        return Math.Min(distance / MouseTracker.MaxDistance, 1);
                    };
                case ColorStyle.Continuous:
                    return delegate(Point start, Point stop, double oldPercent, double quantum)
                    {
                        var distance = start.Distance(stop);
                        MouseTracker.MaxDistance = distance;
                        if ((distance / MouseTracker.MaxDistance) > oldPercent)
                        {
                            return Math.Min(oldPercent * quantum, 1);
                        }
                        else
                        {
                            return Math.Min(oldPercent / quantum, 1);
                        }
                    };
                default:
                    return null;
            }
        }


        public static string Name(this RadiusStyle radiusStyle)
        {
            return DrawStyles.RadiusStylesNames[radiusStyle];
        }

        public static string Name(this StopStyles stopStyle)
        {
            return DrawStyles.StopStylesNames[stopStyle];
        }

        public static string Name(this PathStyles pathStyle)
        {
            return DrawStyles.PathStylesNames[pathStyle];
        }

        public static string Name(this ColorStyle colorStyle)
        {
            return DrawStyles.ColorStylesNames[colorStyle];
        }

        public static PathStyles PathStyleFromName(this string pathStyleName)
        {
            return DrawStyles.PathStylesNames.First(kvp => kvp.Value.Equals(pathStyleName)).Key;
        }

        public static StopStyles StopStyleFromName(this string stopStyleName)
        {
            return DrawStyles.StopStylesNames.First(kvp => kvp.Value.Equals(stopStyleName)).Key;
        }

        public static RadiusStyle RadiusStyleFromName(this string radiusStyleName)
        {
            return DrawStyles.RadiusStylesNames.First(kvp => kvp.Value.Equals(radiusStyleName)).Key;
        }

        public static ColorStyle ColorStyleFromName(this string colorStyleName)
        {
            return DrawStyles.ColorStylesNames.First(kvp => kvp.Value.Equals(colorStyleName)).Key;
        }

        public static List<string> RadiusStylesToNameList()
        {
            return DrawStyles.RadiusStylesNames.Values.ToList();
        }

        public static List<string> StopStylesToNameList()
        {
            return DrawStyles.StopStylesNames.Values.ToList();
        }

        public static List<string> PathStylesToNameList()
        {
            return DrawStyles.PathStylesNames.Values.ToList();
        }

        public static List<string> ColorStylesToNameList()
        {
            return DrawStyles.ColorStylesNames.Values.ToList();
        }

    }
}
