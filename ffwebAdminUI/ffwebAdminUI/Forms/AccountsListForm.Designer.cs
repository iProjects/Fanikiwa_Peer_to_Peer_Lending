namespace ffwebAdminUI
{
    partial class AccountsListForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAccountId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtAccountName = new System.Windows.Forms.TextBox();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkClosed = new System.Windows.Forms.CheckBox();
            this.btnEdit = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.LinkLabel();
            this.btnAdd = new System.Windows.Forms.LinkLabel();
            this.btnCloseAccount = new System.Windows.Forms.LinkLabel();
            this.bindingSourceAccounts = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewAccounts = new System.Windows.Forms.DataGridView();
            this.classnamecolumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnClearedBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBookBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnClosed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceAccounts)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccounts)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "AccountName";
            this.dataGridViewTextBoxColumn2.FillWeight = 150F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Name";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 150;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "AccountNo";
            this.dataGridViewTextBoxColumn3.HeaderText = "Account No";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "AccountType";
            dataGridViewCellStyle1.Format = "C2";
            dataGridViewCellStyle1.NullValue = null;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn4.HeaderText = "Type";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Id:";
            // 
            // txtAccountId
            // 
            this.txtAccountId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccountId.Location = new System.Drawing.Point(97, 19);
            this.txtAccountId.MaxLength = 4;
            this.txtAccountId.Name = "txtAccountId";
            this.txtAccountId.Size = new System.Drawing.Size(67, 20);
            this.txtAccountId.TabIndex = 0;
            this.txtAccountId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAccountId_KeyPress);
            this.txtAccountId.Validated += new System.EventHandler(this.txtAccountId_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(267, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Name:";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "BookBalance";
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn5.HeaderText = "Book Balance";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "ClearedBalance";
            this.dataGridViewTextBoxColumn6.FillWeight = 150F;
            this.dataGridViewTextBoxColumn6.HeaderText = "Cleared Balance";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 150;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 150;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Branch";
            this.dataGridViewTextBoxColumn8.HeaderText = "Branch";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // txtAccountName
            // 
            this.txtAccountName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccountName.Location = new System.Drawing.Point(307, 19);
            this.txtAccountName.MaxLength = 200;
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(223, 20);
            this.txtAccountName.TabIndex = 2;
            this.txtAccountName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAccountName_KeyPress);
            this.txtAccountName.Validated += new System.EventHandler(this.txtAccountName_Validated);
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Branch";
            this.dataGridViewTextBoxColumn7.FillWeight = 150F;
            this.dataGridViewTextBoxColumn7.HeaderText = "Branch";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtAccountId);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtAccountName);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(889, 54);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // chkClosed
            // 
            this.chkClosed.AutoSize = true;
            this.chkClosed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkClosed.Location = new System.Drawing.Point(15, 16);
            this.chkClosed.Name = "chkClosed";
            this.chkClosed.Size = new System.Drawing.Size(55, 17);
            this.chkClosed.TabIndex = 36;
            this.chkClosed.Text = "Closed";
            this.chkClosed.UseVisualStyleBackColor = true;
            this.chkClosed.CheckedChanged += new System.EventHandler(this.chkClosed_CheckedChanged);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = true;
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnEdit.LinkColor = System.Drawing.Color.Yellow;
            this.btnEdit.Location = new System.Drawing.Point(660, 11);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(37, 18);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.TabStop = true;
            this.btnEdit.Text = "Edit";
            this.btnEdit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnEdit_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkClosed);
            this.groupBox1.Controls.Add(this.btnEdit);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnCloseAccount);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 557);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(889, 41);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnClose.LinkColor = System.Drawing.Color.Yellow;
            this.btnClose.Location = new System.Drawing.Point(819, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(52, 18);
            this.btnClose.TabIndex = 3;
            this.btnClose.TabStop = true;
            this.btnClose.Text = "Close";
            this.btnClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnClose_LinkClicked);
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = true;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnAdd.LinkColor = System.Drawing.Color.Yellow;
            this.btnAdd.Location = new System.Drawing.Point(622, 11);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 18);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.TabStop = true;
            this.btnAdd.Text = "Add";
            this.btnAdd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnAdd_LinkClicked);
            // 
            // btnCloseAccount
            // 
            this.btnCloseAccount.AutoSize = true;
            this.btnCloseAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnCloseAccount.LinkColor = System.Drawing.Color.Yellow;
            this.btnCloseAccount.Location = new System.Drawing.Point(699, 11);
            this.btnCloseAccount.Name = "btnCloseAccount";
            this.btnCloseAccount.Size = new System.Drawing.Size(118, 18);
            this.btnCloseAccount.TabIndex = 2;
            this.btnCloseAccount.TabStop = true;
            this.btnCloseAccount.Text = "Close Account";
            this.btnCloseAccount.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnCloseAccount_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewAccounts);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(889, 503);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // dataGridViewAccounts
            // 
            this.dataGridViewAccounts.AllowUserToAddRows = false;
            this.dataGridViewAccounts.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAccounts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAccounts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.classnamecolumn,
            this.ColumnClearedBalance,
            this.ColumnBookBalance,
            this.ColumnLimit,
            this.ColumnClosed});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewAccounts.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewAccounts.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewAccounts.MultiSelect = false;
            this.dataGridViewAccounts.Name = "dataGridViewAccounts";
            this.dataGridViewAccounts.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAccounts.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewAccounts.Size = new System.Drawing.Size(883, 484);
            this.dataGridViewAccounts.TabIndex = 0;
            this.dataGridViewAccounts.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAccounts_CellContentDoubleClick);
            this.dataGridViewAccounts.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewAccounts_DataError);
            // 
            // classnamecolumn
            // 
            this.classnamecolumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.classnamecolumn.DataPropertyName = "AccountName";
            this.classnamecolumn.HeaderText = "Name";
            this.classnamecolumn.Name = "classnamecolumn";
            this.classnamecolumn.ReadOnly = true;
            this.classnamecolumn.Width = 400;
            // 
            // ColumnClearedBalance
            // 
            this.ColumnClearedBalance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnClearedBalance.DataPropertyName = "ClearedBalance";
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.ColumnClearedBalance.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnClearedBalance.HeaderText = "Cleared Balance";
            this.ColumnClearedBalance.Name = "ColumnClearedBalance";
            this.ColumnClearedBalance.ReadOnly = true;
            this.ColumnClearedBalance.Width = 120;
            // 
            // ColumnBookBalance
            // 
            this.ColumnBookBalance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnBookBalance.DataPropertyName = "BookBalance";
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = null;
            this.ColumnBookBalance.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColumnBookBalance.HeaderText = "Book Balance";
            this.ColumnBookBalance.Name = "ColumnBookBalance";
            this.ColumnBookBalance.ReadOnly = true;
            // 
            // ColumnLimit
            // 
            this.ColumnLimit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnLimit.DataPropertyName = "Limit";
            dataGridViewCellStyle6.Format = "C2";
            dataGridViewCellStyle6.NullValue = null;
            this.ColumnLimit.DefaultCellStyle = dataGridViewCellStyle6;
            this.ColumnLimit.HeaderText = "Limit";
            this.ColumnLimit.Name = "ColumnLimit";
            this.ColumnLimit.ReadOnly = true;
            // 
            // ColumnClosed
            // 
            this.ColumnClosed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnClosed.DataPropertyName = "Closed";
            this.ColumnClosed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnClosed.HeaderText = "Closed";
            this.ColumnClosed.Name = "ColumnClosed";
            this.ColumnClosed.ReadOnly = true;
            this.ColumnClosed.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnClosed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // AccountsListForm
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(889, 598);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "AccountsListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Accounts";
            this.Load += new System.EventHandler(this.AccountsListForm_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceAccounts)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccounts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAccountId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.TextBox txtAccountName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkClosed;
        private System.Windows.Forms.LinkLabel btnEdit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel btnClose;
        private System.Windows.Forms.LinkLabel btnAdd;
        private System.Windows.Forms.LinkLabel btnCloseAccount;
        private System.Windows.Forms.BindingSource bindingSourceAccounts;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridViewAccounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn classnamecolumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnClearedBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBookBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLimit;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnClosed;
    }
}