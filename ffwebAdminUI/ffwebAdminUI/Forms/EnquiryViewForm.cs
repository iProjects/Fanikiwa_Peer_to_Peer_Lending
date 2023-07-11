using fCommon.Utility;
using System;
using System.Collections.Generic;
using System.Windows.Forms; 
using System.Linq;
using fPeerLending.Entities;
using fPeerLending.Business;
using fanikiwaGL.Entities;

namespace ffwebAdminUI
{
    public partial class EnquiryViewForm : Form
    { 
        Account _Account; 
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;
        TransactionsComponent sc;
        AccountsComponent ac;

        public EnquiryViewForm()
        {
            InitializeComponent();

            sc = new TransactionsComponent();
            ac = new AccountsComponent();
        }
        public EnquiryViewForm(Account acc)
        {
            InitializeComponent();

            if (acc == null)
                throw new ArgumentNullException("Account");
            _Account = acc;

            txtAccountId.Text = _Account.AccountID.ToString();

            sc = new TransactionsComponent();
            ac = new AccountsComponent();
        }

        private void btnSearch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            errorProvider1.Clear();
            try
            {
                bindingSourceTransactions.DataSource = null;
                DateTime strtdate = dateTimePickerStartDate.Value;
                DateTime enddate = dateTimePickerEndDate.Value;

                foreach (Control ctrl in groupBoxSearchCriteria.Controls)
                {
                    if (ctrl.GetType() == typeof(RadioButton))
                    {
                        if (((RadioButton)ctrl).Checked)
                        {
                            switch (((RadioButton)ctrl).Name)
                            {
                                case "radioButtonTransactionReference":
                                    if (string.IsNullOrEmpty(txtTransRef.Text))
                                    {
                                        MessageBox.Show("Transaction Reference cannot be null");
                                        return;
                                    }
                                    string _Transref = txtTransRef.Text.Trim().ToUpper();
                                    var _transactionsquery = (from ts in sc.GetViewTransactionsByContraReference(_Transref)
                                                              select ts).ToList();
                                    List<TransactionModel> lstTransactions = _transactionsquery.ToList();
                                    
                                    bindingSourceTransactions.DataSource = lstTransactions;
                                    groupBox2.Text = "TRANSACTIONS  LIST   [  " + bindingSourceTransactions.Count.ToString() + "  ]";
                                    break;
                                case "radioButtonAccountId":
                                    if (string.IsNullOrEmpty(txtAccountId.Text))
                                    {
                                        MessageBox.Show("Account Id cannot be null");
                                        return;
                                    } 
                                    int accid = int.Parse(txtAccountId.Text);
                                    Account AccountPosting = ac.GetAccount(accid);
                                    if (AccountPosting != null)
                                    { 
                                        var _txnsquery = from ts in sc.GetAccountViewTransactionsByDate(accid,strtdate,enddate)
                                                                 select ts;
                                        List<TransactionModel> lsttxns = _txnsquery.ToList();

                                        if (this.dataGridViewTransactions.Columns.Contains("cbAccount"))
                                        {
                                            dataGridViewTransactions.Columns.Remove("cbAccount");
                                        }

                                        bindingSourceTransactions.DataSource = lsttxns;
                                        groupBox2.Text = "TRANSACTIONS  LIST   [  " + bindingSourceTransactions.Count.ToString() + "  ]";
                                    }
                                    else
                                    {
                                        errorProvider1.Clear();
                                        errorProvider1.SetError(txtAccountId, "Error Retrieving the Transactions!");
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnSearchAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                SearchAccountsSimpleForm saf = new SearchAccountsSimpleForm() { Owner = this };
                saf.OnAccountListSelected += new SearchAccountsSimpleForm.AccountSelectHandler(saf_OnAccountListSelected);
                saf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void saf_OnAccountListSelected(object sender, AccountSelectEventArgs e)
        {
            try
            {
                SetAccountId(e._Account);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void SetAccountId(Account _Account)
        {
            try
            {
                if (_Account != null)
                {
                    txtAccountId.Text = _Account.AccountID.ToString();
                    lblAccountInfo.Text = "Name :[  " + _Account.AccountName + "  ]  No :[  " + _Account.AccountNo + "  ]   Book Balance :[  " + _Account.BookBalance.ToString("C2") + "  ]";
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void EnquiryViewForm_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime v = DateTime.Today;
                this.dateTimePickerEndDate.Value = DateTime.Today;
                this.dateTimePickerStartDate.Value = DateTime.Today.AddMonths(-3);

                groupBoxAccountId.Visible = false;
                groupBoxTransRef.Visible = false;
                btnSearch.Enabled = false;
                btnPrint.Enabled = false;
                radioButtonAccountId.Checked = true;

                if (!string.IsNullOrEmpty(txtAccountId.Text))
                {
                    btnSearch.Enabled = true;
                    btnPrint.Enabled = true;
                    Account AccountPosting =ac.GetAccount(int.Parse(txtAccountId.Text));
                    if (AccountPosting != null)
                    {
                        lblAccountInfo.Text = "Name :[  " + _Account.AccountName + "  ]  No :[  " + _Account.AccountNo + "  ]   Book Balance :[  " + _Account.BookBalance.ToString("C2") + "  ]";
                    }
                }

                this.dataGridViewTransactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewTransactions.AutoGenerateColumns = false;
                dataGridViewTransactions.DataSource = bindingSourceTransactions;

                AutoCompleteStringCollection acsaccid = new AutoCompleteStringCollection();
                acsaccid.AddRange(this.AutoComplete_AccountIds());
                txtAccountId.AutoCompleteCustomSource = acsaccid;
                txtAccountId.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtAccountId.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acstransref = new AutoCompleteStringCollection();
                acstransref.AddRange(this.AutoComplete_TransactionReferences());
                txtTransRef.AutoCompleteCustomSource = acstransref;
                txtTransRef.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtTransRef.AutoCompleteSource =
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
        private string[] AutoComplete_TransactionReferences()
        {
            try
            {
                var transrefquery = from ts in sc.GetAllTransactions()
                                    orderby ts.PostDate ascending
                                    select ts.ContraReference;
                return transrefquery.ToArray();
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
        private void btnPrint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                int _AccountId;
                DateTime _startdate = dateTimePickerStartDate.Value;
                DateTime _enddate = dateTimePickerEndDate.Value;

                if (int.TryParse(txtAccountId.Text, out _AccountId))
                {
                    //Viewer.PDFViewer _viewer = new Viewer.PDFViewer(_AccountId, _startdate, _enddate, user, connection);
                    //_viewer.WindowState = FormWindowState.Normal;
                    //_viewer.StartPosition = FormStartPosition.CenterScreen;
                    //_viewer.Show();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtAccountId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered == true)
            {
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
        private void txtAccountId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (Control ctrl in groupBoxSearchCriteria.Controls)
                {
                    if (ctrl.GetType() == typeof(RadioButton))
                    {
                        if (((RadioButton)ctrl).Checked)
                        {
                            switch (((RadioButton)ctrl).Name)
                            {
                                case "radioButtonAccountId":
                                    if (string.IsNullOrEmpty(txtAccountId.Text))
                                    {
                                        btnSearch.Enabled = false;
                                        btnPrint.Enabled = false;
                                    }
                                    else
                                    {
                                        lblAccountInfo.Text = string.Empty;
                                        int _accountid = 0;
                                        if (int.TryParse(txtAccountId.Text.Trim(), out _accountid))
                                        {
                                            var _accountqueryquery = (from acs in ac.GetAllAccounts()
                                                                      where acs.AccountID == _accountid
                                                                      select acs).FirstOrDefault();
                                            if (_accountqueryquery != null)
                                            {
                                                lblAccountInfo.Text = "[" + _accountqueryquery.AccountNo + " ]  -  [ " + _accountqueryquery.AccountName.Trim() + " ]   Book Balance:  " + _accountqueryquery.BookBalance.ToString("C2");
                                            }
                                        }

                                        btnSearch.Enabled = true;
                                        btnPrint.Enabled = true;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void radioButtonAccountId_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bindingSourceTransactions.DataSource = null;
                groupBox2.Text = "TRANSACTIONS  LIST   [  " + bindingSourceTransactions.Count.ToString() + "  ]";

                foreach (Control ctrl in groupBoxSearchCriteria.Controls)
                {
                    if (ctrl.GetType() == typeof(RadioButton))
                    {
                        if (((RadioButton)ctrl).Checked)
                        {
                            switch (((RadioButton)ctrl).Name)
                            {
                                case "radioButtonAccountId":
                                    groupBoxAccountId.Visible = true;
                                    break;
                                default:
                                    groupBoxAccountId.Visible = false;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void radioButtonTransactionReference_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bindingSourceTransactions.DataSource = null;
                groupBox2.Text = "TRANSACTIONS  LIST   [  " + bindingSourceTransactions.Count.ToString() + "  ]";

                foreach (Control ctrl in groupBoxSearchCriteria.Controls)
                {
                    if (ctrl.GetType() == typeof(RadioButton))
                    {
                        if (((RadioButton)ctrl).Checked)
                        {
                            switch (((RadioButton)ctrl).Name)
                            {

                                case "radioButtonTransactionReference":
                                    groupBoxTransRef.Visible = true;
                                    groupBoxTransRef.Location = groupBoxAccountId.Location;
                                    break;
                                default:
                                    groupBoxTransRef.Visible = false;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtTransRef_TextChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (Control ctrl in groupBoxSearchCriteria.Controls)
                {
                    if (ctrl.GetType() == typeof(RadioButton))
                    {
                        if (((RadioButton)ctrl).Checked)
                        {
                            switch (((RadioButton)ctrl).Name)
                            {
                                case "radioButtonTransactionReference":
                                    if (string.IsNullOrEmpty(txtTransRef.Text))
                                    {
                                        btnSearch.Enabled = false;
                                    }
                                    else
                                    {
                                        btnSearch.Enabled = true;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
         










    }
}