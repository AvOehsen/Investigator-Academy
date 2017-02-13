using Rules.Character;
using Rules.Skills;
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
        public const string KEY_ABILITIES = "abilities";


        public static CharacterItem Create1920HumanCharacter(SkillCollection skills)
        {
            CharacterItem result = new CharacterItem();

            AddAttributes(result);
            AddSkills(result, skills);

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
            result.GetValue<NumericalValue>("Age").Value = 18;

            result.AddValue(KEY_POINTS, new NumericalValue("Hp", 0, 20));
            result.AddValue(KEY_POINTS, new NumericalValue("San", 0, 99));
            result.AddValue(KEY_POINTS, new NumericalValue("Mp", 0, 26));
            result.AddValue(KEY_POINTS, new NumericalValue("Lp", 0, 99));
        }

        private static void AddAbility(CharacterItem character, string name, int min = 1, int max = 80)
        {
            character.AddValue(KEY_ABILITIES, new NumericalValue(name, min, max));
        }

        private static void AddSkills(CharacterItem character, SkillCollection skills)
        {
            foreach (var skill in skills.Where(s => s.Hidden == false))
                character.AddValue(KEY_ABILITIES, new SkillValue(skill, 80));
        }
    }
}
