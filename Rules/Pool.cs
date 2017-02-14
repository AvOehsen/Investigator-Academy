using Rules.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules
{
    public class Pool
    {
        public string Name { get; private set; }
        public int Score { get; private set; }
        private int _maxValue;

        private Dictionary<NumericalValue, int> _investedScore = new Dictionary<NumericalValue, int>();

        public Pool(string name, int score)
        {
            Name = name;
            Score = score;
            _maxValue = score;
        }

        public void Decrease(NumericalValue value, int ammount = 1, bool negative = false)
        {
            if(Score >= ammount)
            {
                int newValue = negative ? value.Value -= ammount : value.Value += ammount;

                if (newValue >= value.Min && newValue <= value.Max)
                {
                    value.Value = newValue;
                    Score -= ammount;

                    if (!_investedScore.ContainsKey(value))
                        _investedScore[value] = 0;
                    _investedScore[value] += ammount;
                }
            }
        }

        public void Increase(NumericalValue value, int ammount = 1, bool negative = false)
        {
            if(_investedScore.ContainsKey(value) && _investedScore[value] >= ammount)
            {
                if (negative)
                    value.Value += ammount;
                else
                    value.Value -= ammount;

                Score += ammount;
                _investedScore[value] -= ammount;
            }
        }

        //public int Decrease(int value = 1)
        //{
        //    if(Score >= value)
        //    {
        //        Score -= value;
        //        return value;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        //public int Increase(int value = 1)
        //{
        //    if(Score + value <= _maxValue)
        //    {
        //        Score += value;
        //        return value;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
    }
}
