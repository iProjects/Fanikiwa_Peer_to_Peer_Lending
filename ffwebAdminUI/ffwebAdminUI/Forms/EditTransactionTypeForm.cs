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
    public partial class EditTransactionTypeForm : Form
    {
        TransactionsComponent tc;
        AccountsComponent ac;
        TransactionType txn;
        List<object> PrintObject;
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;

        public EditTransactionTypeForm(TransactionType txntype)
        {
            InitializeComponent();

            tc = new TransactionsComponent();
            ac = new AccountsComponent();
            txn = txntype;
            PrintObject = new List<object>();
        }
        private void EditTransactionTypeForm_Load(object sender, EventArgs e)
        {
            try
            {
                groupBoxComputation.Visible = false;
                groupBoxFlat.Visible = false;
                groupBoxTiered.Visible = false;
                groupBoxLookUp.Visible = false;

                var debitcredit = new BindingList<KeyValuePair<string, string>>();
                debitcredit.Add(new KeyValuePair<string, string>("D", "Debit"));
                debitcredit.Add(new KeyValuePair<string, string>("C", "Credit"));
                cboDebitCredit.DataSource = debitcredit;
                cboDebitCredit.ValueMember = "Key";
                cboDebitCredit.DisplayMember = "Value";
                cboDebitCredit.SelectedIndex = -1;

                var _TxnTypeView = new BindingList<KeyValuePair<short, string>>();
                _TxnTypeView.Add(new KeyValuePair<short, string>(1, "Single Entry"));
                _TxnTypeView.Add(new KeyValuePair<short, string>(2, "Double Entry"));
                _TxnTypeView.Add(new KeyValuePair<short, string>(3, "Muilti Entry"));
                cboTxnView.DataSource = _TxnTypeView;
                cboTxnView.ValueMember = "Key";
                cboTxnView.DisplayMember = "Value";
                cboTxnView.SelectedIndex = -1;

                var _ComTxnTypes = tc.GetNonCommTransactionTypes();
                cboCommissionTransactionType.DataSource = _ComTxnTypes;
                cboCommissionTransactionType.ValueMember = "TransactionTypeID";
                cboCommissionTransactionType.DisplayMember = "Description";
                cboCommissionTransactionType.SelectedIndex = -1;

                var _CommComputationMethods = new BindingList<KeyValuePair<string, string>>();
                _CommComputationMethods.Add(new KeyValuePair<string, string>("F", "Flat"));
                _CommComputationMethods.Add(new KeyValuePair<string, string>("T", "Tiered"));
                _CommComputationMethods.Add(new KeyValuePair<string, string>("L", "Look Up"));
                cboCommComputationMethod.DataSource = _CommComputationMethods;
                cboCommComputationMethod.ValueMember = "Key";
                cboCommComputationMethod.DisplayMember = "Value";
                cboCommComputationMethod.SelectedIndex = -1;

                var _DrCommCalcMethods = new BindingList<KeyValuePair<string, string>>();
                _DrCommCalcMethods.Add(new KeyValuePair<string, string>("F", "Flat"));
                _DrCommCalcMethods.Add(new KeyValuePair<string, string>("T", "Tiered"));
                _DrCommCalcMethods.Add(new KeyValuePair<string, string>("L", "Look Up"));
                cboDrCommCalcMethod.DataSource = _DrCommCalcMethods;
                cboDrCommCalcMethod.ValueMember = "Key";
                cboDrCommCalcMethod.DisplayMember = "Value";
                cboDrCommCalcMethod.SelectedIndex = -1;

                var _CrCommCalcMethods = new BindingList<KeyValuePair<string, string>>();
                _CrCommCalcMethods.Add(new KeyValuePair<string, string>("F", "Flat"));
                _CrCommCalcMethods.Add(new KeyValuePair<string, string>("T", "Tiered"));
                _CrCommCalcMethods.Add(new KeyValuePair<string, string>("L", "Look Up"));
                cboCrCommCalcMethod.DataSource = _CrCommCalcMethods;
                cboCrCommCalcMethod.ValueMember = "Key";
                cboCrCommCalcMethod.DisplayMember = "Value";
                cboCrCommCalcMethod.SelectedIndex = -1;

                var _ValueDateOffsets = new BindingList<KeyValuePair<short, string>>();
                _ValueDateOffsets.Add(new KeyValuePair<short, string>(1, "Flat"));
                _ValueDateOffsets.Add(new KeyValuePair<short, string>(2, "Tiered"));
                _ValueDateOffsets.Add(new KeyValuePair<short, string>(3, "Look Up"));
                cboValueDateOffset.DataSource = _ValueDateOffsets;
                cboValueDateOffset.ValueMember = "Key";
                cboValueDateOffset.DisplayMember = "Value";
                cboValueDateOffset.SelectedIndex = -1;

                var _NarrativeFlags = new BindingList<KeyValuePair<short, string>>();
                _NarrativeFlags.Add(new KeyValuePair<short, string>(1, "Flat"));
                _NarrativeFlags.Add(new KeyValuePair<short, string>(2, "Tiered"));
                _NarrativeFlags.Add(new KeyValuePair<short, string>(3, "Look Up"));
                cboNarrativeFlag.DataSource = _NarrativeFlags;
                cboNarrativeFlag.ValueMember = "Key";
                cboNarrativeFlag.DisplayMember = "Value";
                cboNarrativeFlag.SelectedIndex = -1;

                var _CommissionNarrativeFlags = new BindingList<KeyValuePair<short, string>>();
                _CommissionNarrativeFlags.Add(new KeyValuePair<short, string>(1, "Flat"));
                _CommissionNarrativeFlags.Add(new KeyValuePair<short, string>(2, "Tiered"));
                _CommissionNarrativeFlags.Add(new KeyValuePair<short, string>(3, "Look Up"));
                cboCommissionNarrativeFlag.DataSource = _CommissionNarrativeFlags;
                cboCommissionNarrativeFlag.ValueMember = "Key";
                cboCommissionNarrativeFlag.DisplayMember = "Value";
                cboCommissionNarrativeFlag.SelectedIndex = -1;

                var _TxnClasses = new BindingList<KeyValuePair<short, string>>();
                _TxnClasses.Add(new KeyValuePair<short, string>(1, "Flat"));
                _TxnClasses.Add(new KeyValuePair<short, string>(2, "Tiered"));
                _TxnClasses.Add(new KeyValuePair<short, string>(3, "Look Up"));
                cboTxnClass.DataSource = _TxnClasses;
                cboTxnClass.ValueMember = "Key";
                cboTxnClass.DisplayMember = "Value";
                cboTxnClass.SelectedIndex = -1;

                var _DialogFlags = new BindingList<KeyValuePair<short, string>>();
                _DialogFlags.Add(new KeyValuePair<short, string>(1, "Flat"));
                _DialogFlags.Add(new KeyValuePair<short, string>(2, "Tiered"));
                _DialogFlags.Add(new KeyValuePair<short, string>(3, "Look Up"));
                cboDialogFlag.DataSource = _DialogFlags;
                cboDialogFlag.ValueMember = "Key";
                cboDialogFlag.DisplayMember = "Value";
                cboDialogFlag.SelectedIndex = -1;

                var _ChargeWho = new BindingList<KeyValuePair<string, string>>();
                _ChargeWho.Add(new KeyValuePair<string, string>("D", "Debit"));
                _ChargeWho.Add(new KeyValuePair<string, string>("C", "Credit"));
                cboChargeWho.DataSource = _ChargeWho;
                cboChargeWho.ValueMember = "Key";
                cboChargeWho.DisplayMember = "Value";
                cboChargeWho.SelectedIndex = -1;

                var _StatFlags = new BindingList<KeyValuePair<string, string>>();
                _StatFlags.Add(new KeyValuePair<string, string>("F", "Flat"));
                _StatFlags.Add(new KeyValuePair<string, string>("T", "Tiered"));
                _StatFlags.Add(new KeyValuePair<string, string>("L", "Look Up"));
                cboStatFlag.DataSource = _StatFlags;
                cboStatFlag.ValueMember = "Key";
                cboStatFlag.DisplayMember = "Value";
                cboStatFlag.SelectedIndex = -1;

                var _Screens = new BindingList<KeyValuePair<string, string>>();
                _Screens.Add(new KeyValuePair<string, string>("F", "Flat"));
                _Screens.Add(new KeyValuePair<string, string>("T", "Tiered"));
                _Screens.Add(new KeyValuePair<string, string>("L", "Look Up"));
                cboScreen.DataSource = _Screens;
                cboScreen.ValueMember = "Key";
                cboScreen.DisplayMember = "Value";
                cboScreen.SelectedIndex = -1;

                AutoCompleteStringCollection acscyr = new AutoCompleteStringCollection();
                acscyr.AddRange(this.AutoComplete_DrAccountIds());
                txtDefaultContraAccount.AutoCompleteCustomSource = acscyr;
                txtDefaultContraAccount.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtDefaultContraAccount.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acsctrm = new AutoCompleteStringCollection();
                acsctrm.AddRange(this.AutoComplete_CrAccountIds());
                txtDefaultMainAccount.AutoCompleteCustomSource = acsctrm;
                txtDefaultMainAccount.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtDefaultMainAccount.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acssda = new AutoCompleteStringCollection();
                acssda.AddRange(this.AutoComplete_SuspenseDrAccountIds());
                txtSuspenseDrAccount.AutoCompleteCustomSource = acssda;
                txtSuspenseDrAccount.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtSuspenseDrAccount.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscsca = new AutoCompleteStringCollection();
                acscsca.AddRange(this.AutoComplete_SuspenseCrAccountIds());
                txtSuspenseCrAccount.AutoCompleteCustomSource = acscsca;
                txtSuspenseCrAccount.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtSuspenseCrAccount.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscommdr = new AutoCompleteStringCollection();
                acscommdr.AddRange(this.AutoComplete_CommDrAccountIds());
                txtCommDebitAccount.AutoCompleteCustomSource = acscommdr;
                txtCommDebitAccount.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtCommDebitAccount.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscommcr = new AutoCompleteStringCollection();
                acscommcr.AddRange(this.AutoComplete_CommCrAccountIds());
                txtCommCreditAccount.AutoCompleteCustomSource = acscommcr;
                txtCommCreditAccount.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtCommCreditAccount.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscsshrtcd = new AutoCompleteStringCollection();
                acscsshrtcd.AddRange(this.AutoComplete_ShortCode());
                txtShortCode.AutoCompleteCustomSource = acscsshrtcd;
                txtShortCode.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtShortCode.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscdscrptn = new AutoCompleteStringCollection();
                acscdscrptn.AddRange(this.AutoComplete_Description());
                txtDescription.AutoCompleteCustomSource = acscdscrptn;
                txtDescription.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtDescription.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                InitializeControls();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private string[] AutoComplete_ShortCode()
        {
            try
            {
                var shortcodequery = from bk in tc.GetAllTransactionTypes()
                                     select bk.ShortCode;
                return shortcodequery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_Description()
        {
            try
            {
                var shortcodequery = from bk in tc.GetAllTransactionTypes()
                                     select bk.Description;
                return shortcodequery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_DrAccountIds()
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
        private string[] AutoComplete_CrAccountIds()
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
        private string[] AutoComplete_SuspenseDrAccountIds()
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
        private string[] AutoComplete_SuspenseCrAccountIds()
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
        private string[] AutoComplete_CommDrAccountIds()
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
        private string[] AutoComplete_CommCrAccountIds()
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
        private void InitializeControls()
        {

            try
            {
                #region "Txn Info"
                if (txn.ShortCode != null)
                {
                    txtShortCode.Text = txn.ShortCode.Trim();
                }
                if (txn.Description != null)
                {
                    txtDescription.Text = txn.Description.Trim();
                }
                if (txn.DebitCredit != null)
                {
                    cboDebitCredit.SelectedValue = txn.DebitCredit;
                }
                if (txn.TxnTypeView != null)
                {
                    cboTxnView.SelectedValue = txn.TxnTypeView;
                }
                if (txn.TxnClass != null)
                {
                    cboTxnClass.SelectedValue = txn.TxnClass;
                }
                if (txn.AmountExpression != null)
                {
                    txtAmountExpression.Text = txn.AmountExpression.Trim();
                }
                if (txn.DialogFlag != null)
                {
                    cboDialogFlag.SelectedValue = txn.DialogFlag;
                }
                if (txn.NarrativeFlag != null)
                {
                    cboNarrativeFlag.SelectedValue = txn.NarrativeFlag;
                }
                if (txn.ChargeWho != null)
                {
                    cboChargeWho.SelectedValue = txn.ChargeWho.Trim();
                }
                chkForcePost.Checked = txn.ForcePost;
                chkCanSuspend.Checked = txn.CanSuspend;
                if (txn.SuspenseDrAccount != null)
                {
                    txtSuspenseDrAccount.Text = txn.SuspenseDrAccount.ToString();
                }
                if (txn.SuspenseCrAccount != null)
                {
                    txtSuspenseCrAccount.Text = txn.SuspenseCrAccount.ToString();
                }
                #endregion "Txn Info"
                #region "Defaults"
                if (txn.DefaultAmount != null)
                {
                    txtDefaultAmount.Text = txn.DefaultAmount.ToString();
                }
                if (txn.DefaultMainAccount != null)
                {
                    txtDefaultMainAccount.Text = txn.DefaultMainAccount.ToString();
                }
                if (txn.DefaultContraAccount != null)
                {
                    txtDefaultContraAccount.Text = txn.DefaultContraAccount.ToString();
                }
                if (txn.DefaultMainNarrative != null)
                {
                    txtDefaultMainNarrative.Text = txn.DefaultMainNarrative.Trim();
                }
                if (txn.DefaultContraNarrative != null)
                {
                    txtDefaultContraNarrative.Text = txn.DefaultContraNarrative.Trim();
                }
                #endregion "Defaults"
                #region "Views"
                if (txn.ValueDateOffset != null)
                {
                    cboValueDateOffset.SelectedValue = txn.ValueDateOffset;
                }
                if (txn.StatFlag != null)
                {
                    cboStatFlag.SelectedValue = txn.StatFlag.Trim();
                }
                if (txn.Screen != null)
                {
                    cboScreen.SelectedValue = txn.Screen.Trim();
                }
                #endregion "Views"
                #region "Receipts"
                chkPrintReceipts.Checked = txn.PrintReceipt;
                chkPrintReceiptPrompt.Checked = txn.PrintReceiptPrompt;
                if (txn.ReceiptLayout != null)
                {
                    txtReceiptLayout.Text = txn.ReceiptLayout.Trim();
                }
                #endregion "Receipts"
                #region "Commssion"
                chkChargeCommission.Checked = txn.ChargeCommission;
                chkChargeCommissionToTransaction.Checked = txn.ChargeCommissionToTransaction;
                if (txn.CommissionDrAccount != null)
                {
                    txtCommDebitAccount.Text = txn.CommissionDrAccount.ToString();
                }
                chkCommissionDrAnotherAccount.Checked = txn.CommissionDrAnotherAccount;
                if (txn.CommissionCrAccount != null)
                {
                    txtCommCreditAccount.Text = txn.CommissionCrAccount.ToString();
                }
                if (txn.CommissionTransactionType != null)
                {
                    cboCommissionTransactionType.SelectedValue = txn.CommissionTransactionType;
                }
                if (txn.CommComputationMethod != null)
                {
                    cboCommComputationMethod.SelectedValue = txn.CommComputationMethod.Trim();
                }
                if (txn.CommissionAmountExpression != null)
                {
                    txtCommissionAmountExpression.Text = txn.CommissionAmountExpression.Trim();
                }
                if (txn.CommissionNarrativeFlag != null)
                {
                    cboCommissionNarrativeFlag.SelectedValue = txn.CommissionNarrativeFlag;
                }
                if (txn.CommissionMainNarrative != null)
                {
                    txtCommissionMainNarrative.Text = txn.CommissionMainNarrative.Trim();
                }
                if (txn.CommissionContraNarrative != null)
                {
                    txtCommissionContraNarrative.Text = txn.CommissionContraNarrative.Trim();
                }
                if (txn.DrCommCalcMethod != null)
                {
                    cboDrCommCalcMethod.SelectedValue = txn.DrCommCalcMethod.Trim();
                }
                if (txn.CrCommCalcMethod != null)
                {
                    cboCrCommCalcMethod.SelectedValue = txn.CrCommCalcMethod.Trim();
                }
                if (txn.CommComputationMethod != null)
                {
                    if (txn.CommComputationMethod.Equals("T"))
                    {
                        cboTieredTables.SelectedValue = txn.TieredTableId;
                    }
                }
                if (txn.CommComputationMethod != null)
                {
                    if (txn.CommComputationMethod.Equals("L"))
                    {
                        cboLookUpTieredTable.SelectedValue = txn.TieredTableId;
                    }
                }
                chkFlatCommAbsolute.Checked = txn.Absolute;
                if (txn.CommissionAmount != null)
                {
                    txtFlatCommAmount.Text = txn.CommissionAmount.ToString();
                }
                #endregion "Commssion"
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        /*public method to diable all controls when form is called by parent from 'View Details' button*/
        public void DisableControls()
        {

            #region "Txn Info"
            txtShortCode.Enabled = false;
            txtDescription.Enabled = false;
            cboDebitCredit.Enabled = false;
            cboTxnView.Enabled = false;
            cboTxnClass.Enabled = false;
            txtAmountExpression.Enabled = false;
            cboDialogFlag.Enabled = false;
            cboNarrativeFlag.Enabled = false;
            cboChargeWho.Enabled = false;
            chkForcePost.Enabled = false;
            chkCanSuspend.Enabled = false;
            txtSuspenseDrAccount.Enabled = false;
            txtSuspenseCrAccount.Enabled = false;
            btnSearchSuspenseDrAccount.Enabled = false;
            btnSearchSuspenseCrAccount.Enabled = false;
            #endregion "Txn Info"
            #region "Defaults"
            txtDefaultAmount.Enabled = false;
            txtDefaultMainAccount.Enabled = false;
            txtDefaultContraAccount.Enabled = false;
            txtDefaultMainNarrative.Enabled = false;
            txtDefaultContraNarrative.Enabled = false;
            btnSearchDefaultMainAccount.Enabled = false;
            btnSearchDefaultContraAccount.Enabled = false;
            #endregion "Defaults"
            #region "Views"
            cboValueDateOffset.Enabled = false;
            cboStatFlag.Enabled = false;
            cboScreen.Enabled = false;
            #endregion "Views"
            #region "Receipts"
            chkPrintReceipts.Enabled = false;
            chkPrintReceiptPrompt.Enabled = false;
            txtReceiptLayout.Enabled = false;
            txtText.Enabled = false;
            btnText.Enabled = false;
            btnAddField.Enabled = false;
            btnAddNewLine.Enabled = false;
            btnClear.Enabled = false;
            #endregion "Receipts"
            #region "Commssion"
            chkChargeCommission.Enabled = false;
            chkChargeCommissionToTransaction.Enabled = false;
            txtCommDebitAccount.Enabled = false;
            chkCommissionDrAnotherAccount.Enabled = false;
            txtCommCreditAccount.Enabled = false;
            cboCommissionTransactionType.Enabled = false;
            cboCommComputationMethod.Enabled = false;
            txtCommissionAmountExpression.Enabled = false;
            cboCommissionNarrativeFlag.Enabled = false;
            txtCommissionMainNarrative.Enabled = false;
            txtCommissionContraNarrative.Enabled = false;
            cboDrCommCalcMethod.Enabled = false;
            cboCrCommCalcMethod.Enabled = false;
            cboTieredTables.Enabled = false;
            cboLookUpTieredTable.Enabled = false;
            chkFlatCommAbsolute.Enabled = false;
            txtFlatCommAmount.Enabled = false;
            btnSearchCommDebitAccount.Enabled = false;
            btnSearchCommCreditAccount.Enabled = false;
            btnCreateTieredTable.Enabled = false;
            btnLookUpCreateTieredTable.Enabled = false;
            dataGridViewLookUpTieredTableDetails.Enabled = false;
            dataGridViewTieredTableDetails.Enabled = false;
            #endregion "Commssion"
            btnSave.Enabled = false;
            btnSave.Visible = false;
            btnClose.Location = btnSave.Location;
        }
        private void btnUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            errorProvider1.Clear();
            if (IsTransactionTypeValid())
            {
                try
                { 
                    #region "Txn Info"
                    if (!string.IsNullOrEmpty(txtShortCode.Text))
                    {
                        txn.ShortCode = Utils.ConvertFirstLetterToUpper(txtShortCode.Text.Trim().ToUpper());
                    }
                    if (!string.IsNullOrEmpty(txtDescription.Text))
                    {
                        txn.Description = Utils.ConvertFirstLetterToUpper(txtDescription.Text.Trim());
                    }
                    if (cboDebitCredit.SelectedIndex != -1)
                    {
                        txn.DebitCredit = cboDebitCredit.SelectedValue.ToString();
                    }
                    if (cboTxnView.SelectedIndex != -1)
                    {
                        txn.TxnTypeView = short.Parse(cboTxnView.SelectedValue.ToString());
                    }
                    if (cboTxnClass.SelectedIndex != -1)
                    {
                        txn.TxnClass = short.Parse(cboTxnClass.SelectedValue.ToString());
                    }
                    if (!string.IsNullOrEmpty(txtAmountExpression.Text))
                    {
                        txn.AmountExpression = txtAmountExpression.Text.Trim();
                    }
                    if (cboDialogFlag.SelectedIndex != -1)
                    {
                        txn.DialogFlag = short.Parse(cboDialogFlag.SelectedValue.ToString());
                    }
                    if (cboNarrativeFlag.SelectedIndex != -1)
                    {
                        txn.NarrativeFlag = short.Parse(cboNarrativeFlag.SelectedValue.ToString());
                    }
                    if (cboChargeWho.SelectedIndex != -1)
                    {
                        txn.ChargeWho = cboChargeWho.SelectedValue.ToString();
                    }
                    txn.ForcePost = chkForcePost.Checked;
                    txn.CanSuspend = chkCanSuspend.Checked;
                    if (!string.IsNullOrEmpty(txtSuspenseDrAccount.Text))
                    {
                        txn.SuspenseDrAccount = int.Parse(txtSuspenseDrAccount.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtSuspenseCrAccount.Text))
                    {
                        txn.SuspenseCrAccount = int.Parse(txtSuspenseCrAccount.Text.Trim());
                    }
                    #endregion "Txn Info"
                    #region "Defaults"
                    if (!string.IsNullOrEmpty(txtDefaultAmount.Text))
                    {
                        txn.DefaultAmount = decimal.Parse(txtDefaultAmount.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtDefaultMainAccount.Text))
                    {
                        txn.DefaultMainAccount = int.Parse(txtDefaultMainAccount.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtDefaultContraAccount.Text))
                    {
                        txn.DefaultContraAccount = int.Parse(txtDefaultContraAccount.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtDefaultMainNarrative.Text))
                    {
                        txn.DefaultMainNarrative = txtDefaultMainNarrative.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtDefaultContraNarrative.Text))
                    {
                        txn.DefaultContraNarrative = txtDefaultContraNarrative.Text.Trim();
                    }
                    #endregion "Defaults"
                    #region "Views"
                    if (cboValueDateOffset.SelectedIndex != -1)
                    {
                        txn.ValueDateOffset = short.Parse(cboValueDateOffset.SelectedValue.ToString());
                    }
                    if (cboStatFlag.SelectedIndex != -1)
                    {
                        txn.StatFlag = cboStatFlag.SelectedValue.ToString();
                    }
                    if (cboScreen.SelectedIndex != -1)
                    {
                        txn.Screen = cboScreen.SelectedValue.ToString();
                    }
                    #endregion "Views"
                    #region "Receipts"
                    txn.PrintReceipt = chkPrintReceipts.Checked;
                    txn.PrintReceiptPrompt = chkPrintReceipts.Checked;
                    if (!string.IsNullOrEmpty(txtReceiptLayout.Text))
                    {
                        txn.ReceiptLayout = txtReceiptLayout.Text.Trim();
                    }
                    #endregion "Receipts"
                    #region "Commssion"
                    txn.ChargeCommission = chkChargeCommission.Checked;
                    txn.ChargeCommissionToTransaction = chkChargeCommissionToTransaction.Checked;
                    if (!string.IsNullOrEmpty(txtCommDebitAccount.Text))
                    {
                        txn.CommissionDrAccount = int.Parse(txtCommDebitAccount.Text.Trim());
                    }
                    txn.CommissionDrAnotherAccount = chkCommissionDrAnotherAccount.Checked;
                    if (!string.IsNullOrEmpty(txtCommCreditAccount.Text))
                    {
                        txn.CommissionCrAccount = int.Parse(txtCommCreditAccount.Text.Trim());
                    }
                    if (cboCommissionTransactionType.SelectedIndex != -1)
                    {
                        txn.CommissionTransactionType = int.Parse(cboCommissionTransactionType.SelectedValue.ToString());
                    }
                    if (cboCommComputationMethod.SelectedIndex != -1)
                    {
                        txn.CommComputationMethod = cboCommComputationMethod.SelectedValue.ToString();
                    }
                    if (!string.IsNullOrEmpty(txtCommissionAmountExpression.Text))
                    {
                        txn.CommissionAmountExpression = txtCommissionAmountExpression.Text.Trim();
                    }
                    if (cboCommissionNarrativeFlag.SelectedIndex != -1)
                    {
                        txn.CommissionNarrativeFlag = short.Parse(cboCommissionNarrativeFlag.SelectedValue.ToString());
                    }
                    if (!string.IsNullOrEmpty(txtCommissionMainNarrative.Text))
                    {
                        txn.CommissionMainNarrative = txtCommissionMainNarrative.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtCommissionContraNarrative.Text))
                    {
                        txn.CommissionContraNarrative = txtCommissionContraNarrative.Text.Trim();
                    }
                    if (cboDrCommCalcMethod.SelectedIndex != -1)
                    {
                        txn.DrCommCalcMethod = cboDrCommCalcMethod.SelectedValue.ToString();
                    }
                    if (cboCrCommCalcMethod.SelectedIndex != -1)
                    {
                        txn.CrCommCalcMethod = cboCrCommCalcMethod.SelectedValue.ToString();
                    }
                    if (cboCommComputationMethod.SelectedIndex != -1)
                    {
                        if (cboCommComputationMethod.SelectedValue.ToString().Equals("T"))
                        {
                            txn.TieredTableId = int.Parse(cboTieredTables.SelectedValue.ToString());
                        } 
                    }
                    if (cboCommComputationMethod.SelectedIndex != -1)
                    {
                        if (cboCommComputationMethod.SelectedValue.ToString().Equals("L"))
                        {
                            txn.TieredTableId = int.Parse(cboLookUpTieredTable.SelectedValue.ToString());
                        } 
                    }
                    txn.Absolute = chkFlatCommAbsolute.Checked;
                    if (!string.IsNullOrEmpty(txtFlatCommAmount.Text))
                    {
                        txn.CommissionAmount = decimal.Parse(txtFlatCommAmount.Text.Trim());
                    }
                    #endregion "Commssion"

                    tc.UpdateTransactionType(txn);

                    TransactionTypesListForm f = (TransactionTypesListForm)this.Owner;
                    f.RefreshGrid();
                    this.Close();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        public bool IsTransactionTypeValid()
        {
            bool noerror = true;
            if (string.IsNullOrEmpty(txtShortCode.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtShortCode, "Short Code cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtDescription, "Description cannot be null!");
                return false;
            }
            if (cboDebitCredit.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cboDebitCredit, "Select Debit/Credit!");
                return false;
            }
            if (cboTxnView.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cboTxnView, "Select Transaction Type View!");
                return false;
            }
            if (cboTxnClass.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cboTxnClass, "Select Transaction Class!");
                return false;
            }
            if (cboChargeWho.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cboChargeWho, "Select Charge Who!");
                return false;
            }
            return noerror;
        }
        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
        private void btnSearchDefaultMainAccount_Click(object sender, EventArgs e)
        {
            try
            {
                SearchAccountsSimpleForm saf = new SearchAccountsSimpleForm() { Owner = this };
                saf.OnAccountListSelected += new SearchAccountsSimpleForm.AccountSelectHandler(saf_OnDefaultMainAccountListSelected);
                saf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void saf_OnDefaultMainAccountListSelected(object sender, AccountSelectEventArgs e)
        {
            SetDefaultMainAccountId(e._Account);
        }
        private void SetDefaultMainAccountId(Account _Account)
        {
            if (_Account != null)
            {
                txtDefaultMainAccount.Text = _Account.AccountID.ToString();
                lblDefaultMainAccount.Text = "Name: [ " + _Account.AccountName + " ] Book Balance: [ " + _Account.BookBalance.ToString("N2") + " ] Cleared Balance: [ " + _Account.ClearedBalance.ToString("N2") + " ] ";
            }
        }
        private void btnSearchDefaultContraAccount_Click(object sender, EventArgs e)
        {
            try
            {
                SearchAccountsSimpleForm saf = new SearchAccountsSimpleForm() { Owner = this };
                saf.OnAccountListSelected += new SearchAccountsSimpleForm.AccountSelectHandler(saf_OnDefaultContraAccountListSelected);
                saf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void saf_OnDefaultContraAccountListSelected(object sender, AccountSelectEventArgs e)
        {
            SetDefaultContraAccountId(e._Account);
        }
        private void SetDefaultContraAccountId(Account _Account)
        {
            if (_Account != null)
            {
                txtDefaultContraAccount.Text = _Account.AccountID.ToString();
                lblDefaultContraAccount.Text = "Name: [ " + _Account.AccountName + " ] Book Balance: [ " + _Account.BookBalance.ToString("N2") + " ] Cleared Balance: [ " + _Account.ClearedBalance.ToString("N2") + " ] ";
            }
        }
        private void btnAddField_Click(object sender, EventArgs e)
        {
            try
            {
                FormFieldSelector f = new FormFieldSelector();
                f.OnReceiptItemListSelected += new FormFieldSelector.ReceiptItemSelectHandler(f_OnReceiptItemListSelected);
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        void f_OnReceiptItemListSelected(object sender, ReceiptItemEventArgs e)
        {
            PrintObject.Add(e._PrintField);
            this.txtReceiptLayout.Text += " {" + e._PrintField.Id + "} ";
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.PrintObject.Clear();
            this.txtReceiptLayout.Text = string.Empty;
        }
        private void btnText_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtText.Text))
                this.txtReceiptLayout.Text += Utils.ConvertFirstLetterToUpper(txtText.Text);
        }
        private void btnAddNewLine_Click(object sender, EventArgs e)
        {
            this.txtReceiptLayout.Text += System.Environment.NewLine;

        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //txtReceiptLayout.Copy();
                Clipboard.SetDataObject(txtReceiptLayout.Text, true);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //txtReceiptLayout.Paste();
                if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
                    txtReceiptLayout.SelectedText = txtReceiptLayout.SelectedText + Clipboard.GetDataObject().GetData(DataFormats.Text).ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtDefaultAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }
        private void txtDefaultAmount_KeyDown(object sender, KeyEventArgs e)
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
        private void chkChargeCommission_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkChargeCommission.Checked)
                {
                    groupBoxComputation.Visible = true;
                }
                else
                {
                    groupBoxComputation.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void cboCommComputationMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboCommComputationMethod.SelectedIndex != -1)
                {
                    switch (cboCommComputationMethod.SelectedValue.ToString())
                    {
                        case "F":
                            groupBoxFlat.Visible = true;
                            groupBoxFlat.Dock = DockStyle.Fill;
                            groupBoxTiered.Visible = false;
                            groupBoxTiered.Dock = DockStyle.None;
                            groupBoxLookUp.Visible = false;
                            groupBoxLookUp.Dock = DockStyle.None;
                            break;
                        case "T":
                            groupBoxFlat.Visible = false;
                            groupBoxFlat.Dock = DockStyle.None;
                            groupBoxTiered.Visible = true;
                            groupBoxTiered.Dock = DockStyle.Fill;
                            groupBoxLookUp.Visible = false;
                            groupBoxLookUp.Dock = DockStyle.None;

                            LoadTieredTables();

                            break;
                        case "L":
                            groupBoxFlat.Visible = false;
                            groupBoxFlat.Dock = DockStyle.None;
                            groupBoxTiered.Visible = false;
                            groupBoxTiered.Dock = DockStyle.None;
                            groupBoxLookUp.Visible = true;
                            groupBoxLookUp.Dock = DockStyle.Fill;

                            LoadTieredTables();

                            break;
                    }
                }
                if (cboCommComputationMethod.SelectedIndex == -1)
                {

                    groupBoxFlat.Visible = false;
                    groupBoxFlat.Dock = DockStyle.None;
                    groupBoxTiered.Visible = false;
                    groupBoxTiered.Dock = DockStyle.None;
                    groupBoxLookUp.Visible = false;
                    groupBoxLookUp.Dock = DockStyle.None;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnSearchCommDebitAccount_Click(object sender, EventArgs e)
        {
            try
            {
                SearchAccountsSimpleForm saf = new SearchAccountsSimpleForm() { Owner = this };
                saf.OnAccountListSelected += new SearchAccountsSimpleForm.AccountSelectHandler(saf_OnCommDrAccountListSelected);
                saf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void saf_OnCommDrAccountListSelected(object sender, AccountSelectEventArgs e)
        {
            SetCommDrAccountId(e._Account);
        }
        private void SetCommDrAccountId(Account _Account)
        {
            if (_Account != null)
            {
                txtCommDebitAccount.Text = _Account.AccountID.ToString();
                lblCommDrAccount.Text = "Name: [ " + _Account.AccountName + " ] Book Balance: [ " + _Account.BookBalance.ToString("N2") + " ] Cleared Balance: [ " + _Account.ClearedBalance.ToString("N2") + " ] ";
            }
        }
        private void btnSearchCommCreditAccount_Click(object sender, EventArgs e)
        {
            try
            {
                SearchAccountsSimpleForm saf = new SearchAccountsSimpleForm() { Owner = this };
                saf.OnAccountListSelected += new SearchAccountsSimpleForm.AccountSelectHandler(saf_OnCommCrAccountListSelected);
                saf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void saf_OnCommCrAccountListSelected(object sender, AccountSelectEventArgs e)
        {
            SetCommCrAccountId(e._Account);
        }
        private void SetCommCrAccountId(Account _Account)
        {
            if (_Account != null)
            {
                txtCommCreditAccount.Text = _Account.AccountID.ToString();
                lblCommCrAccount.Text = "Name: [ " + _Account.AccountName + " ] Book Balance: [ " + _Account.BookBalance.ToString("N2") + " ] Cleared Balance: [ " + _Account.ClearedBalance.ToString("N2") + " ] ";
            }
        }

        private void txtCommDebitAccount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblCommDrAccount.Text = string.Empty;
                AccountsComponent ac = new AccountsComponent();
                if (!string.IsNullOrEmpty(txtCommDebitAccount.Text))
                {
                    int accountid = int.Parse(txtCommDebitAccount.Text);
                    Account _Account = ac.GetAccount(accountid);
                    if (_Account != null)
                    {
                        txtCommDebitAccount.Text = _Account.AccountID.ToString();
                        lblCommDrAccount.Text = "Name: [ " + _Account.AccountName + " ] Book Balance: [ " + _Account.BookBalance.ToString("N2") + " ] Cleared Balance: [ " + _Account.ClearedBalance.ToString("N2") + " ] ";
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void txtCommCreditAccount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblCommCrAccount.Text = string.Empty;
                AccountsComponent ac = new AccountsComponent();
                if (!string.IsNullOrEmpty(txtCommCreditAccount.Text))
                {
                    int accountid = int.Parse(txtCommCreditAccount.Text);
                    Account _Account = ac.GetAccount(accountid);
                    if (_Account != null)
                    {
                        txtCommCreditAccount.Text = _Account.AccountID.ToString();
                        lblCommCrAccount.Text = "Name: [ " + _Account.AccountName + " ] Book Balance: [ " + _Account.BookBalance.ToString("N2") + " ] Cleared Balance: [ " + _Account.ClearedBalance.ToString("N2") + " ] ";
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtCommDebitAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }
        private void txtCommDebitAccount_KeyDown(object sender, KeyEventArgs e)
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
        private void txtCommCreditAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }
        private void txtCommCreditAccount_KeyDown(object sender, KeyEventArgs e)
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

        private void btnCreateTieredTable_Click(object sender, EventArgs e)
        {
            try
            {
                AddTieredTableForm attf = new AddTieredTableForm() { Owner = this };
                attf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        public void LoadTieredTables()
        {
            try
            {
                cboTieredTables.DataSource = null;
                cboLookUpTieredTable.DataSource = null;

                List<TieredTable> TieredTables = tc.SelectTieredTables();
                cboTieredTables.DataSource = TieredTables;
                cboTieredTables.ValueMember = "Id";
                cboTieredTables.DisplayMember = "Description";
                cboTieredTables.SelectedIndex = -1;

                if (txn.CommComputationMethod.Equals("T"))
                {
                    cboTieredTables.SelectedValue = txn.TieredTableId;
                }

                List<TieredTable> lookUpTieredTables = tc.SelectTieredTables();
                cboLookUpTieredTable.DataSource = lookUpTieredTables;
                cboLookUpTieredTable.ValueMember = "Id";
                cboLookUpTieredTable.DisplayMember = "Description";
                cboLookUpTieredTable.SelectedIndex = -1;

                if (txn.CommComputationMethod.Equals("L"))
                {
                    cboLookUpTieredTable.SelectedValue = txn.TieredTableId;
                }

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void cboTieredTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bindingSourceTieredTableDetails.DataSource = null;

                if (cboTieredTables.SelectedIndex != -1)
                {
                    TieredTable tt = (TieredTable)cboTieredTables.SelectedItem;

                    List<TieredDet> _TDets = tc.SelectTableTieredDets(tt.Id);
                    bindingSourceTieredTableDetails.DataSource = _TDets;
                    dataGridViewTieredTableDetails.AutoGenerateColumns = false;
                    dataGridViewTieredTableDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridViewTieredTableDetails.DataSource = bindingSourceTieredTableDetails;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void dataGridViewTieredTableDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void cboLookUpTieredTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bindingSourceLookUpTieredTableDetails.DataSource = null;

                if (cboLookUpTieredTable.SelectedIndex != -1)
                {
                    TieredTable tt = (TieredTable)cboLookUpTieredTable.SelectedItem;

                    List<TieredDet> _TDets = tc.SelectTableTieredDets(tt.Id);
                    bindingSourceLookUpTieredTableDetails.DataSource = _TDets;
                    dataGridViewLookUpTieredTableDetails.AutoGenerateColumns = false;
                    dataGridViewLookUpTieredTableDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridViewLookUpTieredTableDetails.DataSource = bindingSourceLookUpTieredTableDetails;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void btnLookUpCreateTieredTable_Click(object sender, EventArgs e)
        {
            try
            {
                AddTieredTableForm attf = new AddTieredTableForm() { Owner = this };
                attf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void dataGridViewLookUpTieredTableDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void txtSuspenseDrAccount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblSuspenseDrAccount.Text = string.Empty;
                AccountsComponent ac = new AccountsComponent();
                if (!string.IsNullOrEmpty(txtSuspenseDrAccount.Text))
                {
                    int accountid = int.Parse(txtSuspenseDrAccount.Text);
                    Account _Account = ac.GetAccount(accountid);
                    if (_Account != null)
                    {
                        txtSuspenseDrAccount.Text = _Account.AccountID.ToString();
                        lblSuspenseDrAccount.Text = "Name: [ " + _Account.AccountName + " ] Book Balance: [ " + _Account.BookBalance.ToString("N2") + " ] Cleared Balance: [ " + _Account.ClearedBalance.ToString("N2") + " ] ";
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void txtSuspenseCrAccount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblSuspenseCrAccount.Text = string.Empty;
                AccountsComponent ac = new AccountsComponent();
                if (!string.IsNullOrEmpty(txtSuspenseCrAccount.Text))
                {
                    int accountid = int.Parse(txtSuspenseCrAccount.Text);
                    Account _Account = ac.GetAccount(accountid);
                    if (_Account != null)
                    {
                        txtSuspenseCrAccount.Text = _Account.AccountID.ToString();
                        lblSuspenseCrAccount.Text = "Name: [ " + _Account.AccountName + " ] Book Balance: [ " + _Account.BookBalance.ToString("N2") + " ] Cleared Balance: [ " + _Account.ClearedBalance.ToString("N2") + " ] ";
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void btnSearchSuspenseDrAccount_Click(object sender, EventArgs e)
        {

            try
            {
                SearchAccountsSimpleForm saf = new SearchAccountsSimpleForm() { Owner = this };
                saf.OnAccountListSelected += new SearchAccountsSimpleForm.AccountSelectHandler(saf_OnSuspenseDrAccountListSelected);
                saf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void saf_OnSuspenseDrAccountListSelected(object sender, AccountSelectEventArgs e)
        {
            SetSuspenseDrAccountId(e._Account);
        }
        private void SetSuspenseDrAccountId(Account _Account)
        {
            if (_Account != null)
            {
                txtSuspenseDrAccount.Text = _Account.AccountID.ToString();
                lblSuspenseDrAccount.Text = "Name: [ " + _Account.AccountName + " ] Book Balance: [ " + _Account.BookBalance.ToString("N2") + " ] Cleared Balance: [ " + _Account.ClearedBalance.ToString("N2") + " ] ";
            }
        }

        private void btnSearchSuspenseCrAccount_Click(object sender, EventArgs e)
        {
            try
            {
                SearchAccountsSimpleForm saf = new SearchAccountsSimpleForm() { Owner = this };
                saf.OnAccountListSelected += new SearchAccountsSimpleForm.AccountSelectHandler(saf_OnSuspenseCrAccountListSelected);
                saf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void saf_OnSuspenseCrAccountListSelected(object sender, AccountSelectEventArgs e)
        {
            SetSuspenseCrAccountId(e._Account);
        }
        private void SetSuspenseCrAccountId(Account _Account)
        {
            if (_Account != null)
            {
                txtSuspenseCrAccount.Text = _Account.AccountID.ToString();
                lblSuspenseCrAccount.Text = "Name: [ " + _Account.AccountName + " ] Book Balance: [ " + _Account.BookBalance.ToString("N2") + " ] Cleared Balance: [ " + _Account.ClearedBalance.ToString("N2") + " ] ";
            }
        }

        private void txtSuspenseDrAccount_KeyDown(object sender, KeyEventArgs e)
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

        private void txtSuspenseDrAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        private void txtSuspenseCrAccount_KeyDown(object sender, KeyEventArgs e)
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

        private void txtSuspenseCrAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        private void txtDefaultMainAccount_KeyDown(object sender, KeyEventArgs e)
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

        private void txtDefaultMainAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        private void txtDefaultContraAccount_KeyDown(object sender, KeyEventArgs e)
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

        private void txtDefaultContraAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        private void txtDefaultMainAccount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblDefaultMainAccount.Text = string.Empty;
                AccountsComponent ac = new AccountsComponent();
                if (!string.IsNullOrEmpty(txtDefaultMainAccount.Text))
                {
                    int accountid = int.Parse(txtDefaultMainAccount.Text);
                    Account _Account = ac.GetAccount(accountid);
                    if (_Account != null)
                    {
                        txtDefaultMainAccount.Text = _Account.AccountID.ToString();
                        lblDefaultMainAccount.Text = "Name: [ " + _Account.AccountName + " ] Book Balance: [ " + _Account.BookBalance.ToString("N2") + " ] Cleared Balance: [ " + _Account.ClearedBalance.ToString("N2") + " ] ";
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void txtDefaultContraAccount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblDefaultContraAccount.Text = string.Empty;
                AccountsComponent ac = new AccountsComponent();
                if (!string.IsNullOrEmpty(txtDefaultContraAccount.Text))
                {
                    int accountid = int.Parse(txtDefaultContraAccount.Text);
                    Account _Account = ac.GetAccount(accountid);
                    if (_Account != null)
                    {
                        txtDefaultContraAccount.Text = _Account.AccountID.ToString();
                        lblDefaultContraAccount.Text = "Name: [ " + _Account.AccountName + " ] Book Balance: [ " + _Account.BookBalance.ToString("N2") + " ] Cleared Balance: [ " + _Account.ClearedBalance.ToString("N2") + " ] ";
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void txtFlatCommAmount_KeyDown(object sender, KeyEventArgs e)
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

        private void txtFlatCommAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }



    }
}