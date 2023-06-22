namespace DAoCToolSuite.ChimpTool
{
    partial class AHKForm
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
            BrowseButton = new Button();
            label4 = new Label();
            ScriptPathTextBox = new TextBox();
            label1 = new Label();
            CharacterNameTextBox = new TextBox();
            RealmTextBox = new TextBox();
            label2 = new Label();
            ServerTextBox = new TextBox();
            label3 = new Label();
            openFileDialog1 = new OpenFileDialog();
            SubmitButton = new Button();
            DeleteButton = new Button();
            VersionCheckBox = new CheckBox();
            label5 = new Label();
            SuspendLayout();
            // 
            // BrowseButton
            // 
            BrowseButton.Location = new Point(497, 147);
            BrowseButton.Name = "BrowseButton";
            BrowseButton.Size = new Size(75, 23);
            BrowseButton.TabIndex = 11;
            BrowseButton.Text = "Browse";
            BrowseButton.UseVisualStyleBackColor = true;
            BrowseButton.Click += BrowseButton_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 129);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 10;
            label4.Text = "Script File";
            // 
            // ScriptPathTextBox
            // 
            ScriptPathTextBox.Location = new Point(12, 147);
            ScriptPathTextBox.Name = "ScriptPathTextBox";
            ScriptPathTextBox.Size = new Size(478, 23);
            ScriptPathTextBox.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 74);
            label1.Name = "label1";
            label1.Size = new Size(96, 15);
            label1.TabIndex = 12;
            label1.Text = "Character Name:";
            // 
            // CharacterNameTextBox
            // 
            CharacterNameTextBox.Location = new Point(12, 92);
            CharacterNameTextBox.Name = "CharacterNameTextBox";
            CharacterNameTextBox.ReadOnly = true;
            CharacterNameTextBox.Size = new Size(174, 23);
            CharacterNameTextBox.TabIndex = 13;
            CharacterNameTextBox.TabStop = false;
            // 
            // RealmTextBox
            // 
            RealmTextBox.Location = new Point(192, 92);
            RealmTextBox.Name = "RealmTextBox";
            RealmTextBox.ReadOnly = true;
            RealmTextBox.Size = new Size(174, 23);
            RealmTextBox.TabIndex = 14;
            RealmTextBox.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(192, 74);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 15;
            label2.Text = "Realm:";
            // 
            // ServerTextBox
            // 
            ServerTextBox.Location = new Point(372, 92);
            ServerTextBox.Name = "ServerTextBox";
            ServerTextBox.ReadOnly = true;
            ServerTextBox.Size = new Size(118, 23);
            ServerTextBox.TabIndex = 16;
            ServerTextBox.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(372, 74);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 17;
            label3.Text = "Server:";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // SubmitButton
            // 
            SubmitButton.Location = new Point(415, 190);
            SubmitButton.Name = "SubmitButton";
            SubmitButton.Size = new Size(75, 23);
            SubmitButton.TabIndex = 18;
            SubmitButton.Text = "Submit";
            SubmitButton.UseVisualStyleBackColor = true;
            SubmitButton.Click += SubmitButton_Click;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(497, 190);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(75, 23);
            DeleteButton.TabIndex = 19;
            DeleteButton.Text = "Remove";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // VersionCheckBox
            // 
            VersionCheckBox.AutoSize = true;
            VersionCheckBox.Enabled = false;
            VersionCheckBox.Location = new Point(12, 190);
            VersionCheckBox.Name = "VersionCheckBox";
            VersionCheckBox.Size = new Size(104, 19);
            VersionCheckBox.TabIndex = 20;
            VersionCheckBox.Text = "Use version 2.0";
            VersionCheckBox.UseVisualStyleBackColor = true;
            VersionCheckBox.CheckedChanged += VersionCheckBox_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(12, 9);
            label5.Name = "label5";
            label5.Size = new Size(421, 56);
            label5.TabIndex = 21;
            label5.Text = "IMPORTANT NOTES:\r\n   1. Remove or comment out any #Warn entries in the script files.\r\n   2. Madgrim's Preamble is included with ChimpTool.\r\n   3. You must exit the game completely to swap profiles.";
            // 
            // AHKForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(584, 226);
            Controls.Add(label5);
            Controls.Add(VersionCheckBox);
            Controls.Add(DeleteButton);
            Controls.Add(SubmitButton);
            Controls.Add(label3);
            Controls.Add(ServerTextBox);
            Controls.Add(label2);
            Controls.Add(RealmTextBox);
            Controls.Add(CharacterNameTextBox);
            Controls.Add(label1);
            Controls.Add(BrowseButton);
            Controls.Add(label4);
            Controls.Add(ScriptPathTextBox);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "AHKForm";
            Text = "Associate AHK Script with Character";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BrowseButton;
        private Label label4;
        private TextBox ScriptPathTextBox;
        private Label label1;
        private TextBox CharacterNameTextBox;
        private TextBox RealmTextBox;
        private Label label2;
        private TextBox ServerTextBox;
        private Label label3;
        private OpenFileDialog openFileDialog1;
        private Button SubmitButton;
        private Button DeleteButton;
        private CheckBox VersionCheckBox;
        private Label label5;
    }
}