using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules.Actions
{
    public class FreeOption : AbstractOption
    {
        private string _name;
        private Action<FreeOption> _select;
        private Func<FreeOption, bool> _done;
        private Func<FreeOption, bool> _enabled;

        public override string Description
        {
            get { return _name; }
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

        public FreeOption(IRuleAction action, string name, Action<FreeOption> select, Func<FreeOption, bool> done, Func<FreeOption, bool> enabled) : base(action)
        {
            _name = name;
            _select = select;
            _done = done;
            _enabled = enabled;
        }

        public override void Select()
        {
            base.Select();

            _select(this);

            if (_done != null)
                Action.IsDone = _done(this);
        }

    }
}
