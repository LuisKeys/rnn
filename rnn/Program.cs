using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using rnnlib;

namespace rnn
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create RNN
            NNUnit unit = new NNUnit(10000, 4096, 2, 100);

            //Load a picture
            FeatureLoader fl = new FeatureLoader();
            string file = fl.getDownloadFolderPath() + "perro0001.bmp";
            fl.loadPicture(file, unit);

            Calculations calc = new Calculations();
            calc.forwardProp(unit);

            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }
    }
}
