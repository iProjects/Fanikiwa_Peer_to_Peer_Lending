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
    public partial class SearchAccountsSimpleForm : Form
    {
        IQueryable<Account> _Accounts;
        //delegate
        public delegate void AccountSelectHandler(object sender, AccountSelectEventArgs e);
        //event
        public event AccountSelectHandler OnAccountListSelected;
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;
        TransactionsComponent sc;
        AccountsComponent ac;

        public SearchAccountsSimpleForm()
        {
            InitializeComponent();

            sc = new TransactionsComponent();
            ac = new AccountsComponent();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (dataGridViewAccounts.SelectedRows.Count != 0)
            {

                try
                {
                    Account selectedAccount = (Account)bindingSourceAccounts.Current;
                    OnAccountListSelected(this, new AccountSelectEventArgs(selectedAccount));

                    this.Close();

                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }

        private void SearchAccountsSimpleForm_Load(object sender, EventArgs e)
        {
            try
            { 
                dataGridViewAccounts.AutoGenerateColumns = false;
                this.dataGridViewAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewAccounts.DataSource = bindingSourceAccounts;

                _Accounts = ac.GetAllAccounts().Where(i => i.Closed == false).AsQueryable();

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
                                   where acs.Closed == false
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
        private string[] AutoComplete_AccNames()
        {
            try
            {
                var accnamequery = from acs in ac.GetAllAccounts()
                                   where acs.Closed == false
                                   select acs.AccountName;
                return accnamequery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
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
                groupBox1.Text = bindingSourceAccounts.Count.ToString();
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
        private void dataGridViewAccounts_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewAccounts.SelectedRows.Count != 0)
            {

                try
                {
                    Account selectedAccount = (Account)bindingSourceAccounts.Current;
                    OnAccountListSelected(this, new AccountSelectEventArgs(selectedAccount));

                    this.Close();

                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
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



    }
}