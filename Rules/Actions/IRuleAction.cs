using Rules.Actionsets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules.Actions
{
    public interface IRuleAction
    {
        bool IsDone { get; set; }
        IEnumerable<IOption> Options { get; }
        string Name { get; }

    }

    public abstract class AbstractRuleAction : IRuleAction
    {
        abstract public string Name { get; }
        protected AbstractActionset Actionset { get; private set; }

        private bool _isDone;
        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                _isDone = value;
                if (_isDone)
                    Actionset.SetDone(this);
            }
        }

        private List<IOption> _options = new List<IOption>();

        public IEnumerable<IOption> Options
        {
            get { return _options; }
        }

        public AbstractRuleAction(AbstractActionset actionset)
        {
            Actionset = actionset;
        }

        protected void AddOption(IOption option)
        {
            _options.Add(option);
        }
        
    }
}
