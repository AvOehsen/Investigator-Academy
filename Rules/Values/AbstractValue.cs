using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules.Values
{
    public abstract class AbstractValue
    {
        public string Name { get; private set; }

        public AbstractValue(string name)
        {
            Name = name;
        }

        public abstract new string ToString();
    }
}
