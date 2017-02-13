namespace CthulhuGen
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.sheet_groupBox = new System.Windows.Forms.GroupBox();
            this.sheet_richTextBox = new System.Windows.Forms.RichTextBox();
            this.actions_groupBox = new System.Windows.Forms.GroupBox();
            this.actions_flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.pool_groupBox = new System.Windows.Forms.GroupBox();
            this.pool_label = new System.Windows.Forms.Label();
            this.pdf_button = new System.Windows.Forms.Button();
            this.sheet_groupBox.SuspendLayout();
            this.actions_groupBox.SuspendLayout();
            this.pool_groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // sheet_groupBox
            // 
            this.sheet_groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sheet_groupBox.Controls.Add(this.sheet_richTextBox);
            this.sheet_groupBox.Location = new System.Drawing.Point(12, 12);
            this.sheet_groupBox.Name = "sheet_groupBox";
            this.sheet_groupBox.Size = new System.Drawing.Size(602, 734);
            this.sheet_groupBox.TabIndex = 0;
            this.sheet_groupBox.TabStop = false;
            this.sheet_groupBox.Text = "Character Sheet";
            // 
            // sheet_richTextBox
            // 
            this.sheet_richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sheet_richTextBox.Location = new System.Drawing.Point(3, 16);
            this.sheet_richTextBox.Name = "sheet_richTextBox";
            this.sheet_richTextBox.Size = new System.Drawing.Size(596, 715);
            this.sheet_richTextBox.TabIndex = 0;
            this.sheet_richTextBox.Text = "";
            // 
            // actions_groupBox
            // 
            this.actions_groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actions_groupBox.Controls.Add(this.actions_flowLayoutPanel);
            this.actions_groupBox.Location = new System.Drawing.Point(620, 62);
            this.actions_groupBox.Name = "actions_groupBox";
            this.actions_groupBox.Size = new System.Drawing.Size(352, 658);
            this.actions_groupBox.TabIndex = 1;
            this.actions_groupBox.TabStop = false;
            this.actions_groupBox.Text = "Actions";
            // 
            // actions_flowLayoutPanel
            // 
            this.actions_flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actions_flowLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.actions_flowLayoutPanel.Name = "actions_flowLayoutPanel";
            this.actions_flowLayoutPanel.Size = new System.Drawing.Size(346, 639);
            this.actions_flowLayoutPanel.TabIndex = 0;
            // 
            // pool_groupBox
            // 
            this.pool_groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pool_groupBox.Controls.Add(this.pool_label);
            this.pool_groupBox.Location = new System.Drawing.Point(620, 12);
            this.pool_groupBox.Name = "pool_groupBox";
            this.pool_groupBox.Size = new System.Drawing.Size(352, 44);
            this.pool_groupBox.TabIndex = 3;
            this.pool_groupBox.TabStop = false;
            this.pool_groupBox.Text = "Pool";
            // 
            // pool_label
            // 
            this.pool_label.AutoSize = true;
            this.pool_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pool_label.Location = new System.Drawing.Point(6, 16);
            this.pool_label.Name = "pool_label";
            this.pool_label.Size = new System.Drawing.Size(44, 20);
            this.pool_label.TabIndex = 0;
            this.pool_label.Text = "Pool";
            // 
            // pdf_button
            // 
            this.pdf_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pdf_button.Location = new System.Drawing.Point(623, 726);
            this.pdf_button.Name = "pdf_button";
            this.pdf_button.Size = new System.Drawing.Size(349, 23);
            this.pdf_button.TabIndex = 4;
            this.pdf_button.Text = "Save to PDF";
            this.pdf_button.UseVisualStyleBackColor = true;
            this.pdf_button.Click += new System.EventHandler(this.pdf_button_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.pdf_button);
            this.Controls.Add(this.pool_groupBox);
            this.Controls.Add(this.actions_groupBox);
            this.Controls.Add(this.sheet_groupBox);
            this.Name = "MainForm";
            this.Text = "Investigator Academy - Dummy GUI";
            this.sheet_groupBox.ResumeLayout(false);
            this.actions_groupBox.ResumeLayout(false);
            this.pool_groupBox.ResumeLayout(false);
            this.pool_groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox sheet_groupBox;
        private System.Windows.Forms.RichTextBox sheet_richTextBox;
        private System.Windows.Forms.GroupBox actions_groupBox;
        private System.Windows.Forms.FlowLayoutPanel actions_flowLayoutPanel;
        private System.Windows.Forms.GroupBox pool_groupBox;
        private System.Windows.Forms.Label pool_label;
        private System.Windows.Forms.Button pdf_button;
    }
}

