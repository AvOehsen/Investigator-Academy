using Rules.Jobs;
using Rules.Skills;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Rules
{
    public static class JobFactory
    {

        public static IEnumerable<JobItem> Create1920Jobs(SkillCollection availableSkills)
        {
            /*yield return CreateAcademic();
            yield return CreateHistorian();
            yield return CriminalInvestigator();
            yield return CreatePrivateInvestigator();*/

            List<JobItem> jobs = new List<JobItem>();

            XmlDocument doc = new XmlDocument();
            doc.Load("jobs.xml");
            foreach (XmlElement jobNode in doc.SelectNodes("jobs/job"))
            {
                string name = jobNode.GetAttribute("name");
                int minFinance = int.Parse(jobNode.GetAttribute("minFinance"));
                int maxFinance = int.Parse(jobNode.GetAttribute("maxFinance"));

                JobItem job = new JobItem(name, minFinance, maxFinance);

                //additional skills-point-sources
                foreach (XmlElement alternativeSkillsource in jobNode.SelectNodes("skillpoints/attribute"))
                    job.AlternativeSkillsources.Add(alternativeSkillsource.InnerText);
                
                //base-skills
                foreach (XmlElement baseSkill in jobNode.SelectNodes("baseSkill/skill"))
                {
                    var skill = availableSkills.FirstOrDefault(s => s.Name == baseSkill.InnerText);
                    if (skill != null)
                        job.AddSkill(skill);
                    else
                        job.AddSkill(availableSkills.GetOrCreateSkill(baseSkill.InnerText));
                }

                //selectable sets
                foreach (XmlElement selectableSkill in jobNode.SelectNodes("selectSkill"))
                {
                    int count = int.Parse(selectableSkill.GetAttribute("count"));
                    List<SkillItem> skills = new List<SkillItem>();

                    //defined skill
                    foreach (XmlElement item in selectableSkill.SelectNodes("skill"))
                    {
                        var skill = availableSkills.FirstOrDefault(s => s.Name == item.InnerText);
                        if (skill != null)
                            skills.Add(skill);
                        else
                            skills.Add(availableSkills.GetOrCreateSkill(item.InnerText));
                    }

                    //skillset
                    foreach (XmlElement item in selectableSkill.SelectNodes("skillType"))
                        skills.AddRange(availableSkills.Where(s => s.Type == item.InnerText));

                    //all
                    if (selectableSkill.SelectSingleNode("all") != null)
                        skills.AddRange(availableSkills);

                    job.AddSelectableSkill(count, skills.ToArray());
                }


                jobs.Add(job);
            }

            return jobs;
        }

        //private static JobItem CreateAcademic()
        //{
        //    JobItem result = new JobItem("Akademiker", 20, 70);

        //    result.AddSkill("Bibliotkelsnutzung");
        //    //TODO: fremdsprache nach wahl
        //    //TODO: Muttersprache
        //    result.AddSkill("Psychologie");
        //    result.AddFreeChoice(4);

        //    return result;
        //}

        //private static JobItem CreateHistorian()
        //{
        //    JobItem result = new JobItem("Altertumsforscher", 30, 70);

        //    result.AddSkill("Bibliotheksnutzung");
        //    //TODO: fremdsprache nach wahl
        //    result.AddSkill("Geschichte");
        //    //TODO: kunst nach wahl
        //    result.AddSkill("Verborgenes erkennen, Werte schätzen");
        //    result.AddSelectableSkill(1, "Charme", "Einschüchtern", "Überreden", "Überzeugen");
        //    result.AddFreeChoice(1);
            
        //    return result;
        //}

        //private static JobItem CriminalInvestigator()
        //{
        //    JobItem result = new JobItem("Kriminalbeamter", 20, 50, "Ge", "St");
            
        //    result.AddSelectableSkill(1, "Fernakmpf (Faustfeuerwaffe)", "Fernkampf (Gewehr)");
        //    result.AddSelectableSkill(1, "Handwerk/Kunst (Schauspielerei)", "Verkleiden");
        //    result.AddSkill("Horchen", "Psychologie", "Rechtswesen", "Verborgenes erkennen");
        //    result.AddSelectableSkill(1, "Charme", "Einschüchtern", "Überreden", "Überzeugen");
        //    result.AddFreeChoice(1);

        //    return result;
        //}

        //private static JobItem CreatePrivateInvestigator()
        //{
        //    JobItem result = new JobItem("Privatermittler", 9, 30, "Ge", "St");
            
        //    result.AddSkill("Bibliotheksnutzung", "Handwerk/Kust (Fotografie)", "Psychologie", "Rechtswesen", "Verborgenes erkennen", "Verkleiden");
        //    result.AddSelectableSkill(1, "Charme", "Einschüchtern", "Überreden", "Überzeugen");
        //    result.AddFreeChoice(1);

        //    return result;
        //}
    }
}
