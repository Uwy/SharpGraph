using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SharpGraph.Extensions;


namespace SharpGraph.Models
{
    public class Theme
    {
        private readonly Painter painter;
        private readonly double radiusFactor;

        private readonly DrawStyles.ColorPick colorPick;
        private double oldPercent;
        private readonly double quantumColorPick;

        public double QuantumColorPick
        {
            get
            {
                return this.quantumColorPick;
            }
        }

        public double RadiusFactor
        {
            get
            {
                return this.radiusFactor;
            }
        }
        private readonly Tuple<PathStyles, StopStyles, RadiusStyle, ColorStyle> styles;
        public Tuple<PathStyles, StopStyles, RadiusStyle, ColorStyle> Styles
        {
            get
            {
                return this.styles;
            }
        }

        private readonly Gradient colors;
        public IEnumerable<Color> ColorsSrc
        {
            get
            {
                return this.colors.ColorsSrc;
            }
        }

        public Theme(Color[] colorSrc, Tuple<PathStyles, StopStyles, RadiusStyle, ColorStyle> styles, double radiusFactor, double quantumColorPick)
        {
            this.styles = styles;
            this.colors = new Gradient(colorSrc.ThrowIfEmpty());
            this.painter = new Painter(styles.ThrowIfNull());
            this.colorPick = styles.Item4.Delegate().ThrowIfNull();
            this.radiusFactor = radiusFactor;
            this.quantumColorPick = quantumColorPick;
            this.oldPercent = 0.5;
        }

        public Theme(Color colorSrc, Tuple<PathStyles, StopStyles, RadiusStyle, ColorStyle> styles, double radiusFactor, double quantumColorPick)
            : this(new Color[] { colorSrc }, styles, radiusFactor, quantumColorPick)
        { }

        public void DrawMousePath(WriteableBitmap output, Point start, Point stop)
        {
            this.oldPercent = this.colorPick(start, stop, this.oldPercent, this.quantumColorPick);
            //Console.WriteLine("P : " + oldPercent);
            this.painter.DrawPath(output, this.colors.GetColor(this.oldPercent), start, stop);
        }

        public void DrawMouseStop(WriteableBitmap output, Point position, int rawRadius)
        {
            this.painter.DrawStop(output, this.colors.GetColor(this.oldPercent), position, rawRadius, this.radiusFactor);
        }

        public static Theme Default()
        {
            return new Theme(new Color[] { Colors.Red, Colors.Blue, Colors.Green}, Tuple.Create(PathStyles.SimpleLine, StopStyles.Circles, RadiusStyle.SquareRoot, ColorStyle.Continuous), 1.5, 1.1);
        }


    }
}
