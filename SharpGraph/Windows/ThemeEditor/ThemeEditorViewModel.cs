using SharpGraph.Models;
using SharpGraph.UserControls.ColorEditor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGraph.Extensions;
using SharpGraph.ViewModelUtilities;
using System.Windows;
using System.Windows.Media;

namespace SharpGraph.Windows.ThemeEditor
{
    public class ThemeEditorViewModel : INotifyPropertyChanged
    {
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

        public event EventHandlerSender<Theme> EditFinished;

        private readonly ObservableCollection<ColorEditorViewModel> colors;
        public ObservableCollection<ColorEditorViewModel> Colors
        {
            get
            {
                return this.colors;
            }
        }

        #region Styles
        private Tuple<PathStyles, StopStyles, RadiusStyle, ColorStyle> styles;
        public string PathStyle
        {
            get
            {
                return this.styles.Item1.Name();
            }
            set
            {
                this.styles = this.styles.AssignToCopy(value.PathStyleFromName());
                this.OnPropertyChanged("PathStyle");
            }

        }
        public string StopStyle
        {
            get
            {
                return this.styles.Item2.Name();
            }
            set
            {
                this.styles = this.styles.AssignToCopy(value.StopStyleFromName());
                this.OnPropertyChanged("StopStyle");
            }

        }
        public string RadiusStyle
        {
            get
            {
                return this.styles.Item3.Name();
            }
            set
            {
                this.styles = this.styles.AssignToCopy(value.RadiusStyleFromName());
                this.OnPropertyChanged("RadiusStyle");
            }
        }
        public string ColorStyle
        {
            get
            {
                return this.styles.Item4.Name();
            }
            set
            {
                this.styles = this.styles.AssignToCopy(value.ColorStyleFromName());
                this.OnPropertyChanged("ColorStyle");
            }
        }
        public List<string> StopStyles
        {
            get
            {
                return DrawStyles.StopStylesToNameList();
            }
        }
        public List<string> PathStyles
        {
            get
            {
                return DrawStyles.PathStylesToNameList();
            }
        }
        public List<string> RadiusStyles
        {
            get
            {
                return DrawStyles.RadiusStylesToNameList();
            }
        }
        public List<string> ColorStyles
        {
            get
            {
                return DrawStyles.ColorStylesToNameList();
            }
        }
        #endregion

        private double radiusFactor;
        public double RadiusFactor
        {
            get
            {
                return this.radiusFactor;
            }
            set
            {
                this.radiusFactor = value;
                this.OnPropertyChanged("RadiusFactor");
            }
        }

        private double quantumColor;
        public double QuantumColor
        {
            get
            {
                return this.quantumColor;
            }
            set
            {
                this.quantumColor = value;
                this.OnPropertyChanged("QuantumColor");
            }
        }

        private readonly ThemeEditorCommands commands;
        public ThemeEditorCommands Commands
        {
            get
            {
                return this.commands;
            }
        }

        public ThemeEditorViewModel(Theme currentTheme)
        {
            this.quantumColor = currentTheme.QuantumColorPick;
            this.radiusFactor = currentTheme.RadiusFactor;
            this.colors = new ObservableCollection<ColorEditorViewModel>();
            foreach (var color in currentTheme.ColorsSrc)
            {
                var colorEditor = new ColorEditorViewModel(color);
                colorEditor.OnDown += this.OnDownHandler;
                colorEditor.OnUp += this.OnUpHandler;
                colorEditor.OnDelete += this.OnDeleteHandler;
                this.colors.Add(colorEditor);
            }
            this.ResetColorEditorsProperties();
            this.styles = currentTheme.Styles;
            this.commands = new ThemeEditorCommands();
            this.commands.OnOk += this.OkHandler;
            this.commands.OnSave += this.SaveHandler;
            this.commands.OnCancel += this.CancelHandler;
            
        }

        private void ResetColorEditorsProperties()
        {
            var canDelete = this.colors.Count > 2;
            foreach(var colorEditor in this.colors)
            {
                colorEditor.CanDown = true;
                colorEditor.CanUp = true;
                colorEditor.CanDelete = canDelete;
            }
            this.colors[0].CanUp = false;
            this.colors[this.colors.Count - 1].CanDown = false;
        }

        private void OnUpHandler(ColorEditorViewModel sender)
        {
            var index = this.colors.IndexOf(sender);
            if(index.Between(0, this.colors.Count, false))
            {
                this.colors.Move(index, index - 1);
            }
            this.ResetColorEditorsProperties();
        }

        private void OnDownHandler(ColorEditorViewModel sender)
        {
            var index = this.colors.IndexOf(sender);
            if (index.Between(-1, this.colors.Count - 1, false))
            {
                this.colors.Move(index, index + 1);
            }
            this.ResetColorEditorsProperties();
        }

        private void OnDeleteHandler(ColorEditorViewModel sender)
        {
            if (this.colors.Count > 2)
            {
                this.colors.Remove(sender);
            }
            this.ResetColorEditorsProperties();
        }

        private void CancelHandler(object parameters)
        {
            var window = parameters as Window;
            if (window != null)
            {
                this.CloseWindow(window);
            }
            else
            {
                Console.WriteLine("window is null");
            }
        }

        private void SaveHandler(object parameters)
        {
            var colors = new List<Color>();
            foreach (var colorEditor in this.colors)
            {
                colors.Add(colorEditor.Color);
            }
            this.EditFinished(new Theme(colors.ToArray(), this.styles, this.radiusFactor, this.quantumColor));
        }

        private void OkHandler(object parameters)
        {
            this.SaveHandler(parameters);
            this.CancelHandler(parameters);
        }

        private void CloseWindow(Window window)
        {
            window.ThrowIfNull().Close();
        }
    }
}
