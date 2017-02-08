using Rules.Actions;
using Rules.Actionsets;
using Rules.Character;
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

        public void NewCharacter()
        {
            _currentCharacter = CharacterFactory.Create1920HumanCharacter();
            _currentActionset = new DefaultCharacterCreation(_currentCharacter);
            _currentActionset.Run();
        }
    }
}
