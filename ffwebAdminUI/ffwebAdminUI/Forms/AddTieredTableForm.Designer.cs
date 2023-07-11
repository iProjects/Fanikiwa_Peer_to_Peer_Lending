namespace ffwebAdminUI
{
    partial class AddTieredTableForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.LinkLabel();
            this.groupBoxTieredDet = new System.Windows.Forms.GroupBox();
            this.dataGridViewTieredTableDetails = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCreateTieredTable = new System.Windows.Forms.LinkLabel();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bindingSourceTieredTableDetails = new System.Windows.Forms.BindingSource(this.components);
            this.ColumnTieredID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAbsolute = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBoxTieredDet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTieredTableDetails)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTieredTableDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 267);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(613, 46);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = true;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnAdd.LinkColor = System.Drawing.Color.Yellow;
            this.btnAdd.Location = new System.Drawing.Point(211, 19);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 18);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.TabStop = true;
            this.btnAdd.Text = "Add";
            this.btnAdd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnAdd_LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnClose.LinkColor = System.Drawing.Color.Yellow;
            this.btnClose.Location = new System.Drawing.Point(277, 19);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(52, 18);
            this.btnClose.TabIndex = 16;
            this.btnClose.TabStop = true;
            this.btnClose.Text = "Close";
            this.btnClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnClose_LinkClicked);
            // 
            // groupBoxTieredDet
            // 
            this.groupBoxTieredDet.Controls.Add(this.dataGridViewTieredTableDetails);
            this.groupBoxTieredDet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTieredDet.Location = new System.Drawing.Point(0, 47);
            this.groupBoxTieredDet.Name = "groupBoxTieredDet";
            this.groupBoxTieredDet.Size = new System.Drawing.Size(613, 220);
            this.groupBoxTieredDet.TabIndex = 0;
            this.groupBoxTieredDet.TabStop = false;
            this.groupBoxTieredDet.Text = "Table Details";
            // 
            // dataGridViewTieredTableDetails
            // 
            this.dataGridViewTieredTableDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTieredTableDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnTieredID,
            this.ColumnMin,
            this.ColumnMax,
            this.ColumnRate,
            this.ColumnAbsolute});
            this.dataGridViewTieredTableDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTieredTableDetails.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewTieredTableDetails.MultiSelect = false;
            this.dataGridViewTieredTableDetails.Name = "dataGridViewTieredTableDetails";
            this.dataGridViewTieredTableDetails.Size = new System.Drawing.Size(607, 201);
            this.dataGridViewTieredTableDetails.TabIndex = 0;
            this.dataGridViewTieredTableDetails.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewTieredTableDetails_CellValidating);
            this.dataGridViewTieredTableDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewTieredTableDetails_DataError);
            this.dataGridViewTieredTableDetails.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridViewTieredTableDetails_DefaultValuesNeeded);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCreateTieredTable);
            this.groupBox3.Controls.Add(this.txtDescription);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(613, 47);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // btnCreateTieredTable
            // 
            this.btnCreateTieredTable.AutoSize = true;
            this.btnCreateTieredTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnCreateTieredTable.LinkColor = System.Drawing.Color.Yellow;
            this.btnCreateTieredTable.Location = new System.Drawing.Point(342, 16);
            this.btnCreateTieredTable.Name = "btnCreateTieredTable";
            this.btnCreateTieredTable.Size = new System.Drawing.Size(58, 18);
            this.btnCreateTieredTable.TabIndex = 1;
            this.btnCreateTieredTable.TabStop = true;
            this.btnCreateTieredTable.Text = "Create";
            this.btnCreateTieredTable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnCreateTieredTable_LinkClicked);
            // 
            // txtTieredTable
            // 
            this.txtDescription.Location = new System.Drawing.Point(133, 15);
            this.txtDescription.MaxLength = 150;
            this.txtDescription.Name = "txtTieredTable";
            this.txtDescription.Size = new System.Drawing.Size(181, 20);
            this.txtDescription.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Description";
            // 
            // ColumnTieredID
            // 
            this.ColumnTieredID.DataPropertyName = "TieredID";
            this.ColumnTieredID.HeaderText = "Table ID";
            this.ColumnTieredID.Name = "ColumnTieredID";
            this.ColumnTieredID.Visible = false;
            // 
            // ColumnMin
            // 
            this.ColumnMin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnMin.DataPropertyName = "Min";
            this.ColumnMin.HeaderText = "Min";
            this.ColumnMin.Name = "ColumnMin";
            // 
            // ColumnMax
            // 
            this.ColumnMax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnMax.DataPropertyName = "Max";
            this.ColumnMax.HeaderText = "Max";
            this.ColumnMax.Name = "ColumnMax";
            // 
            // ColumnRate
            // 
            this.ColumnRate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnRate.DataPropertyName = "Rate";
            this.ColumnRate.HeaderText = "Rate";
            this.ColumnRate.Name = "ColumnRate";
            // 
            // ColumnAbsolute
            // 
            this.ColumnAbsolute.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnAbsolute.DataPropertyName = "Absolute";
            this.ColumnAbsolute.HeaderText = "Absolute";
            this.ColumnAbsolute.Name = "ColumnAbsolute";
            this.ColumnAbsolute.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnAbsolute.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // AddTieredTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(613, 313);
            this.Controls.Add(this.groupBoxTieredDet);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "AddTieredTableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Tiered Table";
            this.Load += new System.EventHandler(this.AddTieredTableForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxTieredDet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTieredTableDetails)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTieredTableDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxTieredDet;
        private System.Windows.Forms.LinkLabel btnAdd;
        private System.Windows.Forms.LinkLabel btnClose;
        private System.Windows.Forms.DataGridView dataGridViewTieredTableDetails;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.LinkLabel btnCreateTieredTable;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource bindingSourceTieredTableDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTieredID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnAbsolute;
    }
}