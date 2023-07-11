using fanikiwaGL.Entities;
using fCommon.Utility;
using ffWebAdmin.UI.MVC.Filters;
using ffWebAdmin.UI.MVC.Models;
using fPeerLending.Business;
using fPeerLending.Entities;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ffWebAdmin.UI.MVC.Controllers
{
    [HandleError]
    public class OffersController : Controller
    { 
        #region "Offers" 
        [Authorize]
        public ActionResult Offers()
        { 
                ListOffersComponent lc = new ListOffersComponent();
                RegistrationComponent rc = new RegistrationComponent();

                List<Offer> offers = lc.GetAllOffers();

                foreach (var offer in offers)
                {
                    Member _offerowner = rc.GetMemberByID(offer.MemberId);
                    if (_offerowner != null)
                    {
                        ViewBag.OfferOwner = _offerowner.Email + " \n" + _offerowner.Surname + " \n" + _offerowner.OtherNames;
                    }
                }

                //Display the offers   
                return View(offers); 
        } 
        #endregion "Offers"

    }
     


}
