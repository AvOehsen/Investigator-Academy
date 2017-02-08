using Rules.Actions;
using Rules.Character;
using Rules.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rules.Actionsets
{
    public abstract class AbstractActionset
    {
        private List<IRuleAction> _actions = new List<IRuleAction>();
        private CharacterItem _currentCharacter;

        public IEnumerable<IRuleAction> Actions { get { return _actions; } }

        public AbstractActionset(CharacterItem currentCharacter)
        {
            _currentCharacter = currentCharacter;
        }

        public abstract void Run();

        protected void ClearOptions()
        {
            _actions.Clear();
        }
        
        protected void AddSelectOption<T>(string valueName, SelectOptionType type, Action<SelectOption<T>> onSelect, bool done)
            where T : AbstractValue
        {
            RuleAction<T> action = _actions.FirstOrDefault(a => a.Value.Name == valueName) as RuleAction<T>;
            if (action == null)
            {
                action = new RuleAction<T>(_currentCharacter.GetValue<T>(valueName));
                action.Actionset = this;
                _actions.Add(action);
            }

            action.AddSelection(type, done, onSelect);
        }

        internal void SetDone(IRuleAction action)
        {
            _actions.Remove(action);
        }

        protected async Task WaitAll()
        {
            while (_actions.Any(a => !a.IsDone))
                await Task.Delay(100);
        }

        protected async Task WaitAll(params IRuleAction[] actions)
        {
            while (actions.Any(a => !a.IsDone))
                await Task.Delay(100);
        }
        
    }
}
