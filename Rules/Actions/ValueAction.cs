using Rules.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rules.Actionsets;

namespace Rules.Actions
{
    public class ValueAction<ValueType> : AbstractRuleAction where ValueType : AbstractValue
    {
        private ValueType _value;

        public ValueAction(AbstractActionset actionset, ValueType value) : base(actionset)
        {
            _value = value;
            
        }

        public override string Name
        {
            get{ return _value.Name; }
        }

        internal void AddValueOption(ValueOption<ValueType> option)
        {
            option.Value = _value;
            AddOption(option);
        }
    }
}
