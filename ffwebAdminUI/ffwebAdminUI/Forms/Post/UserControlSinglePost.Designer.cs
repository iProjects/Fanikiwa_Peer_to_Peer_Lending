namespace ffwebAdminUI
{
    partial class UserControlSinglePost
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBoxDrAccount = new System.Windows.Forms.GroupBox();
            this.lblDrAccountDetails = new System.Windows.Forms.Label();
            this.txtDrAccount = new System.Windows.Forms.TextBox();
            this.btnSelectDrAccount = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnPost = new System.Windows.Forms.Button();
            this.groupBoxValueDate = new System.Windows.Forms.GroupBox();
            this.dtpValueDate = new System.Windows.Forms.DateTimePicker();
            this.groupBoxDrNarrative = new System.Windows.Forms.GroupBox();
            this.txtDebitNarrative = new System.Windows.Forms.TextBox();
            this.groupBoxCrNarrative = new System.Windows.Forms.GroupBox();
            this.txtCrNarrative = new System.Windows.Forms.TextBox();
            this.lblCrAccountDetails = new System.Windows.Forms.Label();
            this.tableLayoutPanelSPost = new System.Windows.Forms.TableLayoutPanel();
            this.lblValueDate = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblCreditAccount = new System.Windows.Forms.Label();
            this.lblDebitAccount = new System.Windows.Forms.Label();
            this.groupBoxCreditAccount = new System.Windows.Forms.GroupBox();
            this.txtCrAccount = new System.Windows.Forms.TextBox();
            this.btnSelectCrAccount = new System.Windows.Forms.Button();
            this.lblDrNarrative = new System.Windows.Forms.Label();
            this.lblCrNarrative = new System.Windows.Forms.Label();
            this.groupBoxAmount = new System.Windows.Forms.GroupBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBoxDrAccount.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBoxValueDate.SuspendLayout();
            this.groupBoxDrNarrative.SuspendLayout();
            this.groupBoxCrNarrative.SuspendLayout();
            this.tableLayoutPanelSPost.SuspendLayout();
            this.groupBoxCreditAccount.SuspendLayout();
            this.groupBoxAmount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxDrAccount
            // 
            this.groupBoxDrAccount.Controls.Add(this.lblDrAccountDetails);
            this.groupBoxDrAccount.Controls.Add(this.txtDrAccount);
            this.groupBoxDrAccount.Controls.Add(this.btnSelectDrAccount);
            this.groupBoxDrAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDrAccount.Location = new System.Drawing.Point(130, 10);
            this.groupBoxDrAccount.Name = "groupBoxDrAccount";
            this.groupBoxDrAccount.Size = new System.Drawing.Size(598, 68);
            this.groupBoxDrAccount.TabIndex = 0;
            this.groupBoxDrAccount.TabStop = false;
            // 
            // lblDrAccountDetails
            // 
            this.lblDrAccountDetails.AutoSize = true;
            this.lblDrAccountDetails.Location = new System.Drawing.Point(3, 33);
            this.lblDrAccountDetails.Name = "lblDrAccountDetails";
            this.lblDrAccountDetails.Size = new System.Drawing.Size(19, 13);
            this.lblDrAccountDetails.TabIndex = 32;
            this.lblDrAccountDetails.Text = "::::";
            // 
            // txtDrAccount
            // 
            this.txtDrAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDrAccount.Location = new System.Drawing.Point(6, 10);
            this.txtDrAccount.MaxLength = 4;
            this.txtDrAccount.Name = "txtDrAccount";
            this.txtDrAccount.Size = new System.Drawing.Size(142, 20);
            this.txtDrAccount.TabIndex = 0;
            this.txtDrAccount.TabIndexChanged += new System.EventHandler(this.txtDrAccount_TabIndexChanged);
            this.txtDrAccount.TextChanged += new System.EventHandler(this.txtDrAccount_TextChanged);
            this.txtDrAccount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDrAccount_KeyDown);
            this.txtDrAccount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDrAccount_KeyPress);
            this.txtDrAccount.Leave += new System.EventHandler(this.txtDrAccount_Leave);
            this.txtDrAccount.Validated += new System.EventHandler(this.txtDrAccount_Validated);
            // 
            // btnSelectDrAccount
            // 
            this.btnSelectDrAccount.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSelectDrAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectDrAccount.Location = new System.Drawing.Point(214, 7);
            this.btnSelectDrAccount.Name = "btnSelectDrAccount";
            this.btnSelectDrAccount.Size = new System.Drawing.Size(36, 23);
            this.btnSelectDrAccount.TabIndex = 1;
            this.btnSelectDrAccount.Text = ":::";
            this.btnSelectDrAccount.UseVisualStyleBackColor = false;
            this.btnSelectDrAccount.Click += new System.EventHandler(this.btnSelectDrAccount_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnPost);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(130, 559);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(598, 51);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            // 
            // btnPost
            // 
            this.btnPost.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnPost.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPost.Location = new System.Drawing.Point(93, 9);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(75, 22);
            this.btnPost.TabIndex = 0;
            this.btnPost.Text = "Post";
            this.btnPost.UseVisualStyleBackColor = false;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // groupBoxValueDate
            // 
            this.groupBoxValueDate.Controls.Add(this.dtpValueDate);
            this.groupBoxValueDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxValueDate.Location = new System.Drawing.Point(130, 510);
            this.groupBoxValueDate.Name = "groupBoxValueDate";
            this.groupBoxValueDate.Size = new System.Drawing.Size(598, 43);
            this.groupBoxValueDate.TabIndex = 7;
            this.groupBoxValueDate.TabStop = false;
            // 
            // dtpValueDate
            // 
            this.dtpValueDate.Location = new System.Drawing.Point(6, 6);
            this.dtpValueDate.Name = "dtpValueDate";
            this.dtpValueDate.Size = new System.Drawing.Size(266, 20);
            this.dtpValueDate.TabIndex = 0;
            // 
            // groupBoxDrNarrative
            // 
            this.groupBoxDrNarrative.Controls.Add(this.txtDebitNarrative);
            this.groupBoxDrNarrative.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDrNarrative.Location = new System.Drawing.Point(130, 262);
            this.groupBoxDrNarrative.Name = "groupBoxDrNarrative";
            this.groupBoxDrNarrative.Size = new System.Drawing.Size(598, 50);
            this.groupBoxDrNarrative.TabIndex = 3;
            this.groupBoxDrNarrative.TabStop = false;
            // 
            // txtDebitNarrative
            // 
            this.txtDebitNarrative.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDebitNarrative.Location = new System.Drawing.Point(6, 9);
            this.txtDebitNarrative.MaxLength = 250;
            this.txtDebitNarrative.Name = "txtDebitNarrative";
            this.txtDebitNarrative.Size = new System.Drawing.Size(272, 20);
            this.txtDebitNarrative.TabIndex = 0;
            // 
            // groupBoxCrNarrative
            // 
            this.groupBoxCrNarrative.Controls.Add(this.txtCrNarrative);
            this.groupBoxCrNarrative.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxCrNarrative.Location = new System.Drawing.Point(130, 318);
            this.groupBoxCrNarrative.Name = "groupBoxCrNarrative";
            this.groupBoxCrNarrative.Size = new System.Drawing.Size(598, 50);
            this.groupBoxCrNarrative.TabIndex = 4;
            this.groupBoxCrNarrative.TabStop = false;
            // 
            // txtCrNarrative
            // 
            this.txtCrNarrative.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCrNarrative.Location = new System.Drawing.Point(6, 9);
            this.txtCrNarrative.MaxLength = 250;
            this.txtCrNarrative.Name = "txtCrNarrative";
            this.txtCrNarrative.Size = new System.Drawing.Size(272, 20);
            this.txtCrNarrative.TabIndex = 0;
            // 
            // lblCrAccountDetails
            // 
            this.lblCrAccountDetails.AutoSize = true;
            this.lblCrAccountDetails.Location = new System.Drawing.Point(5, 31);
            this.lblCrAccountDetails.Name = "lblCrAccountDetails";
            this.lblCrAccountDetails.Size = new System.Drawing.Size(19, 13);
            this.lblCrAccountDetails.TabIndex = 32;
            this.lblCrAccountDetails.Text = "::::";
            // 
            // tableLayoutPanelSPost
            // 
            this.tableLayoutPanelSPost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelSPost.ColumnCount = 3;
            this.tableLayoutPanelSPost.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.2292F));
            this.tableLayoutPanelSPost.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.74893F));
            this.tableLayoutPanelSPost.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.021862F));
            this.tableLayoutPanelSPost.Controls.Add(this.lblValueDate, 0, 8);
            this.tableLayoutPanelSPost.Controls.Add(this.lblAmount, 0, 7);
            this.tableLayoutPanelSPost.Controls.Add(this.lblCreditAccount, 0, 6);
            this.tableLayoutPanelSPost.Controls.Add(this.lblDebitAccount, 0, 1);
            this.tableLayoutPanelSPost.Controls.Add(this.groupBoxDrAccount, 1, 1);
            this.tableLayoutPanelSPost.Controls.Add(this.groupBoxCreditAccount, 1, 6);
            this.tableLayoutPanelSPost.Controls.Add(this.lblDrNarrative, 0, 4);
            this.tableLayoutPanelSPost.Controls.Add(this.lblCrNarrative, 0, 5);
            this.tableLayoutPanelSPost.Controls.Add(this.groupBox4, 1, 9);
            this.tableLayoutPanelSPost.Controls.Add(this.groupBoxValueDate, 1, 8);
            this.tableLayoutPanelSPost.Controls.Add(this.groupBoxDrNarrative, 1, 4);
            this.tableLayoutPanelSPost.Controls.Add(this.groupBoxCrNarrative, 1, 5);
            this.tableLayoutPanelSPost.Controls.Add(this.groupBoxAmount, 1, 7);
            this.tableLayoutPanelSPost.Location = new System.Drawing.Point(3, 0);
            this.tableLayoutPanelSPost.Name = "tableLayoutPanelSPost";
            this.tableLayoutPanelSPost.RowCount = 12;
            this.tableLayoutPanelSPost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0.9828451F));
            this.tableLayoutPanelSPost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.730166F));
            this.tableLayoutPanelSPost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.682085F));
            this.tableLayoutPanelSPost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.74268F));
            this.tableLayoutPanelSPost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.425779F));
            this.tableLayoutPanelSPost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.425779F));
            this.tableLayoutPanelSPost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.730166F));
            this.tableLayoutPanelSPost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.136606F));
            this.tableLayoutPanelSPost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.467913F));
            this.tableLayoutPanelSPost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.449695F));
            this.tableLayoutPanelSPost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.569843F));
            this.tableLayoutPanelSPost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.65644F));
            this.tableLayoutPanelSPost.Size = new System.Drawing.Size(740, 766);
            this.tableLayoutPanelSPost.TabIndex = 0;
            // 
            // lblValueDate
            // 
            this.lblValueDate.AutoSize = true;
            this.lblValueDate.Location = new System.Drawing.Point(3, 507);
            this.lblValueDate.Name = "lblValueDate";
            this.lblValueDate.Size = new System.Drawing.Size(60, 13);
            this.lblValueDate.TabIndex = 45;
            this.lblValueDate.Text = "Value Date";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(3, 445);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(47, 13);
            this.lblAmount.TabIndex = 35;
            this.lblAmount.Text = "Amount*";
            // 
            // lblCreditAccount
            // 
            this.lblCreditAccount.AutoSize = true;
            this.lblCreditAccount.Location = new System.Drawing.Point(3, 371);
            this.lblCreditAccount.Name = "lblCreditAccount";
            this.lblCreditAccount.Size = new System.Drawing.Size(77, 13);
            this.lblCreditAccount.TabIndex = 34;
            this.lblCreditAccount.Text = "Credit Account";
            // 
            // lblDebitAccount
            // 
            this.lblDebitAccount.AutoSize = true;
            this.lblDebitAccount.Location = new System.Drawing.Point(3, 7);
            this.lblDebitAccount.Name = "lblDebitAccount";
            this.lblDebitAccount.Size = new System.Drawing.Size(75, 13);
            this.lblDebitAccount.TabIndex = 4;
            this.lblDebitAccount.Text = "Debit Account";
            // 
            // groupBoxCreditAccount
            // 
            this.groupBoxCreditAccount.Controls.Add(this.lblCrAccountDetails);
            this.groupBoxCreditAccount.Controls.Add(this.txtCrAccount);
            this.groupBoxCreditAccount.Controls.Add(this.btnSelectCrAccount);
            this.groupBoxCreditAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxCreditAccount.Location = new System.Drawing.Point(130, 374);
            this.groupBoxCreditAccount.Name = "groupBoxCreditAccount";
            this.groupBoxCreditAccount.Size = new System.Drawing.Size(598, 68);
            this.groupBoxCreditAccount.TabIndex = 5;
            this.groupBoxCreditAccount.TabStop = false;
            // 
            // txtCrAccount
            // 
            this.txtCrAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCrAccount.Location = new System.Drawing.Point(6, 10);
            this.txtCrAccount.MaxLength = 4;
            this.txtCrAccount.Name = "txtCrAccount";
            this.txtCrAccount.Size = new System.Drawing.Size(142, 20);
            this.txtCrAccount.TabIndex = 0;
            this.txtCrAccount.TabIndexChanged += new System.EventHandler(this.txtCrAccount_TabIndexChanged);
            this.txtCrAccount.TextChanged += new System.EventHandler(this.txtCrAccount_TextChanged);
            this.txtCrAccount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCrAccount_KeyDown);
            this.txtCrAccount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCrAccount_KeyPress);
            this.txtCrAccount.Leave += new System.EventHandler(this.txtCrAccount_Leave);
            this.txtCrAccount.Validated += new System.EventHandler(this.txtCrAccount_Validated);
            // 
            // btnSelectCrAccount
            // 
            this.btnSelectCrAccount.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSelectCrAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectCrAccount.Location = new System.Drawing.Point(214, 8);
            this.btnSelectCrAccount.Name = "btnSelectCrAccount";
            this.btnSelectCrAccount.Size = new System.Drawing.Size(36, 23);
            this.btnSelectCrAccount.TabIndex = 1;
            this.btnSelectCrAccount.Text = ":::";
            this.btnSelectCrAccount.UseVisualStyleBackColor = false;
            this.btnSelectCrAccount.Click += new System.EventHandler(this.btnSelectCrAccount_Click);
            // 
            // lblDrNarrative
            // 
            this.lblDrNarrative.AutoSize = true;
            this.lblDrNarrative.Location = new System.Drawing.Point(3, 259);
            this.lblDrNarrative.Name = "lblDrNarrative";
            this.lblDrNarrative.Size = new System.Drawing.Size(78, 13);
            this.lblDrNarrative.TabIndex = 59;
            this.lblDrNarrative.Text = "Debit Narrative";
            // 
            // lblCrNarrative
            // 
            this.lblCrNarrative.AutoSize = true;
            this.lblCrNarrative.Location = new System.Drawing.Point(3, 315);
            this.lblCrNarrative.Name = "lblCrNarrative";
            this.lblCrNarrative.Size = new System.Drawing.Size(80, 13);
            this.lblCrNarrative.TabIndex = 60;
            this.lblCrNarrative.Text = "Credit Narrative";
            // 
            // groupBoxAmount
            // 
            this.groupBoxAmount.Controls.Add(this.txtAmount);
            this.groupBoxAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxAmount.Location = new System.Drawing.Point(130, 448);
            this.groupBoxAmount.Name = "groupBoxAmount";
            this.groupBoxAmount.Size = new System.Drawing.Size(598, 56);
            this.groupBoxAmount.TabIndex = 6;
            this.groupBoxAmount.TabStop = false;
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Location = new System.Drawing.Point(6, 10);
            this.txtAmount.MaxLength = 8;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(272, 20);
            this.txtAmount.TabIndex = 0;
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmount_KeyDown);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // UserControlSinglePost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelSPost);
            this.Name = "UserControlSinglePost";
            this.Size = new System.Drawing.Size(740, 766);
            this.Load += new System.EventHandler(this.UserControlSinglePost_Load);
            this.groupBoxDrAccount.ResumeLayout(false);
            this.groupBoxDrAccount.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBoxValueDate.ResumeLayout(false);
            this.groupBoxDrNarrative.ResumeLayout(false);
            this.groupBoxDrNarrative.PerformLayout();
            this.groupBoxCrNarrative.ResumeLayout(false);
            this.groupBoxCrNarrative.PerformLayout();
            this.tableLayoutPanelSPost.ResumeLayout(false);
            this.tableLayoutPanelSPost.PerformLayout();
            this.groupBoxCreditAccount.ResumeLayout(false);
            this.groupBoxCreditAccount.PerformLayout();
            this.groupBoxAmount.ResumeLayout(false);
            this.groupBoxAmount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxDrAccount;
        private System.Windows.Forms.Label lblDrAccountDetails;
        private System.Windows.Forms.TextBox txtDrAccount;
        private System.Windows.Forms.Button btnSelectDrAccount;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.GroupBox groupBoxValueDate;
        private System.Windows.Forms.DateTimePicker dtpValueDate;
        private System.Windows.Forms.GroupBox groupBoxDrNarrative;
        private System.Windows.Forms.TextBox txtDebitNarrative;
        private System.Windows.Forms.GroupBox groupBoxCrNarrative;
        private System.Windows.Forms.TextBox txtCrNarrative;
        private System.Windows.Forms.Label lblCrAccountDetails;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSPost;
        private System.Windows.Forms.Label lblValueDate;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblCreditAccount;
        private System.Windows.Forms.Label lblDebitAccount;
        private System.Windows.Forms.GroupBox groupBoxCreditAccount;
        private System.Windows.Forms.TextBox txtCrAccount;
        private System.Windows.Forms.Button btnSelectCrAccount;
        private System.Windows.Forms.Label lblDrNarrative;
        private System.Windows.Forms.Label lblCrNarrative;
        private System.Windows.Forms.GroupBox groupBoxAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.ErrorProvider errorProvider1;



    }
}
