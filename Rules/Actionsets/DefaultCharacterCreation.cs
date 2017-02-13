using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rules.Character;
using Rules.Values;
using Rules.Actions;
using Rules.Jobs;
using Rules.Skills;

namespace Rules.Actionsets
{
    public class DefaultCharacterCreation : AbstractActionset
    {
        private IEnumerable<JobItem> _jobs;
        private SkillCollection _skills;

        public DefaultCharacterCreation(CharacterItem currentCharacter, IEnumerable<JobItem> jobs, SkillCollection skills) : base(currentCharacter)
        {
            _jobs = jobs;
            _skills = skills;
        }

        public override async void Run()
        {
            await RollAttributes();
            await SetAge();
            await CalculatePoints();
            await SelectJob();
            await Hobby();
        }

        private async Task RollAttributes()
        {
            ClearOptions();

            AddValueOption<NumericalValue>("St", "Roll", o => o.Value.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5, o => o.WasSelected);
            AddValueOption<NumericalValue>("Ko", "Roll", o => o.Value.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5, o => o.WasSelected);
            AddValueOption<NumericalValue>("Ge", "Roll", o => o.Value.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5, o => o.WasSelected);
            AddValueOption<NumericalValue>("Er", "Roll", o => o.Value.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5, o => o.WasSelected);
            AddValueOption<NumericalValue>("Ma", "Roll", o => o.Value.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5, o => o.WasSelected);

            AddValueOption<NumericalValue>("Gr", "Roll", o => o.Value.Value = (Die.RollD6() + Die.RollD6() + 6) * 5, o => o.WasSelected);
            AddValueOption<NumericalValue>("In", "Roll", o => o.Value.Value = (Die.RollD6() + Die.RollD6() + 6) * 5, o => o.WasSelected);
            AddValueOption<NumericalValue>("Bi", "Roll", o => o.Value.Value = (Die.RollD6() + Die.RollD6() + 6) * 5, o => o.WasSelected);

            await WaitAll();

            //TODO: allow bad rolls to spend additional points on bad attributes
        }

        private async Task SetAge()
        {
            ClearOptions();

            AddValueOption<NumericalValue>("Age", "+", o => o.Value.Value++);
            AddValueOption<NumericalValue>("Age", "-", o => o.Value.Value--);

            await WaitAll(AddFreeOption("Done", "done", o => { }, o => o.WasSelected));

            ClearOptions();

            int age = Character.GetValue<NumericalValue>("Age").Value;
            if(age <= 19)
            {
                Character.GetValue<NumericalValue>("Bi").Value -= 5;
                await DecreaseAttributes(5, "St", "Gr");

            }
            else if(age <= 39)
            {
                await MakeCheck(1, "Bi");
            }
            else if(age <= 49)
            {
                await MakeCheck(2, "Bi");
                Character.GetValue<NumericalValue>("Er").Value -= 5;
                await DecreaseAttributes(5, "St", "Ko", "Ge");
            }
            else if(age <= 59)
            {
                await MakeCheck(3, "Bi");
                Character.GetValue<NumericalValue>("Bi").Value -= 10;
                await DecreaseAttributes(10, "St", "Ko", "Ge");
            }
            else if(age <= 69)
            {
                await MakeCheck(4, "Bi");
                Character.GetValue<NumericalValue>("Bi").Value -= 15;
                await DecreaseAttributes(20, "St", "Ko", "Ge");
            }
            else if(age <= 79)
            {
                await MakeCheck(4, "Bi");
                Character.GetValue<NumericalValue>("Bi").Value -= 20;
                await DecreaseAttributes(40, "St", "Ko", "Ge");
            }
            else
            {
                await MakeCheck(4, "Bi");
                Character.GetValue<NumericalValue>("Bi").Value -= 25;
                await DecreaseAttributes(80, "St", "Ko", "Ge");
            }
        }

        private async Task MakeCheck(int num, string attribute)
        {
            for (int i = 0; i < num; i++)
            {
                AddValueOption<NumericalValue>(attribute, "improve", MakeCheck, o => o.WasSelected);
                await WaitAll();
            }
        }

        private void MakeCheck(ValueOption<NumericalValue> val)
        {
            if (Die.MakeCheck(val.Value.Value))
                val.Value.Value += Die.RollD10();
        }

        private async Task DecreaseAttributes(int points, params string[] attributes)
        {
            ClearOptions();
            NewPool("decrease_age_attributes", points);

            foreach (var attribute in attributes)
            {
                int maxValue = Character.GetValue<NumericalValue>(attribute).Value;
                AddValueOption<NumericalValue>(attribute, "decrease", o => o.Value.Value -= Pool.Decrease(1));
                AddValueOption<NumericalValue>(attribute, "increase", o => { if (o.Value.Value < maxValue) o.Value.Value += Pool.Increase(1); });
            }

            await WaitAll(AddFreeOption("done", "done", o => { }, enabled: e => Pool.Score == 0));
        }

