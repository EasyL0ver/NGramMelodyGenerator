using System;


namespace rhythm
{
    public class RhythmUnit
    {
        private int value;
        private int dotsAmount = 0;
        private bool isLegato = false;
        private bool isPause = false;

        public RhythmUnit(int value,int dotsAmount,bool isLegato,bool isPause)
        {
            this.value = value;
            this.dotsAmount = dotsAmount;
            this.isLegato = isLegato;
            this.isPause = isPause;
        }

        public RhythmUnit(int value)
        {
            this.value = value;
        }

        public bool IsEqual(RhythmUnit unit)
        {
            if (unit != null)
            {
                if ((dotsAmount == unit.GetIsDotted()) && (isLegato == unit.GetIsLegato()) && (value == unit.GetValue()) && (unit != null) && isPause == unit.GetIsPause()) return true;
            }
            return false;
        }

        public void Reset()
        {
            isLegato = false;
            isPause = false;
            dotsAmount = 0;
        }


        public double GetRealValue()
        {
            double fractionValue = 1D / value;
            if (dotsAmount == 1) fractionValue = fractionValue * (3D / 2D);
            if (dotsAmount == 2) fractionValue = fractionValue * (7D / 4D);
            if (dotsAmount == 3) fractionValue = fractionValue * (15D / 8D);

            return fractionValue;
        }


        public RhythmUnit GetClone()
        {
            return new RhythmUnit(this.value, this.dotsAmount, this.isLegato, this.isPause);
        }

        public int GetIsDotted() { return dotsAmount; }
        public bool GetIsLegato() { return isLegato; }
        public int GetValue() { return value; }
        public bool GetIsPause() { return isPause; }

        public void IncrementDot() { dotsAmount++; }
        public void SetIsLegato(bool isLegato) { this.isLegato = isLegato; }
        public void SetIsPause(bool isPause) { this.isPause = isPause; }
        public void SetValue(int value) { this.value = value; }

    }
}