using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAoCToolSuite
{
    public partial class DAoCToolSuiteForm : Form
    {
        private static DAoCToolSuite.ChimpTool.MainForm ChimpToolForm { get; set; } = new();
        private static DAoCToolSuite.CharacterTool.MainForm CharacterToolForm { get; set; } = new();

        public DAoCToolSuiteForm()
        {
            InitializeComponent();
        }
        private void chimpToolButton_Click(object sender, EventArgs e)
        {
            if (ChimpToolForm is null || ChimpToolForm.IsDisposed)
                ChimpToolForm = new();
            ChimpToolForm.Show();
            ChimpToolForm.Focus();

        }

        private void characterToolButton_Click(object sender, EventArgs e)
        {
            if (CharacterToolForm is null || CharacterToolForm.IsDisposed)
                CharacterToolForm = new();

            CharacterToolForm.Show();
            CharacterToolForm.Focus();
        }
    }
}
