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
        private Theme theme;
        private int stillCounter;

        public Theme Theme
        {
            get
            {
                return this.theme;
            }
            set
            {
                this.theme = value;
            }

        }

        public BitmapSource PaintZoneData
        {
            get
            {
                return this.paintZoneData;
            }
        }

        public bool IsTracking
        {
            get
            {
                return this.tracker.IsTracking;
            }
        }

        public PaintZoneModel(double width, double height)
        {
            this.theme = Theme.Default();
            this.paintZoneData = BitmapFactory.New(width.Round(), height.Round());
            this.tracker = new MouseTracker();
            this.tracker.OnMouseMoove += new MouseTracker.OnMouseMooveHandler(this.HandleMouseMoove);
            this.tracker.OnMouseStill += new MouseTracker.OnMouseStillHandler(this.HandleMouseStill);
            this.stillCounter = 0;
            this.ResetImage();
        }

        public PaintZoneModel() : this(System.Windows.SystemParameters.WorkArea.Width, System.Windows.SystemParameters.WorkArea.Height)
        {}

        private void HandleMouseStill(Point waitPoint)
        {
            this.stillCounter++;
        }

        private void HandleMouseMoove(Point oldPosition, Point currentPosition)
        {
            if (this.stillCounter != 0)
            {
                this.theme.DrawMouseStop(this.paintZoneData, oldPosition, this.stillCounter);
                this.stillCounter = 0;
            }
            this.theme.DrawMousePath(this.paintZoneData, oldPosition, currentPosition);
        }

        public void ResetImage()
        {
            Painter.FillImage(this.paintZoneData, Colors.Transparent);
        }

        public void StartTracking()
        {
            this.tracker.Start();
        }

        public void StopTracking()
        {
            this.tracker.Stop();
        }

    }
}
