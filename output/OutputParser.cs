using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rhythm;
using System.Reflection;

namespace pond_generator.output
{
    class OutputParser
    {
        private String output ="";
        UnitParser unitParser;
        OutputDictionary outputDictionary;
        OutputManager owner;
        int timeSignature = 0;

        public OutputParser(OutputManager owner)
        {
            outputDictionary = new OutputDictionary();
            unitParser = new UnitParser(this);
            this.owner = owner;
        }

        public OutputDictionary GetDictionary() { return outputDictionary; }

        private void AddToOutput(String str) { output = output + str;  }

        public void Parse(int?[] noteValues,List<rhythm.RhythmSet> rhythmValues,int timeSignature)
        {
            int currentPosition = 0;
            this.timeSignature = timeSignature;

            //dodaj naglowek
            //dodaj time
            AddToOutput(Environment.NewLine + "\\relative c'' {"+ outputDictionary.GetHeader(timeSignature) + " | ");
            
            for(int i =0;i<rhythmValues.Count;i++)
            {
                List<RhythmUnit> s = rhythmValues[i].GetUnits();
                foreach(RhythmUnit u in s)
                {
                    if (currentPosition < noteValues.Length)
                    {
                        AddToOutput(unitParser.Parse(noteValues[currentPosition], u));
                    }else
                    {
                        AddToOutput(unitParser.Parse(null, u));
                    }
                    currentPosition++;

                   
                }

                //dodaj koneic wiersza jesli to nie ostatni rythm set
                if(i!=rhythmValues.Count-1) AddToOutput("| ");
                else output = output + "\\bar  \"||\" }";
            }

            System.IO.File.WriteAllText(owner.GetTargetPath(), output);

        }
    }
}
