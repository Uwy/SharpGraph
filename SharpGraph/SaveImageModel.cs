using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SharpGraph
{
    class SaveImageModel
    {
        private readonly string fileName;
        private readonly BitmapSource imageToSave;

        public SaveImageModel(BitmapSource imageToSave)
        {
            this.imageToSave = imageToSave.ThrowIfNull();
            var saveDialog = new SaveFileDialog();
            saveDialog.FileName = "SharpGraph " + System.DateTime.Now.ToString().Replace(new char[] { '/', ':' }, '-');
            saveDialog.DefaultExt = ".png";
            saveDialog.Filter = "PNG Image Files (.png)|*.png";
            var result = saveDialog.ShowDialog();
            if (result == true)
            {
                Console.WriteLine(saveDialog.FileName);
                this.fileName = saveDialog.FileName;
                this.Save();
            }
        }

        private void Save()
        {
            BitmapEncoder encoder;
            switch(this.fileName.Substring(this.fileName.Length - 3))
            {
                case "bmp":
                    encoder = new BmpBitmapEncoder();
                    break;
                case "png":
                    encoder = new PngBitmapEncoder();
                    break;
                case "jpeg":
                case "jpg":
                    encoder = new JpegBitmapEncoder();
                    break;
                default:
                    encoder = new PngBitmapEncoder();
                    break;
            }
            this.SaveUsingEncoder(encoder);
        }

        private void SaveUsingEncoder(BitmapEncoder encoder)
        {
            encoder.Frames.Add(BitmapFrame.Create(this.imageToSave));
            using (var stream = File.Create(this.fileName))
            {
                encoder.Save(stream);
            }
        }
    }
}
