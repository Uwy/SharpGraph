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
    class Painter : ICloneable
    {
        #region ICloneable
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        private readonly DrawStyles.DrawPath drawPath;
        private readonly DrawStyles.DrawStop drawStop;
        private readonly DrawStyles.RadiusSize radiusSize;

        public Painter(PathStyles pathStyle, StopStyles stopStyle, RadiusStyles radiusStyle)
        {
            this.drawPath = pathStyle.Delegate().ThrowIfNull();
            this.drawStop = stopStyle.Delegate().ThrowIfNull();
            this.radiusSize = radiusStyle.Delegate().ThrowIfNull();
        }

        public Painter(Tuple<PathStyles, StopStyles, RadiusStyles> styles) 
            : this(styles.Item1, styles.Item2, styles.Item3)
        { }

        public void DrawPath(WriteableBitmap output, Color color, Point start, Point stop)
        {
            this.drawPath(output, color, start, stop);
        }

        public void DrawStop(WriteableBitmap output, Color color, Point position, int rawRadius, double stopFactor)
        {
            this.drawStop(output, color, position, this.radiusSize(rawRadius, stopFactor));
        }

        public static void FillImage(WriteableBitmap output, Color color)
        {
            output.FillRectangle(0, 0, output.Width.Round(), output.Height.Round(), color);
        }
    }
}
