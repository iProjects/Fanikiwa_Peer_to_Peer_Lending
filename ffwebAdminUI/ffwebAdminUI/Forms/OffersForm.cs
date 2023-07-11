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
    public partial class OffersForm : Form
    {
        public OffersForm()
        {
            InitializeComponent();
        }

        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnViewDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void OffersForm_Load(object sender, EventArgs e)
        {
            try
            {
                ListOffersComponent lc = new ListOffersComponent();
                RegistrationComponent rc = new RegistrationComponent();

                var _Status = new BindingList<KeyValuePair<string, string>>();
                _Status.Add(new KeyValuePair<string, string>("Open", "Open"));
                _Status.Add(new KeyValuePair<string, string>("Closed", "Closed"));
                _Status.Add(new KeyValuePair<string, string>("Processing", "Processing"));
                DataGridViewComboBoxColumn colCboxStatus = new DataGridViewComboBoxColumn();
                colCboxStatus.HeaderText = "Status";
                colCboxStatus.Name = "cbStatus";
                colCboxStatus.DataSource = _Status;
                // The display member is the name column in the column datasource  
                colCboxStatus.DisplayMember = "Value";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxStatus.DataPropertyName = "Status";
                // The value member is the primary key of the parent table  
                colCboxStatus.ValueMember = "Key";
                colCboxStatus.MaxDropDownItems = 10;
                colCboxStatus.Width = 90;
                colCboxStatus.DisplayIndex = 3;
                colCboxStatus.MinimumWidth = 5;
                colCboxStatus.FlatStyle = FlatStyle.Flat;
                colCboxStatus.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxStatus.ReadOnly = true;
                //colCboxStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (!this.dataGridViewOffers.Columns.Contains("cbStatus"))
                {
                    dataGridViewOffers.Columns.Add(colCboxStatus);
                }

                var _OfferTypes = new BindingList<KeyValuePair<string, string>>();
                _OfferTypes.Add(new KeyValuePair<string, string>("B", "Borrow"));
                _OfferTypes.Add(new KeyValuePair<string, string>("L", "Lend"));
                DataGridViewComboBoxColumn colCboxOfferType = new DataGridViewComboBoxColumn();
                colCboxOfferType.HeaderText = "Offer Type";
                colCboxOfferType.Name = "cbOfferType";
                colCboxOfferType.DataSource = _OfferTypes;
                // The display member is the name column in the column datasource  
                colCboxOfferType.DisplayMember = "Value";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxOfferType.DataPropertyName = "OfferType";
                // The value member is the primary key of the parent table  
                colCboxOfferType.ValueMember = "Key";
                colCboxOfferType.MaxDropDownItems = 10;
                colCboxOfferType.Width = 100;
                colCboxOfferType.DisplayIndex = 4;
                colCboxOfferType.MinimumWidth = 5;
                colCboxOfferType.FlatStyle = FlatStyle.Flat;
                colCboxOfferType.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxOfferType.ReadOnly = true;
                //colCboxOfferType.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (!this.dataGridViewOffers.Columns.Contains("cbOfferType"))
                {
                    dataGridViewOffers.Columns.Add(colCboxOfferType);
                }

                var _PublicOffer = new BindingList<KeyValuePair<string, string>>();
                _PublicOffer.Add(new KeyValuePair<string, string>("V", "Private"));
                _PublicOffer.Add(new KeyValuePair<string, string>("B", "Public"));
                DataGridViewComboBoxColumn colCboxPublicOffer = new DataGridViewComboBoxColumn();
                colCboxPublicOffer.HeaderText = "Public Offer";
                colCboxPublicOffer.Name = "cbPublicOffer";
                colCboxPublicOffer.DataSource = _PublicOffer;
                // The display member is the name column in the column datasource  
                colCboxPublicOffer.DisplayMember = "Value";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxPublicOffer.DataPropertyName = "PublicOffer";
                // The value member is the primary key of the parent table  
                colCboxPublicOffer.ValueMember = "Key";
                colCboxPublicOffer.MaxDropDownItems = 10;
                colCboxPublicOffer.Width = 100;
                colCboxPublicOffer.DisplayIndex = 5;
                colCboxPublicOffer.MinimumWidth = 5;
                colCboxPublicOffer.FlatStyle = FlatStyle.Flat;
                colCboxPublicOffer.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxPublicOffer.ReadOnly = true;
                //colCboxPublicOffer.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (!this.dataGridViewOffers.Columns.Contains("cbPublicOffer"))
                {
                    dataGridViewOffers.Columns.Add(colCboxPublicOffer);
                }
                List<Offer> model = lc.GetAllOffers();

                foreach (var offer in model)
                {
                    Member _offerowner = rc.GetMemberByID(offer.MemberId);
                    if (_offerowner != null)
                    {
                        //offer.OfferOwner = _offerowner.Email + " \n" + _offerowner.Surname + " \n" + _offerowner.OtherNames;
                    }
                }

                dataGridViewOffers.AutoGenerateColumns = false;
                this.dataGridViewOffers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                bindingSourceOffers.DataSource = model;
                dataGridViewOffers.DataSource = bindingSourceOffers;
                groupBox2.Text = bindingSourceOffers.Count.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void dataGridViewOffers_DataError(object sender, DataGridViewDataErrorEventArgs e)
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
