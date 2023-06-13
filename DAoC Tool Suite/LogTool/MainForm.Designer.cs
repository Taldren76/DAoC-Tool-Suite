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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            BrowseButton = new Button();
            groupBox1 = new GroupBox();
            LogFileTextBox = new TextBox();
            splitContainer1 = new SplitContainer();
            panel1 = new Panel();
            dataGridView1 = new DataGridView();
            ResetButton = new Button();
            LogDatesComboBox = new ComboBox();
            ParseButton = new Button();
            label1 = new Label();
            FilterPlayersOnlyCheckBox = new CheckBox();
            OverlayTransparentCheckBox = new CheckBox();
            FontColorPanel = new Panel();
            OverLayFontColorButton = new Button();
            label2 = new Label();
            OverLayOpacityControl = new NumericUpDown();
            LockOverlayButton = new Button();
            OverlayButton = new Button();
            colorDialog1 = new ColorDialog();
            ParseProgressBar = new TextProgressBar();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OverLayOpacityControl).BeginInit();
            SuspendLayout();
            // 
            // BrowseButton
            // 
            BrowseButton.Location = new Point(598, 22);
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
            groupBox1.Size = new Size(679, 60);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "LogFile Location and Name";
            // 
            // LogFileTextBox
            // 
            LogFileTextBox.Location = new Point(6, 22);
            LogFileTextBox.Name = "LogFileTextBox";
            LogFileTextBox.Size = new Size(586, 23);
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
            splitContainer1.Panel1.Controls.Add(panel1);
            splitContainer1.Panel1.Controls.Add(ResetButton);
            splitContainer1.Panel1.Controls.Add(LogDatesComboBox);
            splitContainer1.Panel1.Controls.Add(ParseButton);
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.ForeColor = SystemColors.ControlText;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(FilterPlayersOnlyCheckBox);
            splitContainer1.Panel2.Controls.Add(OverlayTransparentCheckBox);
            splitContainer1.Panel2.Controls.Add(FontColorPanel);
            splitContainer1.Panel2.Controls.Add(OverLayFontColorButton);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Panel2.Controls.Add(OverLayOpacityControl);
            splitContainer1.Panel2.Controls.Add(LockOverlayButton);
            splitContainer1.Panel2.Controls.Add(OverlayButton);
            splitContainer1.Size = new Size(679, 280);
            splitContainer1.SplitterDistance = 525;
            splitContainer1.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Controls.Add(dataGridView1);
            panel1.Location = new Point(3, 54);
            panel1.Name = "panel1";
            panel1.Size = new Size(519, 226);
            panel1.TabIndex = 5;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.BackgroundColor = Color.Black;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.BackColor = Color.Black;
            dataGridViewCellStyle1.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.GridColor = Color.DimGray;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.ShowCellErrors = false;
            dataGridView1.ShowCellToolTips = false;
            dataGridView1.ShowEditingIcon = false;
            dataGridView1.ShowRowErrors = false;
            dataGridView1.Size = new Size(519, 226);
            dataGridView1.TabIndex = 114;
            dataGridView1.TabStop = false;
            // 
            // ResetButton
            // 
            ResetButton.BackColor = SystemColors.ControlLightLight;
            ResetButton.ForeColor = SystemColors.ControlText;
            ResetButton.Location = new Point(264, 25);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(75, 23);
            ResetButton.TabIndex = 4;
            ResetButton.Text = "Reset";
            ResetButton.UseVisualStyleBackColor = false;
            ResetButton.Click += ResetButton_Click;
            // 
            // LogDatesComboBox
            // 
            LogDatesComboBox.BackColor = SystemColors.ControlLightLight;
            LogDatesComboBox.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            LogDatesComboBox.ForeColor = SystemColors.ControlText;
            LogDatesComboBox.FormattingEnabled = true;
            LogDatesComboBox.Location = new Point(6, 25);
            LogDatesComboBox.Name = "LogDatesComboBox";
            LogDatesComboBox.Size = new Size(171, 22);
            LogDatesComboBox.TabIndex = 2;
            LogDatesComboBox.SelectedIndexChanged += LogDatesComboBox_SelectedIndexChange;
            // 
            // ParseButton
            // 
            ParseButton.BackColor = SystemColors.ControlLightLight;
            ParseButton.ForeColor = SystemColors.ControlText;
            ParseButton.Location = new Point(183, 25);
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
            // FilterPlayersOnlyCheckBox
            // 
            FilterPlayersOnlyCheckBox.AutoSize = true;
            FilterPlayersOnlyCheckBox.Location = new Point(12, 163);
            FilterPlayersOnlyCheckBox.Name = "FilterPlayersOnlyCheckBox";
            FilterPlayersOnlyCheckBox.Size = new Size(91, 19);
            FilterPlayersOnlyCheckBox.TabIndex = 7;
            FilterPlayersOnlyCheckBox.Text = "Players Only";
            FilterPlayersOnlyCheckBox.UseVisualStyleBackColor = true;
            FilterPlayersOnlyCheckBox.CheckedChanged += FilterPlayersOnlyCheckBox_CheckedChanged;
            // 
            // OverlayTransparentCheckBox
            // 
            OverlayTransparentCheckBox.AutoSize = true;
            OverlayTransparentCheckBox.Location = new Point(12, 138);
            OverlayTransparentCheckBox.Name = "OverlayTransparentCheckBox";
            OverlayTransparentCheckBox.Size = new Size(87, 19);
            OverlayTransparentCheckBox.TabIndex = 6;
            OverlayTransparentCheckBox.Text = "Transparent";
            OverlayTransparentCheckBox.UseVisualStyleBackColor = true;
            OverlayTransparentCheckBox.CheckedChanged += OverlayTransparentCheckBox_CheckedChanged;
            // 
            // FontColorPanel
            // 
            FontColorPanel.BorderStyle = BorderStyle.FixedSingle;
            FontColorPanel.Location = new Point(105, 109);
            FontColorPanel.Name = "FontColorPanel";
            FontColorPanel.Size = new Size(23, 23);
            FontColorPanel.TabIndex = 5;
            // 
            // OverLayFontColorButton
            // 
            OverLayFontColorButton.Location = new Point(12, 109);
            OverLayFontColorButton.Name = "OverLayFontColorButton";
            OverLayFontColorButton.Size = new Size(87, 23);
            OverLayFontColorButton.TabIndex = 4;
            OverLayFontColorButton.Text = "Font Color";
            OverLayFontColorButton.UseVisualStyleBackColor = true;
            OverLayFontColorButton.Click += OverLayFontColorButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 62);
            label2.Name = "label2";
            label2.Size = new Size(91, 15);
            label2.TabIndex = 3;
            label2.Text = "Overlay Opacity";
            // 
            // OverLayOpacityControl
            // 
            OverLayOpacityControl.Location = new Point(12, 80);
            OverLayOpacityControl.Name = "OverLayOpacityControl";
            OverLayOpacityControl.Size = new Size(116, 23);
            OverLayOpacityControl.TabIndex = 2;
            OverLayOpacityControl.ValueChanged += OverLayOpacityControl_ValueChanged;
            // 
            // LockOverlayButton
            // 
            LockOverlayButton.Location = new Point(12, 36);
            LockOverlayButton.Name = "LockOverlayButton";
            LockOverlayButton.Size = new Size(116, 23);
            LockOverlayButton.TabIndex = 1;
            LockOverlayButton.Text = "Lock Overlay";
            LockOverlayButton.UseVisualStyleBackColor = true;
            LockOverlayButton.Click += LockOverlayButton_Click;
            // 
            // OverlayButton
            // 
            OverlayButton.Location = new Point(12, 7);
            OverlayButton.Name = "OverlayButton";
            OverlayButton.Size = new Size(116, 23);
            OverlayButton.TabIndex = 0;
            OverlayButton.Text = "Show Overlay";
            OverlayButton.UseVisualStyleBackColor = true;
            OverlayButton.Click += OverlayButton_Click;
            // 
            // ParseProgressBar
            // 
            ParseProgressBar.CustomText = "";
            ParseProgressBar.ForeColor = Color.Chartreuse;
            ParseProgressBar.Location = new Point(15, 361);
            ParseProgressBar.Name = "ParseProgressBar";
            ParseProgressBar.ProgressColor = Color.LightGreen;
            ParseProgressBar.Size = new Size(670, 23);
            ParseProgressBar.TabIndex = 3;
            ParseProgressBar.TextColor = Color.Black;
            ParseProgressBar.TextFont = new Font("Times New Roman", 11F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            ParseProgressBar.Visible = false;
            ParseProgressBar.VisualMode = ProgressBarDisplayMode.CurrProgress;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 387);
            Controls.Add(ParseProgressBar);
            Controls.Add(splitContainer1);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(720, 430);
            MinimumSize = new Size(720, 430);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LogTool v0.5.0";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)OverLayOpacityControl).EndInit();
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
        private Label label2;
        private NumericUpDown OverLayOpacityControl;
        private Button OverLayFontColorButton;
        private ColorDialog colorDialog1;
        private Panel FontColorPanel;
        private CheckBox OverlayTransparentCheckBox;
        private CheckBox FilterPlayersOnlyCheckBox;
        private DataGridView dataGridView1;
        private Panel panel1;
        private TextProgressBar ParseProgressBar;
    }
}
