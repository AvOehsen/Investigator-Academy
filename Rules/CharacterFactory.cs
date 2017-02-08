using Rules.Character;
using Rules.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rules
{
    public class CharacterFactory
    {
        private const string KEY_ATTRIBUTES = "attributes";


        public static CharacterItem Create1920HumanCharacter()
        {
            CharacterItem result = new CharacterItem();

            AddAttributes(result);

            return result;
        }

        private static void AddAttributes(CharacterItem result)
        {
            result.AddValue(KEY_ATTRIBUTES, new NumericalValue("St", 15, 90));
            result.AddValue(KEY_ATTRIBUTES, new NumericalValue("Ko", 15, 90));
            result.AddValue(KEY_ATTRIBUTES, new NumericalValue("Ge", 15, 90));
            result.AddValue(KEY_ATTRIBUTES, new NumericalValue("Er", 15, 90));
            result.AddValue(KEY_ATTRIBUTES, new NumericalValue("Ma", 15, 90));

            result.AddValue(KEY_ATTRIBUTES, new NumericalValue("Gr", 40, 90));
            result.AddValue(KEY_ATTRIBUTES, new NumericalValue("In", 40, 90));
            result.AddValue(KEY_ATTRIBUTES, new NumericalValue("Bi", 40, 90));

            result.AddValue("things", new CalcValue("Hp", () =>(int)Math.Floor((result.GetValue<NumericalValue>("Ko").Value + result.GetValue<NumericalValue>("Gr").Value) / 10.0)));
            result.AddValue("things", new CalcValue("Mp", () => result.GetValue<NumericalValue>("Ma").Value / 5));
            result.AddValue("things", new CalcValue("San", () => result.GetValue<NumericalValue>("Ma").Value));
        }
    }
}
