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
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace ffWebAdmin.UI.MVC.Controllers
{
    [Authorize]
    [HandleError]
    public class TransactionTypeController : Controller
    {
        [Authorize]
        public ActionResult GetAllTransactionTypes()
        {
            return RedirectToAction("TransactionTypes");
        }
        [Authorize]
        public ActionResult TransactionTypes()
        {
            TransactionsComponent tc = new TransactionsComponent();

            var _transactiontypesquery = from tx in tc.GetAllTransactionTypes()
                                         select tx;
            List<TransactionType> model = _transactiontypesquery.ToList();

            //Display the Transactions
            return View(model);
        }
        [Authorize]
        public ActionResult CreateTransactionType()
        {
            TransactionType model = new TransactionType();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateTransactionType(TransactionType model)
        {
            TransactionsComponent tc = new TransactionsComponent();

            TransactionType _TransactionType = model;
            tc.CreateTransactionType(_TransactionType);

            return RedirectToAction("GetAllTransactionTypes");
        }
        [Authorize]
        public ActionResult EditTransactionType(int id)
        {
            TransactionsComponent tc = new TransactionsComponent();
            TransactionType model = tc.SelectTransactionTypeById(id);
            List<TransactionType> CommissionTransactionTypes = tc.GetAllTransactionTypes();
            ViewBag.CommissionTransactionTypes = CommissionTransactionTypes;

            return View(model);
        }
        [HttpPost]
        public ActionResult EditTransactionType(TransactionType model)
        {
            TransactionsComponent tc = new TransactionsComponent();

            TransactionType _TransactionType = model;
            tc.UpdateTransactionType(_TransactionType);

            return RedirectToAction("GetAllTransactionTypes");
        }




    }
}