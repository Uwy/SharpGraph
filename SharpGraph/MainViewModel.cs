using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SharpGraph
{
    class MainViewModel : INotifyPropertyChanged
    {
        private readonly PaintZoneModel paintZoneModel;

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged

        private readonly MainCommands mainCommands;
        public MainCommands MainCommands
        {
            get
            {
                return this.mainCommands;
            }
        }

        public BitmapSource PaintZoneImage
        {
            get
            {
                return this.paintZoneModel.PaintZoneData;
            }
        }


        public MainViewModel()
        {
            this.paintZoneModel = new PaintZoneModel();
            this.mainCommands = new MainCommands();
        }

    }
}
