using ffwebAdminUI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using fCommon.Utility;

namespace ffwebAdminUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);

                //var applicationContext = new Main();
                Application.Run(new MainForm());
                //Application.Run(new RunDiary());
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

        }

        static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Console.WriteLine(ex.ToString());
            Utils.ShowError(ex);
        }

        static void ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Console.WriteLine(ex.ToString());
            Utils.ShowError(ex);
        }



    }
}
