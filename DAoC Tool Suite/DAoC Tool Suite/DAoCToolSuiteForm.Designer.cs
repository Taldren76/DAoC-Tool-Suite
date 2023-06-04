﻿using System.Drawing;

namespace DAoCToolSuite
{
    partial class DAoCToolSuiteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DAoCToolSuiteForm));
            chimpToolButton = new Button();
            characterToolButton = new Button();
            logParserButton = new Button();
            chimpLabel = new Label();
            characterToolLabel = new Label();
            LogTool = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // chimpToolButton
            // 
            chimpToolButton.Image = Properties.Resources.ChimpTool;
            chimpToolButton.Location = new Point(12, 12);
            chimpToolButton.Name = "chimpToolButton";
            chimpToolButton.Size = new Size(140, 140);
            chimpToolButton.TabIndex = 0;
            chimpToolButton.UseVisualStyleBackColor = true;
            chimpToolButton.Click += chimpToolButton_Click;
            // 
            // characterToolButton
            // 
            characterToolButton.Image = Properties.Resources.CharacterTool;
            characterToolButton.Location = new Point(158, 12);
            characterToolButton.Name = "characterToolButton";
            characterToolButton.Size = new Size(140, 140);
            characterToolButton.TabIndex = 1;
            characterToolButton.UseVisualStyleBackColor = true;
            characterToolButton.Click += characterToolButton_Click;
            // 
            // logParserButton
            // 
            logParserButton.Enabled = false;
            logParserButton.Image = Properties.Resources.LogParser;
            logParserButton.Location = new Point(304, 12);
            logParserButton.Name = "logParserButton";
            logParserButton.Size = new Size(140, 140);
            logParserButton.TabIndex = 2;
            logParserButton.UseVisualStyleBackColor = true;
            // 
            // chimpLabel
            // 
            chimpLabel.Font = new Font("Verdana", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            chimpLabel.Location = new Point(12, 157);
            chimpLabel.Name = "chimpLabel";
            chimpLabel.Size = new Size(140, 15);
            chimpLabel.TabIndex = 3;
            chimpLabel.Text = "ChimpTool";
            chimpLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // characterToolLabel
            // 
            characterToolLabel.Font = new Font("Verdana", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            characterToolLabel.Location = new Point(158, 153);
            characterToolLabel.Name = "characterToolLabel";
            characterToolLabel.Size = new Size(140, 23);
            characterToolLabel.TabIndex = 4;
            characterToolLabel.Text = "CharacterTool";
            characterToolLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LogTool
            // 
            LogTool.Enabled = false;
            LogTool.Font = new Font("Verdana", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            LogTool.Location = new Point(304, 153);
            LogTool.Name = "LogTool";
            LogTool.Size = new Size(140, 23);
            LogTool.TabIndex = 5;
            LogTool.Text = "LogTool";
            LogTool.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            button1.Location = new Point(441, 153);
            button1.Name = "button1";
            button1.Size = new Size(19, 23);
            button1.TabIndex = 6;
            button1.Text = "R";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // DAoCToolSuiteForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(460, 181);
            Controls.Add(button1);
            Controls.Add(LogTool);
            Controls.Add(characterToolLabel);
            Controls.Add(chimpLabel);
            Controls.Add(logParserButton);
            Controls.Add(characterToolButton);
            Controls.Add(chimpToolButton);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DAoCToolSuiteForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DAoC Tool Suite v0.2.0";
            FormClosing += DAoCTestSuiteForm_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private Button chimpToolButton;
        private Button characterToolButton;
        private Button logParserButton;
        private Label chimpLabel;
        private Label characterToolLabel;
        private Label LogTool;
        private Button button1;
    }
}
