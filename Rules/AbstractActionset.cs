using Rules.Actions;
using Rules.Character;
using Rules.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rules
{
    public abstract class AbstractActionset
    {
        private List<IRuleAction> _actions = new List<IRuleAction>();
        private CharacterItem _currentCharacter;

        public AbstractActionset(CharacterItem currentCharacter)
        {
            _currentCharacter = currentCharacter;
        }

        public abstract void Run();

        public async void Foo()
        {
            AddSelectOption<NumericalValue>("foo", SelectOptionType.Decrease, o => o.Action.TargetValue.Value = 12, true);

            await WaitAll();
        }

        protected void ClearOptions()
        {
            _actions.Clear();
        }
        
        private void AddSelectOption<T>(string valueName, SelectOptionType type, Action<SelectOption<T>> onSelect, bool done)
            where T : AbstractValue
        {
            RuleAction<T> action = _actions.FirstOrDefault(a => a.Value.Name == valueName) as RuleAction<T>;
            if (action == null)
            {
                action = new RuleAction<T>(_currentCharacter.GetValue<T>(valueName));
                _actions.Add(action);
            }

            action.AddSelection(type, done, onSelect);
        }

        protected async Task WaitAll()
        {
            while (_actions.Any(a => !a.IsDone))
                await Task.Delay(100);
        }

        protected async Task WaitAll(params IRuleAction[] actions)
        {
        }

        protected async Task WaitOne(params IRuleAction[] actions)
        {
        }
    }
}
