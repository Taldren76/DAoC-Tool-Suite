using System.Drawing;

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
            LogParserButton = new Button();
            chimpLabel = new Label();
            characterToolLabel = new Label();
            LogTool = new Label();
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
            chimpToolButton.Click += ChimpToolButton_Click;
            // 
            // characterToolButton
            // 
            characterToolButton.Image = Properties.Resources.CharacterTool;
            characterToolButton.Location = new Point(158, 12);
            characterToolButton.Name = "characterToolButton";
            characterToolButton.Size = new Size(140, 140);
            characterToolButton.TabIndex = 1;
            characterToolButton.UseVisualStyleBackColor = true;
            characterToolButton.Click += CharacterToolButton_Click;
            // 
            // LogParserButton
            // 
            LogParserButton.Image = Properties.Resources.LogParser;
            LogParserButton.Location = new Point(304, 12);
            LogParserButton.Name = "LogParserButton";
            LogParserButton.Size = new Size(140, 140);
            LogParserButton.TabIndex = 2;
            LogParserButton.UseVisualStyleBackColor = true;
            LogParserButton.Click += LogParserButton_Click;
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
            LogTool.Font = new Font("Verdana", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            LogTool.Location = new Point(304, 153);
            LogTool.Name = "LogTool";
            LogTool.Size = new Size(140, 23);
            LogTool.TabIndex = 5;
            LogTool.Text = "LogTool";
            LogTool.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DAoCToolSuiteForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(460, 181);
            Controls.Add(LogTool);
            Controls.Add(characterToolLabel);
            Controls.Add(chimpLabel);
            Controls.Add(LogParserButton);
            Controls.Add(characterToolButton);
            Controls.Add(chimpToolButton);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(476, 220);
            MinimumSize = new Size(476, 220);
            Name = "DAoCToolSuiteForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DAoC Tool Suite v0.9.5";
            FormClosing += DAoCTestSuiteForm_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private Button chimpToolButton;
        private Button characterToolButton;
        private Button LogParserButton;
        private Label chimpLabel;
        private Label characterToolLabel;
        private Label LogTool;
    }
}
