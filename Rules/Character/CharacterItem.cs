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

        public string Job { get; set; }


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

        public int GetNumericalValue(string name)
        {
            NumericalValue val = GetValue<NumericalValue>(name);
            if (val == null)
                return -1;
            else
                return val.Value;
        }

        public T GetValue<T>(string name) where T : AbstractValue
        {
            foreach (var cat in _values.Values)
                foreach (var item in cat.Values)
                    if (item.Name == name)
                        return item as T;

            return null;
        }
    }
}
