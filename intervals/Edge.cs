using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intervals
{
    class Edge
    {
        private int count=0;
        private Node origin;
        private Node target;
        private bool isNormalized = false;
        private bool isValid = true;

        double lowerBoundary;
        double upperBoundary;

        public Edge(Node origin,Node target)
        {
            this.origin = origin;
            this.target = target;
        }

        public int GetCount(){return count;}
        public void Increment(){count++;} 

        public void SetBoundaries(double lowerBoundary,double upperBoundary)
        {
            this.lowerBoundary = lowerBoundary;
            this.upperBoundary = upperBoundary;
        }
        public void SetNormalized(bool isNormalized)
        {
            this.isNormalized = isNormalized;
        }

        public bool IsNormalized() { return isNormalized; }
        
        public bool CheckRoll(double roll)
        {
            if (roll >= lowerBoundary && roll <= upperBoundary) return true;
            return false;
        }

        public Node GetTarget() { return target; }
        public bool GetIsValid() { return isValid;}
        public void SetIsValid(bool isValid) { this.isValid = isValid; }
        


    }
}
