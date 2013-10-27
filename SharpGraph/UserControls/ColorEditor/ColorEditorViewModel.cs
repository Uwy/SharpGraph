using SharpGraph.ViewModelUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SharpGraph.Extensions;
using Xceed.Wpf.Toolkit;

namespace SharpGraph.UserControls.ColorEditor
{


    public class ColorEditorViewModel : INotifyPropertyChanged
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

        public event EventHandlerSender<ColorEditorViewModel> OnUp;
        public event EventHandlerSender<ColorEditorViewModel> OnDown;
        public event EventHandlerSender<ColorEditorViewModel> OnDelete;

        private Color color;
        public Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
                this.OnPropertyChanged("ColorImage");
            }
        }
        private bool canUp;
        public bool CanUp
        {
            get
            {
                return this.canUp;
            }
            set
            {
                this.canUp = value;
                this.commands.Up.RaiseCanExecuteChanged();
            }
        }

        private bool canDown;
        public bool CanDown
        {
            get
            {
                return this.canDown;
            }
            set
            {
                this.canDown = value;
                this.commands.Down.RaiseCanExecuteChanged();
            }
        }

        private bool canDelete;
        public bool CanDelete
        {
            get
            {
                return this.canDelete;
            }
            set
            {
                this.canDelete = value;
                this.commands.Delete.RaiseCanExecuteChanged();
            }
        }

        private readonly ColorEditorCommands commands;
        public ColorEditorCommands Commands
        {
            get
            {
                return this.commands;
            }
        }


        public ColorEditorViewModel(Color color)
        {
            this.canUp = true;
            this.canDown = true;
            this.color = color;
            this.commands = new ColorEditorCommands();
            this.commands.CanDown += new CanBeExecutedHandler(param => this.canDown);
            this.commands.CanUp += new CanBeExecutedHandler(param => this.canUp);
            this.commands.CanDelete += new CanBeExecutedHandler(param => this.canDelete);
            this.commands.OnDelete += this.OnDeleteHandler;
            this.commands.OnDown += this.OnDownHandler;
            this.commands.OnUp += this.OnUpHandler;
        }

        public void OnDeleteHandler(object param)
        {
            if (this.OnDelete != null)
            {
                this.OnDelete(this);
            }
        }

        public void OnUpHandler(object param)
        {
            if (this.OnUp != null)
            {
                this.OnUp(this);
            }
        }

        public void OnDownHandler(object param)
        {
            if (this.OnDown != null)
            {
                this.OnDown(this);
            }
        }
    }
}
