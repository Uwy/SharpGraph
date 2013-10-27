using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SharpGraph.Extensions
{
    public static class ImageExtensions
    {

        public static WriteableBitmap FillImage(this WriteableBitmap output, Color color)
        {
            output.FillRectangle(0, 0, output.Width.Round(), output.Height.Round(), color);
            return output;
        }
    }
}
