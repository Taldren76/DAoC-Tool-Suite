using System.Windows.Forms;

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
            TablePanel = new Panel();
            dataGridView1 = new DataGridView();
            ResetButton = new Button();
            LogDatesComboBox = new ComboBox();
            ParseButton = new Button();
            label1 = new Label();
            UpdateButton = new Button();
            checkedListBox1 = new CheckedListBox();
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
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            ThreeDFontCheckBox = new CheckBox();
            SectionLabel = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            TablePanel.SuspendLayout();
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
            splitContainer1.Panel1.Controls.Add(TablePanel);
            splitContainer1.Panel1.Controls.Add(ResetButton);
            splitContainer1.Panel1.Controls.Add(LogDatesComboBox);
            splitContainer1.Panel1.Controls.Add(ParseButton);
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            splitContainer1.Panel1.ForeColor = SystemColors.ControlText;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(SectionLabel);
            splitContainer1.Panel2.Controls.Add(ThreeDFontCheckBox);
            splitContainer1.Panel2.Controls.Add(UpdateButton);
            splitContainer1.Panel2.Controls.Add(checkedListBox1);
            splitContainer1.Panel2.Controls.Add(FilterPlayersOnlyCheckBox);
            splitContainer1.Panel2.Controls.Add(OverlayTransparentCheckBox);
            splitContainer1.Panel2.Controls.Add(FontColorPanel);
            splitContainer1.Panel2.Controls.Add(OverLayFontColorButton);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Panel2.Controls.Add(OverLayOpacityControl);
            splitContainer1.Panel2.Controls.Add(LockOverlayButton);
            splitContainer1.Panel2.Controls.Add(OverlayButton);
            splitContainer1.Panel2.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            splitContainer1.Size = new Size(679, 385);
            splitContainer1.SplitterDistance = 525;
            splitContainer1.TabIndex = 2;
            // 
            // TablePanel
            // 
            TablePanel.BackColor = Color.Black;
            TablePanel.Controls.Add(dataGridView1);
            TablePanel.ForeColor = Color.White;
            TablePanel.Location = new Point(3, 54);
            TablePanel.Name = "TablePanel";
            TablePanel.Size = new Size(519, 328);
            TablePanel.TabIndex = 5;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.BackgroundColor = Color.FromArgb(0, 0, 0);
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(0, 0, 0);
            dataGridViewCellStyle1.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(0, 0, 0);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(255, 255, 255);
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.GridColor = Color.FromArgb(0, 0, 0);
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
            dataGridView1.Size = new Size(519, 328);
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
            label1.Size = new Size(95, 14);
            label1.TabIndex = 3;
            label1.Text = "Start Location";
            // 
            // UpdateButton
            // 
            UpdateButton.Location = new Point(53, 359);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(75, 23);
            UpdateButton.TabIndex = 9;
            UpdateButton.Text = "Update";
            UpdateButton.UseVisualStyleBackColor = true;
            UpdateButton.Click += UpdateButton_Click;
            // 
            // checkedListBox1
            // 
            checkedListBox1.BackColor = SystemColors.ControlLightLight;
            checkedListBox1.CheckOnClick = true;
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Items.AddRange(new object[] { "Spell", "Healing", "Melee", "Defense", "Mitigation", "Pet" });
            checkedListBox1.Location = new Point(12, 247);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(116, 106);
            checkedListBox1.TabIndex = 8;
            // 
            // FilterPlayersOnlyCheckBox
            // 
            FilterPlayersOnlyCheckBox.AutoSize = true;
            FilterPlayersOnlyCheckBox.Location = new Point(12, 186);
            FilterPlayersOnlyCheckBox.Name = "FilterPlayersOnlyCheckBox";
            FilterPlayersOnlyCheckBox.Size = new Size(104, 18);
            FilterPlayersOnlyCheckBox.TabIndex = 7;
            FilterPlayersOnlyCheckBox.Text = "Players Only";
            FilterPlayersOnlyCheckBox.UseVisualStyleBackColor = true;
            FilterPlayersOnlyCheckBox.CheckedChanged += FilterPlayersOnlyCheckBox_CheckedChanged;
            // 
            // OverlayTransparentCheckBox
            // 
            OverlayTransparentCheckBox.AutoSize = true;
            OverlayTransparentCheckBox.Checked = true;
            OverlayTransparentCheckBox.CheckState = CheckState.Checked;
            OverlayTransparentCheckBox.Location = new Point(12, 138);
            OverlayTransparentCheckBox.Name = "OverlayTransparentCheckBox";
            OverlayTransparentCheckBox.Size = new Size(102, 18);
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
            label2.Size = new Size(106, 14);
            label2.TabIndex = 3;
            label2.Text = "Overlay Opacity";
            // 
            // OverLayOpacityControl
            // 
            OverLayOpacityControl.Location = new Point(12, 80);
            OverLayOpacityControl.Name = "OverLayOpacityControl";
            OverLayOpacityControl.Size = new Size(116, 22);
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
            ParseProgressBar.CustomText = "Loading File";
            ParseProgressBar.ForeColor = Color.Chartreuse;
            ParseProgressBar.Location = new Point(15, 470);
            ParseProgressBar.Name = "ParseProgressBar";
            ParseProgressBar.ProgressColor = Color.Chartreuse;
            ParseProgressBar.Size = new Size(670, 23);
            ParseProgressBar.TabIndex = 3;
            ParseProgressBar.TextColor = Color.Black;
            ParseProgressBar.TextFont = new Font("Times New Roman", 11F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            ParseProgressBar.Visible = false;
            ParseProgressBar.VisualMode = ProgressBarDisplayMode.TextAndPercentage;
            // 
            // Column1
            // 
            Column1.HeaderText = "Column1";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 5;
            // 
            // Column2
            // 
            Column2.HeaderText = "Column2";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 5;
            // 
            // ThreeDFontCheckBox
            // 
            ThreeDFontCheckBox.AutoSize = true;
            ThreeDFontCheckBox.Checked = true;
            ThreeDFontCheckBox.CheckState = CheckState.Checked;
            ThreeDFontCheckBox.Location = new Point(12, 162);
            ThreeDFontCheckBox.Name = "ThreeDFontCheckBox";
            ThreeDFontCheckBox.Size = new Size(82, 18);
            ThreeDFontCheckBox.TabIndex = 10;
            ThreeDFontCheckBox.Text = "3D Effect";
            ThreeDFontCheckBox.UseVisualStyleBackColor = true;
            ThreeDFontCheckBox.CheckedChanged += ThreeDFontCheckBox_CheckedChanged;
            // 
            // SectionLabel
            // 
            SectionLabel.AutoSize = true;
            SectionLabel.Location = new Point(12, 230);
            SectionLabel.Name = "SectionLabel";
            SectionLabel.Size = new Size(117, 14);
            SectionLabel.TabIndex = 11;
            SectionLabel.Text = "Overlay Sections:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 497);
            Controls.Add(splitContainer1);
            Controls.Add(ParseProgressBar);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(720, 540);
            MinimizeBox = false;
            MinimumSize = new Size(720, 540);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LogTool v0.8.0";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            TablePanel.ResumeLayout(false);
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
        private Panel TablePanel;
        private TextProgressBar ParseProgressBar;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private CheckedListBox checkedListBox1;
        private Button UpdateButton;
        private CheckBox ThreeDFontCheckBox;
        private Label SectionLabel;
    }
}
