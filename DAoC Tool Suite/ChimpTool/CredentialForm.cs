using SQLLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAoCToolSuite.ChimpTool
{
    public partial class CredentialForm : Form
    {
        public string AccountName { get; set; } = string.Empty;

        public CredentialForm()
        {
            InitializeComponent();
            GameLocationTextBox.Text = string.IsNullOrEmpty(Properties.Settings.Default.GameDllLocation) ? "C:\\" : Properties.Settings.Default.GameDllLocation;
            if (!string.IsNullOrEmpty(AccountName))
            {
                CredentialModel? credentialModel = SqliteDataAccess.LoadAccountCredentials(AccountName).FirstOrDefault(); 
                if (credentialModel != null)
                {
                    LoginTextBox.Text = credentialModel.Login;
                    PasswordTextBox.Text = credentialModel.Password;
                }
            }
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

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1 = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.MyComputer,
                SelectedPath = GameLocationTextBox.Text,       
            };

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                GameLocationTextBox.Text = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.GameDllLocation = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            SqliteDataAccess.AddAccountCredentials(AccountName, LoginTextBox.Text, PasswordTextBox.Text);
            Close();
        }
    }
}
