using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules.Skills
{
    public class SkillItem
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int DefaultValue { get; set; }
        public bool Hidden { get; set; }

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(Type))
                    return Name;
                else
                    return Type + " (" + Name + ")";
            }
        }
    }
}
