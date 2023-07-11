using fCommon.Utility;
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

namespace ffwebAdminUI
{
    public partial class EditCOAForm : Form
    { 
        int _Parent;
        COA _COA;
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;
        TransactionsComponent tc;
        AccountsComponent ac;

        public EditCOAForm(int Parent, COA coa)
        {
            InitializeComponent();

            tc = new TransactionsComponent();
            ac = new AccountsComponent();
        
            if (coa == null)
                throw new ArgumentNullException("COA");
            _COA = coa;

            if (Parent == null)
                throw new ArgumentNullException("Parent");
            _Parent = Parent;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();            
            if (IsCOAValid())
            {
                try
                { 
                    if (!string.IsNullOrEmpty(txtShortCode.Text))
                    {
                        _COA.ShortCode = Utils.ConvertFirstLetterToUpper(txtShortCode.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtDescription.Text))
                    {
                        _COA.Description = Utils.ConvertFirstLetterToUpper(txtDescription.Text.Trim());
                    } 
                    int _COALevel;
                    if (!string.IsNullOrEmpty(txtCOALevel.Text) && int.TryParse(txtCOALevel.Text, out _COALevel))
                    {
                        _COA.COALevel = int.Parse(txtCOALevel.Text);
                    } 
                    int _Rorder;
                    if (!string.IsNullOrEmpty(txtRorder.Text) && int.TryParse(txtRorder.Text, out _Rorder))
                    {
                        _COA.Rorder = int.Parse(txtRorder.Text);
                    }

                    tc.UpdateCOA(_COA);

                    COAForm f = (COAForm)this.Owner;
                    f.BuildTree();
                    this.Close();
                }
                catch (Exception ex)
                {

                    Utils.ShowError(ex);

                }
            }
        } 
        private bool IsCOAValid()
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
            if (string.IsNullOrEmpty(txtCOALevel.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtCOALevel, "COA Level cannot be null!");
                return false;
            }
            int coalevel;
            if (!int.TryParse(txtCOALevel.Text, out coalevel))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtCOALevel, "COA Level must be integer!");
                return false;
            }
            if (string.IsNullOrEmpty(txtRorder.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtRorder, "Rorder cannot be null!");
                return false;
            }
            int rorder;
            if (!int.TryParse(txtRorder.Text, out rorder))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtRorder, "Rorder must be integer!");
                return false;
            }
            return noerror;
        } 
        public void DisableControls()
        {
            try
            {
                txtDescription.Enabled = false;
                txtShortCode.Enabled = false;
                txtCOALevel.Enabled = false;
                txtRorder.Enabled = false;
                btnUpdate.Enabled = false;
                btnUpdate.Visible = false;
                btnUpdate.Enabled = false;
                btnUpdate.Visible = false;
                btnClose.Location = btnUpdate.Location;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);

            }
        }
        private void InitializeControls()
        {
            try
            {
                if (_COA.ShortCode != null)
                {
                    txtShortCode.Text = _COA.ShortCode.Trim();
                }
                if (_COA.Description != null)
                {
                    txtDescription.Text = _COA.Description.Trim();
                } 
                if (_COA.COALevel != null)
                {
                    txtCOALevel.Text = _COA.COALevel.ToString();
                }
                if (_COA.Rorder != null)
                {
                    txtRorder.Text = _COA.Rorder.ToString();
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
        private void txtCOALevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        private void txtCOALevel_KeyDown(object sender, KeyEventArgs e)
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

        private void txtRorder_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        private void txtRorder_KeyDown(object sender, KeyEventArgs e)
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

        private void EditCOAForm_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeControls();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);

            }
        }
    }
}
