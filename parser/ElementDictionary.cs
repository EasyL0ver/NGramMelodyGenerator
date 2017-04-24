using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rhythm;

namespace pond_generator.parser
{
    class ElementDictionary
    {
        Dictionary<String, int?> pitchDictionary;
        Dictionary<String, RhythmUnit> rhythmDictionary;
        Dictionary<String, String> additionalSymbolsDictionary;
        String[] ignoredSubstrings;

        public ElementDictionary()
        {
            pitchDictionary = new Dictionary<String, int?>();
            pitchDictionary.Add("c", 40);
            pitchDictionary.Add("d", 41);
            pitchDictionary.Add("e", 42);
            pitchDictionary.Add("f", 43);
            pitchDictionary.Add("g", 44);
            pitchDictionary.Add("a", 45);
            pitchDictionary.Add("b", 46);


            pitchDictionary.Add("r", null);




            rhythmDictionary = new Dictionary<string, RhythmUnit>();
            rhythmDictionary.Add("1", new RhythmUnit(1));
            rhythmDictionary.Add("2", new RhythmUnit(2));
            rhythmDictionary.Add("4", new RhythmUnit(4));
            rhythmDictionary.Add("8", new RhythmUnit(8));
            rhythmDictionary.Add("16", new RhythmUnit(16));
            rhythmDictionary.Add("32", new RhythmUnit(32));
            rhythmDictionary.Add("64", new RhythmUnit(64));

            additionalSymbolsDictionary = new Dictionary<string, String>();
            additionalSymbolsDictionary.Add("is", "TONE_UP");
            additionalSymbolsDictionary.Add("es", "TONE_DOWN");
            additionalSymbolsDictionary.Add(".", "RHYTHM_DOT");
            additionalSymbolsDictionary.Add("~", "RHYTHM_TIE");
            additionalSymbolsDictionary.Add("r", "IS_PAUSE");
            additionalSymbolsDictionary.Add("'", "OCTAVE_UP");
            additionalSymbolsDictionary.Add(",", "OCTAVE_DOWN");
            additionalSymbolsDictionary.Add("6", "IS_64");
            additionalSymbolsDictionary.Add("3", "IS_32");

            ignoredSubstrings =new String[]{">", "<","{","}","\\trill","\\","\\p", "appoggiatura","(",")"
                ,"acciaccatura","[","]","slurSolid","slurDotted","grace","arpeggio","pagina", " s2 " ," s4 "," s8 ", "s16",

                " s32 "," s64","\\p","!","-","\\i"};







        }

        public int? TranslateNoteToMidi(String str)
        {
            int? value = null;
            if (str != null)
            {
                pitchDictionary.TryGetValue(str, out value);
            }
            return value;
        }

        public RhythmUnit TranslateToRhythmUnit(String str)
        {
            RhythmUnit unit = null;
            rhythmDictionary.TryGetValue(str, out unit);
            return unit;
        }

        public String GetOrder(String str)
        {
            String order = null;
            additionalSymbolsDictionary.TryGetValue(str, out order);
            return order;
        }

        public String[] GetIgnoredSubstrings() { return ignoredSubstrings; }
        public String[] GetNoteArray() { return pitchDictionary.Keys.ToArray(); }
        public String[] GetRhythmicValuesArray() { return rhythmDictionary.Keys.ToArray(); }
        public String[] GetKnownSymbols() { return additionalSymbolsDictionary.Keys.ToArray(); }
       


    }
}
