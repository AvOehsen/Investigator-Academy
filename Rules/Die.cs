using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules
{
    public static class Die
    {
        private static Random _random = new Random();

        public static int RollD6()
        {
            return _random.Next(1, 7);
        }

        internal static int RollD10()
        {
            return _random.Next(1, 11);
        }
    }
}
