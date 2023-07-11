using DotNetOpenAuth.AspNet;
using fanikiwaGL.Entities;
using fCommon.Utility;
using ffWebAdmin.UI.MVC.Filters;
using ffWebAdmin.UI.MVC.Models;
using fPeerLending.Business;
using fPeerLending.Entities;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.ExtraInformation;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Web.WebPages.OAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using WebMatrix.WebData;

namespace ffWebAdmin.UI.MVC.Controllers
{
    [Authorize]
    [HandleError]
    public class TransactionController : Controller
    {

        [Authorize]
        public ActionResult FanikiwaTransactions()
        {
            RegistrationComponent rc = new RegistrationComponent();
            TransactionsComponent tc = new TransactionsComponent();
            List<TransactionModel> txnsView = new List<TransactionModel>();
            List<TransactionModel> model = new List<TransactionModel>();

            var _txnsquery = from tx in tc.GetAllTransactions()
                             select tx;
            List<fanikiwaGL.Entities.Transaction> txns = _txnsquery.ToList();

            //go through the transactins and compute running balance
            decimal amount = 0M;
            decimal bal = amount;
            foreach (var txn in txns)
            {
                TransactionModel txnv = new TransactionModel();
                txnv.PostDate = txn.PostDate;
                txnv.TransactionID = txn.TransactionID;
                txnv.Narrative = txn.Narrative;

                if (txn.Amount > 0)
                {
                    txnv.Credit = txn.Amount;
                    txnv.Debit = 0;
                }
                else
                {
                    txnv.Credit = 0;
                    txnv.Debit = txn.Amount;
                }

                bal += txn.Amount;
                txnv.Balance = bal;

                //add to view
                txnsView.Add(txnv);

            }

            model = txnsView.ToList();

            return View(model);
        }
        [Authorize]
        public ActionResult FanikiwaAccounts()
        {
            AccountsComponent ac = new AccountsComponent();

            var _accountsquery = from tx in ac.GetAllAccounts()
                                 select tx;
            List<Account> model = _accountsquery.ToList();

            return View(model);
        }




    }
}