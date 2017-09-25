using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace rnnlib
{
    public class FeatureLoader
    {
        public void loadPicture(string path, NNUnit nnunit)
        {
            Bitmap image = new Bitmap(path);
            int [] buffer = imageToBytes(image);
            nnunit.InputValues = normalize(buffer);

        }

        private int[] imageToBytes(Bitmap image)
        {
            int [] pixelValues = new int[image.Width * image.Height];
            int dataIndex = 0;
            // Do some processing
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    pixelValues[dataIndex++] = pixelColor.R * 0xFFFF + pixelColor.G * 0xFF + pixelColor.B;
                }
            }

            return pixelValues;
        }

        public double[] normalize(int [] data) {
            double[] normalizedData = new double[data.Length];

            double max = data.Max();
            double min = data.Min();
            double amplitude = max - min;

            int index = 0;
            foreach(int dataByte in data) 
            {
                normalizedData[index++] = (double)dataByte / amplitude + min;
            }

            return normalizedData;
        }

        public string getDownloadFolderPath()
        {
            return Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty).ToString() + "\\";
        }
    }
}
