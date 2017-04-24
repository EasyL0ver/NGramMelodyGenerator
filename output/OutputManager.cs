using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rhythm;
using intervals;

namespace pond_generator.output
{
   
    class OutputManager
    {
        Chain intervalSource;
        RhythmAgent rhythmSource;
        OutputParser outputParser;
        string targetPath;
        int index = 0;


        public OutputManager(Chain intervalSource,RhythmAgent rhythmSource,string targetPath)
        {
            this.intervalSource = intervalSource;
            this.rhythmSource = rhythmSource;
            this.targetPath = targetPath;
            outputParser = new OutputParser(this);
        }

        public String GetTargetPath() { return targetPath; }



        public void Run(int index,int timeSignature,int firstNote)
        {
            this.index = index;

            List<RhythmSet> sets = new List<RhythmSet>();
            List<Interval> intervals = intervalSource.RollNew(index-1);

            //interval to note values
            int?[] noteValues = new int?[index];
            noteValues[0] = firstNote;
            for(int i =1;i<index;i++)
            {
                if (intervals[i - 1].GetIsPause()) noteValues[i] = null;
                else noteValues[i] = noteValues[i - 1] + intervals[i - 1].GetValue();             
            }

            while(index>0)
            {
                //musi dawac kopie !
                RhythmSet set = rhythmSource.GetRandomSet(timeSignature).GetClone();
                sets.Add(set);
                index = index - set.GetSize();
            }

            sets[sets.Count-1].PausifyLast(Math.Abs(index));


            outputParser.Parse(noteValues, sets,timeSignature);



            //rub funkcje



            index = 0;

        }



    }
}
