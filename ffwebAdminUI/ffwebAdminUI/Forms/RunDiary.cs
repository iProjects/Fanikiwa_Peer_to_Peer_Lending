using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using fCommon.Utility;

namespace ffwebAdminUI.Forms
{
    public partial class RunDiary : Form
    {
        string path_to_diary_app = string.Empty;

        private string TAG;
        private List<notificationdto> _lstnotificationdto = new List<notificationdto>();
        //Event declaration:
        //event for publishing messages to output
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname_from_parent;

        public RunDiary(EventHandler<notificationmessageEventArgs> notificationmessageEventname_from_parent)
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);

            TAG = this.GetType().Name;

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname += notificationmessageHandler;
            _notificationmessageEventname_from_parent += notificationmessageEventname_from_parent;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished RunDiary initialization", TAG));

        }

        private void RunDiary_Load(object sender, EventArgs e)
        {
            try
            {
                path_to_diary_app = System.Configuration.ConfigurationManager.AppSettings["PATH_TO_DIARY_APP"];
                txtPath.Text = path_to_diary_app;

                btnRun_Click(sender, e);

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished RunDiary load", TAG));
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Utils.LogEventViewer(ex);
            Log.WriteToErrorLogFile(ex);
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
        }

        private void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Utils.LogEventViewer(ex);
            Log.WriteToErrorLogFile(ex);
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
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
                _notificationmessageEventname_from_parent.Invoke(this, new notificationmessageEventArgs(args.message, TAG));

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
                Console.WriteLine(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void Run_Diary(string file_path, string diary_date)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = file_path;
                startInfo.Arguments = diary_date;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                Process p = new Process();
                p.StartInfo = startInfo;
                p.EnableRaisingEvents = true;
                p.OutputDataReceived += on_output_data_received;
                p.ErrorDataReceived += on_error_data_received;
                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                if (isvalid())
                {
                    Run_Diary(txtPath.Text, this.dateTimePicker_diary_date.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }
        private bool isvalid()
        {
            bool no_error = true;
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                errorProvider1.SetError(txtPath, "diary executable path cannot be null!");
                no_error = false;
            }

            return no_error;
        }
        private void on_output_data_received(object sender, DataReceivedEventArgs e)
        {
            try
            {
                Console.WriteLine(e.Data.ToString());

                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(e.Data.ToString(), TAG));
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void on_error_data_received(object sender, DataReceivedEventArgs e)
        {
            try
            {
                Console.WriteLine(e.Data.ToString());

                Invoke(new System.Action(() =>
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(e.Data.ToString(), TAG));
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


    }
}
