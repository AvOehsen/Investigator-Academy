using Rules.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules.Actions
{
    public class ValueOption<ValueType> : AbstractOption where ValueType : AbstractValue
    {
        private string _description;
        private Action<ValueOption<ValueType>> _select;
        private Func<ValueOption<ValueType>, bool> _done;
        private Func<ValueOption<ValueType>, bool> _enabled;

        public ValueOption(IRuleAction action, string description, Action<ValueOption<ValueType>> select, Func<ValueOption<ValueType>, bool> done, Func<ValueOption<ValueType>, bool> enabled) : base(action)
        {
            _description = description;
            _select = select;
            _done = done;
            _enabled = enabled;
        }

        public override string Description
        {
            get { return _description; }
        }

        public override bool Enabled
        {
            get
            {
                if (_enabled != null)
                    return _enabled(this);
                else
                    return true;
            }
        }

        public ValueType Value { get; internal set; }

        public override void Select()
        {
            base.Select();

            _select(this);

            if (_done != null)
                Action.IsDone = _done(this);
        }
    }
}
