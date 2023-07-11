using fanikiwaGL.Entities;
using fCommon.Utility;
using ffWebAdmin.UI.MVC.Filters;
using ffWebAdmin.UI.MVC.Models;
using fPeerLending.Business;
using fPeerLending.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ffWebAdmin.UI.MVC.Controllers
{
    [HandleError]
    public class HelpController : Controller
    {
        //
        // GET: /Help/

        public ActionResult Help()
        {
            return View();
        }


    }
}
