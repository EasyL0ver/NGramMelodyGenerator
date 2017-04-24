using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rhythm;

namespace pond_generator.parser
{
    class LineParser
    {
        
        private List<int?> currentNoteList;
        private List<rhythm.RhythmUnit> currentRhythmList;
        private FileReader owner;
        bool isCorrect;

        public LineParser(FileReader owner)
        {
            this.owner = owner;
        }
        

        public void ProcessLine (String line,int timeSignature)
        {
            isCorrect = true;
            Console.WriteLine("parsing line " + line.Trim());
            currentNoteList = new List<int?>();
            currentRhythmList = new List<rhythm.RhythmUnit>();


            String[] elements = SplitLine(RemoveChords(line));
            
            foreach(String a in elements)
            {
                owner.GetProcessor().Process(a);
            }


            if (isCorrect) PushToTargets();
            else
            {
                Console.WriteLine("Failed to load the line");
                owner.GetNoteTarget().GetBuffer().Reset();
            }


            
        }

        private void PushToTargets()
        {
            Console.WriteLine("Line correct");
            owner.GetRhythmTarget().PushToRhythms(currentRhythmList, owner.GetTimeSignature());
            owner.GetNoteTarget().GetBuffer().PushList(currentNoteList);
        }

        private string[] SplitLine (String line)
        {
            char[] delimiterChars = { ' ' };
            return line.Split(delimiterChars);
        }

        public void ThrowError()
        {
            isCorrect = false;
            Console.WriteLine("line error");
        }

        private String RemoveChords(String str)
        {

            while(true)
            {
                String[] output = str.Split('<', '>');
                if (output.Length == 1)
                {
                    return str;
                }else
                {
                    try
                    {
                        output[1] = RemovePoly(output[1]);
                        str = String.Join(output[0], output[1], output[2]);
                    }catch(IndexOutOfRangeException e)
                    {
                        
                    }
                }

            }

        }

        private String RemovePoly(String str)
        {
            String[] output = str.Split(' ', ' ');
            if(output.Length>=2) return output[1];
            return null;
        }
        
        public rhythm.RhythmUnit GetLastRhytmicValue()
        {
            if (currentRhythmList.Count == 0) return new RhythmUnit(4,0,false,false);
            return currentRhythmList[currentRhythmList.Count - 1];
        }
        public void pushToCurrentLists(int? note,rhythm.RhythmUnit rhythmUnit)
        {
            currentNoteList.Add(note);
            currentRhythmList.Add(rhythmUnit);
        }

     


    }
}
