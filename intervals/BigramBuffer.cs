using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intervals
{
    class BigramBuffer
    {
        Chain owner;
        int?[] buffer;

        public BigramBuffer(Chain owner)
        {
            this.owner = owner;
            buffer = new int?[3];
        }
        public void Reset() { buffer = new int?[3]; }
        public void PushList(List<int?> list) { foreach (int? i in list) Push(i); }

        private void Push(int? number)
        {
            buffer[2] = buffer[1];
            buffer[1] = buffer[0];
            buffer[0] = number;

            if (isValid()) owner.BigramLearn((int)(buffer[1]-buffer[2]),(int)(buffer[0]-buffer[1]));
        }

        private bool isValid()
        {
            bool valid = true;
            foreach(int? i in buffer) if (i == null) valid = false;
            return valid;
            
        }
    }
}
