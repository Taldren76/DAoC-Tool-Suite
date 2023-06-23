using Logger;
using SQLLibrary;
using System.IO;

namespace DAoCToolSuite.ChimpTool
{
    public partial class AHKForm : Form
    {
        LogManager Logger => LogManager.Instance;
        public string CharacterName { get; set; } = string.Empty;
        public string Realm { get; set; } = string.Empty;
        public string Server { get; set; } = string.Empty;
        public string WebID { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public string? AHKScriptPath { get; set; } = null;
        public int Version { get; set; } = 1;

        public AHKForm(string characterName, string realm, string server, string webID, string account)
        {
            InitializeComponent();
            CharacterName = characterName;
            Realm = realm;
            Server = server;
            WebID = webID;
            Account = account;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            CharacterNameTextBox.Text = CharacterName;
            RealmTextBox.Text = Realm;
            ServerTextBox.Text = Server;
            AHKModel? model = SqliteDataAccess.LoadAHKModelByWebID(WebID, Account);
            if (model is not null && !string.IsNullOrEmpty(model.AHKScriptPath))
            {
                Logger.Debug($"Previous AHK association detected for WebID:{WebID} on Account:{Account}.");
                AHKScriptPath = model.AHKScriptPath;
                ScriptPathTextBox.Text = AHKScriptPath;
                UseVersion2 = model.Version == 2;
                VersionCheckBox.Checked = UseVersion2;
            }
            else
            {
                Logger.Debug($"No previous AHK association found for WebID:{WebID} on Account:{Account}.");
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

            openFileDialog1.InitialDirectory = string.IsNullOrEmpty(AHKScriptPath) ? "c:\\" : Path.GetDirectoryName(AHKScriptPath);
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "ahk files (*.ahk)|*.ahk|All files (*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                AHKScriptPath = openFileDialog1.FileName;
                ScriptPathTextBox.Text = AHKScriptPath;
            }
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AHKScriptPath))
            {
                SqliteDataAccess.DelAHK(WebID, Account);
                Logger.Debug($"Removing AHK association for WebID:{WebID} on Account:{Account}");
            }
            else
            {
                SqliteDataAccess.AddAHKModel(WebID, Account, AHKScriptPath, UseVersion2 ? 2 : 1);
                Logger.Debug($"Adding AHK association for WebID:{WebID} on Account:{Account} for script at {AHKScriptPath}");
            }
            Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            SqliteDataAccess.DelAHK(WebID, Account);
            Close();
        }
        private bool UseVersion2 { get; set; } = false;
        private void VersionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UseVersion2 = (sender as CheckBox)?.Checked ?? false;
        }
    }
}
