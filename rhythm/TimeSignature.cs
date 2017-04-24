using System;
using System.Collections.Generic;



namespace rhythm
{
    public class TimeSignature
    {
        private List<RhythmSet> sets;
        private RhythmAgent owner;
        private bool isNormalized = false;
      
        public TimeSignature(RhythmAgent owner)
        {
            this.owner = owner;
            sets = new List<RhythmSet>();
        }

        public TimeSignature(RhythmAgent owner,List<RhythmSet> sets)
        {
            this.owner = owner;
            this.sets = sets;
        }


        public void AddRhythmSet(RhythmSet rhythmSet)
        {
            if (IsUnique(rhythmSet)) sets.Add(rhythmSet);
            isNormalized = false;

        }

        private bool IsUnique(RhythmSet set)
        {
            foreach (RhythmSet a in sets)
            {
                if (a.IsTheSame(set))
                {
                    a.Increment(1);
                    return false;
                }
            }
            return true;
        }

        public RhythmSet GetRandomSet()
        {
            if (!isNormalized) Normalize();

            double roll = owner.GetRoll();

            foreach(RhythmSet a in sets)
            {
                if (roll <= a.GetUpperBound() && roll >= a.GetLowerBound()) return a;
            }
            return null;
        }


        public void CleanUp(int timeSignature)
        {
            foreach (RhythmSet s in sets) s.CheckViabilty(timeSignature);
        }


        private void Normalize()
        {
            int overallCount = 0;
            double currentBoundary = 0;

            foreach(RhythmSet a in sets)
            {
                overallCount = overallCount + a.GetCount();
            }

            foreach (RhythmSet a in sets)
            {
                a.SetBoundaries(currentBoundary, (currentBoundary + ((double)a.GetCount() / overallCount)));
                currentBoundary = currentBoundary + ((double)a.GetCount() / overallCount);
            }

            isNormalized = true;

        }
      

        //public int GetTimeSignatureName() { return timeSignatureName; }







    }
}
