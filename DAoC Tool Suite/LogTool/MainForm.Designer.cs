namespace DAoCToolSuite.LogTool
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            BrowseButton = new Button();
            groupBox1 = new GroupBox();
            LogFileTextBox = new TextBox();
            splitContainer1 = new SplitContainer();
            ResetButton = new Button();
            LogDatesComboBox = new ComboBox();
            ParseButton = new Button();
            label1 = new Label();
            OverlayButton = new Button();
            LockOverlayButton = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // BrowseButton
            // 
            BrowseButton.Location = new Point(698, 21);
            BrowseButton.Name = "BrowseButton";
            BrowseButton.Size = new Size(75, 23);
            BrowseButton.TabIndex = 0;
            BrowseButton.Text = "Browse";
            BrowseButton.UseVisualStyleBackColor = true;
            BrowseButton.Click += BrowseButton_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(LogFileTextBox);
            groupBox1.Controls.Add(BrowseButton);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(776, 60);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "LogFile Location and Name";
            // 
            // LogFileTextBox
            // 
            LogFileTextBox.Location = new Point(6, 22);
            LogFileTextBox.Name = "LogFileTextBox";
            LogFileTextBox.Size = new Size(686, 23);
            LogFileTextBox.TabIndex = 1;
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(12, 78);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = SystemColors.Control;
            splitContainer1.Panel1.Controls.Add(ResetButton);
            splitContainer1.Panel1.Controls.Add(LogDatesComboBox);
            splitContainer1.Panel1.Controls.Add(ParseButton);
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.ForeColor = SystemColors.ControlText;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(LockOverlayButton);
            splitContainer1.Panel2.Controls.Add(OverlayButton);
            splitContainer1.Size = new Size(776, 360);
            splitContainer1.SplitterDistance = 631;
            splitContainer1.TabIndex = 2;
            // 
            // ResetButton
            // 
            ResetButton.BackColor = SystemColors.Control;
            ResetButton.ForeColor = SystemColors.ControlText;
            ResetButton.Location = new Point(228, 25);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(75, 23);
            ResetButton.TabIndex = 4;
            ResetButton.Text = "Reset";
            ResetButton.UseVisualStyleBackColor = false;
            ResetButton.Click += ResetButton_Click;
            // 
            // LogDatesComboBox
            // 
            LogDatesComboBox.BackColor = SystemColors.Control;
            LogDatesComboBox.ForeColor = SystemColors.ControlText;
            LogDatesComboBox.FormattingEnabled = true;
            LogDatesComboBox.Location = new Point(6, 25);
            LogDatesComboBox.Name = "LogDatesComboBox";
            LogDatesComboBox.Size = new Size(135, 23);
            LogDatesComboBox.TabIndex = 2;
            LogDatesComboBox.SelectedIndexChanged += LogDatesComboBox_SelectedIndexChange;
            // 
            // ParseButton
            // 
            ParseButton.BackColor = SystemColors.Control;
            ParseButton.ForeColor = SystemColors.ControlText;
            ParseButton.Location = new Point(147, 24);
            ParseButton.Name = "ParseButton";
            ParseButton.Size = new Size(75, 23);
            ParseButton.TabIndex = 1;
            ParseButton.Text = "Start";
            ParseButton.UseVisualStyleBackColor = false;
            ParseButton.Click += ParseButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(6, 7);
            label1.Name = "label1";
            label1.Size = new Size(80, 15);
            label1.TabIndex = 3;
            label1.Text = "Start Location";
            // 
            // OverlayButton
            // 
            OverlayButton.Location = new Point(20, 24);
            OverlayButton.Name = "OverlayButton";
            OverlayButton.Size = new Size(103, 23);
            OverlayButton.TabIndex = 0;
            OverlayButton.Text = "Show Overlay";
            OverlayButton.UseVisualStyleBackColor = true;
            OverlayButton.Click += OverlayButton_Click;
            // 
            // LockOverlayButton
            // 
            LockOverlayButton.Location = new Point(20, 74);
            LockOverlayButton.Name = "LockOverlayButton";
            LockOverlayButton.Size = new Size(103, 23);
            LockOverlayButton.TabIndex = 1;
            LockOverlayButton.Text = "Lock Overlay";
            LockOverlayButton.UseVisualStyleBackColor = true;
            LockOverlayButton.Click += LockOverlayButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "LogTool v0.1.0";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button BrowseButton;
        private GroupBox groupBox1;
        private TextBox LogFileTextBox;
        private SplitContainer splitContainer1;
        private Button ParseButton;
        private ComboBox LogDatesComboBox;
        private Label label1;
        private Button ResetButton;
        private Button OverlayButton;
        private Button LockOverlayButton;
    }
}