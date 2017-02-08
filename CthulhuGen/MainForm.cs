using Rules;
using Rules.Actions;
using Rules.Character;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CthulhuGen
{
    public partial class MainForm : Form
    {
        private RuleCore _core = new RuleCore();

        public MainForm()
        {
            _core.NewCharacter();

            InitializeComponent();

            Rebuild();
        }

        private void Rebuild()
        {
            RebuildCharacterSheet();
            RebuildActions();
        }

        private void RebuildCharacterSheet()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var category in _core.Character.Categories)
            {
                sb.AppendLine(category);
                foreach (var value in _core.Character.GetValues(category))
                {
                    sb.AppendLine(value.ToString());
                }
                sb.AppendLine();
            }

            sheet_richTextBox.Text = sb.ToString();
        }

        private void RebuildActions()
        {
            actions_flowLayoutPanel.Controls.Clear();

            foreach (var action in _core.Actions)
            {
                GroupBox box = new GroupBox();
                box.Text = action.Value.Name;
                
                actions_flowLayoutPanel.Controls.Add(box);

                FlowLayoutPanel panel = new FlowLayoutPanel();
                box.Controls.Add(panel);


                foreach (var option in action.Options)
                {
                    if(option is ISelectOption)
                    {
                        ISelectOption selectOption = option as ISelectOption;
                        Button b = new Button();
                        b.Text = selectOption.Type.ToString();
                        b.Click += (s, e) => selectOption.Select();
                        b.Click += (s, e) => Rebuild();

                        panel.Controls.Add(b);
                    }
                }
            }
        }
    }
}
