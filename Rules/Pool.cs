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

        public Pool(string name, int score)
        {
            Name = name;
            Score = score;
            _maxValue = score;
        }

        public int Decrease(int value = 1)
        {
            if(Score >= value)
            {
                Score -= value;
                return value;
            }
            else
            {
                return 0;
            }
        }

        public int Increase(int value = 1)
        {
            if(Score + value <= _maxValue)
            {
                Score += value;
                return value;
            }
            else
            {
                return 0;
            }
        }
    }
}
