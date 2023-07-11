using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using fanikiwaMpesaGateway.fanikiwaMpesaWcfServiceProxy;

namespace fanikiwaMpesaGateway
{
    public partial class fanikiwaMpesaGateway : Form
    {
        public fanikiwaMpesaGateway()
        {
            InitializeComponent();
        }

        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnDiscoverService_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //StaticTransactionsServiceClient spostclient = new StaticTransactionsServiceClient();
            txtServiceResults.Text = "Discover Service has been hit...\n" + "http://localhost:4063/fanikiwaMpesaWcfService.svc";
        }

        private void fanikiwaMpesaGateway_Load(object sender, EventArgs e)
        {
            txtServiceUrl.Text = "http://localhost:4063/fanikiwaMpesaWcfService.svc";
        }




    }
}
