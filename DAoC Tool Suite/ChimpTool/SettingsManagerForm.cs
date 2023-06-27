using Logger;
using SQLLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace DAoCToolSuite.ChimpTool
{

    public partial class SettingsManagerForm : Form
    {
        private LogManager Logger => LogManager.Instance;
        private BindingSource BindingSource { get; set; } = new();
        private string FirstName { get; set; } = string.Empty;
        private string Realm { get; set; } = string.Empty;
        private string Class { get; set; } = string.Empty;
        private List<SettingsBackUpModel> Backups { get; set; } = new();

        public SettingsManagerForm(string firstName, string realm, string _class)
        {
            InitializeComponent();
            RestoreDataGridView.DataSource = BindingSource;
            FirstName = firstName;
            Realm = realm;
            Class = _class;
            LoadBackups();
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

        private void LoadBackups()
        {
            try
            {
                Logger.Debug("Loading Setting Backups");
                Backups = SqliteDataAccess.LoadSettingBackUps(FirstName, Realm, Class);
                AttachBackups();
                FormatGridView();
                Logger.Debug("Setting Backups Loaded");

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void FormatGridView()
        {
            List<string> visibleColumns = new() { "DateOnly", "Description" };
            List<string> visibleColumnHeaderNames = new() { "Date", "Description" };
            _ = RestoreDataGridView.Rows.Count;
            int columnCount = RestoreDataGridView.Columns.Count;
            int nonVisibleIndex = visibleColumns.Count;


            //Sets the column names, order, and what data from the DB to display.
            //Values pulled from the config file.

            if (columnCount > 0)
            {
                for (int index = 0; index < columnCount; index++)
                {
                    DataGridViewColumn column = RestoreDataGridView.Columns[index];
                    if (!visibleColumns.Contains(column.Name))
                    {
                        column.Visible = false;
                        column.DisplayIndex = nonVisibleIndex;
                        nonVisibleIndex++;
                    }
                    else
                    {
                        column.DisplayIndex = visibleColumns.IndexOf(column.Name);
                        column.HeaderText = visibleColumnHeaderNames[visibleColumns.IndexOf(column.Name)];
                        column.ValueType = typeof(string);
                        //Logger.Debug($"{column.Name},{index},{column.DisplayIndex},{column.Visible}");
                        column.AutoSizeMode = column.Name switch
                        {
                            "Description" => DataGridViewAutoSizeColumnMode.Fill,
                            _ => DataGridViewAutoSizeColumnMode.AllCells,
                        };
                    }

                }
            }
        }

        private void AttachBackups()
        {
            BindingSource.DataSource = Backups ?? new();
        }

        private void RestoreDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow? row = RestoreDataGridView.Rows[e.RowIndex];
            if (row != null)
            {
                Logger.Debug($"Doubleclick detected");
                PerformRestore();
                Close();
            }
        }

        private void PerformRestore()
        {
            Logger.Debug("PerformRestore started.");
            List<string> charNames = new();
            DataGridViewSelectedRowCollection selectedRows = RestoreDataGridView.SelectedRows;
            if (selectedRows is null)
            {
                return;
            }


            foreach (DataGridViewRow row in selectedRows)
            {
                string? dbIndexStr = row?.Cells["index"]?.Value?.ToString();
                if (dbIndexStr is not null)
                {
                    int dbIndex = int.TryParse(dbIndexStr, out dbIndex) ? dbIndex : -1;
                    if (dbIndex > -1)
                    {
                        SettingsBackUpModel settingsBackup = SqliteDataAccess.LoadSettingByIndex(dbIndex);
                        File.WriteAllText($"{settingsBackup.Path}\\{settingsBackup.INIFileName}", settingsBackup.INIData);
                        if (settingsBackup.IGNFileName is not null)
                        {
                            File.WriteAllText($"{settingsBackup.Path}\\{settingsBackup.IGNFileName}", settingsBackup.IGNData);
                        }

                        if (selectedRows.Count > 1 && settingsBackup.FirstName is not null)
                        {
                            charNames.Add(settingsBackup.FirstName);
                        }
                        else
                        {
                            _ = MessageBox.Show($"Restored character files for {settingsBackup.FirstName}\n{settingsBackup.Description}", "Restore Character Settings", MessageBoxButtons.OK);
                        }
                    }
                }
            }
            if (selectedRows.Count > 1)
            {
                _ = MessageBox.Show($"Restored character files for {string.Join(", ", charNames)}", "Restore Character Settings", MessageBoxButtons.OK);
            }
        }

        private void RestoreButton_Click(object sender, EventArgs e)
        {

            Logger.Debug("Restore button clicked.");
            PerformRestore();
            Close();
        }
    }



}
