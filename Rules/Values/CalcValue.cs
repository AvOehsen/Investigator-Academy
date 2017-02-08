using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules.Values
{
    public class CalcValue : AbstractValue
    {
        private Func<int> _func;
        public int Value { get { return _func(); } }

        public CalcValue(string name, Func<int> func) : base(name)
        {
            _func = func;
        }

        public override string ToString()
        {
            return string.Format("{0}:\t{1}", Name, Value);
        }
    }
}
