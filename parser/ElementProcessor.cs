using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pond_generator.parser
{
    class ElementProcessor
    {

        private LineParser owner;
        private ElementDictionary dictionary;

        public ElementProcessor(ElementDictionary dictionary)
        {
            //this.owner = owner;
            this.dictionary = dictionary;
        }

        public void SetOwner(LineParser owner) { this.owner = owner; }


        public void Process(String str)
        {
            if (!String.IsNullOrWhiteSpace(str))
            {
                int? currentNote;

                rhythm.RhythmUnit currentRhythmUnit= new rhythm.RhythmUnit(0); ;


                str = RemoveIgnored(str,dictionary.GetIgnoredSubstrings());
                if (!String.IsNullOrWhiteSpace(str))
                {
                    str = str.Trim();
                    String consoleSave = str;
                    //sprawdz nutke 
                    String whichNote = ContainsWhichIndex(str, dictionary.GetNoteArray(), 0);
                    str = DeleteFirst(str, whichNote);
                    String whichRhythmValue = ContainsWhich(str, dictionary.GetRhythmicValuesArray());
                    str = DeleteFirst(str, whichRhythmValue);

                    currentNote = dictionary.TranslateNoteToMidi(whichNote);

                    
                    


                    if (whichRhythmValue != null) { currentRhythmUnit = dictionary.TranslateToRhythmUnit(whichRhythmValue); }
                    else { currentRhythmUnit.SetValue(owner.GetLastRhytmicValue().GetValue()); }
                    if (currentNote == null) currentRhythmUnit.SetIsPause(true);

                    currentRhythmUnit = new rhythm.RhythmUnit(0);


                    if (whichRhythmValue != null) { currentRhythmUnit = dictionary.TranslateToRhythmUnit(whichRhythmValue); }
                    else { currentRhythmUnit = owner.GetLastRhytmicValue(); }


                    //sprawdz rytm
                    while (ContainsWhich(str, dictionary.GetKnownSymbols()) != null)
                    {
                        //sprawdzaj znaki dodatkowe


                        String whichSymbol = ContainsWhich(str, dictionary.GetKnownSymbols());
                        str = DeleteFirst(str, whichSymbol);


                      
                        ExecuteOrder(dictionary.GetOrder(whichSymbol), currentNote, currentRhythmUnit);


                    }
                 
                    if (String.IsNullOrWhiteSpace(str) || String.IsNullOrEmpty(str))
                    {



                        owner.pushToCurrentLists(currentNote, currentRhythmUnit.GetClone());
                    }
                    else
                    {
                        owner.ThrowError();
                        Console.WriteLine("Failed to process element: " + consoleSave);
                    }

                    currentRhythmUnit.Reset();

                    //currentNote = null;
                    //currentRhythmUnit = null;

                    currentNote = null;
                    currentRhythmUnit = null;

                }
               
            }
        }

        private String ContainsWhich(String input,String[] array)
        {
            if (!String.IsNullOrWhiteSpace(input))
            {
                foreach (String s in array) if (input.Contains(s)) return s;
            }
            return null;
        }

        private String RemoveIgnored(String input, String[] array)
        {
            if (!String.IsNullOrWhiteSpace(input))
            {
                foreach (String s in array) input = DeleteFirst(input, s);
            }
            return input;
        }

        private String ContainsWhichIndex(String input, String[] array,int index)
        {
            if (!String.IsNullOrWhiteSpace(input))
            {
                String sub = Char.ToString(input[index]);
                foreach (String s in array) if (sub.Contains(s)) return s;
            }
            return null;
        }

        private String DeleteFirst(String input,String toDelete)
        {
            //wyszukuje pierwszej instancji i usuwa
            if(!String.IsNullOrWhiteSpace(input) && !String.IsNullOrWhiteSpace(toDelete))
            {
                int index = input.IndexOf(toDelete);
                int length = toDelete.Length;
                try
                {
                    String startOfString = input.Substring(0, index);
                    String endOfString = input.Substring(index + length);
                    return startOfString + " " + endOfString;
                }
                catch (ArgumentOutOfRangeException e) { }
            
            }

            return input;
        }



        private void ExecuteOrder(String order,int? currentNote,rhythm.RhythmUnit currentRhythmUnit)

        {
            switch(order)
            {
                case "TONE_UP":
                    if (currentNote != null) currentNote++;
                    break;

                case "TONE_DOWN":
                    if (currentNote != null) currentNote--;
                    break;

                case "RHYTHM_DOT":
                    currentRhythmUnit.IncrementDot();
                    break;

                case "RHYTHM_TIE":
                    currentRhythmUnit.SetIsLegato(true);
                    break;

                case "IS_PAUSE":
                    currentRhythmUnit.SetIsPause(true);
                    break;

                case "OCTAVE_UP":
                    if (currentNote != null) currentNote=currentNote +12;
                    break;

                case "OCTAVE_DOWN":
                    if (currentNote != null) currentNote=currentNote -12;
                    break;

                case "IS_32":
                    currentRhythmUnit.SetValue(32);
                    break;

                case "IS_64":
                    if(currentRhythmUnit.GetValue()==4) currentRhythmUnit.SetValue(64);
                    if (currentRhythmUnit.GetValue() == 1) currentRhythmUnit.SetValue(16);

                    break;

            }
        }
    }
}


