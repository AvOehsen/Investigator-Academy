using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules.Values
{
    public class NumericalValue : AbstractValue
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Value { get; set; }

        public NumericalValue(string name, int min, int max) : base(name)
        {
            Min = min;
            Max = max;
        }


        public override string ToString()
        {
            return string.Format("{0}:\t{1}", Name, Value);
        }
    }
}
