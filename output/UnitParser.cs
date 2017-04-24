using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rhythm;

namespace pond_generator.output
{
    class UnitParser
    {
        OutputParser owner;
        OutputDictionary dictionary;

        public UnitParser(OutputParser owner)
        {
            this.owner = owner;
            dictionary = owner.GetDictionary();
        }

        public String Parse(int? noteValue,RhythmUnit rhythmUnit)
        {
            String pitch;
            String time;

            if(noteValue==null || rhythmUnit.GetIsPause())
            {
                pitch = dictionary.GetRestSymbol();
            }
            else
            {
                pitch = dictionary.TranslatePitch((int)noteValue);
            }


            time = dictionary.TranslateTime(rhythmUnit);

            return pitch + time + " ";

        }
    }
}
