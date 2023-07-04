namespace DAoCToolSuite.ChimpTool
{
    partial class MoveAccountForm
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
            AccountComboBox = new ComboBox();
            MoveButton = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // AccountComboBox
            // 
            AccountComboBox.FormattingEnabled = true;
            AccountComboBox.Location = new Point(12, 27);
            AccountComboBox.Name = "AccountComboBox";
            AccountComboBox.Size = new Size(236, 23);
            AccountComboBox.TabIndex = 0;
            AccountComboBox.KeyPress += AccountComboBox_KeyPress;
            // 
            // MoveButton
            // 
            MoveButton.Location = new Point(173, 56);
            MoveButton.Name = "MoveButton";
            MoveButton.Size = new Size(75, 23);
            MoveButton.TabIndex = 1;
            MoveButton.Text = "Move";
            MoveButton.UseVisualStyleBackColor = true;
            MoveButton.Click += MoveButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 2;
            label1.Text = "Move to page:";
            // 
            // MoveAccountForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(260, 88);
            Controls.Add(label1);
            Controls.Add(MoveButton);
            Controls.Add(AccountComboBox);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "MoveAccountForm";
            Text = "Move";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox AccountComboBox;
        private Button MoveButton;
        private Label label1;
    }
}