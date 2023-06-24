using SQLLibrary;
using System.IO;

namespace DAoCToolSuite.ChimpTool
{
    public partial class CredentialForm : Form
    {
        public string AccountName { get; set; } = string.Empty;

        public CredentialForm()
        {
            InitializeComponent();
            GameLocationTextBox.Text = string.IsNullOrEmpty(Properties.Settings.Default.GameDllLocation) ? "C:\\" : Environment.ExpandEnvironmentVariables(Properties.Settings.Default.GameDllLocation);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
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
                if (File.Exists(folderBrowserDialog1.SelectedPath + "\\game.dll"))
                {
                    GameLocationTextBox.Text = folderBrowserDialog1.SelectedPath;
                    Properties.Settings.Default.GameDllLocation = folderBrowserDialog1.SelectedPath;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    _ = MessageBox.Show($"Game.dll was not found at:\n{folderBrowserDialog1.SelectedPath}", "File Not Found", MessageBoxButtons.OK);
                }
            }
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(GameLocationTextBox.Text + "\\game.dll"))
            {
                _ = SqliteDataAccess.AddAccountCredentials(AccountName, LoginTextBox.Text, PasswordTextBox.Text);
                Close();
            }
            else
            {
                _ = MessageBox.Show($"Game.dll was not found at:\n{GameLocationTextBox.Text}", "File Not Found", MessageBoxButtons.OK);
            }
        }
    }
}
