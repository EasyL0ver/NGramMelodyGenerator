using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intervals
{
    abstract class Node
    {

        protected Chain owner;
        protected List<Edge> children;
        protected bool isActive = false;


        // content

     

        public void RollNext()
        {

            {
                double randomRoll = owner.GetRoll();
                Edge winner = null;
                foreach (Edge e in children)
                {
                    if (e.CheckRoll(randomRoll) && e.GetIsValid())
                    {
                        winner = e;
                        break;
                    }
                }


                winner.GetTarget().OnEntry();
                isActive = false;


            }           
        }

        public virtual void OnEntry() { }

        public void setActive(bool isActive)
        {
            this.isActive = isActive;
        }
       
        public virtual void Normalize()  { }

        public void AddOne()
        {
            foreach (Edge e in children) if(e.GetIsValid()) e.Increment();
        }

        public void AddChildren(Edge child)
        {
            children.Add(child);
        }

        public void Connect(Node target)
        {
            Edge edge = new Edge(this, target);
            AddChildren(edge);

        }

        public abstract Interval GetInterval();

    }
}
