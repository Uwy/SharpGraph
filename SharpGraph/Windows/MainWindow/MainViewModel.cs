using SharpGraph.Models;
using SharpGraph.Windows.ThemeEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SharpGraph.Windows.MainWindow
{
    class MainViewModel : INotifyPropertyChanged
    {
        private readonly PaintZone paintZone;
        private readonly MainCommands commands;

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

        public MainCommands Commands
        {
            get
            {
                return this.commands;
            }
        }

        public BitmapSource PaintZoneImage
        {
            get
            {
                return this.paintZone.PaintZoneData;
            }
        }

        public MainViewModel()
        {
            this.paintZone = new PaintZone();
            this.commands = new MainCommands();
            this.commands.OnSave += this.SaveHandler;
            this.commands.OnStart += this.StartHandler;
            this.commands.OnStop += this.StopHandler;
            this.commands.OnReset += this.ResetHandler;
            this.commands.OnEditTheme += this.EditThemeHandler;

            this.Commands.CanStop += this.CanStopHandler;
            this.Commands.CanStart += this.CanStartHandler;

        }

        private void EditThemeHandler(object parameters)
        {
            var themeEditorViewModel = new ThemeEditorViewModel(this.paintZone.Theme);
            themeEditorViewModel.EditFinished += this.EditFinishedHandler;
            var themeEditor = new ThemeEditor.ThemeEditor(themeEditorViewModel);
            themeEditor.Show();
        }

        private void EditFinishedHandler(Theme sender)
        {
            this.paintZone.Theme = sender;
        }

        private void SaveHandler(object parameter)
        {
            var saveModel = new SaveImageModel(this.PaintZoneImage);
        }

        private void StartHandler(object parameter)
        {
            this.paintZone.StartTracking();
            this.commands.Start.RaiseCanExecuteChanged();
            this.commands.Stop.RaiseCanExecuteChanged();
        }

        private void StopHandler(object parameter)
        {
            this.paintZone.StopTracking();
            this.commands.Start.RaiseCanExecuteChanged();
            this.commands.Stop.RaiseCanExecuteChanged();
        }

        private void ResetHandler(object parameter)
        {
            this.paintZone.ResetImage();
        }

        private bool CanStartHandler(object parameter)
        {

            return !this.paintZone.IsTracking;
        }

        private bool CanStopHandler(object parameter)
        {
            return this.paintZone.IsTracking;
        }
    }
}
