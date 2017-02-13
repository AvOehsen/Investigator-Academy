using Rules.Skills;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules
{
    public static class SkillFactory
    {

        public static SkillCollection Build1920Skills()
        {
            SkillCollection result = new SkillCollection();

            using (var reader = File.OpenText("skills.csv"))
            {
                string line = reader.ReadLine();
                while(!string.IsNullOrEmpty(line))
                {
                    string[] token = line.Split(',');

                    SkillItem item = new SkillItem();
                    item.Name = token[0];
                    item.Type = token[1];
                    item.DefaultValue = int.Parse(token[2]);
                    item.Hidden = int.Parse(token[3]) == 1;

                    result.Add(item);

                    line = reader.ReadLine();
                }
            }


            return result;
        }
    }
}
