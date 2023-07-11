namespace ffwebAdminUI
{
    partial class EditAccountForm
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
            System.Windows.Forms.Label telephoneLabel;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label indexLabel;
            System.Windows.Forms.Label schoolNameLabel;
            System.Windows.Forms.Label label1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditAccountForm));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnClose = new System.Windows.Forms.LinkLabel();
            this.btnUpdate = new System.Windows.Forms.LinkLabel();
            this.bindingSourceBank = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceCustomerId = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceAccountTypes = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboCOA = new System.Windows.Forms.ComboBox();
            this.txtCustomerID = new System.Windows.Forms.TextBox();
            this.cboAccountTypes = new System.Windows.Forms.ComboBox();
            this.btnSearchCustomerId = new System.Windows.Forms.Button();
            this.txtAccountNo = new System.Windows.Forms.TextBox();
            this.txtAccountName = new System.Windows.Forms.TextBox();
            telephoneLabel = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            indexLabel = new System.Windows.Forms.Label();
            schoolNameLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceBank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCustomerId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceAccountTypes)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // telephoneLabel
            // 
            telephoneLabel.AutoSize = true;
            telephoneLabel.Location = new System.Drawing.Point(25, 106);
            telephoneLabel.Name = "telephoneLabel";
            telephoneLabel.Size = new System.Drawing.Size(78, 13);
            telephoneLabel.TabIndex = 132;
            telephoneLabel.Text = "Account Type*";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(36, 78);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(68, 13);
            label4.TabIndex = 130;
            label4.Text = "Account No*";
            // 
            // indexLabel
            // 
            indexLabel.AutoSize = true;
            indexLabel.Location = new System.Drawing.Point(48, 22);
            indexLabel.Name = "indexLabel";
            indexLabel.Size = new System.Drawing.Size(56, 13);
            indexLabel.TabIndex = 127;
            indexLabel.Text = "Owner ID*";
            // 
            // schoolNameLabel
            // 
            schoolNameLabel.AutoSize = true;
            schoolNameLabel.Location = new System.Drawing.Point(22, 50);
            schoolNameLabel.Name = "schoolNameLabel";
            schoolNameLabel.Size = new System.Drawing.Size(82, 13);
            schoolNameLabel.TabIndex = 128;
            schoolNameLabel.Text = "Account Name*";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(70, 134);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(33, 13);
            label1.TabIndex = 134;
            label1.Text = "COA*";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnClose.LinkColor = System.Drawing.Color.Yellow;
            this.btnClose.Location = new System.Drawing.Point(257, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(52, 18);
            this.btnClose.TabIndex = 1;
            this.btnClose.TabStop = true;
            this.btnClose.Text = "Close";
            this.btnClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnClose_LinkClicked);
            // 
            // btnUpdate
            // 
            this.btnUpdate.AutoSize = true;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.LinkColor = System.Drawing.Color.Yellow;
            this.btnUpdate.Location = new System.Drawing.Point(154, 10);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(61, 18);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.TabStop = true;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnUpdate_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 177);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(489, 37);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboCOA);
            this.groupBox2.Controls.Add(label1);
            this.groupBox2.Controls.Add(this.txtCustomerID);
            this.groupBox2.Controls.Add(this.cboAccountTypes);
            this.groupBox2.Controls.Add(telephoneLabel);
            this.groupBox2.Controls.Add(this.btnSearchCustomerId);
            this.groupBox2.Controls.Add(this.txtAccountNo);
            this.groupBox2.Controls.Add(label4);
            this.groupBox2.Controls.Add(indexLabel);
            this.groupBox2.Controls.Add(this.txtAccountName);
            this.groupBox2.Controls.Add(schoolNameLabel);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(489, 177);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // cboCOA
            // 
            this.cboCOA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCOA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCOA.FormattingEnabled = true;
            this.cboCOA.Location = new System.Drawing.Point(106, 132);
            this.cboCOA.Name = "cboCOA";
            this.cboCOA.Size = new System.Drawing.Size(315, 21);
            this.cboCOA.TabIndex = 133;
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomerID.Location = new System.Drawing.Point(106, 19);
            this.txtCustomerID.MaxLength = 50;
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.Size = new System.Drawing.Size(220, 20);
            this.txtCustomerID.TabIndex = 0;
            this.txtCustomerID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerID_KeyDown);
            this.txtCustomerID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCustomerID_KeyPress);
            // 
            // cboAccountTypes
            // 
            this.cboAccountTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAccountTypes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboAccountTypes.FormattingEnabled = true;
            this.cboAccountTypes.Location = new System.Drawing.Point(106, 103);
            this.cboAccountTypes.Name = "cboAccountTypes";
            this.cboAccountTypes.Size = new System.Drawing.Size(315, 21);
            this.cboAccountTypes.TabIndex = 3;
            // 
            // btnSearchCustomerId
            // 
            this.btnSearchCustomerId.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSearchCustomerId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchCustomerId.Location = new System.Drawing.Point(363, 16);
            this.btnSearchCustomerId.Name = "btnSearchCustomerId";
            this.btnSearchCustomerId.Size = new System.Drawing.Size(58, 23);
            this.btnSearchCustomerId.TabIndex = 6;
            this.btnSearchCustomerId.TabStop = false;
            this.btnSearchCustomerId.Text = ":::";
            this.btnSearchCustomerId.UseVisualStyleBackColor = false;
            // 
            // txtAccountNo
            // 
            this.txtAccountNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccountNo.Location = new System.Drawing.Point(106, 75);
            this.txtAccountNo.MaxLength = 50;
            this.txtAccountNo.Name = "txtAccountNo";
            this.txtAccountNo.Size = new System.Drawing.Size(315, 20);
            this.txtAccountNo.TabIndex = 2;
            // 
            // txtAccountName
            // 
            this.txtAccountName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccountName.Location = new System.Drawing.Point(106, 47);
            this.txtAccountName.MaxLength = 100;
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(315, 20);
            this.txtAccountName.TabIndex = 1;
            // 
            // EditAccountForm
            // 
            this.AcceptButton = this.btnUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(489, 214);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditAccountForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.EditAccountForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceBank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCustomerId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceAccountTypes)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.LinkLabel btnClose;
        private System.Windows.Forms.LinkLabel btnUpdate;
        private System.Windows.Forms.BindingSource bindingSourceBank;
        private System.Windows.Forms.BindingSource bindingSourceCustomerId;
        private System.Windows.Forms.BindingSource bindingSourceAccountTypes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboCOA;
        private System.Windows.Forms.TextBox txtCustomerID;
        private System.Windows.Forms.ComboBox cboAccountTypes;
        private System.Windows.Forms.Button btnSearchCustomerId;
        private System.Windows.Forms.TextBox txtAccountNo;
        private System.Windows.Forms.TextBox txtAccountName;


    }
}