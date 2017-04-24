using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using intervals;
using rhythm;

namespace pond_generator.parser
{
    class FileReader
    {
        private ElementDictionary dictionary;
        private ElementProcessor processor;
        private LineParser lineParser;
        private Chain noteTarget;
        private RhythmAgent rhythmTarget;

        private String file;

        public FileReader(ElementDictionary dictionary,ElementProcessor processor,Chain noteTarget,RhythmAgent rhythmTarget,String file)
        {
            this.dictionary = dictionary;
            this.processor = processor;
            this.noteTarget = noteTarget;
            this.rhythmTarget = rhythmTarget;
            this.file = file;

            lineParser = new LineParser(this);
            processor.SetOwner(lineParser);

            if(file!=null) ParseFile();
        }

        ~FileReader()
        {
            noteTarget.GetBuffer().Reset();
        }

        private void ParseFile()
        {
            int timeSignature = GetTimeSignature();
       

            String[] output = file.Split('|', '|');
            for (int i = 1;i<=(output.Length-1);i++)
            {
                lineParser.ProcessLine(output[i],timeSignature);
            }
        }

        public int GetTimeSignature()
        {
            //String[] output = file.Split('\\time', '\\');
            //todo

            return 3;
        }


        public ElementProcessor GetProcessor() { return processor; }
        public ElementDictionary GetDictionary() { return dictionary; }
        public Chain GetNoteTarget() { return noteTarget; }
        public RhythmAgent GetRhythmTarget() { return rhythmTarget; }
    }
}
