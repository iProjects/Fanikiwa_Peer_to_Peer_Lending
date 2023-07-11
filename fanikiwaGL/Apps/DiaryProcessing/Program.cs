using log4net; 
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.ExtraInformation;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using fanikiwaGL.Business;

namespace DiaryProcessing
{
    class Program
    {
        static ILog log;
        static bool EnableLog = true;
        static void Main(string[] args)
        {
            try
            {
                DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

                IConfigurationSource config = ConfigurationSourceFactory.Create();
                ExceptionPolicyFactory factory = new ExceptionPolicyFactory(config);
                Logger.SetLogWriter(new LogWriterFactory(config).Create(), false);
                ExceptionManager exManager = factory.CreateManager();
                ExceptionPolicy.SetExceptionManager(factory.CreateManager(), false);

                DateTime when = DateTime.Today;

                //parse arguments for date
                if (args.Count() > 0)
                    DateTime.TryParse(args[1], out when);

                log4net.Config.BasicConfigurator.Configure();
                log = log4net.LogManager.GetLogger(typeof(DiaryPostingComponent));
                EnableLog = fCommon.Utility.Config.GetBool("EnableLog", EnableLog);

                RunDiary(when);
            }
            catch (Exception ex)
            {
                log.Error("An error occured during run diary\n", ex);
            }
        }

        static private void RunDiary(DateTime date)
        {
            if (EnableLog) log.Info("Diary run started on [" + date + "]...");
            DiaryPostingComponent dp = new DiaryPostingComponent();
            dp.RunDiary(date);
            if (EnableLog) log.Info("Diary run completed ...");
        }




    }
}