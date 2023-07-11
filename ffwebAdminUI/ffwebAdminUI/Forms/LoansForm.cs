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
    public partial class LoansForm : Form
    {
        public LoansForm()
        {
            InitializeComponent();
        }

        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnViewDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                STO loan = (STO)bindingSourceLoans.Current;
                LoanDetailsForm ldf = new LoanDetailsForm(loan);
                ldf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void LoansForm_Load(object sender, EventArgs e)
        {
            try
            {
                AccountsComponent ac = new AccountsComponent();

                var _draccountsquery = from acc in ac.GetAllAccounts()
                                       select acc;
                List<Account> _DRAccounts = _draccountsquery.ToList();
                DataGridViewComboBoxColumn colCboxDrAccount = new DataGridViewComboBoxColumn();
                colCboxDrAccount.HeaderText = "Debit Account";
                colCboxDrAccount.Name = "cbDRAccount";
                colCboxDrAccount.DataSource = _DRAccounts;
                // The display member is the name column in the column datasource  
                colCboxDrAccount.DisplayMember = "AccountName";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxDrAccount.DataPropertyName = "DrAccount";
                // The value member is the primary key of the parent table  
                colCboxDrAccount.ValueMember = "AccountID";
                colCboxDrAccount.MaxDropDownItems = 10;
                colCboxDrAccount.Width = 100;
                colCboxDrAccount.DisplayIndex = 3;
                colCboxDrAccount.MinimumWidth = 5;
                colCboxDrAccount.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colCboxDrAccount.FlatStyle = FlatStyle.Flat;
                colCboxDrAccount.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxDrAccount.ReadOnly = true;
                if (!this.dataGridViewLoans.Columns.Contains("cbDRAccount"))
                {
                    dataGridViewLoans.Columns.Add(colCboxDrAccount);
                }

                var _craccountsquery = from acc in ac.GetAllAccounts()
                                       select acc;
                List<Account> _CRAccounts = _craccountsquery.ToList();
                DataGridViewComboBoxColumn colCboxCrAccount = new DataGridViewComboBoxColumn();
                colCboxCrAccount.HeaderText = "Credit Account";
                colCboxCrAccount.Name = "cbCRAccount";
                colCboxCrAccount.DataSource = _CRAccounts;
                // The display member is the name column in the column datasource  
                colCboxCrAccount.DisplayMember = "AccountName";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxCrAccount.DataPropertyName = "CrAccount";
                // The value member is the primary key of the parent table  
                colCboxCrAccount.ValueMember = "AccountID";
                colCboxCrAccount.MaxDropDownItems = 10;
                colCboxCrAccount.Width = 100;
                colCboxCrAccount.DisplayIndex = 4;
                colCboxCrAccount.MinimumWidth = 5;
                colCboxCrAccount.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colCboxCrAccount.FlatStyle = FlatStyle.Flat;
                colCboxCrAccount.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxCrAccount.ReadOnly = true;
                if (!this.dataGridViewLoans.Columns.Contains("cbCRAccount"))
                {
                    dataGridViewLoans.Columns.Add(colCboxCrAccount);
                }

               LoansComponent lc=new LoansComponent();

               List<STO> model = lc.ListLoansByAdmin();

                dataGridViewLoans.AutoGenerateColumns = false;
                this.dataGridViewLoans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                bindingSourceLoans.DataSource = model;
                dataGridViewLoans.DataSource = bindingSourceLoans;
                groupBox2.Text = bindingSourceLoans.Count.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void dataGridViewLoans_DataError(object sender, DataGridViewDataErrorEventArgs e)
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
