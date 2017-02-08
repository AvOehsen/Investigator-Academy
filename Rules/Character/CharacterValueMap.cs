using Rules.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules.Character
{
    public class CharacterValueMap : Dictionary<string, Dictionary<string, AbstractValue>>
    {

        public void AddValue(string category, AbstractValue value)
        {
            if (!ContainsKey(category))
                Add(category, new Dictionary<string, AbstractValue>());

            this[category][value.Name] = value;
        }

    }
}
