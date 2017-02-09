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
            _core.WaitForInput += Rebuild;

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
                int numOptions = action.Options.Count();

                GroupBox box = new GroupBox();
                box.Height = 50;
                box.Width = 100 * numOptions;
                box.Text = action.Name;
                
                actions_flowLayoutPanel.Controls.Add(box);
                box.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Right;

                FlowLayoutPanel panel = new FlowLayoutPanel();
                box.Controls.Add(panel);
                panel.Dock = DockStyle.Fill;


                foreach (var option in action.Options)
                {
                    Button b = new Button();
                    b.Width = 90;
                    b.Text = option.Description;
                    b.Click += (s, e) => option.Select();
                    b.Click += (s, e) => Rebuild();

                    panel.Controls.Add(b);
                }
            }
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            Rebuild();
        }
    }
}
