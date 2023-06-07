namespace DAoCToolSuite.ChimpTool
{
    partial class LogViewer
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
            LogViewerPanel = new Panel();
            LogViewerTextBox = new TextBox();
            LogViewerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // LogViewerPanel
            // 
            LogViewerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LogViewerPanel.Controls.Add(LogViewerTextBox);
            LogViewerPanel.Location = new Point(12, 12);
            LogViewerPanel.Name = "LogViewerPanel";
            LogViewerPanel.Size = new Size(1400, 417);
            LogViewerPanel.TabIndex = 0;
            // 
            // LogViewerTextBox
            // 
            LogViewerTextBox.Dock = DockStyle.Fill;
            LogViewerTextBox.Font = new Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            LogViewerTextBox.Location = new Point(0, 0);
            LogViewerTextBox.Multiline = true;
            LogViewerTextBox.Name = "LogViewerTextBox";
            LogViewerTextBox.ScrollBars = ScrollBars.Vertical;
            LogViewerTextBox.Size = new Size(1400, 417);
            LogViewerTextBox.TabIndex = 0;
            LogViewerTextBox.WordWrap = false;
            // 
            // LogViewer
            // 
            AccessibleRole = AccessibleRole.MenuPopup;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1424, 441);
            Controls.Add(LogViewerPanel);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "LogViewer";
            StartPosition = FormStartPosition.Manual;
            Text = "Log Viewer";
            LogViewerPanel.ResumeLayout(false);
            LogViewerPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel LogViewerPanel;
        public TextBox LogViewerTextBox;
    }
}