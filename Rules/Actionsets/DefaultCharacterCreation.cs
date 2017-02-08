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
            //TODO: age
            //TODO: job
            //TOD: skills
        }

        private async Task RollAttributes()
        {
            ClearOptions();

            AddSelectOption<NumericalValue>("St", SelectOptionType.Roll, o => o.Action.TargetValue.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5, true);
            AddSelectOption<NumericalValue>("Ko", SelectOptionType.Roll, o => o.Action.TargetValue.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5, true);
            AddSelectOption<NumericalValue>("Ge", SelectOptionType.Roll, o => o.Action.TargetValue.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5, true);
            AddSelectOption<NumericalValue>("Er", SelectOptionType.Roll, o => o.Action.TargetValue.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5, true);
            AddSelectOption<NumericalValue>("Ma", SelectOptionType.Roll, o => o.Action.TargetValue.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5, true);

            AddSelectOption<NumericalValue>("Gr", SelectOptionType.Roll, o => o.Action.TargetValue.Value = (Die.RollD6() + Die.RollD6() + 6) * 5, true);
            AddSelectOption<NumericalValue>("In", SelectOptionType.Roll, o => o.Action.TargetValue.Value = (Die.RollD6() + Die.RollD6() + 6) * 5, true);
            AddSelectOption<NumericalValue>("Bi", SelectOptionType.Roll, o => o.Action.TargetValue.Value = (Die.RollD6() + Die.RollD6() + 6) * 5, true);

            await WaitAll();
        }
    }
}
