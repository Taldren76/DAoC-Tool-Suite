namespace DAoCToolSuite.ChimpTool
{
    partial class AccountManagerForm
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
            AccountGridView = new DataGridView();
            AccountNameLabel = new Label();
            SelectedAccountTextBox = new TextBox();
            DeleteButton = new Button();
            RenameButton = new Button();
            AddNewButton = new Button();
            RenameAddNewTextBox = new TextBox();
            AccountActionLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)AccountGridView).BeginInit();
            SuspendLayout();
            // 
            // AccountGridView
            // 
            AccountGridView.AllowUserToAddRows = false;
            AccountGridView.AllowUserToDeleteRows = false;
            AccountGridView.AllowUserToResizeColumns = false;
            AccountGridView.AllowUserToResizeRows = false;
            AccountGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            AccountGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            AccountGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            AccountGridView.Location = new Point(12, 12);
            AccountGridView.MultiSelect = false;
            AccountGridView.Name = "AccountGridView";
            AccountGridView.ReadOnly = true;
            AccountGridView.RowHeadersVisible = false;
            AccountGridView.RowTemplate.Height = 25;
            AccountGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            AccountGridView.ShowCellErrors = false;
            AccountGridView.ShowCellToolTips = false;
            AccountGridView.ShowEditingIcon = false;
            AccountGridView.ShowRowErrors = false;
            AccountGridView.Size = new Size(240, 150);
            AccountGridView.TabIndex = 0;
            AccountGridView.CellClick += AccountGridView_CellClick;
            // 
            // AccountNameLabel
            // 
            AccountNameLabel.AutoSize = true;
            AccountNameLabel.Location = new Point(258, 12);
            AccountNameLabel.Name = "AccountNameLabel";
            AccountNameLabel.Size = new Size(137, 15);
            AccountNameLabel.TabIndex = 1;
            AccountNameLabel.Text = "Selected Account Name:";
            // 
            // SelectedAccountTextBox
            // 
            SelectedAccountTextBox.Location = new Point(258, 30);
            SelectedAccountTextBox.Name = "SelectedAccountTextBox";
            SelectedAccountTextBox.ReadOnly = true;
            SelectedAccountTextBox.Size = new Size(212, 23);
            SelectedAccountTextBox.TabIndex = 2;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(393, 59);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(75, 23);
            DeleteButton.TabIndex = 3;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // RenameButton
            // 
            RenameButton.Location = new Point(312, 138);
            RenameButton.Name = "RenameButton";
            RenameButton.Size = new Size(75, 23);
            RenameButton.TabIndex = 4;
            RenameButton.Text = "Rename";
            RenameButton.UseVisualStyleBackColor = true;
            RenameButton.Click += UpdateButton_Click;
            // 
            // AddNewButton
            // 
            AddNewButton.Location = new Point(393, 138);
            AddNewButton.Name = "AddNewButton";
            AddNewButton.Size = new Size(75, 23);
            AddNewButton.TabIndex = 7;
            AddNewButton.Text = "Add New";
            AddNewButton.UseVisualStyleBackColor = true;
            AddNewButton.Click += AddNewButton_Click;
            // 
            // RenameAddNewTextBox
            // 
            RenameAddNewTextBox.Location = new Point(258, 109);
            RenameAddNewTextBox.Name = "RenameAddNewTextBox";
            RenameAddNewTextBox.Size = new Size(212, 23);
            RenameAddNewTextBox.TabIndex = 6;
            // 
            // AccountActionLabel
            // 
            AccountActionLabel.AutoSize = true;
            AccountActionLabel.Location = new Point(258, 91);
            AccountActionLabel.Name = "AccountActionLabel";
            AccountActionLabel.Size = new Size(160, 15);
            AccountActionLabel.TabIndex = 5;
            AccountActionLabel.Text = "Rename Selected / Add New:";
            // 
            // AccountManagerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(480, 174);
            Controls.Add(AddNewButton);
            Controls.Add(RenameAddNewTextBox);
            Controls.Add(AccountActionLabel);
            Controls.Add(RenameButton);
            Controls.Add(DeleteButton);
            Controls.Add(SelectedAccountTextBox);
            Controls.Add(AccountNameLabel);
            Controls.Add(AccountGridView);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "AccountManagerForm";
            Text = "Account Manager";
            ((System.ComponentModel.ISupportInitialize)AccountGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView AccountGridView;
        private Label AccountNameLabel;
        private TextBox SelectedAccountTextBox;
        private Button DeleteButton;
        private Button RenameButton;
        private Button AddNewButton;
        private TextBox RenameAddNewTextBox;
        private Label AccountActionLabel;
    }
}