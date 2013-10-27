using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SharpGraph.Models;

namespace SharpGraph.Windows.ThemeEditor
{
    /// <summary>
    /// Logique d'interaction pour ThemeEditor.xaml
    /// </summary>
    public partial class ThemeEditor : Window
    {
        public ThemeEditor(ThemeEditorViewModel viewModel)
        {

            this.DataContext = viewModel;
            this.InitializeComponent();
        }
    }
}
