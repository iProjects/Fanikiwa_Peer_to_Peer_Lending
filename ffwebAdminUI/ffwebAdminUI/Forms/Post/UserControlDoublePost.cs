using fCommon.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using fPeerLending.Entities;
using fPeerLending.Business;
using fanikiwaGL.Entities;

namespace ffwebAdminUI
{
    public partial class UserControlDoublePost : UserControl
    {
        private StreamReader streamToPrint;
        private Font printFont;
        TransactionType TType; 
        List<Transaction> transactions;
        TransactionsComponent tc;
        AccountsComponent ac;
        FinancialTransactionComponent fc; 
        decimal Amount;
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;


        public UserControlDoublePost(TransactionType ttype)
        {
            InitializeComponent();

            tc = new TransactionsComponent(); 
            fc = new FinancialTransactionComponent();
            ac = new AccountsComponent();

            if (ttype == null)
            {
                throw new ArgumentNullException("Type");
            }
            TType = ttype;
            //user = _user;
            transactions = new List<Transaction>();
             

            //initialize variables for printing 

            //configure the screen
            ConfigureScreen();
        }
        private void UserControlDoublePost_Load(object sender, EventArgs e)
        {
            try
            { 

                this.tableLayoutPanelDPost.RowStyles[4].Height = 0;
                this.tableLayoutPanelDPost.RowStyles[10].Height = 0;
                this.tableLayoutPanelDPost.RowStyles[11].Height = 0;
                 
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        
        private void ConfigureScreen()
        {
            //use the transaction type to hide/show various controls e.g.
            //use the transaction type to set up various control defaults on the screen
            //switch (TType.ScreenViewDebitAccountField)
            //{
            //    case "V": //the field is visible; Visible=true
            //        break;
            //    case "D": //the field is visible but Enabled=false
            //        txtDrAccount.Enabled = false;
            //        btnSelectDrAccount.Enabled = false;
            //        break;
            //    case "X": //Not visible
            //        lblDebitAccount.Visible = false;
            //        lblDrAccountDetails.Visible = false;
            //        btnSelectDrAccount.Visible = false;
            //        txtDrAccount.Visible = false;
            //        groupBoxDrAccount.Visible = false;
            //        this.tableLayoutPanelDPost.RowStyles[2].Height = 0;
            //        break;
            //}
            //switch (TType.ScreenViewModeofPaymentField)
            //{
            //    case "V": //the field is visible; Visible=true
            //        break;
            //    case "D": //the field is visible but Enabled=false
            //        cboModeofPayment.Enabled = false;
            //        break;
            //    case "X": //Not visible
            //        lblModeofPayment.Visible = false;
            //        cboModeofPayment.Visible = false;
            //        groupBoxModeofPayment.Visible = false;
            //        this.tableLayoutPanelDPost.RowStyles[3].Height = 0;
            //        break;
            //}
            //switch (TType.ScreenViewDebitNarrativeField)
            //{
            //    case "V": //the field is visible; Visible=true
            //        break;
            //    case "D": //the field is visible but Enabled=false
            //        txtDebitNarrative.Enabled = false;
            //        break;
            //    case "X": //Not visible
            //        lblDrNarrative.Visible = false;
            //        txtDebitNarrative.Visible = false;
            //        groupBoxDrNarrative.Visible = false;
            //        this.tableLayoutPanelDPost.RowStyles[5].Height = 0;
            //        break;
            //}
            //switch (TType.ScreenViewCreditNarrativeField)
            //{
            //    case "V": //the field is visible; Visible=true
            //        break;
            //    case "D": //the field is visible but Enabled=false
            //        txtCrNarrative.Enabled = false;
            //        break;
            //    case "X": //Not visible
            //        lblCrNarrative.Visible = false;
            //        txtCrNarrative.Visible = false;
            //        groupBoxCrNarrative.Visible = false;
            //        this.tableLayoutPanelDPost.RowStyles[6].Height = 0;
            //        break;
            //}
            //switch (TType.ScreenViewCreditAccountField)
            //{
            //    case "V": //the field is visible; Visible=true
            //        break;
            //    case "D": //the field is visible but Enabled=false
            //        txtCrAccount.Enabled = false;
            //        btnSelectCrAccount.Enabled = false;
            //        break;
            //    case "X": //Not visible
            //        lblCrAccountDetails.Visible = false;
            //        lblCreditAccount.Visible = false;
            //        btnSelectCrAccount.Visible = false;
            //        txtCrAccount.Visible = false;
            //        groupBoxCreditAccount.Visible = false;
            //        this.tableLayoutPanelDPost.RowStyles[7].Height = 0;
            //        break;
            //}
            //switch (TType.ScreenViewAmountField)
            //{
            //    case "V": //the field is visible; Visible=true
            //        break;
            //    case "D": //the field is visible but Enabled=false
            //        txtAmount.Enabled = false;
            //        break;
            //    case "X": //Not visible
            //        lblAmount.Visible = false;
            //        txtAmount.Visible = false;
            //        groupBoxAmount.Visible = false;
            //        this.tableLayoutPanelDPost.RowStyles[8].Height = 0;
            //        break;
            //}
            //switch (TType.ScreenViewValueDateField)
            //{
            //    case "V": //the field is visible; Visible=true
            //        break;
            //    case "D": //the field is visible but Enabled=false
            //        dtpValueDate.Enabled = false;
            //        break;
            //    case "X": //Not visible
            //        this.lblValueDate.Visible = false;
            //        dtpValueDate.Visible = false;
            //        groupBoxValueDate.Visible = false;
            //        this.tableLayoutPanelDPost.RowStyles[9].Height = 0;
            //        break;
            //}

            ////use the transaction type to enter default transactions  
            //if (TType.UseDefaultAmount.HasValue && TType.UseDefaultAmount.Value == true && TType.DefaultAmount != null)
            //{
            //    txtAmount.Text = TType.DefaultAmount.ToString();
            //}
            //if (TType.UseDefaultCreditAccount.HasValue && TType.UseDefaultCreditAccount.Value == true && TType.DefaultCreditAccount != null)
            //{
            //    txtCrAccount.Text = TType.DefaultCreditAccount.ToString();
            //}
            //if (TType.UseDefaultCreditNarrative.HasValue && TType.UseDefaultCreditNarrative.Value == true && TType.DefaultCreditNarrative != null)
            //{
            //    txtCrNarrative.Text = TType.DefaultCreditNarrative.ToString();
            //}
            //if (TType.UseDefaultDebitAccount.HasValue && TType.UseDefaultDebitAccount.Value == true && TType.DefaultDebitAccount != null)
            //{
            //    txtDrAccount.Text = TType.DefaultDebitAccount.ToString();
            //}
            //if (TType.UseDefaultDebitNarrative.HasValue && TType.UseDefaultDebitNarrative.Value == true && TType.DefaultDebitNarrative != null)
            //{
            //    txtDebitNarrative.Text = TType.DefaultDebitNarrative.ToString();
            //}
        }
        private bool IsTransactionValid()
        {
            bool noerror = true;
            //Debit Account
            if (string.IsNullOrEmpty(txtDrAccount.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtDrAccount, "Account ID cannot be null!");
                return false;
            }
            int _draccountid;
            if (!int.TryParse(txtDrAccount.Text, out _draccountid))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtDrAccount, "Account ID must be integer!");
                return false;
            }

            //DrAccountBeforePosting = tc.GetAccount(_draccountid);
            //if (null == DrAccountBeforePosting)
            //{
            //    errorProvider1.Clear();
            //    errorProvider1.SetError(txtDrAccount, "Error retrieving the account!");
            //    return false;
            //}

            //Credit Account
            if (string.IsNullOrEmpty(txtCrAccount.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtCrAccount, "Account ID cannot be null!");
                return false;
            }
            int _craccountid;
            if (!int.TryParse(txtCrAccount.Text, out _craccountid))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtCrAccount, "Account ID must be integer!");
                return false;
            }

