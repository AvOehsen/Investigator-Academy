﻿using Rules.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules.Actions
{
    public interface IRuleAction
    {
        bool IsDone { get; }
        AbstractValue Value { get; }
        IEnumerable<IOption> Options { get; }
    }

    public class RuleAction<T> : IRuleAction where T : AbstractValue
    {
        public bool IsDone { get; private set; }
        public AbstractValue Value { get { return TargetValue; } }
        public T TargetValue { get; private set; }
        private List<AbstractOption<T>> options = new List<AbstractOption<T>>();
        public RuleAction<T> Action { get; set; }

        public IEnumerable<IOption> Options
        {
            get
            {
                return options;
            }
        }

        public RuleAction(T targetValue)
        {
            IsDone = false;
            TargetValue = targetValue;
        }

        public void SetDone()
        {
            IsDone = true;
        }
        
        public SelectOption<T> AddSelection(SelectOptionType type, bool resolvedOnSelect, Action<SelectOption<T>> onSelected)
        {
            var result = new SelectOption<T>();
            result.Action = this;
            result.Type = type;
            result.OnSelected = (a) => { onSelected(result); if (resolvedOnSelect) IsDone = true; };

            options.Add(result);
            return result;
        }
    }
}
