using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rhythm;

namespace pond_generator.output
{
    class OutputDictionary
    {
        private String restSymbol = "r";
        private String dotSymbol = ".";
        private String tieSymbol = "~";
        private String octaveDownSymbol = ",";
        private String octaveUpSymbol = "'";

        private Dictionary<int, String> pitchDictionary;
        private Dictionary<int, String> timeDictionary;
        private Dictionary<int, String> headerDictionary;

        private int middleC = 40;
        private int octavesDown = -3;
        private int octavesUp = 3;

        public OutputDictionary()
        {
            timeDictionary = new Dictionary<int, string>();
            timeDictionary.Add(1, "1");
            timeDictionary.Add(2, "2");
            timeDictionary.Add(4, "4");
            timeDictionary.Add(8, "8");
            timeDictionary.Add(16, "16");
            timeDictionary.Add(32, "32");
            timeDictionary.Add(64, "64");
            timeDictionary.Add(128, "128");
            


            pitchDictionary = new Dictionary<int, string>();

            for (int i = octavesDown; i < octavesUp+1; i++)
            {
                pitchDictionary.Add(40 + (12 * i), "c" + pitchAdd(i));
                pitchDictionary.Add(41 + (12 * i), "cis" + pitchAdd(i));
                pitchDictionary.Add(42 + (12 * i), "d" + pitchAdd(i));
                pitchDictionary.Add(43 + (12 * i), "dis" + pitchAdd(i));
                pitchDictionary.Add(44 + (12 * i), "e" + pitchAdd(i));
                pitchDictionary.Add(45 + (12 * i), "f" + pitchAdd(i));
                pitchDictionary.Add(46 + (12 * i), "fis" + pitchAdd(i));
                pitchDictionary.Add(47 + (12 * i), "g" + pitchAdd(i));
                pitchDictionary.Add(48 + (12 * i), "gis" + pitchAdd(i));
                pitchDictionary.Add(49 + (12 * i), "a" + pitchAdd(i));
                pitchDictionary.Add(50 + (12 * i), "ais" + pitchAdd(i));
                pitchDictionary.Add(51 + (12 * i), "b" + pitchAdd(i));
            }

            headerDictionary = new Dictionary<int, string>();
            headerDictionary.Add(3, "\\time 3/4");
        }

        public String TranslatePitch(int i)
        {
            String value;
            pitchDictionary.TryGetValue(i, out value);
            return value;
        }

        public String TranslateTime(RhythmUnit unit)
        {
            String value;
            timeDictionary.TryGetValue(unit.GetValue(), out value);
            for (int i = 0; i < unit.GetIsDotted();i++)
            {
                value = value + dotSymbol;
            }

            if (unit.GetIsLegato()) value = value + tieSymbol;


            return value;

        }

        public String GetHeader(int index)
        {
            String value;
            headerDictionary.TryGetValue(index, out value);
            return value;
        }
        private String pitchAdd(int i)
        {
            String symbol;
            if (i > 0) symbol = "'";
            if (i < 0) symbol = ",";
            symbol = "";

            String output = "";
            for(int o=0;o<Math.Abs(i);o++)
            {
                output = output + symbol;
            }
           
            return output.Trim();
            
        }
       
        public String GetRestSymbol() { return restSymbol; }
    }
}