            //CrAccountBeforePosting = tc.GetAccount(_craccountid);
            //if (null == CrAccountBeforePosting)
            //{
            //    errorProvider1.Clear();
            //    errorProvider1.SetError(txtCrAccount, "Error retrieving the account!");
            //    return false;
            //}
            //amount 
            if (string.IsNullOrEmpty(txtAmount.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAmount, "Amount cannot be null!");
                return false;
            }

            if (!decimal.TryParse(txtAmount.Text, out Amount))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAmount, "Amount must be decimal!");
                return false;
            } 
            return noerror;
        }
        private void btnPost_Click(object sender, EventArgs e)
        {


            /*
             * 1. validate screen input
             * 2. build up transactions 
             * 3. transaction post- dry run to test whether transactions will fail due to
             *  3.1 blocked _Accounts
             *  3.2 account'st financial status - will the transaction overdraw?
             *  3.3 do _Accounts actually exist
             * 4. Post the list 
             *  4.1 Lock all transactions
             *  4.2 Post
             *  4.3 Unlock
             * 5. if(tt.printreceipt == true) Print a reciept according to layout
             */
                        
            transactions = new List<Transaction>();
            errorProvider1.Clear();
            try
            {
                if (IsTransactionValid())
                {
                    //Biuld up transactions 
                    string contraref = Utils.GetRandomHexNumber(10);
                    int creditaccountid = int.Parse(txtCrAccount.Text);
                    int debitaccountid = int.Parse(txtDrAccount.Text);

                    transactions = fc.CreateTransactionsFromTransactionType(TType.TransactionTypeID, debitaccountid, creditaccountid, Amount, "Double Post", contraref, "sys", "system");
                     

                    //Post transactions
                    try
                    { 
                        TransactionFactory.Post(transactions);

                        MessageBox.Show("Transactions Posted Successfully!", "Fanikiwa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception exc)
                    {
                        Utils.ShowError(exc);
                        return;
                    }

                    //Print
                    string Template = TType.ReceiptLayout;

                    if (TType.PrintReceipt == true)
                    {
                        if (TType.PrintReceiptPrompt == true)
                        {
                            if (DialogResult.Yes == MessageBox.Show("Do you want to print", "Print Receipt", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {

                                PrintReciept(Template, GetPrintObject());
                            }
                        }
                        else
                        {
                            PrintReciept(Template, GetPrintObject());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private object[] GetPrintObject()
        {
            try
            {
                PrintFields pf = new PrintFields();
                List<PrintField> lpf = pf.GetFieldList();
                List<object> po = new List<object>();
                foreach (var item in lpf)
                {
                    object io = GetItemObject(item);
                    po.Add(io);
                }
                return po.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private object GetItemObject(PrintField item)
        {
            try
            {
                //switch (item.Id)
                //{
                //    case 0:
                //        return DateTime.Now.ToString("d/M/yyyy");
                //    case 1:
                //        return school.SchoolName;
                //    case 2:
                //        return school.Address1 + school.Address2;
                //    case 3:
                //        return receiptNo;
                //    case 4:
                //        return DrAccountAfterPosting.AccountName;
                //    case 5:
                //        return CrAccountAfterPosting.AccountName;
                //    case 6:
                //        return DebitTransaction.Narrative;
                //    case 7:
                //        return CreditTransaction.Narrative;
                //    case 8:
                //        return CreditTransaction.Amount;
                //    case 9:
                //        return "Student Name";
                //    case 10:
                //        return "Student Admin No";
                //    case 11:
                //        return "Class";
                //    default:
                //        return "Unknown Field";
                //}
                return "Unknown Field";
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string BuildNarrative(string type,Transaction txn)
        {
            try
            {
                string narr = string.Empty;
                if ("D".Equals(type.ToUpper()))
                {
                    //build debit narrative 
                    //switch (TType.NarrativeFlag)
                    //{
                    //    case "S": //see narrative as per screen input
                    //        narr += txtDebitNarrative.Text;
                    //        break;
                    //    case "E": //see narrative as per screen input + account name
                    //        narr += txtDebitNarrative.Text + ",   Account = " + " Name: [ " + DrAccountAfterPosting.AccountName + " ]  " + "  No: [ " + DrAccountAfterPosting.AccountNo + " ] " + " Amount: [ " + txn.Amount.ToString("#,##0") + " ] ";
                    //        break;
                    //}

                }
                else
                {
                    //build a credit narrative 
                    //switch (TType.NarrativeFlag)
                    //{
                    //    case "S": //see narrative as per screen input
                    //        narr += txtCrNarrative.Text;
                    //        break;
                    //    case "E": //see narrative as per screen input + account name
                    //        narr += txtCrNarrative.Text  + ",   Account = " + " Name: [ " + CrAccountAfterPosting.AccountName + " ]  " + "  No: [ " + CrAccountAfterPosting.AccountNo + " ] " + " Amount: [ " + txn.Amount.ToString("#,##0") + " ] ";
                    //        break;
                    //}
                }
                return narr;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string BuildStudentInfo(string type)
        {
            try
            {
                string narr = string.Empty;
                return narr;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        // The PrintPage event is raised for each page to be printed. 
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            try
            {
                float linesPerPage = 0;
                float yPos = 0;
                int count = 0;
                float leftMargin = ev.MarginBounds.Left;
                float topMargin = ev.MarginBounds.Top;
                string line = null;

                // Calculate the number of lines per page.
                linesPerPage = ev.MarginBounds.Height /
                   printFont.GetHeight(ev.Graphics);

                // Print each line of the file. 
                while (count < linesPerPage &&
                   ((line = streamToPrint.ReadLine()) != null))
                {
                    yPos = topMargin + (count *
                       printFont.GetHeight(ev.Graphics));
                    ev.Graphics.DrawString(line, printFont, Brushes.Black,
                       leftMargin, yPos, new StringFormat());
                    count++;
                }

                // If more lines exist, print another page. 
                if (line != null)
                    ev.HasMorePages = true;
                else
                    ev.HasMorePages = false;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void PrintReciept(string ReceiptLayout, object[] PostObject)
        {
            try
            {
                string output = string.Format(ReceiptLayout, PostObject);
                PrintDocument p = new PrintDocument();
                p.PrintPage += delegate(object sender1, PrintPageEventArgs e1)
                {
                    e1.Graphics.DrawString(output,
                        new Font("Arial", 12),
                        new SolidBrush(Color.Black),
                        new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));

                };

                try
                {
                    p.Print();
                }
                catch (Exception ex)
                {
                    throw new Exception("Exception Occured While Printing", ex);
                }
                //streamToPrint = new StreamReader ("C:\\MyFile.txt");
                //try
                //{
                //    printFont = new Font("Arial", 10);
                //    PrintDocument pd = new PrintDocument();
                //    pd.PrintPage += new PrintPageEventHandler
                //       (this.pd_PrintPage);
                //    pd.Print();
                //}
                //finally
                //{
                //    streamToPrint.Close();
                //}
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        } 
        private void btnSelectDrAccount_Click(object sender, EventArgs e)
        {
            try
            {
                Form myForm = this.FindForm();
                SearchAccountsSimpleForm saf = new SearchAccountsSimpleForm() { Owner = myForm };
                saf.OnAccountListSelected += new SearchAccountsSimpleForm.AccountSelectHandler(saf_OnDrAccountListSelected);
                saf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void saf_OnDrAccountListSelected(object sender, AccountSelectEventArgs e)
        {
            SetDrAccountId(e._Account);
        }
        private void SetDrAccountId(Account _Account)
        {
            if (_Account != null)
            {
                txtDrAccount.Text = _Account.AccountID.ToString();
                lblDrAccountDetails.Text = "[" + _Account.AccountNo + " ]  -  [ " + _Account.AccountName.Trim() + " ]   Book Balance:  " + _Account.BookBalance.ToString("C2");
            }

        }
        private void btnSelectCrAccount_Click(object sender, EventArgs e)
        {
            try
            {
                Form myForm = this.FindForm();
                SearchAccountsSimpleForm saf = new SearchAccountsSimpleForm() { Owner = myForm };
                saf.OnAccountListSelected += new SearchAccountsSimpleForm.AccountSelectHandler(saf_OnCrAccountListSelected);
                saf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void saf_OnCrAccountListSelected(object sender, AccountSelectEventArgs e)
        {
            SetCrAccountId(e._Account);
        }
        private void SetCrAccountId(Account _Account)
        {
            if (_Account != null)
            {
                txtCrAccount.Text = _Account.AccountID.ToString();
                lblCrAccountDetails.Text = "[" + _Account.AccountNo + " ]  -  [ " + _Account.AccountName.Trim() + " ]   Book Balance:  " + _Account.BookBalance.ToString("C2");
            }

        }
        private void btnSearchchequeBank_Click(object sender, EventArgs e)
        {
            try
            {
                //Form myForm = this.FindForm();
                //SearchBanksSimpleForm saf = new Forms.SearchBanksSimpleForm(connection) { Owner = myForm };
                //saf.OnBankListSelected += new SearchBanksSimpleForm.BankSelectHandler(saf_OnBankChequeListSelected);
                //saf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        
        private void btnSearchslipBank_Click(object sender, EventArgs e)
        {
            try
            {
                //Form _parentForm = this.FindForm();
                //SearchBanksSimpleForm saf = new Forms.SearchBanksSimpleForm(connection) { Owner = _parentForm };
                //saf.OnBankListSelected += new SearchBanksSimpleForm.BankSelectHandler(saf_OnBankSlipListSelected);
                //saf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        
        private void txtDrAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered == true)
            {
                e.Handled = true;
            }
        }
        private void txtDrAccount_KeyDown(object sender, KeyEventArgs e)
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
        private void txtCrAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered == true)
            {
                e.Handled = true;
            }
        }
        private void txtCrAccount_KeyDown(object sender, KeyEventArgs e)
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
        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered == true)
            {
                e.Handled = true;
            }
        }
        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
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
        private void txtbsBankSortCode_KeyDown(object sender, KeyEventArgs e)
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
        private void txtbsBankSortCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (nonNumberEntered == true)
                {
                    if (e.KeyChar == 13)
                    {
                    }
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtcqBankSortCode_KeyDown(object sender, KeyEventArgs e)
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
        private void txtcqBankSortCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (nonNumberEntered == true)
                {
                    if (e.KeyChar == 13)
                    {
                    }
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtDrAccount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblDrAccountDetails.Text = string.Empty;
                int _accountid = 0;
                if (int.TryParse(txtDrAccount.Text.Trim(), out _accountid))
                {
                    var _accountqueryquery = (from acs in ac.GetAllAccounts()
                                              where acs.AccountID == _accountid
                                              select acs).FirstOrDefault();
                    if (_accountqueryquery != null)
                    {
                        lblDrAccountDetails.Text = "[" + _accountqueryquery.AccountNo + " ]  -  [ " + _accountqueryquery.AccountName.Trim() + " ]   Book Balance:  " + _accountqueryquery.BookBalance.ToString("C2");
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtDrAccount_Validated(object sender, EventArgs e)
        {
            try
            {
                lblDrAccountDetails.Text = string.Empty;
                int _accountid = 0;
                if (int.TryParse(txtDrAccount.Text.Trim(), out _accountid))
                {
                    var _accountqueryquery = (from acs in ac.GetAllAccounts()
                                              where acs.AccountID == _accountid
                                              select acs).FirstOrDefault();
                    if (_accountqueryquery != null)
                    {
                        lblDrAccountDetails.Text = "[" + _accountqueryquery.AccountNo + " ]  -  [ " + _accountqueryquery.AccountName.Trim() + " ]   Book Balance:  " + _accountqueryquery.BookBalance.ToString("C2");
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtDrAccount_TabIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblDrAccountDetails.Text = string.Empty;
                int _accountid = 0;
                if (int.TryParse(txtDrAccount.Text.Trim(), out _accountid))
                {
                    var _accountqueryquery = (from acs in ac.GetAllAccounts()
                                              where acs.AccountID == _accountid
                                              select acs).FirstOrDefault();
                    if (_accountqueryquery != null)
                    {
                        lblDrAccountDetails.Text = "[" + _accountqueryquery.AccountNo + " ]  -  [ " + _accountqueryquery.AccountName.Trim() + " ]   Book Balance:  " + _accountqueryquery.BookBalance.ToString("C2");
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtDrAccount_Leave(object sender, EventArgs e)
        {
            try
            {
                lblDrAccountDetails.Text = string.Empty;
                int _accountid = 0;
                if (int.TryParse(txtDrAccount.Text.Trim(), out _accountid))
                {
                    var _accountqueryquery = (from acs in ac.GetAllAccounts()
                                              where acs.AccountID == _accountid
                                              select acs).FirstOrDefault();
                    if (_accountqueryquery != null)
                    {
                        lblDrAccountDetails.Text = "[" + _accountqueryquery.AccountNo + " ]  -  [ " + _accountqueryquery.AccountName.Trim() + " ]   Book Balance:  " + _accountqueryquery.BookBalance.ToString("C2");
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtCrAccount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblCrAccountDetails.Text = string.Empty;
                int _accountid = 0;
                if (int.TryParse(txtCrAccount.Text.Trim(), out _accountid))
                {
                    var _accountqueryquery = (from acs in ac.GetAllAccounts()
                                              where acs.AccountID == _accountid
                                              select acs).FirstOrDefault();
                    if (_accountqueryquery != null)
                    {
                        lblCrAccountDetails.Text = "[" + _accountqueryquery.AccountNo + " ]  -  [ " + _accountqueryquery.AccountName.Trim() + " ]   Book Balance:  " + _accountqueryquery.BookBalance.ToString("C2");
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtCrAccount_Validated(object sender, EventArgs e)
        {
            try
            {
                lblCrAccountDetails.Text = string.Empty;
                int _accountid = 0;
                if (int.TryParse(txtCrAccount.Text.Trim(), out _accountid))
                {
                    var _accountqueryquery = (from acs in ac.GetAllAccounts()
                                              where acs.AccountID == _accountid
                                              select acs).FirstOrDefault();
                    if (_accountqueryquery != null)
                    {
                        lblCrAccountDetails.Text = "[" + _accountqueryquery.AccountNo + " ]  -  [ " + _accountqueryquery.AccountName.Trim() + " ]   Book Balance:  " + _accountqueryquery.BookBalance.ToString("C2");
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtCrAccount_TabIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblCrAccountDetails.Text = string.Empty;
                int _accountid = 0;
                if (int.TryParse(txtCrAccount.Text.Trim(), out _accountid))
                {
                    var _accountqueryquery = (from acs in ac.GetAllAccounts()
                                              where acs.AccountID == _accountid
                                              select acs).FirstOrDefault();
                    if (_accountqueryquery != null)
                    {
                        lblCrAccountDetails.Text = "[" + _accountqueryquery.AccountNo + " ]  -  [ " + _accountqueryquery.AccountName.Trim() + " ]   Book Balance:  " + _accountqueryquery.BookBalance.ToString("C2");
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtCrAccount_Leave(object sender, EventArgs e)
        {
            try
            {
                lblCrAccountDetails.Text = string.Empty;
                int _accountid = 0;
                if (int.TryParse(txtCrAccount.Text.Trim(), out _accountid))
                {
                    var _accountqueryquery = (from acs in ac.GetAllAccounts()
                                              where acs.AccountID == _accountid
                                              select acs).FirstOrDefault();
                    if (_accountqueryquery != null)
                    {
                        lblCrAccountDetails.Text = "[" + _accountqueryquery.AccountNo + " ]  -  [ " + _accountqueryquery.AccountName.Trim() + " ]   Book Balance:  " + _accountqueryquery.BookBalance.ToString("C2");
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