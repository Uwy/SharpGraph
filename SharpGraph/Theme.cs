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
    class Theme : ICloneable
    {
        #region ICloneable
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        private readonly Gradient colors;
        private readonly Painter painter;
        private readonly double radiusFactor;

        public IEnumerable<Color> ColorsSrc
        {
            get
            {
                return this.colors.ColorsSrc;
            }
        }

        public Theme(Color[] colorSrc, Tuple<PathStyles, StopStyles, RadiusStyles> styles, double radiusFactor)
        {
            this.colors = new Gradient(colorSrc.ThrowIfEmpty());
            this.painter = new Painter(styles.ThrowIfNull());
            this.radiusFactor = radiusFactor;
        }

        public Theme(Color colorSrc, Tuple<PathStyles, StopStyles, RadiusStyles> styles, double radiusFactor)
        {
            this.colors = new Gradient(colorSrc);
            this.painter = new Painter(styles);
            this.radiusFactor = radiusFactor;
        }

        public void DrawMousePath(WriteableBitmap output, Point start, Point stop)
        {
            this.painter.DrawPath(output, this.colors.GetColor(0.5), start, stop);
        }

        public void DrawMouseStop(WriteableBitmap output, Point position, int rawRadius)
        {
            this.painter.DrawStop(output, this.colors.GetColor(0.5), position, rawRadius, this.radiusFactor);
        }

        public static Theme Default()
        {
            return new Theme(Colors.Black, Tuple.Create(PathStyles.SimpleLine, StopStyles.Circles, RadiusStyles.SquareRoot), 1.5);
        }


    }
}
