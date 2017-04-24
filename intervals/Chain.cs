using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pond_generator;

namespace intervals
{
    class Chain
    {
        private List<IntervalNode> nodes;
        private Node activeNode;
        private Randomizer randomizer;
        private BigramBuffer buffer;

        //output mode
        private bool boundedMode = true;
        private int count = 0;
        private int upperBound = 4;
        private int lowerBound = -7;


        //for display
        private int lastValue = 0;

        private List<Interval> data;
        

        public Chain(int upperBound,int lowerBound)
        {
            randomizer = new Randomizer();
            nodes = new List<IntervalNode>();
            data = new List<Interval>();
            buffer = new BigramBuffer(this);

            this.upperBound = upperBound;
            this.lowerBound = lowerBound;

        }

        public void NormalizeAll() {foreach (Node n in nodes) n.Normalize();}
        public double GetRoll() { return randomizer.Roll(0, 1); }


        public List<Interval> RollNew(int count)
        {
            NormalizeAll();
            AddOne();
            Run(count, new Interval(0));

            data.RemoveAt(data.Count - 1);
            data.RemoveAt(0);
            data.RemoveAt(0);

            PrintData();

            return data;
        }

        public void Run(int count, Interval startingData)
        {
            this.count = count;
            //wrzuc do datalisty pierwsze wejscie
            activeNode.OnEntry();
            while(count>=0)
            {
                activeNode.RollNext();
                count--;
            }
        }

        public BigramBuffer GetBuffer() { return buffer; }

        public void BigramLearn(int first,int second)
        {
            IntervalNode target = GetNodeByValue(first);
            if (target != null) target.GetEdgeByTarget(second).Increment();
        }

        private IntervalNode GetNodeByValue(int value)
        {
            foreach (IntervalNode a in nodes) if (a.GetInterval().GetValue() == value) return a;
            return null;
        }

        public void SetActiveNode(Node activeNode)
        {
            //dodaj wynik do rejestru
            this.activeNode = activeNode;
            data.Add(activeNode.GetInterval());
            lastValue = lastValue + activeNode.GetInterval().GetValue();
            Console.WriteLine(lastValue);

        }

        public void AddNode(IntervalNode node)
        {
            nodes.Add(node);
        }

        public void ConnectAll()
        {
            foreach(Node n in nodes)
            {
                foreach (Node k in nodes)
                {
                    n.Connect(k);
                }
            }
        }

        public void AddOne()
        {
            foreach(Node n in nodes)
            {
                n.AddOne();
            }
        }

        public bool IsBoundedMode() { return boundedMode; }
        public int GetLowerBound() { return lowerBound; }
        public int GetUpperBound() { return upperBound; }

        public void AddNote(Interval interval)
        {

        }

        // grozna lijka here :/

        public int GetLastValue() { return lastValue; }
        public void SetLastValue(int lastValue) { this.lastValue = lastValue; }


        public void PrintData()
        {

            Console.Write("Intervals are: " + Environment.NewLine);
            data.ForEach(i => Console.WriteLine(i.GetValue()));         

            Console.Write("chalo");
            data.ForEach(i => Console.WriteLine(i.GetValue()));

         
        }
    }
}
