using SQLLibrary;
using System.Data;

namespace DAoCToolSuite.ChimpTool
{
    public partial class MoveAccountForm : Form
    {
        private static List<AccountModel> Accounts { get; set; } = new List<AccountModel>();
        private string CurrentAccount { get; set; }
        private string WebID { get; set; }

        public MoveAccountForm(string webID, string account)
        {
            InitializeComponent();
            WebID = webID;
            CurrentAccount = account;
            LoadAccounts();
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
        private void AccountComboBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void LoadAccounts()
        {
            Accounts = SqliteDataAccess.LoadAccounts();
            Accounts = Accounts.Where(x => x.Account != CurrentAccount).ToList();
            AttachAccountList();
        }

        private void AttachAccountList()
        {
            AccountComboBox.DataSource = null;
            AccountComboBox.DataSource = Accounts;
            AccountComboBox.DisplayMember = "Account";
            AutoCompleteStringCollection source = new();
            source.AddRange(Accounts.Select(x => x.Account).ToArray());
            AccountComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AccountComboBox.AutoCompleteCustomSource = source;
            AccountComboBox.Refresh();
        }

        private void MoveButton_Click(object sender, EventArgs e)
        {
            SqliteDataAccess.UpdateCharacterAccount(WebID, CurrentAccount, AccountComboBox.Text);
            Close();
        }
    }
}
