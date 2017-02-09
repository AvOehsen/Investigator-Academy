using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rules.Character;
using Rules.Values;
using Rules.Actions;

namespace Rules.Actionsets
{
    public class DefaultCharacterCreation : AbstractActionset
    {
        public DefaultCharacterCreation(CharacterItem currentCharacter) : base(currentCharacter)
        {
        }

        public override async void Run()
        {
            await RollAttributes();
            await SetAge();
            await CalculatePoints();
            //TODO: job
            //TODO: job-skills
            //TODO: hoby-skills
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

            //TODO: BW, SB, STATUR

            AddValueOption<NumericalValue>("Lp", "Roll", o => o.Value.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5, o => o.WasSelected);

            await WaitAll();
        }
    }
}
