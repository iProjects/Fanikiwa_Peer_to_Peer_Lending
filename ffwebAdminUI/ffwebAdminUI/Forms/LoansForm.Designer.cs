namespace ffwebAdminUI
{
    partial class LoansForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewLoans = new System.Windows.Forms.DataGridView();
            this.ColumnAmountPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPayAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTotalToPay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNextPayDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMaturityDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNoOfPaymentsMade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnViewDetails = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.LinkLabel();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSourceLoans = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLoans)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceLoans)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewLoans);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1081, 615);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // dataGridViewLoans
            // 
            this.dataGridViewLoans.AllowUserToAddRows = false;
            this.dataGridViewLoans.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewLoans.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewLoans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLoans.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnAmountPaid,
            this.ColumnPayAmount,
            this.ColumnTotalToPay,
            this.ColumnStartDate,
            this.ColumnNextPayDate,
            this.ColumnMaturityDate,
            this.ColumnNoOfPaymentsMade});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewLoans.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewLoans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewLoans.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewLoans.MultiSelect = false;
            this.dataGridViewLoans.Name = "dataGridViewLoans";
            this.dataGridViewLoans.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewLoans.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewLoans.Size = new System.Drawing.Size(1075, 596);
            this.dataGridViewLoans.TabIndex = 0;
            this.dataGridViewLoans.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewLoans_DataError);
            // 
            // ColumnAmountPaid
            // 
            this.ColumnAmountPaid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnAmountPaid.DataPropertyName = "AmountPaid";
            this.ColumnAmountPaid.HeaderText = "Amount Paid";
            this.ColumnAmountPaid.Name = "ColumnAmountPaid";
            this.ColumnAmountPaid.ReadOnly = true;
            // 
            // ColumnPayAmount
            // 
            this.ColumnPayAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnPayAmount.DataPropertyName = "PayAmount";
            this.ColumnPayAmount.HeaderText = "Pay Amount";
            this.ColumnPayAmount.Name = "ColumnPayAmount";
            this.ColumnPayAmount.ReadOnly = true;
            // 
            // ColumnTotalToPay
            // 
            this.ColumnTotalToPay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnTotalToPay.DataPropertyName = "TotalToPay";
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.ColumnTotalToPay.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnTotalToPay.HeaderText = "Total To Pay";
            this.ColumnTotalToPay.Name = "ColumnTotalToPay";
            this.ColumnTotalToPay.ReadOnly = true;
            // 
            // ColumnStartDate
            // 
            this.ColumnStartDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnStartDate.DataPropertyName = "StartDate";
            this.ColumnStartDate.HeaderText = "Start Date";
            this.ColumnStartDate.Name = "ColumnStartDate";
            this.ColumnStartDate.ReadOnly = true;
            // 
            // ColumnNextPayDate
            // 
            this.ColumnNextPayDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnNextPayDate.DataPropertyName = "NextPayDate";
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.ColumnNextPayDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnNextPayDate.HeaderText = "Next Pay Date";
            this.ColumnNextPayDate.Name = "ColumnNextPayDate";
            this.ColumnNextPayDate.ReadOnly = true;
            // 
            // ColumnMaturityDate
            // 
            this.ColumnMaturityDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnMaturityDate.DataPropertyName = "MaturityDate";
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.ColumnMaturityDate.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnMaturityDate.HeaderText = "Maturity Date";
            this.ColumnMaturityDate.Name = "ColumnMaturityDate";
            this.ColumnMaturityDate.ReadOnly = true;
            // 
            // ColumnNoOfPaymentsMade
            // 
            this.ColumnNoOfPaymentsMade.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnNoOfPaymentsMade.DataPropertyName = "NoOfPaymentsMade";
            this.ColumnNoOfPaymentsMade.HeaderText = "No Of Payments Made";
            this.ColumnNoOfPaymentsMade.Name = "ColumnNoOfPaymentsMade";
            this.ColumnNoOfPaymentsMade.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnViewDetails);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 615);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1081, 34);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.AutoSize = true;
            this.btnViewDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnViewDetails.LinkColor = System.Drawing.Color.Yellow;
            this.btnViewDetails.Location = new System.Drawing.Point(882, 10);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(100, 18);
            this.btnViewDetails.TabIndex = 0;
            this.btnViewDetails.TabStop = true;
            this.btnViewDetails.Text = "View Details";
            this.btnViewDetails.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnViewDetails_LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnClose.LinkColor = System.Drawing.Color.Yellow;
            this.btnClose.Location = new System.Drawing.Point(986, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(52, 18);
            this.btnClose.TabIndex = 1;
            this.btnClose.TabStop = true;
            this.btnClose.Text = "Close";
            this.btnClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnClose_LinkClicked);
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "FlatRate";
            this.dataGridViewTextBoxColumn6.HeaderText = "Flat Rate";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ShortCode";
            this.dataGridViewTextBoxColumn1.HeaderText = "Short Code";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 20;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn2.HeaderText = "Description";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 200;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "DefaultAmount";
            this.dataGridViewTextBoxColumn3.HeaderText = "Default Amount";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "DefaultCreditAccount";
            this.dataGridViewTextBoxColumn4.HeaderText = "Credit Account";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "DefaultDebitAccount";
            this.dataGridViewTextBoxColumn5.HeaderText = "Debit Account";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "PercentageRate";
            this.dataGridViewTextBoxColumn7.HeaderText = "Percentage Rate";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // LoansForm
            // 
            this.AcceptButton = this.btnViewDetails;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1081, 649);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "LoansForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fanikiwa Loans";
            this.Load += new System.EventHandler(this.LoansForm_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLoans)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceLoans)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridViewLoans;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel btnViewDetails;
        private System.Windows.Forms.LinkLabel btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.BindingSource bindingSourceLoans;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAmountPaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPayAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTotalToPay;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNextPayDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMaturityDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNoOfPaymentsMade;
    }
}