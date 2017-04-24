using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intervals
{
    class IntervalNodeFactory
    {
        private int lower;
        private int upper;
        private bool pauseIncluded;
        private Chain owner;
        private int startingNode;


        public IntervalNodeFactory(int lower,int upper,bool pauseIncluded, Chain owner, int startingNode)
        {
            this.lower = lower;
            this.upper = upper;
            this.pauseIncluded = pauseIncluded;
            this.owner = owner;
            this.startingNode = startingNode;

            Deploy();
        }

        public void Deploy()
        {
            Create();
            owner.ConnectAll();
            //buduje
        }

        private void Create()
        {
            for(int i=lower;i<upper;i++)
            {
                IntervalNode node = new IntervalNode(new Interval(i),owner);
                if (i == startingNode)
                {
                    node.setActive(true);
                    owner.SetActiveNode(node);
                }

                owner.AddNode(node);
            }
        }

       
    }
}
