using fCommon.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using fPeerLending.Entities;
using fPeerLending.Business;
using fanikiwaGL.Entities;

namespace ffwebAdminUI
{
    public partial class PostScreen : Form
    {
        // Create constants to use in the form.
        private const int controlWidth = 300;
        private const int charPerLine = 30;
        private const int lineHeight = 19;
        // Create class variables to use during the form.
        private const int controlCount = 0;
        private Point controlLocation = new Point(10, 50); 
        TransactionType ttype;
        TransactionsComponent sc;
        IQueryable<TransactionType> AuthorizedTransactionTypes;

        public PostScreen()
        {
            InitializeComponent();

            sc = new TransactionsComponent();
        }
        private void PostScreen_Load(object sender, EventArgs e)
        {
            try
            {   
                AuthorizedTransactionTypes = sc.GetAllTransactionTypes().AsQueryable();
                 
                dataGridViewTransactionTypes.AutoGenerateColumns = false;
                this.dataGridViewTransactionTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewTransactionTypes.DataSource = bindingSourceTransactionTypes;
                groupBox2.Text = bindingSourceTransactionTypes.Count.ToString();

                AutoCompleteStringCollection acstransref = new AutoCompleteStringCollection();
                acstransref.AddRange(this.AutoComplete_ShortCode());
                txtTransactionType.AutoCompleteCustomSource = acstransref;
                txtTransactionType.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtTransactionType.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;
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
                var transtypequery = from ts in sc.GetAllTransactionTypes()
                                    orderby ts.TransactionTypeID ascending
                                    select ts.ShortCode;
                return transtypequery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewTransactionTypes.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Select Transaction Type!", "Fanikiwa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (dataGridViewTransactionTypes.SelectedRows.Count != 0)
                {
                    ttype = (TransactionType)bindingSourceTransactionTypes.Current;

                    if (ttype == null)
                        throw new ArgumentNullException("Transaction Type cannot be null!");

                    //show the screen according to the transaction type

                    if (ttype.TxnTypeView == 1) //single entry post
                    {
                        Control usercontrol = new UserControlSinglePost(ttype);//_User control for single posting
                        usercontrol.Dock = DockStyle.Fill;
                        panelControls.Controls.Clear();
                        panelControls.Controls.Add(usercontrol);
                    }
                    else if (ttype.TxnTypeView == 2)//Double entry post
                    {
                        Control usercontrol = new UserControlDoublePost(ttype);//_User control for double posting
                        usercontrol.Dock = DockStyle.Fill;
                        panelControls.Controls.Clear();
                        panelControls.Controls.Add(usercontrol);
                    }
                    else if (ttype.TxnTypeView == 3)//Multi entry post
                    {
                        Control usercontrol = new UserControlMultiPost(ttype);//_User control for multiple posting
                        usercontrol.Dock = DockStyle.Fill;
                        panelControls.Controls.Clear();
                        panelControls.Controls.Add(usercontrol);
                    }
                    else
                    {
                        throw new Exception("Transaction view unknown " + ttype.TxnTypeView);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridViewTransactionTypes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnRetrieve_Click(sender, null);
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

        private void txtTransactionType_TextChanged(object sender, EventArgs e)
        {
            bindingSourceTransactionTypes.DataSource = null;
            if(!string.IsNullOrEmpty(txtTransactionType.Text))
            {
                string txntype=txtTransactionType.Text.Trim().ToUpper();

                var txntypesquery = from tx in sc.GetAllTransactionTypes()
                               where tx.ShortCode.StartsWith(txntype)
                               select tx;

                List<TransactionType> transactiontypes = txntypesquery.ToList();
                bindingSourceTransactionTypes.DataSource = transactiontypes;
                groupBox2.Text = bindingSourceTransactionTypes.Count.ToString();
            }
        }

        






    }
}