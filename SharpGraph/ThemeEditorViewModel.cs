using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SharpGraph
{
    class ThemeEditorViewModel : INotifyPropertyChanged
    {
        private readonly ThemeEditorModel model;
        private ObservableCollection<ColorEditorViewModel> colorsSrc;

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        public ThemeEditorViewModel(Theme current)
        {
            var colorsSrc = current.ColorsSrc.ThrowIfEmpty();
            this.colorsSrc = new ObservableCollection<ColorEditorViewModel>();
            foreach (Color color in colorsSrc)
            {
                this.colorsSrc.Add(new ColorEditorViewModel(color));
            }
            this.model = new ThemeEditorModel(current);

        }

    }
}
