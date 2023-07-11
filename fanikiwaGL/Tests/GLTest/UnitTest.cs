using DiaryProcessing;
using fanikiwaGL.Business;
using fanikiwaGL.Data;
using fanikiwaGL.Entities;
using fanikiwaGL.Framework;
using fanikiwaGL.Framework.ExceptionHandlers;
using fanikiwaGL.Framework.ExceptionTypes;
using fanikiwaGL.Services;
using fanikiwaGL.Services.Contracts;
using fCommon.Utility;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace GLTest
{
    [TestClass]
    public class UnitTest
    {
        
        [TestMethod]
        public void TestGetAccount()
        {

           // DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            AccountDAC acDac = new AccountDAC();
            Account acc = acDac.SelectById(100);
            acc.LimitFlag =(short) AccountLimitStatus.PostingOverDrawingProhibited;
            acc.PassFlag = (short) PassFlag.Locked;
            
            decimal amount = -4000;
            acDac.UpdateById(acc);
           
            Transaction drtransaction = new Transaction();
            drtransaction.AccountID = acc.AccountID;
            
            drtransaction.TransactionTypeId =Config.GetInt("MPESAWITHDRAWALTRANSACTIONTYPE");

            drtransaction.Amount =  amount * -1;
            drtransaction.PostDate = DateTime.Today;
            drtransaction.RecordDate = DateTime.Today;

            drtransaction.ForcePostFlag = true;
            drtransaction.StatementFlag = "Y";
            drtransaction.Authorizer = "SYSTEM";
            drtransaction.UserID = "SYs";
            drtransaction.Reference = Config.GetString("FANIKIWAAGENT");
            drtransaction.ContraReference = "Ref1";

            FinancialPostingComponent fc = new FinancialPostingComponent();
           // fc.PostSingle(drtransaction);
            System.Diagnostics.Debug.WriteLine("Result = " );
        }

        [TestMethod]
        public void Test_run_diary()
        {
            DiaryPostingComponent dp = new DiaryPostingComponent();
            dp.RunDiary(DateTime.Now); 
        }




    }
}