namespace DAoCToolSuite.ChimpTool
{
    partial class CredentialForm
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
            LoginTextBox = new TextBox();
            PasswordTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            SubmitButton = new Button();
            label3 = new Label();
            GameLocationTextBox = new TextBox();
            label4 = new Label();
            BrowseButton = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            SuspendLayout();
            // 
            // LoginTextBox
            // 
            LoginTextBox.Location = new Point(14, 123);
            LoginTextBox.Name = "LoginTextBox";
            LoginTextBox.Size = new Size(231, 23);
            LoginTextBox.TabIndex = 0;
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(259, 124);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PasswordChar = '*';
            PasswordTextBox.Size = new Size(231, 23);
            PasswordTextBox.TabIndex = 1;
            PasswordTextBox.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 105);
            label1.Name = "label1";
            label1.Size = new Size(37, 15);
            label1.TabIndex = 2;
            label1.Text = "Login";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(259, 105);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 3;
            label2.Text = "Password";
            // 
            // SubmitButton
            // 
            SubmitButton.Location = new Point(501, 123);
            SubmitButton.Name = "SubmitButton";
            SubmitButton.Size = new Size(75, 23);
            SubmitButton.TabIndex = 4;
            SubmitButton.Text = "Submit";
            SubmitButton.UseVisualStyleBackColor = true;
            SubmitButton.Click += SubmitButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(189, 9);
            label3.Name = "label3";
            label3.Size = new Size(191, 15);
            label3.TabIndex = 5;
            label3.Text = "Please enter your DAoC credentials\r\n";
            // 
            // GameLocationTextBox
            // 
            GameLocationTextBox.Location = new Point(12, 61);
            GameLocationTextBox.Name = "GameLocationTextBox";
            GameLocationTextBox.Size = new Size(478, 23);
            GameLocationTextBox.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 43);
            label4.Name = "label4";
            label4.Size = new Size(103, 15);
            label4.TabIndex = 7;
            label4.Text = "Game.dll location:";
            // 
            // BrowseButton
            // 
            BrowseButton.Location = new Point(501, 60);
            BrowseButton.Name = "BrowseButton";
            BrowseButton.Size = new Size(75, 23);
            BrowseButton.TabIndex = 8;
            BrowseButton.Text = "Browse";
            BrowseButton.UseVisualStyleBackColor = true;
            BrowseButton.Click += BrowseButton_Click;
            // 
            // CredentialForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(595, 164);
            Controls.Add(BrowseButton);
            Controls.Add(label4);
            Controls.Add(GameLocationTextBox);
            Controls.Add(label3);
            Controls.Add(SubmitButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(PasswordTextBox);
            Controls.Add(LoginTextBox);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "CredentialForm";
            Text = "DAoC Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox LoginTextBox;
        private TextBox PasswordTextBox;
        private Label label1;
        private Label label2;
        private Button SubmitButton;
        private Label label3;
        private TextBox GameLocationTextBox;
        private Label label4;
        private Button BrowseButton;
        private FolderBrowserDialog folderBrowserDialog1;
    }
}