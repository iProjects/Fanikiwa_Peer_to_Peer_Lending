namespace ffwebAdminUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runDiaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transactionTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.accountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.postToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.enquiryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.loansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.offersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.accountTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.chartOfAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.lblLoggedInUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblselecteddatabase = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblversion = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblrunningtime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbltimelapsed = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusUpdates = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.rubDiaryToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.txtlog = new System.Windows.Forms.RichTextBox();
            this.loggedInTimer = new System.Windows.Forms.Timer(this.components);
            this.appNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gLToolStripMenuItem,
            this.adminToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(923, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runDiaryToolStripMenuItem,
            this.toolStripSeparator9,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // runDiaryToolStripMenuItem
            // 
            this.runDiaryToolStripMenuItem.Name = "runDiaryToolStripMenuItem";
            this.runDiaryToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.runDiaryToolStripMenuItem.Text = "Run Diary";
            this.runDiaryToolStripMenuItem.Click += new System.EventHandler(this.runDiaryToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(122, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // gLToolStripMenuItem
            // 
            this.gLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transactionTypesToolStripMenuItem,
            this.toolStripSeparator1,
            this.accountsToolStripMenuItem,
            this.toolStripSeparator2,
            this.postToolStripMenuItem,
            this.toolStripSeparator3,
            this.enquiryToolStripMenuItem,
            this.toolStripSeparator4,
            this.loansToolStripMenuItem,
            this.toolStripSeparator5,
            this.offersToolStripMenuItem,
            this.toolStripSeparator7,
            this.accountTypesToolStripMenuItem,
            this.toolStripSeparator6,
            this.chartOfAccountsToolStripMenuItem});
            this.gLToolStripMenuItem.Name = "gLToolStripMenuItem";
            this.gLToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.gLToolStripMenuItem.Text = "General Ledger";
            // 
            // transactionTypesToolStripMenuItem
            // 
            this.transactionTypesToolStripMenuItem.Name = "transactionTypesToolStripMenuItem";
            this.transactionTypesToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.transactionTypesToolStripMenuItem.Text = "Transaction Types";
            this.transactionTypesToolStripMenuItem.Click += new System.EventHandler(this.transactionTypesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // accountsToolStripMenuItem
            // 
            this.accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
            this.accountsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.accountsToolStripMenuItem.Text = "Accounts";
            this.accountsToolStripMenuItem.Click += new System.EventHandler(this.accountsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(169, 6);
            // 
            // postToolStripMenuItem
            // 
            this.postToolStripMenuItem.Name = "postToolStripMenuItem";
            this.postToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.postToolStripMenuItem.Text = "Post";
            this.postToolStripMenuItem.Click += new System.EventHandler(this.postToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(169, 6);
            // 
            // enquiryToolStripMenuItem
            // 
            this.enquiryToolStripMenuItem.Name = "enquiryToolStripMenuItem";
            this.enquiryToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.enquiryToolStripMenuItem.Text = "Enquiry";
            this.enquiryToolStripMenuItem.Click += new System.EventHandler(this.enquiryToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(169, 6);
            // 
            // loansToolStripMenuItem
            // 
            this.loansToolStripMenuItem.Name = "loansToolStripMenuItem";
            this.loansToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.loansToolStripMenuItem.Text = "Loans";
            this.loansToolStripMenuItem.Click += new System.EventHandler(this.loansToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(169, 6);
            // 
            // offersToolStripMenuItem
            // 
            this.offersToolStripMenuItem.Name = "offersToolStripMenuItem";
            this.offersToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.offersToolStripMenuItem.Text = "Offers";
            this.offersToolStripMenuItem.Click += new System.EventHandler(this.offersToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(169, 6);
            // 
            // accountTypesToolStripMenuItem
            // 
            this.accountTypesToolStripMenuItem.Name = "accountTypesToolStripMenuItem";
            this.accountTypesToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.accountTypesToolStripMenuItem.Text = "Account Types";
            this.accountTypesToolStripMenuItem.Click += new System.EventHandler(this.accountTypesToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(169, 6);
            // 
            // chartOfAccountsToolStripMenuItem
            // 
            this.chartOfAccountsToolStripMenuItem.Name = "chartOfAccountsToolStripMenuItem";
            this.chartOfAccountsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.chartOfAccountsToolStripMenuItem.Text = "Chart Of Accounts";
            this.chartOfAccountsToolStripMenuItem.Click += new System.EventHandler(this.chartOfAccountsToolStripMenuItem_Click);
            // 
            // adminToolStripMenuItem
            // 
            this.adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            this.adminToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.adminToolStripMenuItem.Text = "Admin";
            // 
            // statusStripMain
            // 
            this.statusStripMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.statusStripMain.BackgroundImage = global::ffwebAdminUI.Properties.Resources.statusimg;
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblLoggedInUser,
            this.toolStripStatusLabel1,
            this.lblselecteddatabase,
            this.toolStripStatusLabel2,
            this.lblversion,
            this.toolStripStatusLabel4,
            this.lblrunningtime,
            this.toolStripStatusLabel5,
            this.lbltimelapsed,
            this.toolStripStatusLabel3,
            this.lblStatusUpdates});
            this.statusStripMain.Location = new System.Drawing.Point(0, 648);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(923, 22);
            this.statusStripMain.TabIndex = 2;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // lblLoggedInUser
            // 
            this.lblLoggedInUser.Name = "lblLoggedInUser";
            this.lblLoggedInUser.Size = new System.Drawing.Size(76, 17);
            this.lblLoggedInUser.Text = "loggedinuser";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(97, 17);
            this.toolStripStatusLabel1.Text = "                              ";
            // 
            // lblselecteddatabase
            // 
            this.lblselecteddatabase.Name = "lblselecteddatabase";
            this.lblselecteddatabase.Size = new System.Drawing.Size(97, 17);
            this.lblselecteddatabase.Text = "selecteddatabase";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(97, 17);
            this.toolStripStatusLabel2.Text = "                              ";
            // 
            // lblversion
            // 
            this.lblversion.Name = "lblversion";
            this.lblversion.Size = new System.Drawing.Size(45, 17);
            this.lblversion.Text = "version";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(97, 17);
            this.toolStripStatusLabel4.Text = "                              ";
            // 
            // lblrunningtime
            // 
            this.lblrunningtime.Name = "lblrunningtime";
            this.lblrunningtime.Size = new System.Drawing.Size(30, 17);
            this.lblrunningtime.Text = "date";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(97, 17);
            this.toolStripStatusLabel5.Text = "                              ";
            // 
            // lbltimelapsed
            // 
            this.lbltimelapsed.Name = "lbltimelapsed";
            this.lbltimelapsed.Size = new System.Drawing.Size(78, 17);
            this.lbltimelapsed.Text = "loggedintime";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(97, 17);
            this.toolStripStatusLabel3.Text = "                              ";
            // 
            // lblStatusUpdates
            // 
            this.lblStatusUpdates.Name = "lblStatusUpdates";
            this.lblStatusUpdates.Size = new System.Drawing.Size(38, 17);
            this.lblStatusUpdates.Text = "status";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rubDiaryToolStripButton,
            this.toolStripSeparator8,
            this.exitToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(923, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // rubDiaryToolStripButton
            // 
            this.rubDiaryToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("rubDiaryToolStripButton.Image")));
            this.rubDiaryToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rubDiaryToolStripButton.Name = "rubDiaryToolStripButton";
            this.rubDiaryToolStripButton.Size = new System.Drawing.Size(78, 22);
            this.rubDiaryToolStripButton.Text = "Run Diary";
            this.rubDiaryToolStripButton.Click += new System.EventHandler(this.rubDiaryToolStripButton_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // exitToolStripButton
            // 
            this.exitToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripButton.Image")));
            this.exitToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exitToolStripButton.Name = "exitToolStripButton";
            this.exitToolStripButton.Size = new System.Drawing.Size(46, 22);
            this.exitToolStripButton.Text = "exit";
            this.exitToolStripButton.Click += new System.EventHandler(this.exitToolStripButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.webBrowser);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtlog);
            this.splitContainer1.Size = new System.Drawing.Size(923, 599);
            this.splitContainer1.SplitterDistance = 508;
            this.splitContainer1.TabIndex = 4;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(508, 599);
            this.webBrowser.TabIndex = 0;
            // 
            // txtlog
            // 
            this.txtlog.BackColor = System.Drawing.Color.Black;
            this.txtlog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtlog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtlog.ForeColor = System.Drawing.Color.Lime;
            this.txtlog.Location = new System.Drawing.Point(0, 0);
            this.txtlog.Name = "txtlog";
            this.txtlog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtlog.Size = new System.Drawing.Size(411, 599);
            this.txtlog.TabIndex = 1;
            this.txtlog.Text = "";
            // 
            // appNotifyIcon
            // 
            this.appNotifyIcon.Text = "notifyIcon1";
            this.appNotifyIcon.Visible = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(923, 670);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transactionTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem accountsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem postToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem enquiryToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel lblLoggedInUser;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblselecteddatabase;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lblversion;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel lblrunningtime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel lbltimelapsed;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusUpdates;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem loansToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem offersToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem accountTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem chartOfAccountsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runDiaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adminToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton exitToolStripButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.ToolStripButton rubDiaryToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.RichTextBox txtlog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.Timer loggedInTimer;
        private System.Windows.Forms.NotifyIcon appNotifyIcon;
    }
}