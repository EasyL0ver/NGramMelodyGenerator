using System;
using System.Collections.Generic;


namespace rhythm
{
    public class RhythmSet
    {
        private List<RhythmUnit> set;
        private int count = 1;
        private double upperBound;
        private double lowerBound;

        public RhythmSet(List<RhythmUnit> set)
        {
            this.set = set;
        }

        public RhythmUnit GetUnitWithIndex(int index)
        {
            if (index < set.Count)
            {
                return set[index];
            } else { return null; }
        }

        public bool IsTheSame(RhythmSet checkSet)
        {
            int index = 0;
            while (index<set.Count)
            {
                if (!set[index].IsEqual(checkSet.GetUnitWithIndex(index))) return false;
                index++;
            }
            return true;
           
        }

        public void CheckViabilty(int timeSignature)
        {
            bool viable = true;
            if (set.Count == 0)
            {
                viable = false;
            }
            else
            {
                double tolerance = 1D / 128D;
                double timeSig = (double)timeSignature / 4D;

                double sum = 0D;
                foreach (RhythmUnit r in set)
                {
                    sum = sum + r.GetRealValue();
                }

                if (!(Math.Abs(timeSig - sum) < tolerance)) viable = false;

            }
          
            if (!viable) count = 0;
             
        }

        public RhythmSet GetClone()
        {
            List<RhythmUnit> cloneList = new List<RhythmUnit>();
            foreach (RhythmUnit r in set) cloneList.Add(r.GetClone());
            return new RhythmSet(cloneList);
        }

        public int GetSize()
        {
            return set.Count;
        }

        public bool isEmpty()
        {
            if (set.Count == 0) return true;
            return false;
        }

        public void PausifyLast(int amount)
        {
            if(set.Count>amount)
            {
                for (int i = set.Count - 1; i > set.Count - 1 - amount; i--) set[i].SetIsPause(true);             
            }
        }


        public void SetBoundaries(double lowerBound, double upperBound)
        {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        public void Increment(int i) { count = count + i; }
        public int GetCount() { return count; }

        public List<RhythmUnit> GetUnits() { return set; }

        public void SetUpperBound(double upperBound) { this.upperBound = upperBound; }
        public void SetLowerBound(double lowerBound) { this.lowerBound = lowerBound; }
        public double GetUpperBound() { return upperBound; }
        public double GetLowerBound() { return lowerBound; }

        

    }
}
