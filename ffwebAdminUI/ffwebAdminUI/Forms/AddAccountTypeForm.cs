﻿using fCommon.Utility;
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
    public partial class AddAccountTypeForm : Form
    {
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;
        TransactionsComponent tc;
        AccountsComponent ac;

        public AddAccountTypeForm()
        {
            InitializeComponent();

            tc = new TransactionsComponent();
            ac = new AccountsComponent();
        
        }
        private void AddAccountTypeForm_Load(object sender, EventArgs e)
        {
            try
            {
                var status = new BindingList<KeyValuePair<string, string>>();
                status.Add(new KeyValuePair<string, string>("A", "Active"));
                status.Add(new KeyValuePair<string, string>("N", "Non-Active"));
                cboStatus.DataSource = status;
                cboStatus.ValueMember = "Key";
                cboStatus.DisplayMember = "Value";

                AutoCompleteStringCollection acscsshrtcd = new AutoCompleteStringCollection();
                acscsshrtcd.AddRange(this.AutoComplete_ShortCode());
                txtShortCode.AutoCompleteCustomSource = acscsshrtcd;
                txtShortCode.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtShortCode.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

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
        private string[] AutoComplete_ShortCode()
        {
            try
            {
                var shortcodequery = from bk in tc.GetAllAccountTypes()  
                                     select bk.ShortCode;
                return shortcodequery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_Description()
        {
            try
            {
                var shortcodequery = from bk in tc.GetAllAccountTypes() 
                                     select bk.Description;
                return shortcodequery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
        private void btnAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            errorProvider1.Clear();
            if (Is_AccountTypeValid())
            {
                try
                {
                    AccountType _AccountType = new AccountType();
                    if (!string.IsNullOrEmpty(txtDescription.Text))
                    {
                        _AccountType.ShortCode = Utils.ConvertFirstLetterToUpper(txtShortCode.Text);
                    }
                    if (!string.IsNullOrEmpty(txtDescription.Text))
                    {
                        _AccountType.Description = Utils.ConvertFirstLetterToUpper(txtDescription.Text);
                    } 

                    if (tc.GetAllAccountTypes().Any(i => i.ShortCode == _AccountType.ShortCode))
                    {
                        MessageBox.Show("Short Code " + _AccountType.ShortCode + " Exists!", "Fanikiwa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!tc.GetAllAccountTypes().Any(i => i.ShortCode == _AccountType.ShortCode))
                    {
                        tc.CreateAccountType(_AccountType); 

                        AccountTypesForm ctf = (AccountTypesForm)this.Owner;
                        ctf.RefreshGrid();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private bool Is_AccountTypeValid()
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
            if (cboStatus.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cboStatus, "Select Status!");
                return false;
            }
            return noerror;
        }



    }
}