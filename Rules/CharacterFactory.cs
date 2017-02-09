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
        private const string KEY_CHARACTER = "character";
        private const string KEY_POINTS = "points";


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

            result.AddValue(KEY_CHARACTER, new NumericalValue("Age", 15, 89));
            //TODO: job, birthday, gender, name, etc.

            result.AddValue(KEY_POINTS, new NumericalValue("Hp", 0, 20));
            result.AddValue(KEY_POINTS, new NumericalValue("San", 0, 99));
            result.AddValue(KEY_POINTS, new NumericalValue("Mp", 0, 26));
            result.AddValue(KEY_POINTS, new NumericalValue("Lp", 0, 99));

            //TODO: abilities
        }
    }
}
