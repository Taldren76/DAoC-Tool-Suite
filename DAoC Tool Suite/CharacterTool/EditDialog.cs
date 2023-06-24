using DAoCToolSuite.CharacterTool.Files;
using DAoCToolSuite.CharacterTool.Json;
using Logger;
using SQLLibrary;
using System.Data;

namespace DAoCToolSuite.CharacterTool
{
    public partial class EditDialog : Form
    {
        private static LogManager Logger => LogManager.Instance;
        private static ServerListINI? ServerList { get; set; }
        private static RealmClassINI? RealmList { get; set; }
        public string DAoCCharacterDataFolder { get; private set; }
        private Dictionary<string, int>? _characterList = null;
        private Dictionary<string, int> CharacterList
        {
            get
            {
                _characterList ??= new ParseDirectory(DAoCCharacterDataFolder).Characters;
                return _characterList;
            }
            set => _characterList = value;
        }

        private DataGridViewRow SelectedRow { get; set; }


        public EditDialog(DataGridViewRow selectedRow, string charaacterDataFolder, ServerListINI serverListINI, RealmClassINI realmClassINI)
        {
            InitializeComponent();
            _ = BackUpRealmComboBox.Items.Add("Albion");
            _ = BackUpRealmComboBox.Items.Add("Hibernia");
            _ = BackUpRealmComboBox.Items.Add("Midgard");
            BackUpNameComboBox.Enabled = false;
            DAoCCharacterDataFolder = charaacterDataFolder;
            RealmList = realmClassINI;
            ServerList = serverListINI;
            SelectedRow = selectedRow;
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

        private void BackUpNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is not System.Windows.Forms.ComboBox comboBox)
            {
                return;
            }

            string characterFirstName = comboBox.Text;

            //Populate automatically if already in database
            CharacterModel? characterModel = SqliteDataAccess.LoadCharacterByFirstName(characterFirstName);
            if (characterModel is not null)
            {
                BackUpRealmComboBox.Text = characterModel.Realm;
                //BackUpServerTextBox.Text = characterModel.Server;
                BackUpClassComboBox.Text = characterModel.Class;
            }
            else
            {
                BackUpRealmComboBox.Text = "";
                BackUpClassComboBox.Text = "";
            }

            if (comboBox.SelectedItem is not null)
            {
                string? selectedItem = comboBox.SelectedItem.ToString();
                if (selectedItem is not null && CharacterList.ContainsKey(selectedItem))
                {
                    BackUpServerTextBox.Text = GetServerName(CharacterList[selectedItem]);
                }
            }
        }
        private static string GetServerName(int serverIndex)
        {
            Server? result = ServerList?.Servers?.Server?.Where(x => x.Index == serverIndex).FirstOrDefault();
            return result?.Name ?? $"Unknown({serverIndex})";
        }

        private void PopulateClasses(string realm)
        {
            if (RealmList is null)
            {
                throw new NullReferenceException("RealmList is null.");
            }

            List<string> classes = new();
            switch (realm)
            {
                case "Albion":
                    classes = RealmList?.Realm?.Albion?.Split(',').ToList() ?? new();
                    break;
                case "Hibernia":
                    classes = RealmList?.Realm?.Hibernia?.Split(",").ToList() ?? new();
                    break;
                case "Midgard":
                    classes = RealmList?.Realm?.Midgard?.Split(",").ToList() ?? new();
                    break;
            }

            BackUpClassComboBox.Items.Clear();
            BackUpClassComboBox.ResetText();
            foreach (string cls in classes)
            {
                _ = BackUpClassComboBox.Items.Add(cls);
            }
        }
        private void BackUpRealmComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BackUpRealmComboBox.Text))
            {
                PopulateClasses(BackUpRealmComboBox.Text);
                BackUpClassComboBox.Enabled = true;
                BackUpClassLabel.Enabled = true;
            }
            else
            {
                BackUpClassComboBox.Items.Clear();
                BackUpClassComboBox.ResetText();
                BackUpClassComboBox.Enabled = false;
                BackUpClassLabel.Enabled = false;
            }
        }

        private void SaveBackUp_Click(object sender, EventArgs e)
        {
            try
            {
                SettingsBackUpModel settingsBackup = new()
                {
                    FirstName = SelectedRow?.Cells["FirstName"]?.Value?.ToString(),
                    Realm = BackUpRealmComboBox.Text,
                    Class = BackUpClassComboBox.Text,
                    Path = SelectedRow?.Cells["Path"]?.Value?.ToString(),
                    Description = BackUpDescriptionTextBox.Text,
                    INIFileName = SelectedRow?.Cells["INIFileName"]?.Value?.ToString(),
                    INIData = SelectedRow?.Cells["INIData"]?.Value?.ToString(),
                    IGNFileName = SelectedRow?.Cells["IGNFileName"]?.Value?.ToString(),
                    IGNData = SelectedRow?.Cells["IGNData"]?.Value?.ToString()
                };

                string? dbIndexStr = SelectedRow?.Cells["index"]?.Value?.ToString();
                int dbIndex = int.TryParse(dbIndexStr, out dbIndex) ? dbIndex : -1;
                if (dbIndex < 0)
                {
                    return;
                }

                SqliteDataAccess.UpdateEntryByIndex(dbIndex, settingsBackup);
                Close();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}
