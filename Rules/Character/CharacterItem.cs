using Rules.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules.Character
{
    public class CharacterItem
    {
        private CharacterValueMap _values = new CharacterValueMap();

        public CharacterItem()
        {
        }

        internal void AddValue(string category, AbstractValue numericalValue)
        {
            _values.AddValue(category, numericalValue);
        }

        public IEnumerable<string> Categories
        {
            get { return _values.Keys; }
        }

        public IEnumerable<AbstractValue> GetValues(string category)
        {
            return _values[category].Values;
        }

        internal T GetValue<T>(string name) where T : AbstractValue
        {
            foreach (var cat in _values.Values)
                foreach (var item in cat.Values)
                    if (item.Name == name)
                        return item as T;

            return null;
        }
    }
}
