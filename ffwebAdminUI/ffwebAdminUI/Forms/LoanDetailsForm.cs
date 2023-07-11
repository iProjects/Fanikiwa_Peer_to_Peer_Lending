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
    public partial class LoanDetailsForm : Form
    {
        STO loan;

        public LoanDetailsForm(STO _loan)
        {
            InitializeComponent();
            loan = _loan;

            InitializeControls();
        }

        private void InitializeControls()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void LoanDetailsForm_Load(object sender, EventArgs e)
        {
            try
            {
                
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




    }
}
