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
            RebuildPool();
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

        private void RebuildPool()
        {
            pool_label.Text = string.Format("{0}: {1}", _core.Pool.Name, _core.Pool.Score);
        }

        private void RebuildActions()
        {
            actions_flowLayoutPanel.Controls.Clear();
            actions_flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            actions_flowLayoutPanel.WrapContents = false;
            actions_flowLayoutPanel.AutoScroll = true;

            foreach (var action in _core.Actions)
            {
                int numOptions = action.Options.Count();

                GroupBox box = new GroupBox();
                box.Height = 25 + numOptions * 29;
                box.Width = actions_flowLayoutPanel.Width - 20;
                box.Text = action.Name;
                
                actions_flowLayoutPanel.Controls.Add(box);
                //box.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Right;

                FlowLayoutPanel panel = new FlowLayoutPanel();
                panel.FlowDirection = FlowDirection.TopDown;
                //panel.AutoScroll = true;
                panel.WrapContents = false;
                box.Controls.Add(panel);
                panel.Dock = DockStyle.Fill;


                foreach (var option in action.Options)
                {
                    Button b = new Button();
                    b.Width = box.Width - 20;
                    //b.Height = 24;
                    b.Text = option.Description;
                    b.Enabled = option.Enabled;
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

        private void pdf_button_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Pdf File(*.pdf)|.pdf";

            if (dialog.ShowDialog() == DialogResult.OK)
                PdfAdapter.WriteCharacter(_core.Character, dialog.FileName);
        }
    }
}
