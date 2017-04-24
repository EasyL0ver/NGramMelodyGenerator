using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using intervals;
using rhythm;
using pond_generator.parser;
using pond_generator.output;


using System.IO;

namespace pond_generator
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.SetOut(TextWriter.Null);
            //Console.SetError(TextWriter.Null);

            

            ElementDictionary dictionary = new ElementDictionary();

            Chain noteTarget = new Chain(12,-12);

            IntervalNodeFactory factory = new IntervalNodeFactory(-12, 12, false, noteTarget, 0);
            RhythmAgent rhythmTarget = new RhythmAgent();
            ElementProcessor processor = new ElementProcessor(dictionary);

            //string lines = System.IO.File.ReadAllText(@"C:\Users\Kasia\Desktop\Mozart-Concerto-lys\elo2.ly");


            FileReader reader = new FileReader(dictionary, processor, noteTarget, rhythmTarget,               
                "naglowek|r2 d4 |r8 ais8 c4 d4|r2 a4|nic to nie ma");

            

            
            rhythmTarget.CleanUp();
            int timeSignature = 3;
            string targetPath = @"D:\final.ly";
            OutputManager outputManager = new OutputManager(noteTarget, rhythmTarget,targetPath);
            outputManager.Run(50, timeSignature, 40);


            

            Console.ReadLine();
        }
    }
}
