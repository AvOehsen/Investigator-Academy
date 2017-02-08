using Rules.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules.Actions
{
    public enum SelectOptionType
    {
        Increase,
        Decrease,
        Roll,
        Raise,
        Check,
        Select
    }

    public interface ISelectOption
    {
        SelectOptionType Type { get; }
        void Select();
    }

    public class SelectOption<T>  : AbstractOption<T>, ISelectOption where T : AbstractValue
    {
        public Action<RuleAction<T>> OnSelected;

        public SelectOptionType Type { get; internal set; }

        public void Select()
        {
            OnSelected(Action);
        }
    }
}
