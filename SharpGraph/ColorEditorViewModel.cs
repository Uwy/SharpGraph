using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SharpGraph
{
    class ColorEditorViewModel
    {
        private const int sizeImage = 100;
        private const string editLabel = "Edit";
        private const string deleteLabel = "X";
       
        private readonly WriteableBitmap image;
        public BitmapSource Image 
        {
            get
            {
                return this.image;
            }
        }

        public string EditLabel
        {
            get
            {
                return ColorEditorViewModel.editLabel;
            }
        }

        public string DeleteLabel
        {
            get
            {
                return ColorEditorViewModel.deleteLabel;
            }
        }

        public ColorEditorViewModel(Color color)
        {
            this.image = BitmapFactory.New(ColorEditorViewModel.sizeImage, ColorEditorViewModel.sizeImage);
        }
    }
}
