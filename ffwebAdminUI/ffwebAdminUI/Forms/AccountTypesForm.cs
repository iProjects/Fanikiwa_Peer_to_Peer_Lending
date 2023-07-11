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
using fCommon.Utility;

namespace ffwebAdminUI
{
    public partial class AccountTypesForm : Form
    {
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;
        TransactionsComponent tc;
        AccountsComponent ac;

        public AccountTypesForm()
        {
            InitializeComponent();


            tc = new TransactionsComponent();
            ac = new AccountsComponent();
        
        }

        private void btnAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddAccountTypeForm aaf = new AddAccountTypeForm() { Owner = this };
            aaf.ShowDialog();
        }
        private void btnEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (dataGridViewAccountTypes.SelectedRows.Count != 0)
                {
                    AccountType accounttype = (AccountType)bindingSourceAccountTypes.Current;
                    EditAccountTypeForm eaf = new EditAccountTypeForm(accounttype) { Owner = this };
                    eaf.Text = accounttype.Description.ToString().ToUpper();
                    eaf.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        public void RefreshGrid()
        {
            try
            {
                if (chkInActive.Checked)
                {
                    bindingSourceAccountTypes.DataSource = null;
                    var acttyps = from pc in tc.GetAllAccountTypes() 
                                  select pc;
                    bindingSourceAccountTypes.DataSource = acttyps.ToList();
                    groupBox2.Text = bindingSourceAccountTypes.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewAccountTypes.Rows)
                    {
                        dataGridViewAccountTypes.Rows[dataGridViewAccountTypes.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewAccountTypes.Rows.Count - 1;
                        bindingSourceAccountTypes.Position = nRowIndex;
                    }
                }
                else
                {
                    bindingSourceAccountTypes.DataSource = null;
                    var acttyps = from pc in tc.GetAllAccountTypes()  
                                  select pc;
                    bindingSourceAccountTypes.DataSource = acttyps.ToList();
                    groupBox2.Text = bindingSourceAccountTypes.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewAccountTypes.Rows)
                    {
                        dataGridViewAccountTypes.Rows[dataGridViewAccountTypes.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewAccountTypes.Rows.Count - 1;
                        bindingSourceAccountTypes.Position = nRowIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (dataGridViewAccountTypes.SelectedRows.Count != 0)
                {
                    AccountType accounttype = (AccountType)bindingSourceAccountTypes.Current;

                    var _accountsquery = from acc in ac.GetAllAccounts() 
                                         where acc.Closed == false
                                         where acc.AccountTypeId == accounttype.Id
                                         select acc;
                    List<Account> _accounts = _accountsquery.ToList();

                    if (_accounts.Count > 0)
                    {
                        MessageBox.Show("There is an Account  Associated with this Account Type.\n Delete the Account First!", "Fanikiwa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete Account Type\n" + accounttype.Description.ToString().Trim().ToUpper(), "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            tc.DeleteAccountTypeById(accounttype.Id);

                            RefreshGrid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void dataGridViewAccountTypes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewAccountTypes.SelectedRows.Count != 0)
                {
                    AccountType accounttype = (AccountType)bindingSourceAccountTypes.Current;
                    EditAccountTypeForm eaf = new EditAccountTypeForm(accounttype) { Owner = this };
                    eaf.Text = accounttype.Description.ToString().ToUpper();
                    eaf.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
        private void AccountTypesForm_Load(object sender, EventArgs e)
        {
            try
            {
                var status = new BindingList<KeyValuePair<string, string>>();
                status.Add(new KeyValuePair<string, string>("A", "Active"));
                status.Add(new KeyValuePair<string, string>("N", "Non-Active"));
                DataGridViewComboBoxColumn colCboxStatus = new DataGridViewComboBoxColumn();
                colCboxStatus.HeaderText = "Status";
                colCboxStatus.Name = "cbStatus";
                colCboxStatus.DataSource = status;
                // The display member is the name column in the column datasource  
                colCboxStatus.DisplayMember = "Value";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxStatus.DataPropertyName = "Status";
                // The value member is the primary key of the parent table  
                colCboxStatus.ValueMember = "Key";
                colCboxStatus.MaxDropDownItems = 10;
                colCboxStatus.Width = 80;
                colCboxStatus.DisplayIndex = 3;
                colCboxStatus.MinimumWidth = 5;
                colCboxStatus.FlatStyle = FlatStyle.Flat;
                colCboxStatus.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxStatus.ReadOnly = true;
                colCboxStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (!this.dataGridViewAccountTypes.Columns.Contains("cbStatus"))
                {
                    dataGridViewAccountTypes.Columns.Add(colCboxStatus);
                }

                var acttyps = from pc in tc.GetAllAccountTypes() 
                              select pc;
                bindingSourceAccountTypes.DataSource = acttyps.ToList();
                dataGridViewAccountTypes.AutoGenerateColumns = false;
                dataGridViewAccountTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewAccountTypes.DataSource = bindingSourceAccountTypes;
                groupBox2.Text = bindingSourceAccountTypes.Count.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void chkInActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkInActive.Checked)
                {
                    bindingSourceAccountTypes.DataSource = null;
                    var acttyps = from pc in tc.GetAllAccountTypes() 
                                  select pc;
                    bindingSourceAccountTypes.DataSource = acttyps.ToList();
                    groupBox2.Text = bindingSourceAccountTypes.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewAccountTypes.Rows)
                    {
                        dataGridViewAccountTypes.Rows[dataGridViewAccountTypes.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewAccountTypes.Rows.Count - 1;
                        bindingSourceAccountTypes.Position = nRowIndex;
                    }
                }
                else
                {
                    bindingSourceAccountTypes.DataSource = null;
                    var acttyps = from pc in tc.GetAllAccountTypes()  
                                  select pc;
                    bindingSourceAccountTypes.DataSource = acttyps.ToList();
                    groupBox2.Text = bindingSourceAccountTypes.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewAccountTypes.Rows)
                    {
                        dataGridViewAccountTypes.Rows[dataGridViewAccountTypes.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewAccountTypes.Rows.Count - 1;
                        bindingSourceAccountTypes.Position = nRowIndex;
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