using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using fPeerLending.Entities;
using fPeerLending.Business;
using fanikiwaGL.Entities;
using System.Diagnostics;
using fCommon.Utility;
using System.Threading;
using System.IO;

namespace ffwebAdminUI
{
    public partial class MainForm : Form
    {
        private string TAG;
        private List<notificationdto> _lstnotificationdto = new List<notificationdto>();
        //Event declaration:
        //event for publishing messages to output
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;

        private int loggedinTimeCounter = 0;
        DateTime _startTime = DateTime.Now;

        public MainForm()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);

            TAG = this.GetType().Name;

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname += notificationmessageHandler;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished MainForm initialization", TAG));

        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Utils.LogEventViewer(ex);
            Log.WriteToErrorLogFile_and_EventViewer(ex);
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
        }

        private void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Utils.LogEventViewer(ex);
            Log.WriteToErrorLogFile(ex);
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = "Fanikiwa Peer To Peer Lending Admin";
                this.lblrunningtime.Text = DateTime.Today.ToShortDateString();

                loggedInTimer.Tick += new EventHandler(loggedInTimer_Tick);
                loggedInTimer.Interval = 1000; // 1 second
                loggedInTimer.Start();

                lblLoggedInUser.Text = string.Empty;
                lblselecteddatabase.Text = string.Empty;
                lblStatusUpdates.Text = string.Empty;
                lblversion.Text = string.Empty;
                toolStripStatusLabel1.Text = string.Empty;
                toolStripStatusLabel2.Text = string.Empty;
                toolStripStatusLabel4.Text = string.Empty;

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished MainForm load", TAG));

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        //Event handler declaration:
        private void notificationmessageHandler(object sender, notificationmessageEventArgs args)
        {
            try
            {
                /* Handler logic */


                notificationdto _notificationdto = new notificationdto();

                DateTime currentDate = DateTime.Now;
                String dateTimenow = currentDate.ToString("dd-MM-yyyy HH:mm:ss tt");

                String _logtext = Environment.NewLine + "[ " + dateTimenow + " ]   " + args.message;

                _notificationdto._notification_message = _logtext;
                _notificationdto._created_datetime = dateTimenow;
                _notificationdto.TAG = args.TAG;

                _lstnotificationdto.Add(_notificationdto);
                Console.WriteLine(args.message);

                var _lstmsgdto = from msgdto in _lstnotificationdto
                                 orderby msgdto._created_datetime descending
                                 select msgdto._notification_message;

                String[] _logflippedlines = _lstmsgdto.ToArray();

                if (_logflippedlines.Length > 5000)
                {
                    _lstnotificationdto.Clear();
                }

                txtlog.Lines = _logflippedlines;
                txtlog.ScrollToCaret();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void loggedInTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                loggedinTimeCounter++;
                DateTime nowDate = DateTime.Now;
                TimeSpan t = nowDate - _startTime;
                lbltimelapsed.Text = string.Format("{0} : {1} : {2} : {3}", t.Days, t.Hours, t.Minutes, t.Seconds);

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.LogEventViewer(ex);
            }
        }
        private void NavigateToHomePage()
        {
            try
            {
                string help_file = "index.html";

                string base_directory = AppDomain.CurrentDomain.BaseDirectory;
                string help_path = Path.Combine(base_directory, "help");
                string help_file_path = Path.Combine(help_path, help_file);

                FileInfo fi = new FileInfo(help_file_path);

                if (fi.Exists)
                    this.webBrowser.Navigate(fi.FullName);
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.LogEventViewer(ex);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void transactionTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TransactionTypesListForm ttf = new TransactionTypesListForm() { Owner = this };
            ttf.Show();
        }

        private void accountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountsListForm alf = new AccountsListForm() { Owner = this };
            alf.Show();
        }


        private void postToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PostScreen ps = new PostScreen() { Owner = this };
            ps.Show();
        }

        private void enquiryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnquiryViewForm eqf = new EnquiryViewForm() { Owner = this };
            eqf.Show();
        }

        private void loansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoansForm lnf = new LoansForm() { Owner = this };
            lnf.Show();
        }

        private void offersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OffersForm of = new OffersForm() { Owner = this };
            of.Show();
        }

        private void accountTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountTypesForm atf = new AccountTypesForm() { Owner = this };
            atf.Show();
        }

        private void chartOfAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            COAForm cf = new COAForm("sys") { Owner = this };
            cf.Show();
        }

        private void runDiaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ffwebAdminUI.Forms.RunDiary f = new Forms.RunDiary(_notificationmessageEventname);
            f.ShowDialog();
        }

        private void exitToolStripButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void rubDiaryToolStripButton_Click(object sender, EventArgs e)
        {
            runDiaryToolStripMenuItem_Click(sender, e);
        }




    }
}