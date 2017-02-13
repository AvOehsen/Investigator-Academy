using Rules.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules.Values
{
    public class SkillValue : NumericalValue
    {
        public SkillItem Skill { get; private set; }

        public SkillValue(SkillItem skill, int max) : base(skill.DisplayName, skill.DefaultValue, max)
        {
            Skill = skill;
        }
    }
}
