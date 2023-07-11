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
    public partial class AddTieredTableForm : Form
    {
        TieredTable _TieredTable;
        TransactionsComponent tc;

        public AddTieredTableForm()
        {
            InitializeComponent();

            tc = new TransactionsComponent();
            _TieredTable = new TieredTable();
        }

        private void btnCreateTieredTable_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            { 
                if (string.IsNullOrEmpty(txtDescription.Text))
                {
                    MessageBox.Show("Description cannot be null!", "Fanikiwa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (!string.IsNullOrEmpty(txtDescription.Text))
                {
                    TieredTable tt = new TieredTable();
                    tt.Description = Utils.ConvertFirstLetterToUpper(txtDescription.Text.Trim());

                    _TieredTable = tc.CreateTieredTable(tt);

                    groupBoxTieredDet.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void btnAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            { 
                List<TieredDet> _TableDetails = (List<TieredDet>)bindingSourceTieredTableDetails.List;
                foreach(var _tdet in _TableDetails)
                {
                    TieredDet Tdet=new TieredDet();
                    Tdet.TieredID = _tdet.TieredID;
                    Tdet.Min = _tdet.Min;
                    Tdet.Max = _tdet.Max;
                    Tdet.Rate = _tdet.Rate;
                    Tdet.Absolute = _tdet.Absolute;

                    tc.CreateTieredDet(Tdet);

                    if (this.Owner is AddTransactionTypeForm)
                    {
                        AddTransactionTypeForm f = (AddTransactionTypeForm)this.Owner;
                        f.LoadTieredTables();
                        this.Close();
                    }
                    else if (this.Owner is EditTransactionTypeForm)
                    {
                        EditTransactionTypeForm aet = (EditTransactionTypeForm)this.Owner;
                        this.Close();
                    }
                }
                this.Close();
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

        private void AddTieredTableForm_Load(object sender, EventArgs e)
        {
            try
            {
                groupBoxTieredDet.Visible = false;
                List<TieredDet> _TDets = new List<TieredDet>();
                bindingSourceTieredTableDetails.DataSource = _TDets;
                dataGridViewTieredTableDetails.AutoGenerateColumns = false; 
                dataGridViewTieredTableDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect; 
                dataGridViewTieredTableDetails.DataSource = bindingSourceTieredTableDetails;

                AutoCompleteStringCollection acscdscrptn = new AutoCompleteStringCollection();
                acscdscrptn.AddRange(this.AutoComplete_Description());
                txtDescription.AutoCompleteCustomSource = acscdscrptn;
                txtDescription.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtDescription.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private string[] AutoComplete_Description()
        {
            try
            {
                var descriptionquery = from bk in tc.SelectTieredTables()
                                     select bk.Description;
                return descriptionquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
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
        private void dataGridViewTieredTableDetails_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells["ColumnTieredID"].Value = _TieredTable.Id;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void dataGridViewTieredTableDetails_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewColumn column = dataGridViewTieredTableDetails.Columns[e.ColumnIndex];

            if (column.Name == "ColumnMin")
            {
                CheckColumnMinValue(e);
            }
            if (column.Name == "ColumnMax")
            {
                CheckColumnMaxValue(e);
            }
            if (column.Name == "ColumnRate")
            {
                CheckColumnRateValue(e);
            }
        }
        private void CheckColumnMinValue(DataGridViewCellValidatingEventArgs newValue)
        {

            Int32 ignored = new Int32();
            if (String.IsNullOrEmpty(newValue.FormattedValue.ToString()))
            {
                NotifyUserAndForceRedo("Please enter Min", newValue);
            }
            else if (!Int32.TryParse(newValue.FormattedValue.ToString(), out ignored))
            {
                NotifyUserAndForceRedo("Min must be an Integer", newValue);
            }
        }
        private void CheckColumnMaxValue(DataGridViewCellValidatingEventArgs newValue)
        {

            Int32 ignored = new Int32();
            if (String.IsNullOrEmpty(newValue.FormattedValue.ToString()))
            {
                NotifyUserAndForceRedo("Please enter Max", newValue);
            }
            else if (!Int32.TryParse(newValue.FormattedValue.ToString(), out ignored))
            {
                NotifyUserAndForceRedo("Max must be an Integer", newValue);
            }
        }
        private void CheckColumnRateValue(DataGridViewCellValidatingEventArgs newValue)
        {

            Int32 ignored = new Int32();
            if (String.IsNullOrEmpty(newValue.FormattedValue.ToString()))
            {
                NotifyUserAndForceRedo("Please enter Rate", newValue);
            }
            else if (!Int32.TryParse(newValue.FormattedValue.ToString(), out ignored))
            {
                NotifyUserAndForceRedo("Rate must be an Integer", newValue);
            }
        }
        private void NotifyUserAndForceRedo(string errorMessage, DataGridViewCellValidatingEventArgs newValue)
        {
            MessageBox.Show(errorMessage);
            newValue.Cancel = true;
        }



    }
}
