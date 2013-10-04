using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SharpGraph
{

    public class Gradient
    {
        private readonly List<Color> colorsSrc;

        public Gradient(IEnumerable<Color> colorSrc)
        {
            this.colorsSrc = colorSrc.ThrowIfEmpty().ToList();
            while(this.colorsSrc.Count < 2)
            {
                this.colorsSrc.Add(this.colorsSrc.First());
            }
        }

        public Gradient(Color color)
            : this(new[] { color, color })
        {
        }

        public Color GetColor(double percentValue)
        {
            var positionGradient = (this.colorsSrc.Count() - 1) * percentValue.ThrowIfNotPercentage();
            var monoGradientSrc = Tuple.Create
                (
                    this.colorsSrc[(int)positionGradient],
                    this.colorsSrc[Math.Min((int)positionGradient + 1, this.colorsSrc.Count - 1)]
                );
            return Gradient.GetColorFromGradient(monoGradientSrc, positionGradient % 1);
        }

        private static Color GetColorFromGradient(Tuple<Color, Color> gradientSrc, double percentValue)
        {
            return Color.FromArgb(gradientSrc.Item1.A.ValueAtPercentWayToEnd(percentValue, gradientSrc.Item2.A),
                                  gradientSrc.Item1.R.ValueAtPercentWayToEnd(percentValue, gradientSrc.Item2.R),
                                  gradientSrc.Item1.G.ValueAtPercentWayToEnd(percentValue, gradientSrc.Item2.G),
                                  gradientSrc.Item1.B.ValueAtPercentWayToEnd(percentValue, gradientSrc.Item2.B));
        }

        private static Color GetColorFromGradient(Color start, Color end, double percentValue)
        {
            return Gradient.GetColorFromGradient(Tuple.Create(start, end), percentValue);
        }

    }
}
