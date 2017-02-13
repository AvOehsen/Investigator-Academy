using Rules.Actions;
using Rules.Actionsets;
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
    public class RuleCore
    {
        private CharacterItem _currentCharacter;
        public CharacterItem Character { get { return _currentCharacter; } }

        private AbstractActionset _currentActionset;
        public IEnumerable<IRuleAction> Actions { get { return _currentActionset.Actions; } }

        public Pool Pool { get { return _currentActionset.Pool; } }

        public event Action WaitForInput
        {
            add
            {
                _currentActionset.WaitForInput += value;
            }
            remove
            {
                _currentActionset.WaitForInput -= value;
            }
        }

        public void NewCharacter()
        {
            SkillCollection skills = SkillFactory.Build1920Skills();

            _currentCharacter = CharacterFactory.Create1920HumanCharacter(skills);
            _currentActionset = new DefaultCharacterCreation(_currentCharacter, JobFactory.Create1920Jobs(skills), skills);
            _currentActionset.Run();
        }
    }
}
