using Rules.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules.Jobs
{
    public class JobItem
    {
        public string Name { get; private set; }
        public int MinFinance { get; private set; }
        public int MaxFinance { get; private set; }
        public List<string> AlternativeSkillsources { get; private set; }
        public List<SkillItem> DefaultSkills { get; internal set; }
        public List<SkillSelection> SelectableSkills { get; internal set; }

        public JobItem(string name, int minFinance, int maxFinance)
        {
            Name = name;
            MinFinance = minFinance;
            MaxFinance = maxFinance;
            AlternativeSkillsources = new List<string>();
            DefaultSkills = new List<SkillItem>();
            SelectableSkills = new List<SkillSelection>();
        }

        public void AddSkill(SkillItem skill)
        {
            DefaultSkills.Add(skill);
        }
        
        public void AddSelectableSkill(int num, SkillItem[] skills)
        {
            SelectableSkills.Add(new SkillSelection(num, skills));
        }

        public void AddFreeChoice(int num)
        {
            //TODO
        }

        public class SkillSelection
        {
            public readonly int Number;
            public readonly SkillItem[] Skills;

            public SkillSelection(int number, SkillItem[] skills)
            {
                Number = number;
                Skills = skills;
            }
        }
    }
}
