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
    public class MembersController : Controller
    {
        //
        // GET: /Members/
        [Authorize]
        public ActionResult RegisteredMembers()
        {
            RegistrationComponent rc = new RegistrationComponent();
            List<RegisteredMembers> _registeredMembers = new List<RegisteredMembers>();

            var _membersquery = from sm in rc.GetRegisteredMembers()
                                select sm;
            List<Member> _members = _membersquery.ToList();

            foreach (var member in _members)
            {
                RegisteredMembers _regMember = new RegisteredMembers();
                _regMember.EmailAddress = member.Email;

                if (!_registeredMembers.Any(i => i.EmailAddress == _regMember.EmailAddress))
                {
                    _registeredMembers.Add(_regMember);
                }
            }

            return View(_registeredMembers);
        }



    }
}
