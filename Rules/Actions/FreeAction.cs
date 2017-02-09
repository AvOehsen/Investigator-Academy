using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rules.Actionsets;

namespace Rules.Actions
{
    public class FreeAction : AbstractRuleAction
    {
        private string _name;
        public override string Name
        {
            get { return _name; }
        }

        public FreeAction(AbstractActionset actionset, string name) : base(actionset)
        {
            _name = name;
        }

        public void AddFreeOption(FreeOption option)
        {
            AddOption(option);
        }
    }
}
