using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pond_generator
{
    class Randomizer
    {
        Random r;

        public Randomizer()
        {
            r = new Random();
        }
        public double Roll(double lower,double upper)
        {
            //TODO
            return r.NextDouble();

        }
    }
}
