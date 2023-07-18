namespace DAoCToolSuite.CharacterTool
{
    partial class EditDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditDialog));
            panel5 = new Panel();
            BackUpDescriptionTextBox = new TextBox();
            BackupLabel = new Label();
            SaveBackUp = new Button();
            BackUpDescriptionLabel = new Label();
            BackUpServerTextBox = new TextBox();
            BackUpNameComboBox = new ComboBox();
            BackUpClassLabel = new Label();
            BackUpServerLabel = new Label();
            BackUpClassComboBox = new ComboBox();
            BackUpRealmComboBox = new ComboBox();
            BackupRealmLabel = new Label();
            UpdateButton = new Button();
            panel1 = new Panel();
            UpdateLabel = new Label();
            panel5.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(BackUpDescriptionTextBox);
            panel5.Controls.Add(BackupLabel);
            panel5.Controls.Add(SaveBackUp);
            panel5.Controls.Add(BackUpDescriptionLabel);
            panel5.Controls.Add(BackUpServerTextBox);
            panel5.Controls.Add(BackUpNameComboBox);
            panel5.Controls.Add(BackUpClassLabel);
            panel5.Controls.Add(BackUpServerLabel);
            panel5.Controls.Add(BackUpClassComboBox);
            panel5.Controls.Add(BackUpRealmComboBox);
            panel5.Controls.Add(BackupRealmLabel);
            panel5.Location = new Point(12, 12);
            panel5.Name = "panel5";
            panel5.Size = new Size(737, 165);
            panel5.TabIndex = 25;
            // 
            // BackUpDescriptionTextBox
            // 
            BackUpDescriptionTextBox.Location = new Point(12, 122);
            BackUpDescriptionTextBox.Name = "BackUpDescriptionTextBox";
            BackUpDescriptionTextBox.Size = new Size(612, 23);
            BackUpDescriptionTextBox.TabIndex = 19;
            // 
            // BackupLabel
            // 
            BackupLabel.AutoSize = true;
            BackupLabel.Location = new Point(12, 12);
            BackupLabel.Name = "BackupLabel";
            BackupLabel.Size = new Size(49, 15);
            BackupLabel.TabIndex = 12;
            BackupLabel.Text = "Backup:";
            // 
            // SaveBackUp
            // 
            SaveBackUp.Location = new Point(644, 122);
            SaveBackUp.Name = "SaveBackUp";
            SaveBackUp.Size = new Size(75, 23);
            SaveBackUp.TabIndex = 21;
            SaveBackUp.Text = "Save";
            SaveBackUp.UseVisualStyleBackColor = true;
            SaveBackUp.Click += SaveBackUp_Click;
            // 
            // BackUpDescriptionLabel
            // 
            BackUpDescriptionLabel.AutoSize = true;
            BackUpDescriptionLabel.Location = new Point(12, 104);
            BackUpDescriptionLabel.Name = "BackUpDescriptionLabel";
            BackUpDescriptionLabel.Size = new Size(70, 15);
            BackUpDescriptionLabel.TabIndex = 20;
            BackUpDescriptionLabel.Text = "Description:";
            // 
            // BackUpServerTextBox
            // 
            BackUpServerTextBox.Location = new Point(286, 30);
            BackUpServerTextBox.Name = "BackUpServerTextBox";
            BackUpServerTextBox.ReadOnly = true;
            BackUpServerTextBox.Size = new Size(159, 23);
            BackUpServerTextBox.TabIndex = 13;
            // 
            // BackUpNameComboBox
            // 
            BackUpNameComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            BackUpNameComboBox.FormattingEnabled = true;
            BackUpNameComboBox.Location = new Point(12, 30);
            BackUpNameComboBox.Name = "BackUpNameComboBox";
            BackUpNameComboBox.Size = new Size(240, 23);
            BackUpNameComboBox.TabIndex = 11;
            // 
            // BackUpClassLabel
            // 
            BackUpClassLabel.AutoSize = true;
            BackUpClassLabel.Location = new Point(286, 56);
            BackUpClassLabel.Name = "BackUpClassLabel";
            BackUpClassLabel.Size = new Size(37, 15);
            BackUpClassLabel.TabIndex = 18;
            BackUpClassLabel.Text = "Class:";
            // 
            // BackUpServerLabel
            // 
            BackUpServerLabel.AutoSize = true;
            BackUpServerLabel.Location = new Point(286, 12);
            BackUpServerLabel.Name = "BackUpServerLabel";
            BackUpServerLabel.Size = new Size(42, 15);
            BackUpServerLabel.TabIndex = 14;
            BackUpServerLabel.Text = "Server:";
            // 
            // BackUpClassComboBox
            // 
            BackUpClassComboBox.FormattingEnabled = true;
            BackUpClassComboBox.Location = new Point(286, 78);
            BackUpClassComboBox.Name = "BackUpClassComboBox";
            BackUpClassComboBox.Size = new Size(240, 23);
            BackUpClassComboBox.TabIndex = 17;
            // 
            // BackUpRealmComboBox
            // 
            BackUpRealmComboBox.FormattingEnabled = true;
            BackUpRealmComboBox.Location = new Point(12, 78);
            BackUpRealmComboBox.Name = "BackUpRealmComboBox";
            BackUpRealmComboBox.Size = new Size(240, 23);
            BackUpRealmComboBox.TabIndex = 15;
            BackUpRealmComboBox.SelectedIndexChanged += BackUpRealmComboBox_SelectedIndexChanged;
            // 
            // BackupRealmLabel
            // 
            BackupRealmLabel.AutoSize = true;
            BackupRealmLabel.Location = new Point(12, 56);
            BackupRealmLabel.Name = "BackupRealmLabel";
            BackupRealmLabel.Size = new Size(43, 15);
            BackupRealmLabel.TabIndex = 16;
            BackupRealmLabel.Text = "Realm:";
            // 
            // UpdateButton
            // 
            UpdateButton.Location = new Point(645, 15);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(75, 23);
            UpdateButton.TabIndex = 22;
            UpdateButton.Text = "Update";
            UpdateButton.UseVisualStyleBackColor = true;
            UpdateButton.Click += UpdateButton_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(UpdateLabel);
            panel1.Controls.Add(UpdateButton);
            panel1.Location = new Point(12, 183);
            panel1.Name = "panel1";
            panel1.Size = new Size(737, 53);
            panel1.TabIndex = 26;
            // 
            // UpdateLabel
            // 
            UpdateLabel.AutoSize = true;
            UpdateLabel.Location = new Point(12, 19);
            UpdateLabel.Name = "UpdateLabel";
            UpdateLabel.Size = new Size(298, 15);
            UpdateLabel.TabIndex = 23;
            UpdateLabel.Text = "Update existing entry with current INI and IGN file data.";
            // 
            // EditDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(760, 248);
            Controls.Add(panel1);
            Controls.Add(panel5);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "EditDialog";
            Text = "Edit Record";
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel5;
        public TextBox BackUpDescriptionTextBox;
        private Label BackupLabel;
        private Button SaveBackUp;
        private Label BackUpDescriptionLabel;
        public TextBox BackUpServerTextBox;
        public ComboBox BackUpNameComboBox;
        private Label BackUpClassLabel;
        private Label BackUpServerLabel;
        public ComboBox BackUpClassComboBox;
        public ComboBox BackUpRealmComboBox;
        private Label BackupRealmLabel;
        private Button UpdateButton;
        private Panel panel1;
        private Label UpdateLabel;
    }
}