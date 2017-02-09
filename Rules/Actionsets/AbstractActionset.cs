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
        
        public event Action WaitForInput;

        //TODO: pool

        public AbstractActionset(CharacterItem currentCharacter)
        {
            _currentCharacter = currentCharacter;
        }

        public abstract void Run();

        protected void ClearOptions()
        {
            _actions.Clear();
        }

        protected IRuleAction AddFreeOption(string actionName, string optionName, Action<FreeOption> select, Func<FreeOption, bool> done = null, Func<FreeOption, bool> enabled = null)
        {
            FreeAction action = _actions.FirstOrDefault(a => a is FreeAction && a.Name == actionName) as FreeAction;
            if (action == null)
            {
                action = new FreeAction(this, actionName);
                _actions.Add(action);
            }

            action.AddFreeOption(new FreeOption(action, optionName, select, done, enabled));
            return action;
        }

        protected IRuleAction AddValueOption<ValueType>(string valueName, string optionName, Action<ValueOption<ValueType>> select, Func<ValueOption<ValueType>, bool> done = null, Func<ValueOption<ValueType>, bool> enabled = null)
            where ValueType : AbstractValue
        {
            ValueAction<ValueType> action = _actions.FirstOrDefault(a => a is ValueAction<ValueType> && a.Name == valueName) as ValueAction<ValueType>;
            if (action == null)
            {
                action = new ValueAction<ValueType>(this, _currentCharacter.GetValue<ValueType>(valueName));
                _actions.Add(action);
            }

            action.AddValueOption(new ValueOption<ValueType>(action, optionName, select, done, enabled));
            return action;
        }

        

        //protected void AddSelectValueOption<T>(string valueName, SelectOptionType type, Action<SelectOption<T>> onSelect, bool done)
        //    where T : AbstractValue
        //{
        //    RuleValueAction<T> action = _actions.FirstOrDefault(a => a.Name == valueName) as RuleValueAction<T>;
        //    if (action == null)
        //    {
        //        action = new RuleValueAction<T>(_currentCharacter.GetValue<T>(valueName));
        //        action.Actionset = this;
        //        _actions.Add(action);
        //    }

        //    action.AddSelection(type, done, onSelect);
        //}
        
        internal void SetDone(IRuleAction action)
        {
            _actions.Remove(action);
        }

        protected async Task WaitAll()
        {
            WaitForInput?.Invoke();

            while (_actions.Any(a => !a.IsDone))
                await Task.Delay(100);
        }

        protected async Task WaitAll(params IRuleAction[] actions)
        {
            WaitForInput?.Invoke();

            while (actions.Any(a => !a.IsDone))
                await Task.Delay(100);
        }
        
    }
}
