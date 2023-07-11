namespace ffwebAdminUI
{
    partial class AddAccountForm
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label7;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAccountForm));
            this.btnclose = new System.Windows.Forms.LinkLabel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnAdd = new System.Windows.Forms.LinkLabel();
            this.bindingSourceBank = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceCustomerId = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceAccountTypes = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceAccSchool = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboCOA = new System.Windows.Forms.ComboBox();
            this.txtCustomerId = new System.Windows.Forms.TextBox();
            this.cboAccountTypes = new System.Windows.Forms.ComboBox();
            this.btnSearchCustomerId = new System.Windows.Forms.Button();
            this.txtAccountNo = new System.Windows.Forms.TextBox();
            this.txtAccountName = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtBranches = new System.Windows.Forms.TextBox();
            this.cboLimitFlag = new System.Windows.Forms.ComboBox();
            this.cboPassFlag = new System.Windows.Forms.ComboBox();
            this.cboLimitCheckFlag = new System.Windows.Forms.ComboBox();
            telephoneLabel = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            indexLabel = new System.Windows.Forms.Label();
            schoolNameLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceBank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCustomerId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceAccountTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceAccSchool)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // telephoneLabel
            // 
            telephoneLabel.AutoSize = true;
            telephoneLabel.Location = new System.Drawing.Point(56, 116);
            telephoneLabel.Name = "telephoneLabel";
            telephoneLabel.Size = new System.Drawing.Size(78, 13);
            telephoneLabel.TabIndex = 132;
            telephoneLabel.Text = "Account Type*";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(66, 84);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(68, 13);
            label4.TabIndex = 130;
            label4.Text = "Account No*";
            // 
            // indexLabel
            // 
            indexLabel.AutoSize = true;
            indexLabel.Location = new System.Drawing.Point(67, 21);
            indexLabel.Name = "indexLabel";
            indexLabel.Size = new System.Drawing.Size(67, 13);
            indexLabel.TabIndex = 127;
            indexLabel.Text = "Customer Id*";
            // 
            // schoolNameLabel
            // 
            schoolNameLabel.AutoSize = true;
            schoolNameLabel.Location = new System.Drawing.Point(52, 52);
            schoolNameLabel.Name = "schoolNameLabel";
            schoolNameLabel.Size = new System.Drawing.Size(82, 13);
            schoolNameLabel.TabIndex = 128;
            schoolNameLabel.Text = "Account Name*";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(101, 148);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(33, 13);
            label1.TabIndex = 136;
            label1.Text = "COA*";
            // 
            // btnclose
            // 
            this.btnclose.AutoSize = true;
            this.btnclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnclose.LinkColor = System.Drawing.Color.Yellow;
            this.btnclose.Location = new System.Drawing.Point(221, 11);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(52, 18);
            this.btnclose.TabIndex = 1;
            this.btnclose.TabStop = true;
            this.btnclose.Text = "Close";
            this.btnclose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnClose_LinkClicked);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = true;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnAdd.LinkColor = System.Drawing.Color.Yellow;
            this.btnAdd.Location = new System.Drawing.Point(162, 11);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 18);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.TabStop = true;
            this.btnAdd.Text = "Add";
            this.btnAdd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnAdd_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnclose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 229);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(515, 35);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(515, 229);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // cboCOA
            // 
            this.cboCOA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCOA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCOA.FormattingEnabled = true;
            this.cboCOA.IntegralHeight = false;
            this.cboCOA.Location = new System.Drawing.Point(136, 142);
            this.cboCOA.Name = "cboCOA";
            this.cboCOA.Size = new System.Drawing.Size(315, 21);
            this.cboCOA.TabIndex = 4;
            // 
            // txtCustomerId
            // 
            this.txtCustomerId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomerId.Location = new System.Drawing.Point(136, 17);
            this.txtCustomerId.MaxLength = 4;
            this.txtCustomerId.Name = "txtCustomerId";
            this.txtCustomerId.Size = new System.Drawing.Size(220, 20);
            this.txtCustomerId.TabIndex = 0;
            this.txtCustomerId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerID_KeyDown);
            this.txtCustomerId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCustomerID_KeyPress);
            // 
            // cboAccountTypes
            // 
            this.cboAccountTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAccountTypes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboAccountTypes.FormattingEnabled = true;
            this.cboAccountTypes.Location = new System.Drawing.Point(136, 110);
            this.cboAccountTypes.Name = "cboAccountTypes";
            this.cboAccountTypes.Size = new System.Drawing.Size(315, 21);
            this.cboAccountTypes.TabIndex = 3;
            // 
            // btnSearchCustomerId
            // 
            this.btnSearchCustomerId.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSearchCustomerId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchCustomerId.Location = new System.Drawing.Point(393, 16);
            this.btnSearchCustomerId.Name = "btnSearchCustomerId";
            this.btnSearchCustomerId.Size = new System.Drawing.Size(58, 23);
            this.btnSearchCustomerId.TabIndex = 6;
            this.btnSearchCustomerId.TabStop = false;
            this.btnSearchCustomerId.Text = ":::";
            this.btnSearchCustomerId.UseVisualStyleBackColor = false;
            this.btnSearchCustomerId.Click += new System.EventHandler(this.btnSearchCustomerId_Click);
            // 
            // txtAccountNo
            // 
            this.txtAccountNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccountNo.Location = new System.Drawing.Point(136, 79);
            this.txtAccountNo.MaxLength = 50;
            this.txtAccountNo.Name = "txtAccountNo";
            this.txtAccountNo.Size = new System.Drawing.Size(315, 20);
            this.txtAccountNo.TabIndex = 2;
            // 
            // txtAccountName
            // 
            this.txtAccountName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccountName.Location = new System.Drawing.Point(136, 48);
            this.txtAccountName.MaxLength = 250;
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(315, 20);
            this.txtAccountName.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(509, 210);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.tabPage1.Controls.Add(this.cboAccountTypes);
            this.tabPage1.Controls.Add(this.cboCOA);
            this.tabPage1.Controls.Add(schoolNameLabel);
            this.tabPage1.Controls.Add(label1);
            this.tabPage1.Controls.Add(this.txtAccountName);
            this.tabPage1.Controls.Add(this.txtCustomerId);
            this.tabPage1.Controls.Add(indexLabel);
            this.tabPage1.Controls.Add(label4);
            this.tabPage1.Controls.Add(telephoneLabel);
            this.tabPage1.Controls.Add(this.txtAccountNo);
            this.tabPage1.Controls.Add(this.btnSearchCustomerId);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(501, 184);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Info";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.tabPage2.Controls.Add(this.cboLimitCheckFlag);
            this.tabPage2.Controls.Add(this.cboPassFlag);
            this.tabPage2.Controls.Add(this.cboLimitFlag);
            this.tabPage2.Controls.Add(label7);
            this.tabPage2.Controls.Add(label5);
            this.tabPage2.Controls.Add(label3);
            this.tabPage2.Controls.Add(label2);
            this.tabPage2.Controls.Add(this.txtBranches);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(501, 184);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "data";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(57, 26);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(41, 13);
            label2.TabIndex = 130;
            label2.Text = "Branch";
            // 
            // txtBranches
            // 
            this.txtBranches.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBranches.Location = new System.Drawing.Point(100, 22);
            this.txtBranches.MaxLength = 50;
            this.txtBranches.Name = "txtBranches";
            this.txtBranches.Size = new System.Drawing.Size(315, 20);
            this.txtBranches.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(47, 60);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(50, 13);
            label3.TabIndex = 132;
            label3.Text = "PassFlag";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(49, 95);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(48, 13);
            label5.TabIndex = 134;
            label5.Text = "LimitFlag";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(18, 131);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(79, 13);
            label7.TabIndex = 136;
            label7.Text = "LimitCheckFlag";
            // 
            // cboLimitFlag
            // 
            this.cboLimitFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLimitFlag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboLimitFlag.FormattingEnabled = true;
            this.cboLimitFlag.Location = new System.Drawing.Point(100, 93);
            this.cboLimitFlag.Name = "cboLimitFlag";
            this.cboLimitFlag.Size = new System.Drawing.Size(315, 21);
            this.cboLimitFlag.TabIndex = 3;
            // 
            // cboPassFlag
            // 
            this.cboPassFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPassFlag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPassFlag.FormattingEnabled = true;
            this.cboPassFlag.Location = new System.Drawing.Point(100, 57);
            this.cboPassFlag.Name = "cboPassFlag";
            this.cboPassFlag.Size = new System.Drawing.Size(315, 21);
            this.cboPassFlag.TabIndex = 2;
            // 
            // cboLimitCheckFlag
            // 
            this.cboLimitCheckFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLimitCheckFlag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboLimitCheckFlag.FormattingEnabled = true;
            this.cboLimitCheckFlag.Location = new System.Drawing.Point(100, 129);
            this.cboLimitCheckFlag.Name = "cboLimitCheckFlag";
            this.cboLimitCheckFlag.Size = new System.Drawing.Size(315, 21);
            this.cboLimitCheckFlag.TabIndex = 4;
            // 
            // AddAccountForm
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(515, 264);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddAccountForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Open  Account ";
            this.Load += new System.EventHandler(this.AddAccountForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceBank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCustomerId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceAccountTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceAccSchool)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel btnclose;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.LinkLabel btnAdd;
        private System.Windows.Forms.BindingSource bindingSourceBank;
        private System.Windows.Forms.BindingSource bindingSourceCustomerId;
        private System.Windows.Forms.BindingSource bindingSourceAccountTypes;
        private System.Windows.Forms.BindingSource bindingSourceAccSchool;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboAccountTypes;
        private System.Windows.Forms.Button btnSearchCustomerId;
        private System.Windows.Forms.TextBox txtAccountNo;
        private System.Windows.Forms.TextBox txtAccountName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCustomerId;
        private System.Windows.Forms.ComboBox cboCOA;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtBranches;
        private System.Windows.Forms.ComboBox cboLimitCheckFlag;
        private System.Windows.Forms.ComboBox cboPassFlag;
        private System.Windows.Forms.ComboBox cboLimitFlag;
    }
}