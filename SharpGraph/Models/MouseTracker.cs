using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SharpGraph
{
    class MouseTracker
    {
        #region Cursor Position
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }
        #endregion Cursor Position

        private readonly DispatcherTimer timer;

        public delegate void OnMouseMooveHandler(Point oldPosition, Point currentPosition);
        public delegate void OnMouseStillHandler(Point position);

        public event OnMouseMooveHandler OnMouseMoove;
        public event OnMouseStillHandler OnMouseStill;

        private bool isTracking;
        public bool IsTracking
        {
            get
            {
                return this.isTracking;
            } 
        }

        private const double maxDistanceLimiter = 300;
        private static double maxDistance = 50;
        public static double MaxDistance
        {
            get
            {
                return MouseTracker.maxDistance;
            }
            set
            {

                if ((value > MouseTracker.maxDistance) && (value < MouseTracker.maxDistanceLimiter))
                {
                    MouseTracker.maxDistance = value;
                }
            }
        }

        private Point oldPosition;

        public MouseTracker()
        {
            this.timer = new DispatcherTimer(DispatcherPriority.Send);
            this.timer.Tick += new EventHandler(this.OnTimerTick);
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            this.isTracking = false;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            var currentPosition = MouseTracker.GetMousePosition();
            if (currentPosition == this.oldPosition)
            {
                if(this.OnMouseMoove != null)
                {
                    this.OnMouseStill(currentPosition);
                }
            }
            else
            {
                if (this.OnMouseStill != null)
                {
                    this.OnMouseMoove(this.oldPosition, currentPosition);
                }
            }
            this.oldPosition = currentPosition;
        }

        public void Start()
        {
            this.oldPosition = MouseTracker.GetMousePosition();
            this.isTracking = true;
            this.timer.Start();
        }

        public void Stop()
        {
            this.timer.Stop();
            this.isTracking = false;
        }
    }
}
