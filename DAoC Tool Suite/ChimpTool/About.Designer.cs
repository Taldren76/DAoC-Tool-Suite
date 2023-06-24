﻿namespace DAoCToolSuite.ChimpTool
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            label1 = new Label();
            OKButton = new Button();
            VersionLabel = new Label();
            label3 = new Label();
            pictureBox1 = new PictureBox();
            GitHubLabel = new Label();
            LicenseLabel = new Label();
            label2 = new Label();
            GitHubLinkLabel = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(188, 169);
            label1.Name = "label1";
            label1.Size = new Size(108, 15);
            label1.TabIndex = 0;
            label1.Text = "Created by: Taldren";
            // 
            // OKButton
            // 
            OKButton.Location = new Point(547, 165);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(75, 23);
            OKButton.TabIndex = 1;
            OKButton.Text = "OK";
            OKButton.UseVisualStyleBackColor = true;
            OKButton.Click += OKButton_Click;
            // 
            // VersionLabel
            // 
            VersionLabel.AutoSize = true;
            VersionLabel.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point);
            VersionLabel.Location = new Point(188, 12);
            VersionLabel.Name = "VersionLabel";
            VersionLabel.Size = new Size(75, 18);
            VersionLabel.TabIndex = 2;
            VersionLabel.Text = "Version:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ControlLight;
            label3.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(269, 12);
            label3.Name = "label3";
            label3.Size = new Size(197, 18);
            label3.TabIndex = 3;
            label3.Text = "0.9.5 BETA (20230623)";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(182, 184);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // GitHubLabel
            // 
            GitHubLabel.AutoSize = true;
            GitHubLabel.Font = new Font("Verdana", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            GitHubLabel.Location = new Point(188, 43);
            GitHubLabel.Name = "GitHubLabel";
            GitHubLabel.Size = new Size(56, 16);
            GitHubLabel.TabIndex = 5;
            GitHubLabel.Text = "GitHub:";
            // 
            // LicenseLabel
            // 
            LicenseLabel.AutoSize = true;
            LicenseLabel.Font = new Font("Verdana", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            LicenseLabel.Location = new Point(188, 69);
            LicenseLabel.Name = "LicenseLabel";
            LicenseLabel.Size = new Size(62, 16);
            LicenseLabel.TabIndex = 6;
            LicenseLabel.Text = "License:";
            // 
            // label2
            // 
            label2.BackColor = SystemColors.ControlLight;
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Font = new Font("Verdana", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(188, 88);
            label2.Name = "label2";
            label2.Size = new Size(434, 74);
            label2.TabIndex = 7;
            label2.Text = resources.GetString("label2.Text");
            // 
            // GitHubLinkLabel
            // 
            GitHubLinkLabel.AutoSize = true;
            GitHubLinkLabel.Location = new Point(250, 44);
            GitHubLinkLabel.Name = "GitHubLinkLabel";
            GitHubLinkLabel.Size = new Size(96, 15);
            GitHubLinkLabel.TabIndex = 9;
            GitHubLinkLabel.TabStop = true;
            GitHubLinkLabel.Text = "DAoC-Tool-Suite\r\n";
            GitHubLinkLabel.LinkClicked += GitHubLinkLabel_LinkClicked;
            // 
            // About
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(629, 191);
            Controls.Add(GitHubLinkLabel);
            Controls.Add(label2);
            Controls.Add(LicenseLabel);
            Controls.Add(GitHubLabel);
            Controls.Add(pictureBox1);
            Controls.Add(label3);
            Controls.Add(VersionLabel);
            Controls.Add(OKButton);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "About";
            Text = "About";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button OKButton;
        private Label VersionLabel;
        private Label label3;
        private PictureBox pictureBox1;
        private Label GitHubLabel;
        private Label LicenseLabel;
        private Label label2;
        private LinkLabel GitHubLinkLabel;
    }
}