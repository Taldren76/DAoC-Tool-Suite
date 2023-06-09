﻿namespace DAoCToolSuite.ChimpTool
{
    public partial class LogViewer : Form
    {
        public LogViewer()
        {
            InitializeComponent();
        }
        public void SetLocation()
        {
            if (Owner != null && StartPosition == FormStartPosition.Manual)
            {
                int offset = 0;// Owner.OwnedForms.Length * 38;  // approx. 10mm
                Point p = new(Owner.Left + (Owner.Width / 2) - (Width / 2) + offset, Owner.Top + (Owner.Height / 2) - (Height / 2) + offset);
                Location = p;
            }
        }
    }
}
