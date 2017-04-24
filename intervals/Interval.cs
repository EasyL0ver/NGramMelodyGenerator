using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intervals
{
    class Interval
    {
        private int value = 0;
        private bool isPause = false;

        public Interval(int value)
        {
            this.value = value;
        }

        public Interval(bool isPause)
        {
            this.isPause = isPause;
        }

        public int GetValue() { return value; }
        public bool GetIsPause() { return isPause; }



    }
}
