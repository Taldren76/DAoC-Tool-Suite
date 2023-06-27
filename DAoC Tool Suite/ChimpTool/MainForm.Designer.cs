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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            GridPanel = new Panel();
            LoadingTabelLabel = new Label();
            SearchGridView = new DataGridView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            launchToolStripMenuItem1 = new ToolStripMenuItem();
            refreshToolStripMenuItem1 = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            associateAHKToolStripMenuItem1 = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            moveToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            CharacterSearchPanel = new Panel();
            SearchComboBox = new ComboBox();
            RemoveButton = new Button();
            AlbionTotalsTextBox = new TextBox();
            SearchButton = new Button();
            AccountPanel = new Panel();
            DeleteAccountButton = new Button();
            AddAccountButton = new Button();
            AccountComboBox = new ComboBox();
            HiberniaTotalsTextBox = new TextBox();
            RefreshPanel = new Panel();
            DebugLogButton = new Button();
            RefreshButton = new Button();
            MidgardTotalsTextBox = new TextBox();
            RefreshAllButton = new Button();
            FooterPanel = new Panel();
            TimerLabel0 = new Label();
            TimerLabel1 = new Label();
            LaunchButton = new Button();
            TotalRPLabel = new Label();
            TotalRPTextBox = new TextBox();
            SearchProgressBar = new TextProgressBar();
            menuStrip1 = new MenuStrip();
            characterToolStripMenuItem = new ToolStripMenuItem();
            addAllCharactersToolStripMenuItem = new ToolStripMenuItem();
            RefreshAllToolStripMenuItem = new ToolStripMenuItem();
            RefreshToolStripMenuItem = new ToolStripMenuItem();
            settingsRestoreToolStripMenuItem = new ToolStripMenuItem();
            associateAHKToolStripMenuItem = new ToolStripMenuItem();
            launchToolStripMenuItem = new ToolStripMenuItem();
            accountToolStripMenuItem = new ToolStripMenuItem();
            dAoCCredentialsToolStripMenuItem = new ToolStripMenuItem();
            manageAccountsToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            propertiesToolStripMenuItem = new ToolStripMenuItem();
            backupToolStripMenuItem = new ToolStripMenuItem();
            restoreToolStripMenuItem = new ToolStripMenuItem();
            importJsonToolStripMenuItem = new ToolStripMenuItem();
            exportJsonToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem1 = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            logViewerToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            GridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SearchGridView).BeginInit();
            contextMenuStrip1.SuspendLayout();
            CharacterSearchPanel.SuspendLayout();
            AccountPanel.SuspendLayout();
            RefreshPanel.SuspendLayout();
            FooterPanel.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // GridPanel
            // 
            GridPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GridPanel.Controls.Add(LoadingTabelLabel);
            GridPanel.Controls.Add(SearchGridView);
            GridPanel.Location = new Point(14, 109);
            GridPanel.Margin = new Padding(4, 3, 4, 3);
            GridPanel.Name = "GridPanel";
            GridPanel.Size = new Size(1396, 705);
            GridPanel.TabIndex = 13;
            GridPanel.Visible = false;
            // 
            // LoadingTabelLabel
            // 
            LoadingTabelLabel.Dock = DockStyle.Fill;
            LoadingTabelLabel.Font = new Font("Verdana", 72F, FontStyle.Bold, GraphicsUnit.Point);
            LoadingTabelLabel.Location = new Point(0, 0);
            LoadingTabelLabel.Margin = new Padding(4, 0, 4, 0);
            LoadingTabelLabel.Name = "LoadingTabelLabel";
            LoadingTabelLabel.Size = new Size(1396, 705);
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
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            SearchGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            SearchGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            SearchGridView.ContextMenuStrip = contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.NullValue = "null";
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            SearchGridView.DefaultCellStyle = dataGridViewCellStyle2;
            SearchGridView.Dock = DockStyle.Fill;
            SearchGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            SearchGridView.Location = new Point(0, 0);
            SearchGridView.Margin = new Padding(4, 3, 4, 3);
            SearchGridView.Name = "SearchGridView";
            SearchGridView.ReadOnly = true;
            SearchGridView.RowHeadersVisible = false;
            SearchGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            SearchGridView.ShowCellErrors = false;
            SearchGridView.ShowEditingIcon = false;
            SearchGridView.ShowRowErrors = false;
            SearchGridView.Size = new Size(1396, 705);
            SearchGridView.TabIndex = 15;
            SearchGridView.Visible = false;
            SearchGridView.DataSourceChanged += SearchGridView_DataSourceChanged;
            SearchGridView.CellContextMenuStripNeeded += SearchGridView_CellContextMenuStripNeeded;
            SearchGridView.CellDoubleClick += SearchGridView_CellDoubleClick;
            SearchGridView.CellToolTipTextNeeded += SearchGridView_CellToolTipTextNeeded;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { launchToolStripMenuItem1, refreshToolStripMenuItem1, settingsToolStripMenuItem, associateAHKToolStripMenuItem1, toolStripSeparator1, moveToolStripMenuItem, deleteToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(181, 164);
            // 
            // launchToolStripMenuItem1
            // 
            launchToolStripMenuItem1.Name = "launchToolStripMenuItem1";
            launchToolStripMenuItem1.Size = new Size(180, 22);
            launchToolStripMenuItem1.Text = "Launch";
            launchToolStripMenuItem1.Click += RC_LaunchToolStripMenuItem_Click;
            // 
            // refreshToolStripMenuItem1
            // 
            refreshToolStripMenuItem1.Name = "refreshToolStripMenuItem1";
            refreshToolStripMenuItem1.Size = new Size(180, 22);
            refreshToolStripMenuItem1.Text = "Refresh";
            refreshToolStripMenuItem1.Click += RC_RefreshToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(180, 22);
            settingsToolStripMenuItem.Text = "Restore Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // associateAHKToolStripMenuItem1
            // 
            associateAHKToolStripMenuItem1.Name = "associateAHKToolStripMenuItem1";
            associateAHKToolStripMenuItem1.Size = new Size(180, 22);
            associateAHKToolStripMenuItem1.Text = "Associate AHK";
            associateAHKToolStripMenuItem1.Click += RC_AssociateAHKToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(177, 6);
            // 
            // moveToolStripMenuItem
            // 
            moveToolStripMenuItem.Name = "moveToolStripMenuItem";
            moveToolStripMenuItem.Size = new Size(180, 22);
            moveToolStripMenuItem.Text = "Move";
            moveToolStripMenuItem.Click += moveToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(180, 22);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += RC_DeleteToolStripMenuItem_Click;
            // 
            // CharacterSearchPanel
            // 
            CharacterSearchPanel.Controls.Add(SearchComboBox);
            CharacterSearchPanel.Controls.Add(RemoveButton);
            CharacterSearchPanel.Controls.Add(AlbionTotalsTextBox);
            CharacterSearchPanel.Controls.Add(SearchButton);
            CharacterSearchPanel.Location = new Point(14, 31);
            CharacterSearchPanel.Margin = new Padding(4, 3, 4, 3);
            CharacterSearchPanel.Name = "CharacterSearchPanel";
            CharacterSearchPanel.Size = new Size(397, 72);
            CharacterSearchPanel.TabIndex = 20;
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
            // AccountPanel
            // 
            AccountPanel.Anchor = AnchorStyles.Top;
            AccountPanel.Controls.Add(DeleteAccountButton);
            AccountPanel.Controls.Add(AddAccountButton);
            AccountPanel.Controls.Add(AccountComboBox);
            AccountPanel.Controls.Add(HiberniaTotalsTextBox);
            AccountPanel.Location = new Point(523, 31);
            AccountPanel.Margin = new Padding(4, 3, 4, 3);
            AccountPanel.Name = "AccountPanel";
            AccountPanel.Size = new Size(397, 72);
            AccountPanel.TabIndex = 21;
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
            // RefreshPanel
            // 
            RefreshPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            RefreshPanel.Controls.Add(DebugLogButton);
            RefreshPanel.Controls.Add(RefreshButton);
            RefreshPanel.Controls.Add(MidgardTotalsTextBox);
            RefreshPanel.Controls.Add(RefreshAllButton);
            RefreshPanel.Location = new Point(1013, 31);
            RefreshPanel.Margin = new Padding(4, 3, 4, 3);
            RefreshPanel.Name = "RefreshPanel";
            RefreshPanel.Size = new Size(397, 72);
            RefreshPanel.TabIndex = 22;
            // 
            // DebugLogButton
            // 
            DebugLogButton.Location = new Point(4, 6);
            DebugLogButton.Name = "DebugLogButton";
            DebugLogButton.Size = new Size(107, 29);
            DebugLogButton.TabIndex = 19;
            DebugLogButton.Text = "Debug.Log";
            DebugLogButton.UseVisualStyleBackColor = true;
            DebugLogButton.Click += DebugLog_Click;
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
            // FooterPanel
            // 
            FooterPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            FooterPanel.Controls.Add(TimerLabel0);
            FooterPanel.Controls.Add(TimerLabel1);
            FooterPanel.Controls.Add(LaunchButton);
            FooterPanel.Controls.Add(TotalRPLabel);
            FooterPanel.Controls.Add(TotalRPTextBox);
            FooterPanel.Controls.Add(SearchProgressBar);
            FooterPanel.Location = new Point(0, 820);
            FooterPanel.Margin = new Padding(4, 3, 4, 3);
            FooterPanel.Name = "FooterPanel";
            FooterPanel.Size = new Size(1424, 37);
            FooterPanel.TabIndex = 23;
            // 
            // TimerLabel0
            // 
            TimerLabel0.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            TimerLabel0.AutoSize = true;
            TimerLabel0.BackColor = SystemColors.Control;
            TimerLabel0.Font = new Font("LCD", 18F, FontStyle.Regular, GraphicsUnit.Point);
            TimerLabel0.ForeColor = Color.IndianRed;
            TimerLabel0.ImageAlign = ContentAlignment.MiddleRight;
            TimerLabel0.Location = new Point(1104, 7);
            TimerLabel0.Name = "TimerLabel0";
            TimerLabel0.Size = new Size(94, 30);
            TimerLabel0.TabIndex = 29;
            TimerLabel0.Text = "00:00";
            TimerLabel0.TextAlign = ContentAlignment.MiddleRight;
            TimerLabel0.Visible = false;
            // 
            // TimerLabel1
            // 
            TimerLabel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            TimerLabel1.AutoSize = true;
            TimerLabel1.BackColor = SystemColors.Control;
            TimerLabel1.Font = new Font("LCD", 18F, FontStyle.Regular, GraphicsUnit.Point);
            TimerLabel1.ForeColor = Color.CornflowerBlue;
            TimerLabel1.ImageAlign = ContentAlignment.MiddleRight;
            TimerLabel1.Location = new Point(1194, 7);
            TimerLabel1.Name = "TimerLabel1";
            TimerLabel1.Size = new Size(94, 30);
            TimerLabel1.TabIndex = 28;
            TimerLabel1.Text = "00:00";
            TimerLabel1.TextAlign = ContentAlignment.MiddleRight;
            TimerLabel1.Visible = false;
            // 
            // LaunchButton
            // 
            LaunchButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            LaunchButton.BackColor = SystemColors.ControlLightLight;
            LaunchButton.Font = new Font("Verdana", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            LaunchButton.ForeColor = Color.DarkSlateGray;
            LaunchButton.Location = new Point(1294, 3);
            LaunchButton.Name = "LaunchButton";
            LaunchButton.Size = new Size(116, 34);
            LaunchButton.TabIndex = 27;
            LaunchButton.Text = "LAUNCH";
            LaunchButton.UseVisualStyleBackColor = false;
            LaunchButton.Click += LaunchButton_Click;
            // 
            // TotalRPLabel
            // 
            TotalRPLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            TotalRPLabel.AutoSize = true;
            TotalRPLabel.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            TotalRPLabel.Location = new Point(16, 9);
            TotalRPLabel.Margin = new Padding(4, 0, 4, 0);
            TotalRPLabel.Name = "TotalRPLabel";
            TotalRPLabel.Size = new Size(62, 14);
            TotalRPLabel.TabIndex = 24;
            TotalRPLabel.Text = "RP Total:";
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
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { characterToolStripMenuItem, accountToolStripMenuItem, editToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1424, 24);
            menuStrip1.TabIndex = 24;
            menuStrip1.Text = "menuStrip1";
            // 
            // characterToolStripMenuItem
            // 
            characterToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addAllCharactersToolStripMenuItem, RefreshAllToolStripMenuItem, RefreshToolStripMenuItem, settingsRestoreToolStripMenuItem, associateAHKToolStripMenuItem, launchToolStripMenuItem });
            characterToolStripMenuItem.Name = "characterToolStripMenuItem";
            characterToolStripMenuItem.Size = new Size(70, 20);
            characterToolStripMenuItem.Text = "Character";
            // 
            // addAllCharactersToolStripMenuItem
            // 
            addAllCharactersToolStripMenuItem.Name = "addAllCharactersToolStripMenuItem";
            addAllCharactersToolStripMenuItem.Size = new Size(172, 22);
            addAllCharactersToolStripMenuItem.Text = "Add All Characters";
            addAllCharactersToolStripMenuItem.Click += AddAllCharactersToolStripMenuItem_Click;
            // 
            // RefreshAllToolStripMenuItem
            // 
            RefreshAllToolStripMenuItem.Name = "RefreshAllToolStripMenuItem";
            RefreshAllToolStripMenuItem.Size = new Size(172, 22);
            RefreshAllToolStripMenuItem.Text = "Refresh All";
            RefreshAllToolStripMenuItem.Click += RefreshAllToolStripMenuItem_Click;
            // 
            // RefreshToolStripMenuItem
            // 
            RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem";
            RefreshToolStripMenuItem.Size = new Size(172, 22);
            RefreshToolStripMenuItem.Text = "Refresh";
            RefreshToolStripMenuItem.Click += RefreshToolStripMenuItem_Click;
            // 
            // settingsRestoreToolStripMenuItem
            // 
            settingsRestoreToolStripMenuItem.Name = "settingsRestoreToolStripMenuItem";
            settingsRestoreToolStripMenuItem.Size = new Size(172, 22);
            settingsRestoreToolStripMenuItem.Text = "Restore Settings";
            settingsRestoreToolStripMenuItem.Click += settingsRestoreToolStripMenuItem_Click;
            // 
            // associateAHKToolStripMenuItem
            // 
            associateAHKToolStripMenuItem.Name = "associateAHKToolStripMenuItem";
            associateAHKToolStripMenuItem.Size = new Size(172, 22);
            associateAHKToolStripMenuItem.Text = "Associate AHK";
            associateAHKToolStripMenuItem.Click += AssociateAHKToolStripMenuItem_Click;
            // 
            // launchToolStripMenuItem
            // 
            launchToolStripMenuItem.Name = "launchToolStripMenuItem";
            launchToolStripMenuItem.Size = new Size(172, 22);
            launchToolStripMenuItem.Text = "Launch";
            launchToolStripMenuItem.Click += LaunchToolStripMenuItem_Click;
            // 
            // accountToolStripMenuItem
            // 
            accountToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { dAoCCredentialsToolStripMenuItem, manageAccountsToolStripMenuItem });
            accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            accountToolStripMenuItem.Size = new Size(64, 20);
            accountToolStripMenuItem.Text = "Account";
            // 
            // dAoCCredentialsToolStripMenuItem
            // 
            dAoCCredentialsToolStripMenuItem.Name = "dAoCCredentialsToolStripMenuItem";
            dAoCCredentialsToolStripMenuItem.Size = new Size(170, 22);
            dAoCCredentialsToolStripMenuItem.Text = "DAoC Credentials";
            dAoCCredentialsToolStripMenuItem.Click += DAoCCredentialsToolStripMenuItem_Click;
            // 
            // manageAccountsToolStripMenuItem
            // 
            manageAccountsToolStripMenuItem.Name = "manageAccountsToolStripMenuItem";
            manageAccountsToolStripMenuItem.Size = new Size(170, 22);
            manageAccountsToolStripMenuItem.Text = "Manage Accounts";
            manageAccountsToolStripMenuItem.Click += ManageAccountsToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { propertiesToolStripMenuItem, editToolStripMenuItem1 });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(61, 20);
            editToolStripMenuItem.Text = "Settings";
            // 
            // propertiesToolStripMenuItem
            // 
            propertiesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { backupToolStripMenuItem, restoreToolStripMenuItem, importJsonToolStripMenuItem, exportJsonToolStripMenuItem });
            propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            propertiesToolStripMenuItem.Size = new Size(148, 22);
            propertiesToolStripMenuItem.Text = "DataBase";
            // 
            // backupToolStripMenuItem
            // 
            backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            backupToolStripMenuItem.Size = new Size(153, 22);
            backupToolStripMenuItem.Text = "Full DB Backup";
            backupToolStripMenuItem.Click += BackupToolStripMenuItem_Click;
            // 
            // restoreToolStripMenuItem
            // 
            restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            restoreToolStripMenuItem.Size = new Size(153, 22);
            restoreToolStripMenuItem.Text = "Full DB Restore";
            restoreToolStripMenuItem.Click += RestoreToolStripMenuItem_Click;
            // 
            // importJsonToolStripMenuItem
            // 
            importJsonToolStripMenuItem.Name = "importJsonToolStripMenuItem";
            importJsonToolStripMenuItem.Size = new Size(153, 22);
            importJsonToolStripMenuItem.Text = "Import Json";
            importJsonToolStripMenuItem.Click += ImportJsonToolStripMenuItem_Click;
            // 
            // exportJsonToolStripMenuItem
            // 
            exportJsonToolStripMenuItem.Name = "exportJsonToolStripMenuItem";
            exportJsonToolStripMenuItem.Size = new Size(153, 22);
            exportJsonToolStripMenuItem.Text = "Export Json";
            exportJsonToolStripMenuItem.Click += ExportJsonToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem1
            // 
            editToolStripMenuItem1.Enabled = false;
            editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            editToolStripMenuItem1.Size = new Size(148, 22);
            editToolStripMenuItem1.Text = "Configuration";
            editToolStripMenuItem1.Click += EditToolStripMenuItem1_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { logViewerToolStripMenuItem, aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // logViewerToolStripMenuItem
            // 
            logViewerToolStripMenuItem.Name = "logViewerToolStripMenuItem";
            logViewerToolStripMenuItem.Size = new Size(132, 22);
            logViewerToolStripMenuItem.Text = "Log Viewer";
            logViewerToolStripMenuItem.Click += LogViewerToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(132, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1424, 859);
            Controls.Add(FooterPanel);
            Controls.Add(RefreshPanel);
            Controls.Add(AccountPanel);
            Controls.Add(CharacterSearchPanel);
            Controls.Add(GridPanel);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainForm";
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ChimpTool v2.0.2";
            FormClosing += MainForm_FormClosing;
            FormClosed += MainForm_FormClosed;
            GridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SearchGridView).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            CharacterSearchPanel.ResumeLayout(false);
            CharacterSearchPanel.PerformLayout();
            AccountPanel.ResumeLayout(false);
            AccountPanel.PerformLayout();
            RefreshPanel.ResumeLayout(false);
            RefreshPanel.PerformLayout();
            FooterPanel.ResumeLayout(false);
            FooterPanel.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel GridPanel;
        private DataGridView SearchGridView;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Panel CharacterSearchPanel;
        private Button RemoveButton;
        private TextBox AlbionTotalsTextBox;
        private Button SearchButton;
        private Panel AccountPanel;
        private Button DeleteAccountButton;
        private Button AddAccountButton;
        private TextBox HiberniaTotalsTextBox;
        private Panel RefreshPanel;
        private Button RefreshButton;
        private TextBox MidgardTotalsTextBox;
        private Button RefreshAllButton;
        private Panel FooterPanel;
        private Label TotalRPLabel;
        private TextBox TotalRPTextBox;
        private TextProgressBar SearchProgressBar;
        public ComboBox AccountComboBox;
        public ComboBox SearchComboBox;
        private Label LoadingTabelLabel;
        private Button DebugLogButton;
        private Button LaunchButton;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem propertiesToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem backupToolStripMenuItem;
        private ToolStripMenuItem restoreToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem1;
        private ToolStripMenuItem logViewerToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem characterToolStripMenuItem;
        private ToolStripMenuItem RefreshToolStripMenuItem;
        private ToolStripMenuItem associateAHKToolStripMenuItem;
        private ToolStripMenuItem launchToolStripMenuItem;
        private ToolStripMenuItem accountToolStripMenuItem;
        private ToolStripMenuItem dAoCCredentialsToolStripMenuItem;
        private ToolStripMenuItem addAllCharactersToolStripMenuItem;
        private ToolStripMenuItem RefreshAllToolStripMenuItem;
        private Label TimerLabel1;
        private ToolStripMenuItem importJsonToolStripMenuItem;
        private ToolStripMenuItem exportJsonToolStripMenuItem;
        private Label TimerLabel0;
        private ToolStripMenuItem manageAccountsToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem launchToolStripMenuItem1;
        private ToolStripMenuItem refreshToolStripMenuItem1;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem associateAHKToolStripMenuItem1;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem settingsRestoreToolStripMenuItem;
        private ToolStripMenuItem moveToolStripMenuItem;
    }
}

