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
    }

    public abstract class AbstractOption<T> : IOption where T : AbstractValue
    {
        public RuleAction<T> Action { get; set; }
    }
}
