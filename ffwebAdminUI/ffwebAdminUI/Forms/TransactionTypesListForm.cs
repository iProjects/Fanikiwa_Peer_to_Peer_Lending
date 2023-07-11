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
    public partial class TransactionTypesListForm : Form
    {
        TransactionsComponent sc;

        public TransactionTypesListForm()
        {
            InitializeComponent();

            sc = new TransactionsComponent();
        }

        private void btnAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddTransactionTypeForm attf = new AddTransactionTypeForm() { Owner = this };
            attf.ShowDialog();
        }


        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void TransactionTypesListForm_Load(object sender, EventArgs e)
        {
            try
            {
                var debitcredit = new BindingList<KeyValuePair<string, string>>();
                debitcredit.Add(new KeyValuePair<string, string>("D", "Debit"));
                debitcredit.Add(new KeyValuePair<string, string>("C", "Credit"));
                DataGridViewComboBoxColumn colCboxDebitCredit = new DataGridViewComboBoxColumn();
                colCboxDebitCredit.HeaderText = "Debit/Credit";
                colCboxDebitCredit.Name = "cbDebitCredit";
                colCboxDebitCredit.DataSource = debitcredit;
                // The display member is the name column in the column datasource  
                colCboxDebitCredit.DisplayMember = "Value";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxDebitCredit.DataPropertyName = "DebitCredit";
                // The value member is the primary key of the parent table  
                colCboxDebitCredit.ValueMember = "Key";
                colCboxDebitCredit.MaxDropDownItems = 10;
                colCboxDebitCredit.Width = 100;
                colCboxDebitCredit.DisplayIndex = 8;
                colCboxDebitCredit.MinimumWidth = 5;
                colCboxDebitCredit.FlatStyle = FlatStyle.Flat;
                colCboxDebitCredit.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxDebitCredit.ReadOnly = true;
                colCboxDebitCredit.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (!this.dataGridViewTransactionTypes.Columns.Contains("cbDebitCredit"))
                {
                    dataGridViewTransactionTypes.Columns.Add(colCboxDebitCredit);
                }

                dataGridViewTransactionTypes.AutoGenerateColumns = false;
                this.dataGridViewTransactionTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                bindingSourceTransactionTypes.DataSource = sc.GetAllTransactionTypes();
                dataGridViewTransactionTypes.DataSource = bindingSourceTransactionTypes;
                groupBox2.Text = bindingSourceTransactionTypes.Count.ToString();
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
                //set the datasource to null
                bindingSourceTransactionTypes.DataSource = null;
                //set the datasource to a method
                bindingSourceTransactionTypes.DataSource = sc.GetAllTransactionTypes();
                groupBox2.Text = bindingSourceTransactionTypes.Count.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }


        private void btnEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (dataGridViewTransactionTypes.SelectedRows.Count != 0)
                {
                    TransactionType txntype = (TransactionType)bindingSourceTransactionTypes.Current;
                    EditTransactionTypeForm ettf = new EditTransactionTypeForm(txntype) { Owner = this };
                    ettf.Text = txntype.Description.ToUpper().Trim();
                    ettf.ShowDialog();
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
                if (dataGridViewTransactionTypes.SelectedRows.Count != 0)
                {

                    TransactionType t = (TransactionType)bindingSourceTransactionTypes.Current;

                    if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete TransactionType\n" + t.Description.ToString().Trim().ToUpper(), "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        //tc.DeleteTransactionType(t);
                        RefreshGrid();

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
                if (dataGridViewTransactionTypes.SelectedRows.Count != 0)
                {
                    TransactionType txntype = (TransactionType)bindingSourceTransactionTypes.Current;
                    EditTransactionTypeForm ettf = new EditTransactionTypeForm(txntype) { Owner = this };
                    ettf.DisableControls();
                    ettf.Text = txntype.Description.ToUpper().Trim();
                    ettf.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void dataGridViewTransactionTypes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewTransactionTypes.SelectedRows.Count != 0)
                {
                    TransactionType txntype = (TransactionType)bindingSourceTransactionTypes.Current;
                    EditTransactionTypeForm ettf = new EditTransactionTypeForm(txntype) { Owner = this };
                    ettf.Text = txntype.Description.ToUpper().Trim();
                    ettf.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void dataGridViewTransactionTypes_DataError(object sender, DataGridViewDataErrorEventArgs e)
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