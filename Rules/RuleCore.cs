using Rules.Actions;
using Rules.Character;
using Rules.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules
{
    public class RuleCore
    {
        private CharacterItem _currentCharacter;
        public CharacterItem Character { get { return _currentCharacter; } }

        private List<IRuleAction> _currentActions = new List<IRuleAction>();
        public IEnumerable<IRuleAction> Actions { get { return _currentActions; } }

        //TODO: creation type
        public void NewCharacter()
        {
            _currentCharacter = CharacterFactory.Create1920HumanCharacter();

            CreateAttributes();
        }

        public void CreateAttributes()
        {
            /*RuleAction<NumericalValue> action = new RuleAction<NumericalValue>(_currentCharacter.GetValue<NumericalValue>("St"));
            action.AddSelection(SelectOptionType.Roll, true, o => { o.Action.TargetValue.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5; });
            _currentActions.Add(action);*/

            //roll attributes
            CreateSelectOption<NumericalValue>("St", SelectOptionType.Roll, o => o.Action.TargetValue.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5, true);
            CreateSelectOption<NumericalValue>("Ko", SelectOptionType.Roll, o => o.Action.TargetValue.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5, true);
            CreateSelectOption<NumericalValue>("Ge", SelectOptionType.Roll, o => o.Action.TargetValue.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5, true);
            CreateSelectOption<NumericalValue>("Er", SelectOptionType.Roll, o => o.Action.TargetValue.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5, true);
            CreateSelectOption<NumericalValue>("Ma", SelectOptionType.Roll, o => o.Action.TargetValue.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5, true);

            CreateSelectOption<NumericalValue>("Gr", SelectOptionType.Roll, o => o.Action.TargetValue.Value = (Die.RollD6() + Die.RollD6() + 6) * 5, true);
            CreateSelectOption<NumericalValue>("In", SelectOptionType.Roll, o => o.Action.TargetValue.Value = (Die.RollD6() + Die.RollD6() + 6) * 5, true);
            CreateSelectOption<NumericalValue>("Bi", SelectOptionType.Roll, o => o.Action.TargetValue.Value = (Die.RollD6() + Die.RollD6() + 6) * 5, true);
        }

        private void CreateSelectOption<T>(string valueName, SelectOptionType type, Action<SelectOption<T>> onSelect, bool done)
            where T : AbstractValue
        {
            RuleAction<T> action = _currentActions.FirstOrDefault(a => a.Value.Name == valueName) as RuleAction<T>;
            if (action == null)
            {
                action = new RuleAction<T>(_currentCharacter.GetValue<T>(valueName));
                _currentActions.Add(action);
            }

            action.AddSelection(type, done, onSelect);
        }


        private async void WaitForCompleteActions()
        {
            while (_currentActions.Any(a => !a.IsDone))
                await Task.Delay(100);
        }
    }
}
