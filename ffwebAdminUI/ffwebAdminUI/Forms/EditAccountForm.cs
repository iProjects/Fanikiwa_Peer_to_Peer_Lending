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
    public partial class EditAccountForm : Form
    { 
        Account _account; 
        int _CustomerId; 
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;
        TransactionsComponent tc;
        AccountsComponent ac;

        public EditAccountForm(Account account)
        {
            InitializeComponent();
            tc = new TransactionsComponent();
            ac = new AccountsComponent();
            _account = account;

        }

        public EditAccountForm(Account account, int CustomerId )
        {
            InitializeComponent(); 
            _account = account;
            _CustomerId = CustomerId;
            this.txtCustomerID.Text = _CustomerId.ToString();

        }
        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            errorProvider1.Clear();
            if (IsAccountValid())
            {
                try
                {

                    if (cboAccountTypes.SelectedIndex != -1)
                    {
                        _account.AccountTypeId = int.Parse(cboAccountTypes.SelectedValue.ToString());
                    }
                    if (cboCOA.SelectedIndex != -1)
                    {
                        _account.COAId = int.Parse(cboCOA.SelectedValue.ToString());
                    }

                    ac.UpdateAccount(_account);

                    AccountsListForm f = (AccountsListForm)this.Owner;
                    f.RefreshGrid();
                    this.Close();

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
        private void InitializeControls()
        {
            try
            {
                if (_account.CustomerId != null)
                {
                    txtCustomerID.Text = _account.CustomerId.ToString();
                }
                if (_account.AccountName != null)
                {
                    txtAccountName.Text = _account.AccountName.ToString();
                }
                if (_account.AccountNo != null)
                {
                    txtAccountNo.Text = _account.AccountNo;
                }
                if (_account.AccountTypeId != null)
                {
                    cboAccountTypes.SelectedValue = _account.AccountTypeId;
                }
                if (_account.COAId != null)
                {
                    cboCOA.SelectedValue = _account.COAId;
                } 
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        /*public method to diable all controls when form is called by parent from 'View Details' button*/
        public void DisableControls()
        {
            txtCustomerID.Enabled = false;
            txtAccountNo.Enabled = false;
            txtAccountName.Enabled = false;
            cboAccountTypes.Enabled = false;
            cboCOA.Enabled = false; 
            btnSearchCustomerId.Enabled = false;
            btnUpdate.Enabled = false;
            btnUpdate.Visible = false;
            btnClose.Location = btnUpdate.Location;

        }
        private void EditAccountForm_Load(object sender, EventArgs e)
        {
            try
            {
                 
                //var accttyps = from pc in tc.GetAllAccountTypes()
                //               select pc;
                //List<AccountType> accounttypes = accttyps.ToList();
                //bindingSourceAccountTypes.DataSource = accounttypes;
                //cboAccountTypes.DataSource = bindingSourceAccountTypes;
                //cboAccountTypes.ValueMember = "Id";
                //cboAccountTypes.DisplayMember = "Description";
                //cboAccountTypes.SelectedIndex = -1;

                //var COAsquery = from pc in tc.GetAllCOAs()
                //                orderby pc.ShortCode
                //                select pc;
                //List<COA> COAs = COAsquery.ToList();
                //cboCOA.DataSource = COAs;
                //cboCOA.ValueMember = "Id";
                //cboCOA.DisplayMember = "Description";
                //cboCOA.SelectedIndex = -1;

                InitializeControls();

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
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