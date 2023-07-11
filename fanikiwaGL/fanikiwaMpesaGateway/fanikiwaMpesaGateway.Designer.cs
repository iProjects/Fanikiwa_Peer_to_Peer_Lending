namespace fanikiwaMpesaGateway
{
    partial class fanikiwaMpesaGateway
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServiceResults = new System.Windows.Forms.TextBox();
            this.txtServiceUrl = new System.Windows.Forms.TextBox();
            this.btnDiscoverService = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 218);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(773, 67);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.Location = new System.Drawing.Point(46, 30);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(33, 13);
            this.btnClose.TabIndex = 1;
            this.btnClose.TabStop = true;
            this.btnClose.Text = "Close";
            this.btnClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnClose_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtServiceResults);
            this.groupBox2.Controls.Add(this.txtServiceUrl);
            this.groupBox2.Controls.Add(this.btnDiscoverService);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(773, 218);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Service Url";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(190, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Service Metadata";
            // 
            // txtServiceResults
            // 
            this.txtServiceResults.Location = new System.Drawing.Point(187, 96);
            this.txtServiceResults.Multiline = true;
            this.txtServiceResults.Name = "txtServiceResults";
            this.txtServiceResults.Size = new System.Drawing.Size(376, 116);
            this.txtServiceResults.TabIndex = 1;
            // 
            // txtServiceUrl
            // 
            this.txtServiceUrl.Location = new System.Drawing.Point(39, 40);
            this.txtServiceUrl.Name = "txtServiceUrl";
            this.txtServiceUrl.Size = new System.Drawing.Size(524, 20);
            this.txtServiceUrl.TabIndex = 0;
            // 
            // btnDiscoverService
            // 
            this.btnDiscoverService.AutoSize = true;
            this.btnDiscoverService.Location = new System.Drawing.Point(45, 72);
            this.btnDiscoverService.Name = "btnDiscoverService";
            this.btnDiscoverService.Size = new System.Drawing.Size(49, 13);
            this.btnDiscoverService.TabIndex = 0;
            this.btnDiscoverService.TabStop = true;
            this.btnDiscoverService.Text = "Discover";
            this.btnDiscoverService.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnDiscoverService_LinkClicked);
            // 
            // fanikiwaMpesaGateway
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 285);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "fanikiwaMpesaGateway";
            this.Text = "fanikiwa Mpesa Gateway";
            this.Load += new System.EventHandler(this.fanikiwaMpesaGateway_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel btnClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServiceResults;
        private System.Windows.Forms.TextBox txtServiceUrl;
        private System.Windows.Forms.LinkLabel btnDiscoverService;
    }
}

