using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules.Character
{
    public class CharacterAttributes
    {
        public CharacterValue St { get; set; }
        public CharacterValue Ko { get; set; }
        public CharacterValue Ge { get; set; }
        public CharacterValue Er { get; set; }
        public CharacterValue Ma { get; set; }

        public CharacterValue Gr { get; set; }
        public CharacterValue In { get; set; }
        public CharacterValue Bi { get; set; }

        public CharacterAttributes()
        {
            St = new CharacterValue(15, 90);
            Ko = new CharacterValue(15, 90);
            Ge = new CharacterValue(15, 90);
            Er = new CharacterValue(15, 90);
            Ma = new CharacterValue(15, 90);

            Gr = new CharacterValue(40, 90);
            In = new CharacterValue(40, 90);
            Bi = new CharacterValue(40, 90);
        }

        public void RollAttributes()
        {
            St.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5;
            Ko.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5;
            Ge.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5;
            Er.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5;
            Ma.Value = (Die.RollD6() + Die.RollD6() + Die.RollD6()) * 5;

            Gr.Value = (Die.RollD6() + Die.RollD6() + 6) * 5;
            In.Value = (Die.RollD6() + Die.RollD6() + 6) * 5;
            Bi.Value = (Die.RollD6() + Die.RollD6() + 6) * 5;
        }
    }
}
