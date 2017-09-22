using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rnnlib
{
    public class RandomManager
    {
        Random rnd = null;

        public RandomManager()
        {
            rnd = new Random(DateTime.Now.TimeOfDay.Milliseconds);
        }

        public int getRandom(int max)
        {
            return rnd.Next(max);
        }
    }
}
