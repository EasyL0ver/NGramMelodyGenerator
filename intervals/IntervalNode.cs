using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intervals
{
    class IntervalNode:Node
    {

        Interval interval;

        public IntervalNode(Interval interval,Chain owner)
        {
            this.interval = interval;
            children = new List<Edge>();
            this.owner = owner;
        }

        public override void Normalize()
        {
            int sum = 0;
            double currentBoundary = 0;
            foreach (Edge e in children)
            {
                if(e.GetIsValid()) sum = sum + e.GetCount();
            }

            foreach (Edge e in children)
            {
                if (e.GetIsValid())
                {
                    e.SetBoundaries(currentBoundary, (currentBoundary + ((double)e.GetCount() / sum)));
                    currentBoundary = currentBoundary + ((double)e.GetCount() / sum);
                    e.SetNormalized(true);
                }
            }
        }

        public Edge GetEdgeByTarget(int interval)
        {
            foreach (Edge e in children)
            {
                if (e.GetTarget().GetInterval().GetValue() == interval) return e;              
            }
            return null;
        }

        public override void OnEntry()
        {
            this.isActive = true;
            owner.SetActiveNode(this);
            InvalidateEdges(owner.GetLowerBound(), owner.GetUpperBound());
            this.Normalize();
        }

        private void InvalidateEdges(int lower,int upper)
        {
            foreach (Edge e in children)
            {
                if ((owner.GetLastValue() + e.GetTarget().GetInterval().GetValue() > upper) || (owner.GetLastValue() + e.GetTarget().GetInterval().GetValue() < lower))
                {
                    e.SetIsValid(false);
                }
            }
        }

        public override Interval GetInterval() { return interval; }
    }
}
