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

        private int _value;
        public int Value
        {
            get { return _value; }
            set { _value = Math.Max(Math.Min(value, Max), Min); }
        }

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
