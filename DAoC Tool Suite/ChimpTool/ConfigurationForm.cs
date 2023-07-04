using System.IO;

namespace DAoCToolSuite.ChimpTool
{
    public partial class ConfigurationForm : Form
    {
        public ConfigurationForm()
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

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            GameLocationTextBox.Text = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.GameDllLocation);
            INIFileTextBox.Text = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.DAoCCharacterFileDirectory);
            LeftClickComboBox.Text = Properties.Settings.Default.CharacterLeftClick;
            ShiftLeftClickComboBox.Text = Properties.Settings.Default.CharacterShiftLeftClick;
        }

        private void LeftClickComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Properties.Settings.Default.CharacterLeftClick = comboBox.Text;
            Properties.Settings.Default.Save();
        }

        private void LeftClickComboBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void ShiftLeftClickComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Properties.Settings.Default.CharacterShiftLeftClick = comboBox.Text;
            Properties.Settings.Default.Save();
        }

        private void ShiftLeftClickComboBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            bool close = true;
            if (Directory.Exists(INIFileTextBox.Text))
            {
                Properties.Settings.Default.DAoCCharacterFileDirectory = INIFileTextBox.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                _ = MessageBox.Show($"Could not locate: {INIFileTextBox.Text}", "Folder Not Found", MessageBoxButtons.OK);
                close = false;
            }
            if (File.Exists(GameLocationTextBox.Text + "\\game.dll"))
            {
                Properties.Settings.Default.GameDllLocation = GameLocationTextBox.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                _ = MessageBox.Show($"Game.dll was not found at:\n{GameLocationTextBox.Text}", "Folder Not Found", MessageBoxButtons.OK);
                close = false;
            }

            Properties.Settings.Default.CharacterLeftClick = LeftClickComboBox.Text;
            Properties.Settings.Default.Save();

            Properties.Settings.Default.CharacterShiftLeftClick = ShiftLeftClickComboBox.Text;
            Properties.Settings.Default.Save();

            if (close)
            {
                Close();
            }
        }

        private void INIFileBrowseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new()
            {
                RootFolder = Environment.SpecialFolder.MyComputer,
                SelectedPath = INIFileTextBox.Text,
            };

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (Directory.Exists(folderBrowserDialog1.SelectedPath))
                {
                    INIFileTextBox.Text = folderBrowserDialog1.SelectedPath;
                    Properties.Settings.Default.DAoCCharacterFileDirectory = INIFileTextBox.Text;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    _ = MessageBox.Show($"Could not locate: {INIFileTextBox.Text}", "Folder Not Found", MessageBoxButtons.OK);
                }
            }
        }

        private void GameLocationBrowseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new()
            {
                RootFolder = Environment.SpecialFolder.MyComputer,
                SelectedPath = GameLocationTextBox.Text,
            };

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(folderBrowserDialog1.SelectedPath + "\\game.dll"))
                {
                    GameLocationTextBox.Text = folderBrowserDialog1.SelectedPath;
                    Properties.Settings.Default.GameDllLocation = GameLocationTextBox.Text;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    _ = MessageBox.Show($"Game.dll was not found at:\n{GameLocationTextBox.Text}", "Folder Not Found", MessageBoxButtons.OK);
                }
            }
        }
    }
}
