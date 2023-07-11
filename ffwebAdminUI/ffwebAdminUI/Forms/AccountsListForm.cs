using fCommon.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using fPeerLending.Entities;
using fPeerLending.Business;
using fanikiwaGL.Entities;

namespace ffwebAdminUI
{
    public partial class AccountsListForm : Form
    {
        IQueryable<Account> _Accounts;
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;
        TransactionsComponent tc;
        AccountsComponent ac;

        public AccountsListForm()
        {
            InitializeComponent();

            tc = new TransactionsComponent();
            ac = new AccountsComponent();
        }

        private void AccountsListForm_Load(object sender, EventArgs e)
        {
            try
            {
                _Accounts = (from acs in ac.GetAllAccounts()
                             where acs.Closed == false
                             select acs).AsQueryable();
                bindingSourceAccounts.DataSource = _Accounts.ToList();
                groupBox2.Text = bindingSourceAccounts.Count.ToString();

                dataGridViewAccounts.AutoGenerateColumns = false;
                dataGridViewAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewAccounts.DataSource = bindingSourceAccounts;

                AutoCompleteStringCollection acsaccid = new AutoCompleteStringCollection();
                acsaccid.AddRange(this.AutoComplete_AccountIds());
                txtAccountId.AutoCompleteCustomSource = acsaccid;
                txtAccountId.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtAccountId.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscaccName = new AutoCompleteStringCollection();
                acscaccName.AddRange(this.AutoComplete_AccNames());
                txtAccountName.AutoCompleteCustomSource = acscaccName;
                txtAccountName.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtAccountName.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private string[] AutoComplete_AccountIds()
        {
            try
            {
                var accidsquery = (from acs in ac.GetAllAccounts()
                                   select acs.AccountID).Distinct();
                int[] intarray = accidsquery.ToArray();
                List<string> items = new List<string>();
                for (int i = 0; i < accidsquery.Count(); i++)
                {
                    string strName = intarray[i].ToString();
                    items.Add(strName);
                }
                return items.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_AccountNos()
        {
            try
            {
                var accnosquery = from acs in ac.GetAllAccounts()
                                  select acs.AccountNo;
                return accnosquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_AccNames()
        {
            try
            {
                var accnamequery = from acs in ac.GetAllAccounts()
                                   select acs.AccountName;
                return accnamequery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_CustomerIds()
        {
            try
            {
                var custidsquery = (from acs in ac.GetAllAccounts()
                                    join cm in tc.GetAllCustomers() on acs.CustomerId equals cm.CustomerId
                                    select cm.CustomerId).Distinct();
                int[] intarray = custidsquery.ToArray();
                List<string> items = new List<string>();
                for (int i = 0; i < custidsquery.Count(); i++)
                {
                    string strName = intarray[i].ToString();
                    items.Add(strName);
                }
                return items.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddAccountForm aaf = new AddAccountForm() { Owner = this };
            aaf.ShowDialog();
        }

        private void btnEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (dataGridViewAccounts.SelectedRows.Count != 0)
            {
                try
                {
                    Account account = (Account)bindingSourceAccounts.Current;
                    EditAccountForm eaf = new EditAccountForm(account) { Owner = this };
                    eaf.Text = account.AccountNo + " - " + account.AccountName.ToString().ToUpper();
                    eaf.ShowDialog();

                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        public void RefreshGrid()
        {
            try
            {
                if (chkClosed.Checked)
                {
                    bindingSourceAccounts.DataSource = null;
                    var _Accountsquery = from acs in ac.GetAllAccounts()
                                         select acs;
                    bindingSourceAccounts.DataSource = _Accountsquery.ToList();
                    groupBox2.Text = bindingSourceAccounts.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewAccounts.Rows)
                    {
                        dataGridViewAccounts.Rows[dataGridViewAccounts.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewAccounts.Rows.Count - 1;
                        bindingSourceAccounts.Position = nRowIndex;
                    }
                }
                else
                {
                    bindingSourceAccounts.DataSource = null;
                    var _Accountsquery = from acs in ac.GetAllAccounts()
                                         where acs.Closed == false
                                         select acs;
                    bindingSourceAccounts.DataSource = _Accountsquery.ToList();
                    groupBox2.Text = bindingSourceAccounts.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewAccounts.Rows)
                    {
                        dataGridViewAccounts.Rows[dataGridViewAccounts.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewAccounts.Rows.Count - 1;
                        bindingSourceAccounts.Position = nRowIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

        }
        private void btnViewDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (dataGridViewAccounts.SelectedRows.Count != 0)
                {
                    Account account = (Account)bindingSourceAccounts.Current;
                    EditAccountForm eaf = new EditAccountForm(account) { Owner = this };
                    eaf.Text = account.AccountName.ToString().ToUpper();
                    eaf.DisableControls();
                    eaf.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        // apply the filter
        private void ApplyFilter()
        {
            try
            {
                var filter = CreateFilter(_Accounts);
                // set the filter
                this.bindingSourceAccounts.DataSource = filter;
                groupBox2.Text = bindingSourceAccounts.Count.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private IQueryable<Account> CreateFilter(IQueryable<Account> _account)
        {
            //none
            if (string.IsNullOrEmpty(txtAccountId.Text)
                 && string.IsNullOrEmpty(txtAccountName.Text))
            {
                return _account;
            }
            //all
            if (!string.IsNullOrEmpty(txtAccountId.Text)
               && !string.IsNullOrEmpty(txtAccountName.Text))
            {
                _account = null;
                int _AccId = int.Parse(txtAccountId.Text);
                string _AccName = txtAccountName.Text;
                _account = (from acs in ac.GetAllAccounts()
                            where acs.AccountID == _AccId
                            where acs.AccountName.StartsWith(_AccName)
                            where acs.Closed == false
                            select acs).AsQueryable();
                return _account;
            }
            //accountid
            if (!string.IsNullOrEmpty(txtAccountId.Text)
                 && string.IsNullOrEmpty(txtAccountName.Text))
            {
                _account = null;
                int _AccId = int.Parse(txtAccountId.Text);
                _account = (from acs in ac.GetAllAccounts()
                            where acs.AccountID == _AccId
                            where acs.Closed == false
                            select acs).AsQueryable();
                return _account;
            }
            //accountname
            if (string.IsNullOrEmpty(txtAccountId.Text)
                  && !string.IsNullOrEmpty(txtAccountName.Text))
            {
                _account = null;
                string _AccName = txtAccountName.Text;
                _account = (from acs in ac.GetAllAccounts()
                            where acs.AccountName.StartsWith(_AccName)
                            where acs.Closed == false
                            select acs).AsQueryable();
                return _account;
            }
            return _account;
        }
        private void btnRemoveFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkClosed.Checked)
                {
                    bindingSourceAccounts.DataSource = null;
                    var _Accountsquery = from acs in ac.GetAllAccounts()
                                         select acs;
                    bindingSourceAccounts.DataSource = _Accountsquery.ToList();
                    groupBox2.Text = bindingSourceAccounts.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewAccounts.Rows)
                    {
                        dataGridViewAccounts.Rows[dataGridViewAccounts.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewAccounts.Rows.Count - 1;
                        bindingSourceAccounts.Position = nRowIndex;
                    }
                }
                else
                {
                    bindingSourceAccounts.DataSource = null;
                    var _Accountsquery = from acs in ac.GetAllAccounts()
                                         where acs.Closed == false
                                         select acs;
                    bindingSourceAccounts.DataSource = _Accountsquery.ToList();
                    groupBox2.Text = bindingSourceAccounts.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewAccounts.Rows)
                    {
                        dataGridViewAccounts.Rows[dataGridViewAccounts.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewAccounts.Rows.Count - 1;
                        bindingSourceAccounts.Position = nRowIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void btnCloseAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (dataGridViewAccounts.SelectedRows.Count != 0)
            {
                try
                {
                    Account account = (Account)bindingSourceAccounts.Current;
                    if (account.BookBalance != 0)
                    {
                        MessageBox.Show("Account Balance must be 0", "Fanikiwa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want to Close Account\n" + account.AccountName.Trim().ToUpper(), "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            account.Closed = true;
                            ac.UpdateAccount(account);
                            RefreshGrid();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private void txtAccountNo_Validated(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void txtAccountNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ApplyFilter();
                e.Handled = true;
            }
        }
        private void txtAccountName_Validated(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void txtAccountName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ApplyFilter();
                e.Handled = true;
            }
        }
        private void txtAccountId_Validated(object sender, EventArgs e)
        {
            if (nonNumberEntered == true)
            {
                ApplyFilter();
            }
        }
        private void txtAccountId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered == true)
            {
                if (e.KeyChar == 13)
                {
                    ApplyFilter();
                }
                e.Handled = true;
            }
        }
        private void txtAccountId_KeyDown(object sender, KeyEventArgs e)
        {
            // Initialize the flag to false.
            nonNumberEntered = false;

            // Determine whether the keystroke is a number from the top of the keyboard.
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad.
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace.
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed.
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumberEntered = true;
                    }
                }
            }
            //If shift key was pressed, it'st not a number.
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }
        }
        private void txtCustomerId_Validated(object sender, EventArgs e)
        {
            if (nonNumberEntered == true)
            {
                ApplyFilter();
            }
        }
        private void txtCustomerId_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                if (e.KeyChar == 13)
                {
                    ApplyFilter();
                }
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }
        private void txtCustomerId_KeyDown(object sender, KeyEventArgs e)
        {
            // Initialize the flag to false.
            nonNumberEntered = false;

            // Determine whether the keystroke is a number from the top of the keyboard.
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad.
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace.
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed.
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumberEntered = true;
                    }
                }
            }
            //If shift key was pressed, it'st not a number.
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }
        }
        private void dataGridViewAccounts_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void btnAdvancedSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //SearchAccountForm saf = new SearchAccountForm(connection) { Owner = this };
                //saf.OnAccountListSelected += new SearchAccountForm.AccountSelectHandler(saf_OnAccountListSelected);
                //saf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void saf_OnAccountListSelected(object sender, AccountSelectEventArgs e)
        {
            SetAccountNos(e._Account);
        }
        private void SetAccountNos(Account _Account)
        {
            if (_Account != null)
            {
                bindingSourceAccounts.DataSource = _Account;
                groupBox2.Text = bindingSourceAccounts.Count.ToString();
            }

        }

        private void chkClosed_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkClosed.Checked)
                {
                    bindingSourceAccounts.DataSource = null;
                    var _Accountsquery = from acs in ac.GetAllAccounts()
                                         select acs;
                    bindingSourceAccounts.DataSource = _Accountsquery.ToList();
                    groupBox2.Text = bindingSourceAccounts.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewAccounts.Rows)
                    {
                        dataGridViewAccounts.Rows[dataGridViewAccounts.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewAccounts.Rows.Count - 1;
                        bindingSourceAccounts.Position = nRowIndex;
                    }
                }
                else
                {
                    bindingSourceAccounts.DataSource = null;
                    var _Accountsquery = from acs in ac.GetAllAccounts()
                                         where acs.Closed == false
                                         select acs;
                    bindingSourceAccounts.DataSource = _Accountsquery.ToList();
                    groupBox2.Text = bindingSourceAccounts.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewAccounts.Rows)
                    {
                        dataGridViewAccounts.Rows[dataGridViewAccounts.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewAccounts.Rows.Count - 1;
                        bindingSourceAccounts.Position = nRowIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void dataGridViewAccounts_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewAccounts.SelectedRows.Count != 0)
            {
                try
                {
                    Account account = (Account)bindingSourceAccounts.Current;
                    EditAccountForm eaf = new EditAccountForm(account) { Owner = this };
                    eaf.Text = account.AccountNo + " - " + account.AccountName.ToString().ToUpper();
                    eaf.ShowDialog();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }






    }
}