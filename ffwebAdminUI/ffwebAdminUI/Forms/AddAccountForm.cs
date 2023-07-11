using fCommon.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data; 
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;  
using fPeerLending.Entities;
using fPeerLending.Business;
using fanikiwaGL.Entities; 

namespace ffwebAdminUI
{
    public partial class AddAccountForm : Form
    {  
        int _Parent;
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;
        TransactionsComponent tc;
        AccountsComponent ac;

        #region "Constructor"
        public AddAccountForm()
        {
            InitializeComponent();

            tc = new TransactionsComponent();
            ac = new AccountsComponent();
        }
        public AddAccountForm(int Parent)
        {
            InitializeComponent();
            
            tc = new TransactionsComponent();
            ac = new AccountsComponent();
             
            if (Parent == null)
                throw new ArgumentNullException("Parent");
            _Parent = Parent;
            
        } 
        #endregion "Constructor"

        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
        private void AddAccountForm_Load(object sender, EventArgs e)
        {
            try
            { 
                var passflags = new BindingList<KeyValuePair<short, string>>();
                passflags.Add(new KeyValuePair<short, string>(1, "Debit Posting Prohibited"));
                passflags.Add(new KeyValuePair<short, string>(2, "Credit Posting Prohibited"));
                passflags.Add(new KeyValuePair<short, string>(3, "All Posting Prohibited"));
                passflags.Add(new KeyValuePair<short, string>(4, "Locked")); 
                cboPassFlag.DataSource = passflags;
                cboPassFlag.ValueMember = "Key";
                cboPassFlag.DisplayMember = "Value";
                cboPassFlag.SelectedIndex = -1;

                var limitcheckflags = new BindingList<KeyValuePair<short, string>>();
                limitcheckflags.Add(new KeyValuePair<short, string>(5, "Posting No Limit Checking"));
                limitcheckflags.Add(new KeyValuePair<short, string>(6, "Posting Over Drawing Prohibited"));
                limitcheckflags.Add(new KeyValuePair<short, string>(7, "Posting Drawing On Uncleared Effects Allowed")); 
                cboLimitCheckFlag.DataSource = limitcheckflags;
                cboLimitCheckFlag.ValueMember = "Key";
                cboLimitCheckFlag.DisplayMember = "Value";
                cboLimitCheckFlag.SelectedIndex = -1;

                var limitlags = new BindingList<KeyValuePair<short, string>>();
                limitlags.Add(new KeyValuePair<short, string>(8, "LimitsAllowed"));
                limitlags.Add(new KeyValuePair<short, string>(9, "LimitForAdvanceProhibited"));
                limitlags.Add(new KeyValuePair<short, string>(10, "LimitForBlockingProhibited"));
                limitlags.Add(new KeyValuePair<short, string>(11, "AllLimitsProhibited")); 
                cboLimitFlag.DataSource = limitlags;
                cboLimitFlag.ValueMember = "Key";
                cboLimitFlag.DisplayMember = "Value";
                cboLimitFlag.SelectedIndex = -1;
                
                var accttyps = from pc in tc.GetAllAccountTypes()
                               select pc;
                List<AccountType> AccountTypes = accttyps.ToList();
                cboAccountTypes.DataSource = AccountTypes;
                cboAccountTypes.ValueMember = "Id";
                cboAccountTypes.DisplayMember = "Description";
                cboAccountTypes.SelectedIndex = -1;

                var COAsquery = from pc in tc.GetAllCOAs()
                                orderby pc.ShortCode
                                select pc;
                List<COA> COAs = COAsquery.ToList();
                cboCOA.DataSource = COAs;
                cboCOA.ValueMember = "Id";
                cboCOA.DisplayMember = "Description";
                cboCOA.SelectedIndex = -1;

                AutoCompleteStringCollection acsaccid = new AutoCompleteStringCollection();
                acsaccid.AddRange(this.AutoComplete_CustomerIds());
                txtCustomerId.AutoCompleteCustomSource = acsaccid;
                txtCustomerId.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtCustomerId.AutoCompleteSource =
                     AutoCompleteSource.CustomSource; 

                AutoCompleteStringCollection acscnm = new AutoCompleteStringCollection();
                acscnm.AddRange(this.AutoComplete_AccountName());
                txtAccountName.AutoCompleteCustomSource = acscnm;
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
        private string[] AutoComplete_AccountName()
        {
            try
            {
                var accountsquery = from bk in ac.GetAllAccounts()
                                     where bk.Closed == false 
                                     select bk.AccountName;
                return accountsquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_AccountNo()
        {
            try
            {
                var accountsquery = from bk in ac.GetAllAccounts()
                                     where bk.Closed==false 
                                     select bk.AccountNo;
                return accountsquery.ToArray();
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
                                    select acs.CustomerId).Distinct();
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
        private void btnAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (IsAccountValid())
            {
                try
                {
                    Account _Account = new Account();
                    if (!string.IsNullOrEmpty(txtCustomerId.Text))
                    {
                        _Account.CustomerId = int.Parse(txtCustomerId.Text.ToString());
                    }
                    if (!string.IsNullOrEmpty(txtAccountName.Text))
                    {
                        _Account.AccountName = Utils.ConvertFirstLetterToUpper(txtAccountName.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtAccountNo.Text))
                    {
                        _Account.AccountNo = Utils.ConvertFirstLetterToUpper(txtAccountNo.Text);
                    }
                    if (cboAccountTypes.SelectedIndex != -1)
                    {
                        _Account.AccountTypeId = int.Parse(cboAccountTypes.SelectedValue.ToString());
                    }
                    if (_Parent != null)
                    {
                        _Account.COAId = _Parent;
                    }
                    if (cboCOA.SelectedIndex != -1)
                    {
                        _Account.COAId = int.Parse(cboCOA.SelectedValue.ToString());
                    }
                    _Account.BookBalance = 0;
                    _Account.ClearedBalance = 0;
                    _Account.Limit = 0;
                    if (!string.IsNullOrEmpty(txtBranches.Text))
                    {
                        _Account.Branch = Utils.ConvertFirstLetterToUpper(txtBranches.Text).Trim();
                    }
                    if (cboPassFlag.SelectedIndex != -1)
                    {
                        _Account.PassFlag = short.Parse(cboPassFlag.SelectedValue.ToString());
                    }
                    if (cboLimitFlag.SelectedIndex != -1)
                    {
                        _Account.LimitFlag = short.Parse(cboLimitFlag.SelectedValue.ToString());
                    }
                    if (cboLimitCheckFlag.SelectedIndex != -1)
                    {
                        _Account.LimitCheckFlag = short.Parse(cboLimitCheckFlag.SelectedValue.ToString());
                    } 
                    _Account.AccruedInt = 0;
                    _Account.Bal30 = 0;
                    _Account.Bal60 = 0;
                    _Account.Bal90 = 0;
                    _Account.BalOver90 = 0;
                    _Account.IntRate30 = 0;
                    _Account.IntRate60 = 0;
                    _Account.IntRate90 = 0;
                    _Account.IntRateOver90 = 0;
                    _Account.Closed = false;

                    if (ac.GetAllAccounts().Any(c => c.AccountName == _Account.AccountName))
                    {
                        MessageBox.Show("Account Name " + _Account.AccountName + " Exists!", "Fanikiwa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!ac.GetAllAccounts().Any(c => c.AccountName == _Account.AccountName))
                    {
                        ac.OpenAccount(_Account);

                        if (this.Owner is AccountsListForm)
                        {
                            AccountsListForm f = (AccountsListForm)this.Owner;
                            f.RefreshGrid();
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        public bool IsAccountValid()
        {
            bool noerror = true;
            if (string.IsNullOrEmpty(txtCustomerId.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtCustomerId, "Owner cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtAccountName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAccountName, "Account Name cannot be null!");
                return false;
            } 
            if (cboAccountTypes.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cboAccountTypes, "Select Account Type!");
                return false;
            }
            if (cboCOA.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cboCOA, "Select a Chart of Accounts!");
                return false;
            }
            return noerror;
        }
        private void btnSearchCustomerId_Click(object sender, EventArgs e)
        {
            try
            {
            SearchCustomersSimpleForm scf = new SearchCustomersSimpleForm() { Owner = this };
            scf.OnCustomerListSelected += new SearchCustomersSimpleForm.CustomerSelectHandler(scf_OnCustomerListSelected);
            scf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

        }
        private void scf_OnCustomerListSelected(object sender, CustomerSelectEventArgs e)
        {
            SetAccountNos(e._Customer);
        }
        private void SetAccountNos(Customer _customer)
        {
            if (_customer != null)
            {
                txtCustomerId.Text = _customer.CustomerId.ToString();
            }

        } 
        private void txtCustomerID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        private void txtCustomerID_KeyDown(object sender, KeyEventArgs e)
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
         
    }
}