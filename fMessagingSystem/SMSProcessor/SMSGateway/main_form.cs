using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using fCommon.Utility;
using System.Threading;
using System.IO;
using log4net;

namespace SMSGateway
{
    public partial class main_form : Form
    {
        private string TAG;
        private List<notificationdto> _lstnotificationdto = new List<notificationdto>();
        //Event declaration:
        //event for publishing messages to output
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;

        private int loggedinTimeCounter = 0;
        DateTime _startTime = DateTime.Now;

        ILog log;

        public main_form()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);

            log4net.Config.BasicConfigurator.Configure();
            log = log4net.LogManager.GetLogger(typeof(MyApplicationContext));

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

        private void main_form_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = "Fanikiwa Sms Gateway";
                this.lblrunningtime.Text = DateTime.Today.ToShortDateString();
                loggedInTimer.Tick += new EventHandler(loggedInTimer_Tick);
                loggedInTimer.Interval = 1000; // 1 second
                loggedInTimer.Start();

                var applicationContext = new MyApplicationContext(_notificationmessageEventname);

                //connect sendingModem
                applicationContext.Connect();

                log.Debug("Connected");

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
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings_form settings = new settings_form();
            settings.Show();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            help_form help_form = new help_form();
            help_form.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            this.Close();
        }



    }
}
