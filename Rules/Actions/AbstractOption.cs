using Rules.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules.Actions
{
    public interface IOption
    {
        void Select();
        string Description { get; }
        bool Enabled { get; }

    }

    public abstract class AbstractOption : IOption
    {
        public abstract string Description { get; }
        public abstract bool Enabled { get; }
        public bool WasSelected { get; private set; }
        protected IRuleAction Action { get; private set; }

        public AbstractOption(IRuleAction action)
        {
            Action = action;
            WasSelected = false;
        }

        public virtual void Select()
        {
            WasSelected = true;
        }
    }
}