        private async Task CalculatePoints()
        {
            ClearOptions();

            Character.GetValue<NumericalValue>("San").Value = Character.GetValue<NumericalValue>("Ma").Value;
            Character.GetValue<NumericalValue>("Mp").Value = (int)Math.Floor(Character.GetValue<NumericalValue>("Ma").Value / 5.0);
            Character.GetValue<NumericalValue>("Hp").Value = (int)Math.Floor((Character.GetValue<NumericalValue>("Ko").Value + Character.GetValue<NumericalValue>("Gr").Value) / 10.0);
            
            AddValueOption<NumericalValue>("Lp", "Roll", o => o.Value.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5, o => o.WasSelected);
            //allow addition luck if age < 19

            await WaitAll();
        }

        private async Task SelectJob()
        {
            ClearOptions();
            JobItem selectedJob = null;
            foreach(var job in _jobs)
                AddFreeOption("job", job.Name, o => selectedJob = job, o => o.WasSelected);

            await WaitAll();

            ClearOptions();
            int score = 0;
            if (selectedJob.AlternativeSkillsources.Count > 0)
            {
                NumericalValue altSource = null;
                foreach (var skillSource in selectedJob.AlternativeSkillsources)
                    AddValueOption<NumericalValue>(skillSource, "choose", o => altSource = o.Value);

                await WaitWhile(() => altSource == null);

                score = altSource.Value * 2 + Character.GetValue<NumericalValue>("Bi").Value * 2;
            }
            else
                score = Character.GetValue<NumericalValue>("Bi").Value * 4;

            score -= selectedJob.MinFinance;
            Character.AddValue(CharacterFactory.KEY_ABILITIES, new SkillValue(_skills.GetOrCreateSkill("Finanzkraft", selectedJob.MinFinance), selectedJob.MaxFinance));
            Character.AddValue(CharacterFactory.KEY_ABILITIES, new SkillValue(_skills.GetOrCreateSkill("Muttersprache", Character.GetValue<NumericalValue>("Bi").Value), 80));
            Character.AddValue(CharacterFactory.KEY_ABILITIES, new SkillValue(_skills.GetOrCreateSkill("Ausweichen", Character.GetValue<NumericalValue>("Ge").Value / 2), 80));
            
            NewPool("job_points", score);
            ClearOptions();

            List<SkillItem> skills = new List<SkillItem>();
            skills.Add(_skills.GetSkillByName("Finanzkraft"));
            skills.AddRange(selectedJob.DefaultSkills);
            foreach (var selection in selectedJob.SelectableSkills)
            {
                for (int i = 0; i < selection.Number; i++)
                {
                    ClearOptions();
                    foreach (var skill in selection.Skills)
                    {
                        if (!skills.Contains(skill))
                            AddFreeOption("Select One", skill.DisplayName, o => skills.Add(skill), o => o.WasSelected);
                    }
                    await WaitAll();
                }
            }

            ClearOptions();
            foreach (var skill in skills)
            {
                if (Character.GetValue<NumericalValue>(skill.DisplayName) == null)
                    Character.AddValue(CharacterFactory.KEY_ABILITIES, new SkillValue(skill, 80));

                AddValueOption<NumericalValue>(skill.DisplayName, "increase", o => o.Value.Value += Pool.Decrease(1));
                AddValueOption<NumericalValue>(skill.DisplayName, "decrease", o => o.Value.Value -= Pool.Increase(1));
                
            }

            await WaitAll(AddFreeOption("done", "done", o => { }, o => o.WasSelected));
        }

        private async Task Hobby()
        {
            ClearOptions();
            NewPool("Hobby", Character.GetValue<NumericalValue>("In").Value * 2);

            foreach (var skill in _skills)
            {
                if (Character.GetValue<SkillValue>(skill.DisplayName) != null)
                    AddSkillOptions(skill);
                else
                    AddFreeOption(skill.DisplayName, "Add", o => AddSkillOptions(skill), o => o.WasSelected);
            }

            await WaitAll(AddFreeOption("done", "done", o => { }, o => o.WasSelected));
            ClearOptions();
        }

        private void AddSkillOptions(SkillItem skill)
        {
            if (Character.GetValue<SkillValue>(skill.DisplayName) == null)
                Character.AddValue(CharacterFactory.KEY_ABILITIES, new SkillValue(skill, 80));

            AddValueOption<NumericalValue>(skill.DisplayName, "increase", o => o.Value.Value += Pool.Decrease(1));
            AddValueOption<NumericalValue>(skill.DisplayName, "decrease", o => o.Value.Value -= Pool.Increase(1));
        }
    }
}
