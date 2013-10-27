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
    public class Painter
    {
        private readonly DrawStyles.DrawPath drawPath;
        private readonly DrawStyles.DrawStop drawStop;
        private readonly DrawStyles.RadiusSize radiusSize;

        public Painter(PathStyles pathStyle, StopStyles stopStyle, RadiusStyle radiusStyle)
        {
            this.drawPath = pathStyle.Delegate().ThrowIfNull();
            this.drawStop = stopStyle.Delegate().ThrowIfNull();
            this.radiusSize = radiusStyle.Delegate().ThrowIfNull();
        }

        public Painter(Tuple<PathStyles, StopStyles, RadiusStyle, ColorStyle> styles) 
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
    }
}
