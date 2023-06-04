using System.Linq;
using System.Windows.Forms;
using NLog;

namespace DAoCToolSuite.ChimpTool
{
    partial class MainForm
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            GridPanel = new Panel();
            LoadingTabelLabel = new Label();
            SearchGridView = new DataGridView();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            panel2 = new Panel();
            SearchComboBox = new ComboBox();
            RemoveButton = new Button();
            AlbionTotalsTextBox = new TextBox();
            SearchButton = new Button();
            panel3 = new Panel();
            DeleteAccountButton = new Button();
            AddAccountButton = new Button();
            AccountComboBox = new ComboBox();
            HiberniaTotalsTextBox = new TextBox();
            panel4 = new Panel();
            linkLabel1 = new LinkLabel();
            RefreshButton = new Button();
            MidgardTotalsTextBox = new TextBox();
            RefreshAllButton = new Button();
            panel5 = new Panel();
            OnTopCheckBox = new CheckBox();
            RestoreButton = new Button();
            BackupButton = new Button();
            label1 = new Label();
            TotalRPTextBox = new TextBox();
            SearchProgressBar = new TextProgressBar();
            GridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SearchGridView).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // GridPanel
            // 
            GridPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GridPanel.Controls.Add(LoadingTabelLabel);
            GridPanel.Controls.Add(SearchGridView);
            GridPanel.Location = new Point(14, 72);
            GridPanel.Margin = new Padding(4, 3, 4, 3);
            GridPanel.Name = "GridPanel";
            GridPanel.Size = new Size(1396, 650);
            GridPanel.TabIndex = 13;
            GridPanel.Visible = false;
            // 
            // LoadingTabelLabel
            // 
            LoadingTabelLabel.Font = new Font("Verdana", 72F, FontStyle.Bold, GraphicsUnit.Point);
            LoadingTabelLabel.Location = new Point(19, 17);
            LoadingTabelLabel.Margin = new Padding(4, 0, 4, 0);
            LoadingTabelLabel.Name = "LoadingTabelLabel";
            LoadingTabelLabel.Size = new Size(1360, 617);
            LoadingTabelLabel.TabIndex = 14;
            LoadingTabelLabel.Text = "Loading table ...";
            LoadingTabelLabel.TextAlign = ContentAlignment.MiddleCenter;
            LoadingTabelLabel.Visible = false;
            // 
            // SearchGridView
            // 
            SearchGridView.AllowUserToAddRows = false;
            SearchGridView.AllowUserToDeleteRows = false;
            SearchGridView.AllowUserToResizeColumns = false;
            SearchGridView.AllowUserToResizeRows = false;
            SearchGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            SearchGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            SearchGridView.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            SearchGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            SearchGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.NullValue = "null";
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            SearchGridView.DefaultCellStyle = dataGridViewCellStyle4;
            SearchGridView.Dock = DockStyle.Fill;
            SearchGridView.Location = new Point(0, 0);
            SearchGridView.Margin = new Padding(4, 3, 4, 3);
            SearchGridView.Name = "SearchGridView";
            SearchGridView.RowHeadersVisible = false;
            SearchGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            SearchGridView.ShowCellErrors = false;
            SearchGridView.ShowEditingIcon = false;
            SearchGridView.ShowRowErrors = false;
            SearchGridView.Size = new Size(1396, 650);
            SearchGridView.TabIndex = 15;
            SearchGridView.Visible = false;
            SearchGridView.DataSourceChanged += SearchGridView_DataSourceChanged;
            SearchGridView.CellToolTipTextNeeded += SearchGridView_CellToolTipTextNeeded;
            // 
            // panel2
            // 
            panel2.Controls.Add(SearchComboBox);
            panel2.Controls.Add(RemoveButton);
            panel2.Controls.Add(AlbionTotalsTextBox);
            panel2.Controls.Add(SearchButton);
            panel2.Location = new Point(14, 0);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(397, 72);
            panel2.TabIndex = 20;
            // 
            // SearchComboBox
            // 
            SearchComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            SearchComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            SearchComboBox.FormattingEnabled = true;
            SearchComboBox.Location = new Point(6, 8);
            SearchComboBox.Margin = new Padding(4, 3, 4, 3);
            SearchComboBox.Name = "SearchComboBox";
            SearchComboBox.Size = new Size(238, 23);
            SearchComboBox.TabIndex = 1;
            SearchComboBox.KeyPress += SearchComboBox_CheckEnterKeyPress;
            // 
            // RemoveButton
            // 
            RemoveButton.Image = (Image)resources.GetObject("RemoveButton.Image");
            RemoveButton.Location = new Point(364, 6);
            RemoveButton.Margin = new Padding(4, 3, 4, 3);
            RemoveButton.Name = "RemoveButton";
            RemoveButton.Size = new Size(29, 29);
            RemoveButton.TabIndex = 3;
            RemoveButton.UseVisualStyleBackColor = true;
            RemoveButton.Click += RemoveButton_Click;
            // 
            // AlbionTotalsTextBox
            // 
            AlbionTotalsTextBox.BackColor = Color.DarkRed;
            AlbionTotalsTextBox.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            AlbionTotalsTextBox.ForeColor = Color.White;
            AlbionTotalsTextBox.Location = new Point(4, 42);
            AlbionTotalsTextBox.Margin = new Padding(4, 3, 4, 3);
            AlbionTotalsTextBox.Name = "AlbionTotalsTextBox";
            AlbionTotalsTextBox.ReadOnly = true;
            AlbionTotalsTextBox.Size = new Size(389, 22);
            AlbionTotalsTextBox.TabIndex = 16;
            AlbionTotalsTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // SearchButton
            // 
            SearchButton.Location = new Point(251, 6);
            SearchButton.Margin = new Padding(4, 3, 4, 3);
            SearchButton.Name = "SearchButton";
            SearchButton.Size = new Size(107, 29);
            SearchButton.TabIndex = 2;
            SearchButton.Text = "Add Character";
            SearchButton.UseVisualStyleBackColor = true;
            SearchButton.Click += SearchButton_Click;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top;
            panel3.Controls.Add(DeleteAccountButton);
            panel3.Controls.Add(AddAccountButton);
            panel3.Controls.Add(AccountComboBox);
            panel3.Controls.Add(HiberniaTotalsTextBox);
            panel3.Location = new Point(523, 0);
            panel3.Margin = new Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(397, 72);
            panel3.TabIndex = 21;
            // 
            // DeleteAccountButton
            // 
            DeleteAccountButton.Image = (Image)resources.GetObject("DeleteAccountButton.Image");
            DeleteAccountButton.Location = new Point(364, 6);
            DeleteAccountButton.Margin = new Padding(4, 3, 4, 3);
            DeleteAccountButton.Name = "DeleteAccountButton";
            DeleteAccountButton.Size = new Size(29, 29);
            DeleteAccountButton.TabIndex = 6;
            DeleteAccountButton.UseVisualStyleBackColor = true;
            DeleteAccountButton.Click += DeleteAccountButton_Click;
            // 
            // AddAccountButton
            // 
            AddAccountButton.Location = new Point(251, 6);
            AddAccountButton.Margin = new Padding(4, 3, 4, 3);
            AddAccountButton.Name = "AddAccountButton";
            AddAccountButton.Size = new Size(107, 29);
            AddAccountButton.TabIndex = 5;
            AddAccountButton.Text = "Add Account";
            AddAccountButton.UseVisualStyleBackColor = true;
            AddAccountButton.Click += AddAccountButton_Click;
            // 
            // AccountComboBox
            // 
            AccountComboBox.FormattingEnabled = true;
            AccountComboBox.Location = new Point(6, 8);
            AccountComboBox.Margin = new Padding(4, 3, 4, 3);
            AccountComboBox.Name = "AccountComboBox";
            AccountComboBox.Size = new Size(238, 23);
            AccountComboBox.TabIndex = 4;
            AccountComboBox.TextChanged += AccountComboBox_TextChanged;
            AccountComboBox.KeyPress += AccountComboBox_CheckEnterKeyPress;
            // 
            // HiberniaTotalsTextBox
            // 
            HiberniaTotalsTextBox.Anchor = AnchorStyles.Top;
            HiberniaTotalsTextBox.BackColor = Color.DarkGreen;
            HiberniaTotalsTextBox.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            HiberniaTotalsTextBox.ForeColor = Color.White;
            HiberniaTotalsTextBox.Location = new Point(4, 39);
            HiberniaTotalsTextBox.Margin = new Padding(4, 3, 4, 3);
            HiberniaTotalsTextBox.Name = "HiberniaTotalsTextBox";
            HiberniaTotalsTextBox.Size = new Size(389, 22);
            HiberniaTotalsTextBox.TabIndex = 17;
            HiberniaTotalsTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel4.Controls.Add(linkLabel1);
            panel4.Controls.Add(RefreshButton);
            panel4.Controls.Add(MidgardTotalsTextBox);
            panel4.Controls.Add(RefreshAllButton);
            panel4.Location = new Point(1013, 0);
            panel4.Margin = new Padding(4, 3, 4, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(397, 72);
            panel4.TabIndex = 22;
            // 
            // linkLabel1
            // 
            linkLabel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkLabel1.Location = new Point(4, 6);
            linkLabel1.Margin = new Padding(4, 0, 4, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(114, 29);
            linkLabel1.TabIndex = 7;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Debug.Log";
            linkLabel1.TextAlign = ContentAlignment.MiddleCenter;
            linkLabel1.LinkClicked += DebugLog_LinkClicked;
            // 
            // RefreshButton
            // 
            RefreshButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            RefreshButton.Location = new Point(209, 6);
            RefreshButton.Margin = new Padding(4, 3, 4, 3);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(88, 29);
            RefreshButton.TabIndex = 8;
            RefreshButton.Text = "Refresh";
            RefreshButton.UseVisualStyleBackColor = true;
            RefreshButton.Click += RefreshButton_Click;
            // 
            // MidgardTotalsTextBox
            // 
            MidgardTotalsTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MidgardTotalsTextBox.BackColor = Color.DarkBlue;
            MidgardTotalsTextBox.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            MidgardTotalsTextBox.ForeColor = Color.White;
            MidgardTotalsTextBox.Location = new Point(4, 39);
            MidgardTotalsTextBox.Margin = new Padding(4, 3, 4, 3);
            MidgardTotalsTextBox.Name = "MidgardTotalsTextBox";
            MidgardTotalsTextBox.Size = new Size(389, 22);
            MidgardTotalsTextBox.TabIndex = 18;
            MidgardTotalsTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // RefreshAllButton
            // 
            RefreshAllButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            RefreshAllButton.Location = new Point(303, 6);
            RefreshAllButton.Margin = new Padding(4, 3, 4, 3);
            RefreshAllButton.Name = "RefreshAllButton";
            RefreshAllButton.Size = new Size(88, 29);
            RefreshAllButton.TabIndex = 9;
            RefreshAllButton.Text = "Refresh All";
            RefreshAllButton.UseVisualStyleBackColor = true;
            RefreshAllButton.Click += RefreshAllButton_Click;
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel5.Controls.Add(OnTopCheckBox);
            panel5.Controls.Add(RestoreButton);
            panel5.Controls.Add(BackupButton);
            panel5.Controls.Add(label1);
            panel5.Controls.Add(TotalRPTextBox);
            panel5.Controls.Add(SearchProgressBar);
            panel5.Location = new Point(0, 722);
            panel5.Margin = new Padding(4, 3, 4, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(1424, 37);
            panel5.TabIndex = 23;
            // 
            // OnTopCheckBox
            // 
            OnTopCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            OnTopCheckBox.AutoSize = true;
            OnTopCheckBox.Location = new Point(1316, 11);
            OnTopCheckBox.Margin = new Padding(4, 3, 4, 3);
            OnTopCheckBox.Name = "OnTopCheckBox";
            OnTopCheckBox.Size = new Size(93, 19);
            OnTopCheckBox.TabIndex = 12;
            OnTopCheckBox.Text = "Keep On Top";
            OnTopCheckBox.UseVisualStyleBackColor = true;
            OnTopCheckBox.CheckedChanged += OnTopCheckBox_CheckedChanged;
            // 
            // RestoreButton
            // 
            RestoreButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            RestoreButton.Enabled = false;
            RestoreButton.Location = new Point(1199, 6);
            RestoreButton.Margin = new Padding(4, 3, 4, 3);
            RestoreButton.Name = "RestoreButton";
            RestoreButton.Size = new Size(88, 25);
            RestoreButton.TabIndex = 11;
            RestoreButton.Text = "Restore";
            RestoreButton.UseVisualStyleBackColor = true;
            RestoreButton.Click += RestoreButton_Click;
            // 
            // BackupButton
            // 
            BackupButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BackupButton.Location = new Point(1104, 6);
            BackupButton.Margin = new Padding(4, 3, 4, 3);
            BackupButton.Name = "BackupButton";
            BackupButton.Size = new Size(88, 25);
            BackupButton.TabIndex = 10;
            BackupButton.Text = "Backup";
            BackupButton.UseVisualStyleBackColor = true;
            BackupButton.Click += BackupButton_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(16, 9);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(62, 14);
            label1.TabIndex = 24;
            label1.Text = "RP Total:";
            // 
            // TotalRPTextBox
            // 
            TotalRPTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            TotalRPTextBox.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            TotalRPTextBox.Location = new Point(14, 6);
            TotalRPTextBox.Margin = new Padding(4, 3, 4, 3);
            TotalRPTextBox.Name = "TotalRPTextBox";
            TotalRPTextBox.ReadOnly = true;
            TotalRPTextBox.Size = new Size(279, 22);
            TotalRPTextBox.TabIndex = 19;
            TotalRPTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // SearchProgressBar
            // 
            SearchProgressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SearchProgressBar.CustomText = "";
            SearchProgressBar.ForeColor = Color.Lime;
            SearchProgressBar.Location = new Point(327, 6);
            SearchProgressBar.Margin = new Padding(4, 3, 4, 3);
            SearchProgressBar.Name = "SearchProgressBar";
            SearchProgressBar.ProgressColor = Color.Chartreuse;
            SearchProgressBar.Size = new Size(770, 25);
            SearchProgressBar.TabIndex = 25;
            SearchProgressBar.TextColor = Color.Black;
            SearchProgressBar.TextFont = new Font("Verdana", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            SearchProgressBar.Visible = false;
            SearchProgressBar.VisualMode = ProgressBarDisplayMode.Percentage;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1424, 761);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(GridPanel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainForm";
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ChimpTool v1.1.0";
            FormClosing += MainForm_Closing;
            GridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SearchGridView).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel GridPanel;
        private DataGridView SearchGridView;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Panel panel2;
        private Button RemoveButton;
        private TextBox AlbionTotalsTextBox;
        private Button SearchButton;
        private Panel panel3;
        private Button DeleteAccountButton;
        private Button AddAccountButton;
        private TextBox HiberniaTotalsTextBox;
        private Panel panel4;
        private LinkLabel linkLabel1;
        private Button RefreshButton;
        private TextBox MidgardTotalsTextBox;
        private Button RefreshAllButton;
        private Panel panel5;
        private CheckBox OnTopCheckBox;
        private Button RestoreButton;
        private Button BackupButton;
        private Label label1;
        private TextBox TotalRPTextBox;
        internal TextProgressBar SearchProgressBar;
        public ComboBox AccountComboBox;
        public ComboBox SearchComboBox;
        private Label LoadingTabelLabel;
    }
}

