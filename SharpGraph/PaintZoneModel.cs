using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SharpGraph
{
    class PaintZoneModel
    {
        private readonly WriteableBitmap paintZoneData;
        private readonly MouseTracker tracker;
        private int stillCounter;
        private Gradient colors;

        public BitmapSource PaintZoneData
        {
            get
            {
                return this.paintZoneData;
            }
        }


        public PaintZoneModel(double width, double height)
        {
            this.paintZoneData = BitmapFactory.New(width.Round(), height.Round());
            this.FillImage(Colors.Transparent);
            this.colors = new Gradient(Colors.Black);
            this.tracker = new MouseTracker();
            this.stillCounter = 0;
            this.tracker.OnMouseMoove += new MouseTracker.OnMouseMooveHandler(this.HandleMouseMoove);
            this.tracker.OnMouseStill += new MouseTracker.OnMouseStillHandler(this.HandleMouseStill);
            this.tracker.Start();
        }

        public PaintZoneModel() : this(System.Windows.SystemParameters.WorkArea.Width, System.Windows.SystemParameters.WorkArea.Height)
        {}

        private void DrawLine(Tuple<Point, Point> drawInformation)
        {
            this.DrawLine(drawInformation.Item1, drawInformation.Item2);        
        }

        private void DrawLine(Point start, Point stop)
        {
            //this.paintZoneData.DrawLine(start.X.Round() + 10, start.Y.Round() + 10, stop.X.Round() + 10, stop.Y.Round() + 10, this.colors.GetColor(0));
            this.paintZoneData.DrawLineAa(start.X.Round(), start.Y.Round(), stop.X.Round(), stop.Y.Round(), this.colors.GetColor(0.5));
            //this.paintZoneData.DrawLineBresenham(start.X.Round() - 10, start.Y.Round() - 10, stop.X.Round() - 10, stop.Y.Round() - 10, this.colors.GetColor(1));
            //this.paintZoneData.DrawLineDDA(start.X.Round(), start.Y.Round(), stop.X.Round(), stop.Y.Round(), this.colors.GetColor(0));
        }

        private void FillEllipse(Point start)
        {
            //TODO
            this.paintZoneData.FillEllipseCentered(start.X.Round(), start.Y.Round(), Math.Sqrt(this.stillCounter * 4).Round(), Math.Sqrt(this.stillCounter * 4).Round(), this.colors.GetColor(0.5));
        }

        private void FillImage(Color color)
        {
            this.paintZoneData.FillRectangle(0, 0, this.paintZoneData.PixelWidth, this.paintZoneData.PixelHeight, color);
        }

        private void HandleMouseStill(Point waitPoint)
        {
            this.stillCounter++;
        }



        private void HandleMouseMoove(Point oldPosition, Point currentPosition)
        {
            if (this.stillCounter != 0)
            {
                this.FillEllipse(oldPosition);
                this.stillCounter = 0;
            }
            this.DrawLine(oldPosition, currentPosition);
        }



    }
}
