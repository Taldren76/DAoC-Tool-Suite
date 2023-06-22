using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAoCToolSuite.ChimpTool
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            GitHubLinkLabel.Links.Add(0, 15, "https://github.com/Taldren76/DAoC-Tool-Suite");
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

        private void OKButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GitHubLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = new LinkLabel();
            linkLabel.LinkVisited = true;
            string target = e.Link.LinkData as string;
            System.Diagnostics.Process.Start(new ProcessStartInfo { FileName = @target, UseShellExecute = true });
        }
    }
}
