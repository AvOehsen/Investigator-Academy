using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules.Character
{
    public class CharacterValue
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Value { get; set; }

        public CharacterValue(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public bool Check(int modigfier = 0)
        {
            return ((10 * Die.RollD10()) + Die.RollD10()) % 100 <= Value;
        }

        public bool TryRaise()
        {
            if (!Check())
            {
                Value += Die.RollD10();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
