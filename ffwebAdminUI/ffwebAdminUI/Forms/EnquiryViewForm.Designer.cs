using System;

namespace ffwebAdminUI
{
    partial class EnquiryViewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnquiryViewForm));
            this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearchAccount = new System.Windows.Forms.LinkLabel();
            this.btnSearch = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.groupBoxAccountId = new System.Windows.Forms.GroupBox();
            this.txtAccountId = new System.Windows.Forms.TextBox();
            this.lblAccountInfo = new System.Windows.Forms.Label();
            this.groupBoxTransRef = new System.Windows.Forms.GroupBox();
            this.txtTransRef = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnPrint = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.LinkLabel();
            this.groupBoxSearchCriteria = new System.Windows.Forms.GroupBox();
            this.radioButtonTransactionReference = new System.Windows.Forms.RadioButton();
            this.radioButtonAccountId = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewTransactions = new System.Windows.Forms.DataGridView();
            this.ColumnPostDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNarrative = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTransRef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.bindingSourceTransactions = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBoxAccountId.SuspendLayout();
            this.groupBoxTransRef.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBoxSearchCriteria.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePickerEndDate
            // 
            this.dateTimePickerEndDate.Location = new System.Drawing.Point(73, 37);
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            this.dateTimePickerEndDate.Size = new System.Drawing.Size(129, 20);
            this.dateTimePickerEndDate.TabIndex = 3;
            this.dateTimePickerEndDate.Value = new System.DateTime(2013, 2, 1, 0, 0, 0, 0);
            // 
            // btnSearchAccount
            // 
            this.btnSearchAccount.AutoSize = true;
            this.btnSearchAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchAccount.LinkColor = System.Drawing.Color.Yellow;
            this.btnSearchAccount.Location = new System.Drawing.Point(135, 18);
            this.btnSearchAccount.Name = "btnSearchAccount";
            this.btnSearchAccount.Size = new System.Drawing.Size(47, 13);
            this.btnSearchAccount.TabIndex = 1;
            this.btnSearchAccount.TabStop = true;
            this.btnSearchAccount.Text = "Search";
            this.btnSearchAccount.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnSearchAccount_LinkClicked);
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSize = true;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnSearch.LinkColor = System.Drawing.Color.Yellow;
            this.btnSearch.Location = new System.Drawing.Point(27, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(61, 18);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.TabStop = true;
            this.btnSearch.Text = "Search";
            this.btnSearch.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnSearch_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "End Date*";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.groupBoxAccountId);
            this.groupBox1.Controls.Add(this.groupBoxTransRef);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBoxSearchCriteria);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(868, 147);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Controls.Add(this.dateTimePickerEndDate);
            this.groupBox7.Controls.Add(this.dateTimePickerStartDate);
            this.groupBox7.Location = new System.Drawing.Point(290, 10);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(222, 65);
            this.groupBox7.TabIndex = 15;
            this.groupBox7.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Start Date*";
            // 
            // dateTimePickerStartDate
            // 
            this.dateTimePickerStartDate.Location = new System.Drawing.Point(74, 12);
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.Size = new System.Drawing.Size(128, 20);
            this.dateTimePickerStartDate.TabIndex = 2;
            this.dateTimePickerStartDate.Value = new System.DateTime(2013, 1, 1, 0, 0, 0, 0);
            // 
            // groupBoxAccountId
            // 
            this.groupBoxAccountId.Controls.Add(this.txtAccountId);
            this.groupBoxAccountId.Controls.Add(this.btnSearchAccount);
            this.groupBoxAccountId.Controls.Add(this.lblAccountInfo);
            this.groupBoxAccountId.Location = new System.Drawing.Point(12, 75);
            this.groupBoxAccountId.Name = "groupBoxAccountId";
            this.groupBoxAccountId.Size = new System.Drawing.Size(703, 60);
            this.groupBoxAccountId.TabIndex = 14;
            this.groupBoxAccountId.TabStop = false;
            this.groupBoxAccountId.Text = "Account Id*";
            // 
            // txtAccountId
            // 
            this.txtAccountId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccountId.Location = new System.Drawing.Point(13, 17);
            this.txtAccountId.MaxLength = 4;
            this.txtAccountId.Name = "txtAccountId";
            this.txtAccountId.Size = new System.Drawing.Size(90, 20);
            this.txtAccountId.TabIndex = 0;
            this.txtAccountId.TextChanged += new System.EventHandler(this.txtAccountId_TextChanged);
            this.txtAccountId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAccountId_KeyDown);
            this.txtAccountId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAccountId_KeyPress);
            // 
            // lblAccountInfo
            // 
            this.lblAccountInfo.AutoSize = true;
            this.lblAccountInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountInfo.Location = new System.Drawing.Point(13, 40);
            this.lblAccountInfo.Name = "lblAccountInfo";
            this.lblAccountInfo.Size = new System.Drawing.Size(19, 13);
            this.lblAccountInfo.TabIndex = 8;
            this.lblAccountInfo.Text = "::::";
            // 
            // groupBoxTransRef
            // 
            this.groupBoxTransRef.Controls.Add(this.txtTransRef);
            this.groupBoxTransRef.Location = new System.Drawing.Point(519, 10);
            this.groupBoxTransRef.Name = "groupBoxTransRef";
            this.groupBoxTransRef.Size = new System.Drawing.Size(196, 61);
            this.groupBoxTransRef.TabIndex = 13;
            this.groupBoxTransRef.TabStop = false;
            this.groupBoxTransRef.Text = "Transaction Reference*";
            // 
            // txtTransRef
            // 
            this.txtTransRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTransRef.Location = new System.Drawing.Point(10, 20);
            this.txtTransRef.MaxLength = 50;
            this.txtTransRef.Name = "txtTransRef";
            this.txtTransRef.Size = new System.Drawing.Size(166, 20);
            this.txtTransRef.TabIndex = 9;
            this.txtTransRef.TextChanged += new System.EventHandler(this.txtTransRef_TextChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnPrint);
            this.groupBox4.Controls.Add(this.btnClose);
            this.groupBox4.Controls.Add(this.btnSearch);
            this.groupBox4.Location = new System.Drawing.Point(721, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(121, 125);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            // 
            // btnPrint
            // 
            this.btnPrint.AutoSize = true;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnPrint.LinkColor = System.Drawing.Color.Yellow;
            this.btnPrint.Location = new System.Drawing.Point(27, 51);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(43, 18);
            this.btnPrint.TabIndex = 7;
            this.btnPrint.TabStop = true;
            this.btnPrint.Text = "Print";
            this.btnPrint.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnPrint_LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnClose.LinkColor = System.Drawing.Color.Yellow;
            this.btnClose.Location = new System.Drawing.Point(27, 82);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(52, 18);
            this.btnClose.TabIndex = 6;
            this.btnClose.TabStop = true;
            this.btnClose.Text = "Close";
            this.btnClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnClose_LinkClicked);
            // 
            // groupBoxSearchCriteria
            // 
            this.groupBoxSearchCriteria.Controls.Add(this.radioButtonTransactionReference);
            this.groupBoxSearchCriteria.Controls.Add(this.radioButtonAccountId);
            this.groupBoxSearchCriteria.Location = new System.Drawing.Point(8, 11);
            this.groupBoxSearchCriteria.Name = "groupBoxSearchCriteria";
            this.groupBoxSearchCriteria.Size = new System.Drawing.Size(271, 47);
            this.groupBoxSearchCriteria.TabIndex = 11;
            this.groupBoxSearchCriteria.TabStop = false;
            this.groupBoxSearchCriteria.Text = "Choose a Search Criteria";
            // 
            // radioButtonTransactionReference
            // 
            this.radioButtonTransactionReference.AutoSize = true;
            this.radioButtonTransactionReference.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonTransactionReference.Location = new System.Drawing.Point(121, 19);
            this.radioButtonTransactionReference.Name = "radioButtonTransactionReference";
            this.radioButtonTransactionReference.Size = new System.Drawing.Size(133, 17);
            this.radioButtonTransactionReference.TabIndex = 1;
            this.radioButtonTransactionReference.TabStop = true;
            this.radioButtonTransactionReference.Text = "Transaction Reference";
            this.radioButtonTransactionReference.UseVisualStyleBackColor = true;
            this.radioButtonTransactionReference.CheckedChanged += new System.EventHandler(this.radioButtonTransactionReference_CheckedChanged);
            // 
            // radioButtonAccountId
            // 
            this.radioButtonAccountId.AutoSize = true;
            this.radioButtonAccountId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonAccountId.Location = new System.Drawing.Point(20, 20);
            this.radioButtonAccountId.Name = "radioButtonAccountId";
            this.radioButtonAccountId.Size = new System.Drawing.Size(76, 17);
            this.radioButtonAccountId.TabIndex = 0;
            this.radioButtonAccountId.TabStop = true;
            this.radioButtonAccountId.Text = "Account Id";
            this.radioButtonAccountId.UseVisualStyleBackColor = true;
            this.radioButtonAccountId.CheckedChanged += new System.EventHandler(this.radioButtonAccountId_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewTransactions);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 147);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(868, 514);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "TRANSACTIONS    LIST";
            // 
            // dataGridViewTransactions
            // 
            this.dataGridViewTransactions.AllowUserToAddRows = false;
            this.dataGridViewTransactions.AllowUserToDeleteRows = false;
            this.dataGridViewTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnPostDate,
            this.ColumnNarrative,
            this.ColumnAmount,
            this.ColumnBalance,
            this.ColumnTransRef});
            this.dataGridViewTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTransactions.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewTransactions.MultiSelect = false;
            this.dataGridViewTransactions.Name = "dataGridViewTransactions";
            this.dataGridViewTransactions.ReadOnly = true;
            this.dataGridViewTransactions.Size = new System.Drawing.Size(862, 495);
            this.dataGridViewTransactions.TabIndex = 10;
            // 
            // ColumnPostDate
            // 
            this.ColumnPostDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnPostDate.DataPropertyName = "PostDate";
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.ColumnPostDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnPostDate.HeaderText = "Post Date";
            this.ColumnPostDate.Name = "ColumnPostDate";
            this.ColumnPostDate.ReadOnly = true;
            // 
            // ColumnNarrative
            // 
            this.ColumnNarrative.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnNarrative.DataPropertyName = "Narrative";
            this.ColumnNarrative.HeaderText = "Narrative";
            this.ColumnNarrative.Name = "ColumnNarrative";
            this.ColumnNarrative.ReadOnly = true;
            this.ColumnNarrative.Width = 150;
            // 
            // ColumnAmount
            // 
            this.ColumnAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnAmount.DataPropertyName = "Amount";
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.ColumnAmount.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnAmount.HeaderText = "Amount";
            this.ColumnAmount.Name = "ColumnAmount";
            this.ColumnAmount.ReadOnly = true;
            this.ColumnAmount.Width = 140;
            // 
            // ColumnBalance
            // 
            this.ColumnBalance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnBalance.DataPropertyName = "Balance";
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.ColumnBalance.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnBalance.HeaderText = "Balance";
            this.ColumnBalance.Name = "ColumnBalance";
            this.ColumnBalance.ReadOnly = true;
            this.ColumnBalance.Width = 140;
            // 
            // ColumnTransRef
            // 
            this.ColumnTransRef.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnTransRef.DataPropertyName = "ContraReference";
            this.ColumnTransRef.HeaderText = "Transaction Reference";
            this.ColumnTransRef.Name = "ColumnTransRef";
            this.ColumnTransRef.ReadOnly = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "TransactionID";
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn1.HeaderText = "Transaction Id";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 110;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "PostDate";
            this.dataGridViewTextBoxColumn2.HeaderText = "Post Date";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ValueDate";
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = null;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn3.HeaderText = "Value Date";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "TransactionTypeId";
            dataGridViewCellStyle6.Format = "C2";
            dataGridViewCellStyle6.NullValue = null;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn4.HeaderText = "Transaction Type Id";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Narrative";
            dataGridViewCellStyle7.Format = "C2";
            dataGridViewCellStyle7.NullValue = null;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn5.HeaderText = "Narrative";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Amount";
            this.dataGridViewTextBoxColumn6.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Balance";
            this.dataGridViewTextBoxColumn7.HeaderText = "Balance";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // EnquiryViewForm
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(868, 661);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EnquiryViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enquiry";
            this.Load += new System.EventHandler(this.EnquiryViewForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBoxAccountId.ResumeLayout(false);
            this.groupBoxAccountId.PerformLayout();
            this.groupBoxTransRef.ResumeLayout(false);
            this.groupBoxTransRef.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBoxSearchCriteria.ResumeLayout(false);
            this.groupBoxSearchCriteria.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTransactions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSourceTransactions;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
        private System.Windows.Forms.LinkLabel btnSearchAccount;
        private System.Windows.Forms.LinkLabel btnSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
        private System.Windows.Forms.LinkLabel btnClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridViewTransactions;
        private System.Windows.Forms.TextBox txtAccountId;
        private System.Windows.Forms.LinkLabel btnPrint;
        private System.Windows.Forms.Label lblAccountInfo;
        private System.Windows.Forms.TextBox txtTransRef;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBoxAccountId;
        private System.Windows.Forms.GroupBox groupBoxTransRef;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBoxSearchCriteria;
        private System.Windows.Forms.RadioButton radioButtonTransactionReference;
        private System.Windows.Forms.RadioButton radioButtonAccountId;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPostDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNarrative;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTransRef;
    }
}
