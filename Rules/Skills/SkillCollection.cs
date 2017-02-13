using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules.Skills
{
    public class SkillCollection : List<SkillItem>
    {
        public SkillItem GetOrCreateSkill(string name, int defaultValue = 1)
        {
            SkillItem item = this.FirstOrDefault(i => i.Name == name);
            if(item == null)
            {
                item = new SkillItem();
                Add(item);
            }

            item.Name = name;
            item.DefaultValue = defaultValue;
            item.Hidden = false;
            item.Type = "";
            
            return item;
        }

        public SkillItem GetSkillByName(string name)
        {
            return this.FirstOrDefault(i => i.Name == name);
        }

        public IEnumerable<SkillItem> GetSkillsByType(string type)
        {
            return this.Where(s => s.Type == type);
        }

    }
}
