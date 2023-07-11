using System;
using System.Collections.Generic;
using System.ComponentModel; 
using System.Linq;
using System.Windows.Forms;
using fPeerLending.Entities;
using fPeerLending.Business;
using fanikiwaGL.Entities;
using fCommon.Utility;

namespace ffwebAdminUI
{
    public partial class SearchCustomersSimpleForm : Form
    {
        IQueryable<Customer> _Customer;
        //delegate
        public delegate void CustomerSelectHandler(object sender, CustomerSelectEventArgs e);
        //event
        public event CustomerSelectHandler OnCustomerListSelected;
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;
        TransactionsComponent tc;

        public SearchCustomersSimpleForm()
        {
            InitializeComponent();
            tc = new TransactionsComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (dataGridViewCustomers.SelectedRows.Count > 0)
            {
                try
                {
                    Customer selectedCustomer = (Customer)bindingSourceCustomers.Current;
                    OnCustomerListSelected(this, new CustomerSelectEventArgs(selectedCustomer));

                    this.Close();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private void SearchCustomersSimpleForm_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewCustomers.AutoGenerateColumns = false;
                this.dataGridViewCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewCustomers.DataSource = bindingSourceCustomers;

                _Customer = tc.GetAllCustomers().AsQueryable();

                AutoCompleteStringCollection acscustid = new AutoCompleteStringCollection();
                acscustid.AddRange(this.AutoComplete_CustomerIds());
                txtCustomerId.AutoCompleteCustomSource = acscustid;
                txtCustomerId.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtCustomerId.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acsccustName = new AutoCompleteStringCollection();
                acsccustName.AddRange(this.AutoComplete_CustomerNames());
                txtCustomerName.AutoCompleteCustomSource = acsccustName;
                txtCustomerName.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtCustomerName.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private string[] AutoComplete_CustomerIds()
        {
            try
            {
                var custidsquery = (from cs in tc.GetAllCustomers()
                                    select cs.CustomerId).Distinct();
                int[] intarray = custidsquery.ToArray();
                List<string> items = new List<string>();
                for (int i = 0; i < custidsquery.Count(); i++)
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
        private string[] AutoComplete_CustomerNames()
        {
            try
            {
                var custnamequery = from cs in tc.GetAllCustomers()
                                    select cs.Name;
                return custnamequery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        // apply the filter
        private void ApplyFilter()
        {
            try
            {
                var filter = CreateFilter(_Customer);
                // set the filter
                this.bindingSourceCustomers.DataSource = filter;
                groupBox2.Text = bindingSourceCustomers.Count.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private IQueryable<Customer> CreateFilter(IQueryable<Customer> _customer)
        {
            //none
            if (string.IsNullOrEmpty(txtCustomerId.Text)
                 && string.IsNullOrEmpty(txtCustomerName.Text))
            {
                return _customer;
            }
            //all
            if (!string.IsNullOrEmpty(txtCustomerId.Text)
                 && !string.IsNullOrEmpty(txtCustomerName.Text))
            {
                int _CustomerId = int.Parse(txtCustomerId.Text);
                string _CustomerName = txtCustomerName.Text;
                _customer = (from cs in tc.GetAllCustomers()
                             where cs.CustomerId == _CustomerId
                             where cs.Name.StartsWith(_CustomerName)
                             select cs).AsQueryable();
                return _customer;
            }
            //customerid
            if (!string.IsNullOrEmpty(txtCustomerId.Text) && string.IsNullOrEmpty(txtCustomerName.Text))
            {
                int _CustomerId = int.Parse(txtCustomerId.Text);
                _customer = (from cs in tc.GetAllCustomers()
                             where cs.CustomerId == _CustomerId
                             select cs).AsQueryable();
                return _customer;
            }
            //customername
            if (!string.IsNullOrEmpty(txtCustomerName.Text))
            {
                string _CustomerName = txtCustomerName.Text;
                _customer = (from cs in tc.GetAllCustomers()
                             where cs.Name.StartsWith(_CustomerName)
                             select cs).AsQueryable();
                return _customer;
            }
            return _customer;
        }
        private void txtCustomerName_Validated(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void txtCustomerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ApplyFilter();
                e.Handled = true;
            }
        } 
        private void txtCustomerId_Validated(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void txtCustomerId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ApplyFilter();
                e.Handled = true;
            }
        }
        private void txtCustomerId_KeyDown(object sender, KeyEventArgs e)
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
        private void dataGridViewCustomers_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewCustomers.SelectedRows.Count != 0)
            {
                try
                {
                    Customer selectedCustomer = (Customer)bindingSourceCustomers.Current;
                    OnCustomerListSelected(this, new CustomerSelectEventArgs(selectedCustomer));

                    this.Close();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private void dataGridViewCustomers_DataError(object sender, DataGridViewDataErrorEventArgs e)
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