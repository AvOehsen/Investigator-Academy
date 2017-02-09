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
            //TODO: calc points
            //TODO: job
            //TOD: job-skills
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

            AddValueOption<NumericalValue>("Age", "Increase", o => o.Value.Value++);
            AddValueOption<NumericalValue>("Age", "Decrease", o => o.Value.Value--);
            //TODO: add "done / contineu" option

            await WaitAll(AddFreeOption("Done", "done", o => { }, o => o.WasSelected));

            ClearOptions();

            //TODO: adjust attributes
        }
    }
}
