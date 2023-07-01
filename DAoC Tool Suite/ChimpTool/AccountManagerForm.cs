using Logger;
using SQLLibrary;

namespace DAoCToolSuite.ChimpTool
{
    public partial class AccountManagerForm : Form
    {
        internal static LogManager Logger => LogManager.Instance;
        private BindingSource BindingSource { get; set; } = new();
        private List<AccountModel> Accounts { get; set; } = new();
        private int SelectedAccountIndex { get; set; } = 0;

        public AccountManagerForm()
        {
            InitializeComponent();
            AccountGridView.DataSource = BindingSource;
            LoadAccounts();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            AccountGridView.Rows[0].Selected = true;
            SelectedAccountTextBox.Text = AccountGridView.SelectedCells[0].Value.ToString();
            object raw = AccountGridView.SelectedRows[0].Cells["index"].Value;
            SelectedAccountIndex = (int)raw;
        }


        private void AttachAccounts()
        {
            BindingSource.DataSource = Accounts ?? new();
        }

        private void LoadAccounts()
        {
            try
            {
                Logger.Debug("Loading Chimp Pages");
                Accounts = SqliteDataAccess.LoadAccounts().ToList();
                AttachAccounts();
                FormatGridView();
                Logger.Debug("Chimp Pages Loaded");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void FormatGridView()
        {
            List<string> visibleColumns = new() { "Account" };
            List<string> visibleColumnHeaderNames = new() { "Chimp Page" };
            int columnCount = AccountGridView.Columns.Count;
            int nonVisibleIndex = visibleColumns.Count;

            //Sets the column names, order, and what data from the DB to display.
            //Values pulled from the config file.

            if (columnCount > 0)
            {
                for (int index = 0; index < columnCount; index++)
                {
                    DataGridViewColumn column = AccountGridView.Columns[index];
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
                            "Account" => DataGridViewAutoSizeColumnMode.Fill,
                            _ => DataGridViewAutoSizeColumnMode.AllCells,
                        };
                    }

                }
            }
            AccountGridView.ClearSelection();
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

        private void AccountGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            SelectedAccountTextBox.Text = dataGridView.SelectedCells[0].Value.ToString();
            object raw = dataGridView.SelectedRows[0].Cells["index"].Value;
            SelectedAccountIndex = (int)raw;
        }


        private void UpdateButton_Click(object sender, EventArgs e)
        {
            string? oldChar = SelectedAccountTextBox.Text;
            if (oldChar is null)
            {
                Logger.Debug("No chimp page name captured from DataGridView.");
                return;
            }
            string newChar = RenameAddNewTextBox.Text;
            SqliteDataAccess.RenameAccount(oldChar, newChar);
            RenameAddNewTextBox.Clear();
            LoadAccounts();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (SelectedAccountIndex == 1)
            {
                _ = MessageBox.Show("You may not delete the default chimp page.", "Error", MessageBoxButtons.OK);
                return;
            }
            SqliteDataAccess.RemoveAccount(SelectedAccountTextBox.Text);
            RenameAddNewTextBox.Clear();
            LoadAccounts();
        }

        private void AddNewButton_Click(object sender, EventArgs e)
        {
            string newChar = RenameAddNewTextBox.Text;
            SqliteDataAccess.AddAccount(newChar);
            RenameAddNewTextBox.Clear();
            LoadAccounts();
        }
    }
}
