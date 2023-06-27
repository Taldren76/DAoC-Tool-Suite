namespace DAoCToolSuite.ChimpTool
{
    partial class SettingsManagerForm
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
            RestoreDataGridView = new DataGridView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            restoreToolStripMenuItem = new ToolStripMenuItem();
            updateToolStripMenuItem = new ToolStripMenuItem();
            NoteLabel = new Label();
            RestoreButton = new Button();
            ((System.ComponentModel.ISupportInitialize)RestoreDataGridView).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // RestoreDataGridView
            // 
            RestoreDataGridView.AllowUserToAddRows = false;
            RestoreDataGridView.AllowUserToDeleteRows = false;
            RestoreDataGridView.AllowUserToResizeColumns = false;
            RestoreDataGridView.AllowUserToResizeRows = false;
            RestoreDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            RestoreDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Verdana", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            RestoreDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            RestoreDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            RestoreDataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            RestoreDataGridView.Location = new Point(12, 27);
            RestoreDataGridView.MultiSelect = false;
            RestoreDataGridView.Name = "RestoreDataGridView";
            RestoreDataGridView.ReadOnly = true;
            RestoreDataGridView.RowHeadersVisible = false;
            RestoreDataGridView.RowTemplate.Height = 25;
            RestoreDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RestoreDataGridView.ShowCellErrors = false;
            RestoreDataGridView.ShowCellToolTips = false;
            RestoreDataGridView.ShowEditingIcon = false;
            RestoreDataGridView.ShowRowErrors = false;
            RestoreDataGridView.Size = new Size(652, 187);
            RestoreDataGridView.TabIndex = 0;
            RestoreDataGridView.CellDoubleClick += new DataGridViewCellEventHandler(RestoreDataGridView_CellDoubleClick);
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { restoreToolStripMenuItem, updateToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(114, 48);
            // 
            // restoreToolStripMenuItem
            // 
            restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            restoreToolStripMenuItem.Size = new Size(113, 22);
            restoreToolStripMenuItem.Text = "Restore";
            // 
            // updateToolStripMenuItem
            // 
            updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            updateToolStripMenuItem.Size = new Size(113, 22);
            updateToolStripMenuItem.Text = "Update";
            // 
            // NoteLabel
            // 
            NoteLabel.AutoSize = true;
            NoteLabel.Location = new Point(12, 9);
            NoteLabel.Name = "NoteLabel";
            NoteLabel.Size = new Size(344, 15);
            NoteLabel.TabIndex = 1;
            NoteLabel.Text = "Please use CharacterTool to create and manage setting backups.";
            // 
            // RestoreButton
            // 
            RestoreButton.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point);
            RestoreButton.Location = new Point(569, 221);
            RestoreButton.Name = "RestoreButton";
            RestoreButton.Size = new Size(95, 23);
            RestoreButton.TabIndex = 2;
            RestoreButton.Text = "Restore";
            RestoreButton.UseVisualStyleBackColor = true;
            RestoreButton.Click += RestoreButton_Click;
            // 
            // SettingsManagerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(676, 256);
            Controls.Add(RestoreButton);
            Controls.Add(NoteLabel);
            Controls.Add(RestoreDataGridView);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "SettingsManagerForm";
            Text = "SettingsManager";
            ((System.ComponentModel.ISupportInitialize)RestoreDataGridView).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView RestoreDataGridView;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem restoreToolStripMenuItem;
        private ToolStripMenuItem updateToolStripMenuItem;
        private Label NoteLabel;
        private Button RestoreButton;
    }
}