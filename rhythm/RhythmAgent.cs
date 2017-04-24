using System;
using pond_generator;
using System.Collections.Generic;




namespace rhythm
{
    public class RhythmAgent
    {
        private Dictionary<int, TimeSignature> signatures;
        private Randomizer randomizer;

        public RhythmAgent()
        {
            randomizer = new Randomizer();
            signatures = new Dictionary<int, TimeSignature>();
        }

        public void PushToRhythms(List<RhythmUnit> input,int timeSignature)
        {
            RhythmSet set = new RhythmSet(input);
            if(!signatures.ContainsKey(timeSignature))
            {
                signatures.Add(timeSignature, new TimeSignature(this));
            }

            TimeSignature k;
            signatures.TryGetValue(timeSignature, out k);

            k.AddRhythmSet(set);
        }


        public RhythmSet GetRandomSet(int timeSignature)
        {
            TimeSignature sig;
            signatures.TryGetValue(timeSignature, out sig);
            return sig.GetRandomSet();
        }

        public void CleanUp()
        {
            
            foreach(KeyValuePair<int,TimeSignature> k in signatures)
            {
                k.Value.CleanUp(k.Key);
            }


        }

        public double GetRoll() { return randomizer.Roll(0,1); }

       



    }
}
