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
        private readonly MainCommands mainCommands;

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
            this.mainCommands.OnSave += new MainCommands.CommandHandler(this.SaveHandler);
            this.mainCommands.OnStart += new MainCommands.CommandHandler(this.StartHandler);
            this.mainCommands.OnStop += new MainCommands.CommandHandler(this.StopHandler);
            this.mainCommands.OnReset += new MainCommands.CommandHandler(this.ResetHandler);

            this.MainCommands.CanStop += new MainCommands.CanBeExecutedHandler(this.CanStopHandler);
            this.MainCommands.CanStart += new MainCommands.CanBeExecutedHandler(this.CanStartHandler);

        }

        private void SaveHandler(object parameter)
        {
            var saveModel = new SaveImageModel(this.PaintZoneImage);
        }

        private void StartHandler(object parameter)
        {
            this.paintZoneModel.StartTracking();
            this.mainCommands.Start.RaiseCanExecuteChanged();
            this.mainCommands.Stop.RaiseCanExecuteChanged();
        }

        private void StopHandler(object parameter)
        {
            this.paintZoneModel.StopTracking();
            this.mainCommands.Start.RaiseCanExecuteChanged();
            this.mainCommands.Stop.RaiseCanExecuteChanged();
        }

        private void ResetHandler(object parameter)
        {
            this.paintZoneModel.ResetImage();
        }

        private bool CanStartHandler(object parameter)
        {

            return !this.paintZoneModel.IsTracking;
        }

        private bool CanStopHandler(object parameter)
        {
            return this.paintZoneModel.IsTracking;
        }
    }
}
