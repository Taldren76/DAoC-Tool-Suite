namespace DAoCToolSuite.ChimpTool
{
    partial class ConfigurationForm
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
            LeftClickComboBox = new ComboBox();
            ShiftLeftClickComboBox = new ComboBox();
            groupBox1 = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            INIFileBrowseButton = new Button();
            label3 = new Label();
            INIFileTextBox = new TextBox();
            GameLocationBrowseButton = new Button();
            label4 = new Label();
            GameLocationTextBox = new TextBox();
            SaveButton = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // LeftClickComboBox
            // 
            LeftClickComboBox.FormattingEnabled = true;
            LeftClickComboBox.Items.AddRange(new object[] { "Ywain", "Gaheris" });
            LeftClickComboBox.Location = new Point(15, 37);
            LeftClickComboBox.Name = "LeftClickComboBox";
            LeftClickComboBox.Size = new Size(121, 23);
            LeftClickComboBox.TabIndex = 0;
            LeftClickComboBox.KeyPress += LeftClickComboBox_KeyPress;
            LeftClickComboBox.SelectedIndexChanged += LeftClickComboBox_SelectedIndexChanged;
            // 
            // ShiftLeftClickComboBox
            // 
            ShiftLeftClickComboBox.FormattingEnabled = true;
            ShiftLeftClickComboBox.Items.AddRange(new object[] { "Gaheris", "Ywain" });
            ShiftLeftClickComboBox.Location = new Point(15, 81);
            ShiftLeftClickComboBox.Name = "ShiftLeftClickComboBox";
            ShiftLeftClickComboBox.Size = new Size(121, 23);
            ShiftLeftClickComboBox.TabIndex = 1;
            ShiftLeftClickComboBox.KeyPress += ShiftLeftClickComboBox_KeyPress;
            ShiftLeftClickComboBox.SelectedIndexChanged += ShiftLeftClickComboBox_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(ShiftLeftClickComboBox);
            groupBox1.Controls.Add(LeftClickComboBox);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(148, 117);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Add Character Button";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 3;
            label2.Text = "Left-Click";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 63);
            label1.Name = "label1";
            label1.Size = new Size(96, 15);
            label1.TabIndex = 2;
            label1.Text = "Shift + Left-Click";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(INIFileBrowseButton);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(INIFileTextBox);
            groupBox2.Controls.Add(GameLocationBrowseButton);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(GameLocationTextBox);
            groupBox2.Location = new Point(175, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(585, 117);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "File Location";
            // 
            // INIFileBrowseButton
            // 
            INIFileBrowseButton.Location = new Point(495, 80);
            INIFileBrowseButton.Name = "INIFileBrowseButton";
            INIFileBrowseButton.Size = new Size(75, 23);
            INIFileBrowseButton.TabIndex = 14;
            INIFileBrowseButton.Text = "Browse";
            INIFileBrowseButton.UseVisualStyleBackColor = true;
            INIFileBrowseButton.Click += INIFileBrowseButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 63);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 13;
            label3.Text = "INI Files:";
            // 
            // INIFileTextBox
            // 
            INIFileTextBox.Location = new Point(6, 81);
            INIFileTextBox.Name = "INIFileTextBox";
            INIFileTextBox.Size = new Size(478, 23);
            INIFileTextBox.TabIndex = 12;
            // 
            // GameLocationBrowseButton
            // 
            GameLocationBrowseButton.Location = new Point(495, 36);
            GameLocationBrowseButton.Name = "GameLocationBrowseButton";
            GameLocationBrowseButton.Size = new Size(75, 23);
            GameLocationBrowseButton.TabIndex = 11;
            GameLocationBrowseButton.Text = "Browse";
            GameLocationBrowseButton.UseVisualStyleBackColor = true;
            GameLocationBrowseButton.Click += GameLocationBrowseButton_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 19);
            label4.Name = "label4";
            label4.Size = new Size(57, 15);
            label4.TabIndex = 10;
            label4.Text = "Game.dll:";
            // 
            // GameLocationTextBox
            // 
            GameLocationTextBox.Location = new Point(6, 37);
            GameLocationTextBox.Name = "GameLocationTextBox";
            GameLocationTextBox.Size = new Size(478, 23);
            GameLocationTextBox.TabIndex = 9;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(670, 135);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 4;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // ConfigurationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(772, 165);
            Controls.Add(SaveButton);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ConfigurationForm";
            Text = "Configuration";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        private void LeftClickComboBox_KeyPress1(object sender, KeyPressEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private ComboBox LeftClickComboBox;
        private ComboBox ShiftLeftClickComboBox;
        private GroupBox groupBox1;
        private Label label2;
        private Label label1;
        private GroupBox groupBox2;
        private Button GameLocationBrowseButton;
        private Label label4;
        private TextBox GameLocationTextBox;
        private Button SaveButton;
        private Button INIFileBrowseButton;
        private Label label3;
        private TextBox INIFileTextBox;
    }
}