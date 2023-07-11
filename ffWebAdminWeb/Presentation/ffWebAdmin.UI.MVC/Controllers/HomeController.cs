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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "For System Administrators";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }
        public ActionResult Help()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult SiteMap()
        {
            ViewBag.Message = "";

            return View();
        }
    }
}
