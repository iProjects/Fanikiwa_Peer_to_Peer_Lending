using DotNetOpenAuth.AspNet;
using fanikiwaGL.Entities;
using fCommon.Utility;
using ffWebAdmin.UI.MVC.Filters; 
using ffWebAdmin.UI.MVC.Models;
using fPeerLending.Business;  
using fPeerLending.Entities;
using log4net;
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
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using WebMatrix.WebData;

namespace ffWebAdmin.UI.MVC.Controllers
{
    [Authorize]
    [HandleError]
    public class AdministratorController : Controller
    {

        #region "Roles"
        //[Authorize]
        public ActionResult GetAllRoles()
        {
            AdministratorComponent ac = new AdministratorComponent();
            List<spRole> Roles = ac.GetAllRoles();

            return View("RolesListView", Roles);
        }
        //[Authorize]
        public ActionResult CreateRole()
        {
            spRole model = new spRole();

            return View("CreateRoleView", model);
        }
        [HttpPost]
        public ActionResult CreateRole([Bind] spRole model)
        {
            if (ModelState.IsValid)
            {

                AdministratorComponent ac = new AdministratorComponent();

                spRole role = new spRole();
                role.RoleName = Utils.ConvertFirstLetterToUpper(model.RoleName);

                ac.CreateRole(role);

                return RedirectToAction("GetAllRoles");
            }
            else
            {
                string[] errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray();
                foreach (var err in errors)
                {
                    ModelState.AddModelError(err, err);
                }
                return View("CreateRoleView", model);
            }
        }
        //[Authorize]
        public ActionResult RoleDetails(int id)
        {
            AdministratorComponent ac = new AdministratorComponent();

            spRole model = ac.GetRolebyId(id);

            return View("RoleDetailsView", model);
        }
        //[Authorize]
        public ActionResult EditRole(int id)
        {
            AdministratorComponent ac = new AdministratorComponent();
            spRole model = ac.GetRolebyId(id);
            return View("EditRoleView", model);
        }
        [HttpPost]
        public ActionResult EditRole([Bind] spRole model)
        {
            AdministratorComponent ac = new AdministratorComponent();

            spRole role = ac.GetRolebyId(model.RoleId);
            role.RoleName = Utils.ConvertFirstLetterToUpper(model.RoleName);

            ac.UpdateRole(role);

            return RedirectToAction("GetAllRoles");
        }
        //[Authorize]
        public ActionResult DeleteRole(int id, string description)
        {
            ConfirmDeleteModel model = new ConfirmDeleteModel();
            model.Id = id;
            model.Description = description;

            return View("ConfirmDeleteRoleView", model);
        }
        [HttpPost]
        public ActionResult DeleteRole([Bind] ConfirmDeleteModel model)
        {
            AdministratorComponent ac = new AdministratorComponent();

            spRole role = ac.GetRolebyId(model.Id);
            ac.DeleteRoleById(role.RoleId);

            return RedirectToAction("GetAllRoles");
        }
        #endregion "Roles"
        #region "USers"
        //[Authorize]
        public ActionResult GetAllUSers()
        {
            AdministratorComponent ac = new AdministratorComponent();
            RegistrationComponent rc = new RegistrationComponent();

            spUser model = new spUser();

            var _usersquery = from us in ac.GetAllUsers()
                              join mb in rc.GetRegisteredMembers() on us.MemberId equals mb.MemberId
                              select new spUser
                              {
                                  Password = us.Password,
                                  IsDeleted = us.IsDeleted,
                                  Status = us.Status,
                                  UserName = us.UserName,
                                  UserId = us.UserId,
                                  MemberId = mb.MemberId,
                                  Email = mb.Email
                              };
            List<spUser> _users = _usersquery.ToList();

            return View("UsersListView", _users);
        }
        //[Authorize]
        public ActionResult CreateUSer()
        {
            RegistrationComponent rc = new RegistrationComponent();
            AdministratorComponent dc = new AdministratorComponent();
            spUser model = new spUser();

            var _membersquery = from ep in rc.GetRegisteredMembers()
                                select ep;
            List<Member> _Members = _membersquery.ToList();
            model._Members = _Members;

            return View("CreateUserView", model);
        }
        [HttpPost]
        public ActionResult CreateUSer([Bind] spUser model)
        {
            AdministratorComponent ac = new AdministratorComponent();
            RegistrationComponent rc = new RegistrationComponent();

            Member _registeredMember = rc.GetMemberByID(model.MemberId);
            model.UserName = _registeredMember.Email;

            spUser user = new spUser();
            user.UserName = _registeredMember.Email;
            user.Email = _registeredMember.Email;
            user.MemberId = _registeredMember.MemberId;
            user.Password = model.Password;
            user.ConfirmPassword = model.Password;
            user.Status = "A";
            user.IsDeleted = false;

            var _usersquery = from rl in ac.GetAllUsers()
                              select rl;
            List<spUser> _users = _usersquery.ToList();

            if (_users.Any(i => i.MemberId == user.MemberId))
            {
                ErrorHandlerModel errormodel = new ErrorHandlerModel();
                errormodel.ExceptionMessage = "User Exists!";
                return View("ErrorHandlerView", errormodel);
            }
            if (!_users.Any(i => i.MemberId == user.MemberId))
            {
                //WebSecurity.CreateUserAndAccount(user.UserName, user.Password);
                ac.CreateUser(user);
            }

            return RedirectToAction("GetAllUSers");

        }
        //[Authorize]
        public ActionResult USerDetails(int id)
        {
            AdministratorComponent ac = new AdministratorComponent();
            RegistrationComponent rc = new RegistrationComponent();

            var _userquery = from us in ac.GetAllUsers()
                             where us.UserId == id
                             select new spUser
                             {
                                 IsDeleted = us.IsDeleted,
                                 Status = us.Status,
                                 UserName = us.UserName,
                                 UserId = us.UserId
                             };
            spUser model = _userquery.FirstOrDefault();

            var _membersquery = from ep in rc.GetRegisteredMembers()
                                select ep;
            List<Member> _Members = _membersquery.ToList();
            model._Members = _Members;

            return View("UserDetailsView", model);
        }
        //[Authorize]
        public ActionResult EditUSer(int id)
        {
            AdministratorComponent ac = new AdministratorComponent();
            RegistrationComponent rc = new RegistrationComponent();

            var _userquery = from us in ac.GetAllUsers()
                             where us.UserId == id
                             select new spUser
                             {
                                 IsDeleted = us.IsDeleted,
                                 Status = us.Status,
                                 UserName = us.UserName,
                                 UserId = us.UserId
                             };
            spUser model = _userquery.FirstOrDefault();

            var _membersquery = from ep in rc.GetRegisteredMembers()
                                select ep;
            List<Member> _Members = _membersquery.ToList();
            model._Members = _Members;

            return View("EditUserView", model);
        }
        [HttpPost]
        public ActionResult EditUSer([Bind] spUser model)
        {
            AdministratorComponent ac = new AdministratorComponent();
            spUser user = ac.GetUserbyId(model.UserId);
            user.UserName = model.UserName;
            user.Password = model.Password;
            user.MemberId = model.MemberId;
            user.Status = model.Status;

            ac.UpdateUser(user);

            return RedirectToAction("GetAllUSers");
        }
        //[Authorize]
        public ActionResult DeleteUSer(int id, string description)
        {
            ConfirmDeleteModel model = new ConfirmDeleteModel();
            model.Id = id;
            model.Description = description;

            return View("ConfirmDeleteUserView", model);
        }
        [HttpPost]
        public ActionResult DeleteUSer([Bind] ConfirmDeleteModel model)
        {
            AdministratorComponent ac = new AdministratorComponent();
            spUser user = ac.GetUserbyId(model.Id);

            ac.DeleteUserById(user.UserId);

            return RedirectToAction("GetAllUSers");
        }
        #endregion "USers"
        #region "USersRoles"
        //[Authorize]
        public ActionResult GetAllUSersRoles()
        {
            AdministratorComponent ac = new AdministratorComponent();

            var _usersrolesquery = from ur in ac.GetAllUSersRoles()
                                   join rl in ac.GetAllRoles() on ur.RoleId equals rl.RoleId
                                   join us in ac.GetAllUsers() on ur.UserId equals us.UserId
                                   select new spUsersInRoleModel
                                   {
                                       RoleId = rl.RoleId,
                                       RoleName = rl.RoleName,
                                       UserId = us.UserId,
                                       UserName = us.UserName
                                   };
            List<spUsersInRoleModel> _usersroles = _usersrolesquery.ToList();

            return View("UsersRolesListView", _usersroles);
        }
        [HttpPost]
        public ActionResult GetUSerRoles(int userid)
        {
            AdministratorComponent ac = new AdministratorComponent();
            spUsersInRoleModel model = new spUsersInRoleModel();
            model._Roles = new List<spRole>();
            model._UserRoles = new List<string>();
            model._Users = new List<spUser>();

            var _userrolesquery = from us in ac.GetAllRolesforUser(userid)
                                  select us.RoleId;
            List<int> _userroles = _userrolesquery.ToList();

            var _rolesquery = from rl in ac.GetAllRoles()
                              where _userroles.Contains(rl.RoleId)
                              select rl;
            List<spRole> _roles = _rolesquery.ToList();

            foreach (var role in _roles)
            {
                //model._UserRoles.Add(role.RoleName);
            }

            return View("CreateUserRolesView", model);
        }
        //[HttpPost]
        public ActionResult GetUSerRolesPartial(int userid)
        {
            AdministratorComponent ac = new AdministratorComponent();
            spUsersInRoleModel model = new spUsersInRoleModel();
            model._Roles = new List<spRole>();
            model._UserRoles = new List<string>();
            model._Users = new List<spUser>();

            var _userrolesquery = from us in ac.GetAllRolesforUser(userid)
                                  select us.RoleId;
            List<int> _userroles = _userrolesquery.ToList();

            var _rolesquery = from rl in ac.GetAllRoles()
                              where _userroles.Contains(rl.RoleId)
                              select rl;
            List<spRole> _roles = _rolesquery.ToList();

            foreach (var role in _roles)
            {
                model._UserRoles.Add(role.RoleName);
            }

            return PartialView("PartialUserRolesView", model);
        }
        //[Authorize]
        public ActionResult CreateUSerRole()
        {
            AdministratorComponent ac = new AdministratorComponent();
            spUsersInRoleModel model = new spUsersInRoleModel();

            var _usersquery = from us in ac.GetAllUsers()
                              select new spUser
                              {
                                  IsDeleted = us.IsDeleted,
                                  Status = us.Status,
                                  UserName = us.UserName,
                                  UserId = us.UserId
                              };
            List<spUser> _users = _usersquery.ToList();
            model._Users = _users;

            var _rolesquery = from rl in ac.GetAllRoles()
                              select rl;
            List<spRole> _roles = _rolesquery.ToList();
            model._Roles = _roles;

            return View("CreateUserRolesView", model);
        }
        [HttpPost]
        public ActionResult CreateUSerRole([Bind] spUsersInRoleModel model)
        {
            AdministratorComponent ac = new AdministratorComponent();

            if (ModelState.IsValid)
            {
                spUsersInRoleModel _UsersRolesModel = new spUsersInRoleModel();
                _UsersRolesModel.UserId = model.UserId;
                _UsersRolesModel.RoleId = model.RoleId;

                var _rightsquery = from rl in ac.GetAllUsersInRole()
                                   select rl;
                List<spUsersInRoleModel> _rights = _rightsquery.ToList();

                if (_rights.Any(i => i.UserId == _UsersRolesModel.UserId && i.RoleId == _UsersRolesModel.RoleId))
                {
                    ErrorHandlerModel errormodel = new ErrorHandlerModel();
                    errormodel.ExceptionMessage = "User Role Exists!";
                    return View("ErrorHandlerView", errormodel);
                }

                if (!_rights.Any(i => i.UserId == _UsersRolesModel.UserId && i.RoleId == _UsersRolesModel.RoleId))
                {
                    ac.CreateUSerRole(_UsersRolesModel);
                }

                return RedirectToAction("GetAllUSersRoles");
            }
            else
            {
                var _usersquery = from us in ac.GetAllUsers()
                                  select new spUser
                                  {
                                      IsDeleted = us.IsDeleted,
                                      Status = us.Status,
                                      UserName = us.UserName,
                                      UserId = us.UserId
                                  };
                List<spUser> _users = _usersquery.ToList();
                model._Users = _users;

                var _rolesquery = from rl in ac.GetAllRoles()
                                  select rl;
                List<spRole> _roles = _rolesquery.ToList();
                model._Roles = _roles;

                string[] errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray();
                foreach (var err in errors)
                {
                    ModelState.AddModelError(err, err);
                }
                return View("CreateUserRolesView", model);
            }
        }

        //[Authorize]
        public ActionResult USerRoleDetails(int userid, int roleid)
        {
            AdministratorComponent ac = new AdministratorComponent();

            spUsersInRoleModel model = ac.GetUSerRolebyId(userid, roleid);

            var _usersquery = from us in ac.GetAllUsers()
                              select new spUser
                              {
                                  IsDeleted = us.IsDeleted,
                                  Status = us.Status,
                                  UserName = us.UserName,
                                  UserId = us.UserId
                              };
            List<spUser> _users = _usersquery.ToList();
            model._Users = _users;

            var _rolesquery = from rl in ac.GetAllRoles()
                              select rl;
            List<spRole> _roles = _rolesquery.ToList();
            model._Roles = _roles;

            return View("UserRolesDetailsView", model);
        }

        //[Authorize]
        public ActionResult EditUSerRole(int userid, int roleid)
        {
            AdministratorComponent ac = new AdministratorComponent();
            spUsersInRoleModel model = ac.GetUSerRolebyId(userid, roleid);

            var _usersquery = from us in ac.GetAllUsers()
                              select new spUser
                              {
                                  IsDeleted = us.IsDeleted,
                                  Status = us.Status,
                                  UserName = us.UserName,
                                  UserId = us.UserId
                              };
            List<spUser> _users = _usersquery.ToList();
            model._Users = _users;

            var _rolesquery = from rl in ac.GetAllRoles()
                              select rl;
            List<spRole> _roles = _rolesquery.ToList();
            model._Roles = _roles;

            return View("EditUserRolesView", model);
        }

        [HttpPost]
        public ActionResult EditUSerRole([Bind] spUsersInRoleModel model)
        {
            AdministratorComponent ac = new AdministratorComponent();

            spUsersInRoleModel _UsersRolesModel = ac.GetUSerRolebyId(model.UserId, model.RoleId);
            _UsersRolesModel.UserId = model.UserId;
            _UsersRolesModel.RoleId = model.RoleId;

            ac.UpdateUSerRole(_UsersRolesModel);

            return RedirectToAction("GetAllUSersRoles");
        }

        //[Authorize]
        public ActionResult DeleteUSerRole(int userid, int roleid, string description)
        {
            ConfirmDeleteModel model = new ConfirmDeleteModel();
            model.Id = userid;
            model.Id2 = roleid;
            model.Description = description;

            return View("ConfirmDeleteUserRoleView", model);
        }
        [HttpPost]
        public ActionResult DeleteUSerRole([Bind] ConfirmDeleteModel model)
        {
            AdministratorComponent ac = new AdministratorComponent();

            spUsersInRoleModel _UsersRolesModel = ac.GetUSerRolebyId(model.Id, model.Id2);
            ac.DeleteUserRoleById(_UsersRolesModel.UserId, _UsersRolesModel.RoleId);

            return RedirectToAction("GetAllUSersRoles");
        }

        #endregion "USersRoles"
        #region "Rights"
        //[Authorize]
        public ActionResult GetAllRights()
        {
            AdministratorComponent ac = new AdministratorComponent();

            var _allowedrolemenusquery = from rm in ac.GetAllspAllowedRoleMenus()
                                         join rl in ac.GetAllRoles() on rm.RoleId equals rl.RoleId
                                         join mn in ac.GetAllspMenus() on rm.MenuItemId equals mn.Id
                                         select new spUsersInRoleModel
                                         {
                                             Allowed = rm.Allowed,
                                             MenuDescription = mn.Description,
                                             MenuName = mn.mnuName,
                                             MenuItemId = mn.Id,
                                             RoleId = rl.RoleId,
                                             RoleName = rl.RoleName
                                         };
            List<spUsersInRoleModel> _allowedrolemenus = _allowedrolemenusquery.ToList();

            return View("RightsListView", _allowedrolemenus);
        }
        public bool _IsRoleMenusAllowed(string email, string menuname)
        {
            AdministratorComponent ac = new AdministratorComponent();

            var _allowedrolemenusquery = from rm in ac.GetAllspAllowedRoleMenus()
                                         join rl in ac.GetAllRoles() on rm.RoleId equals rl.RoleId
                                         join mn in ac.GetAllspMenus() on rm.MenuItemId equals mn.Id
                                         join usr in ac.GetAllUSersRoles() on rl.RoleId equals usr.RoleId
                                         join us in ac.GetAllUsers() on usr.UserId equals us.UserId
                                         where us.UserName == email
                                         where mn.mnuName == menuname
                                         select new spUsersInRoleModel
                                         {
                                             Allowed = rm.Allowed,
                                             MenuDescription = mn.Description,
                                             MenuName = mn.mnuName,
                                             MenuItemId = mn.Id,
                                             RoleId = rl.RoleId,
                                             RoleName = rl.RoleName
                                         };
            bool _allowedrolemenus = _allowedrolemenusquery.Select(i => i.Allowed).FirstOrDefault();

            return _allowedrolemenus;
        }
        //[Authorize]
        public ActionResult CreateRight()
        {
            AdministratorComponent ac = new AdministratorComponent();
            spUsersInRoleModel model = new spUsersInRoleModel();

            var _menusquery = from rl in ac.GetAllspMenus()
                              orderby rl.Description ascending
                              select rl;
            List<spMenus> _spMenus = _menusquery.ToList();
            model._Menus = _spMenus;

            var _rolesquery = from rl in ac.GetAllRoles()
                              select rl;
            List<spRole> _roles = _rolesquery.ToList();
            model._Roles = _roles;

            model.Allowed = true;

            return View("CreateRightView", model);
        }
        [HttpPost]
        public ActionResult CreateRight([Bind] spUsersInRoleModel model)
        {
            AdministratorComponent ac = new AdministratorComponent();

            if (ModelState.IsValid)
            {
                spAllowedRoleMenus _right = new spAllowedRoleMenus();
                _right.RoleId = model.RoleId;
                _right.MenuItemId = model.MenuItemId;
                _right.Allowed = model.Allowed;

                var _rightsquery = from rt in ac.GetAllspAllowedRoleMenus()
                                   select new spUsersInRoleModel
                                   {
                                       RightId = rt.Id,
                                       MenuItemId = rt.MenuItemId,
                                       RoleId = rt.RoleId
                                   };
                List<spUsersInRoleModel> _rights = _rightsquery.ToList();

                if (_rights.Any(i => i.RoleId == _right.RoleId && i.MenuItemId == _right.MenuItemId))
                {
                    ErrorHandlerModel errormodel = new ErrorHandlerModel();
                    errormodel.ExceptionMessage = "Right Exists!";
                    return View("ErrorHandlerView", errormodel);
                }

                if (!_rights.Any(i => i.RoleId == _right.RoleId && i.MenuItemId == _right.MenuItemId))
                {
                    ac.CreatespAllowedRoleMenu(_right);
                }

                return RedirectToAction("GetAllRights");
            }
            else
            {
                var _menusquery = from rl in ac.GetAllspMenus()
                                  orderby rl.Description ascending
                                  select rl;
                List<spMenus> _spMenus = _menusquery.ToList();
                model._Menus = _spMenus;

                var _rolesquery = from rl in ac.GetAllRoles()
                                  select rl;
                List<spRole> _roles = _rolesquery.ToList();
                model._Roles = _roles;

                model.Allowed = true;

                string[] errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray();
                foreach (var err in errors)
                {
                    ModelState.AddModelError(err, err);
                }
                return View("CreateRightView", model);
            }
        }

        //[Authorize]
        public ActionResult RightDetails(int id)
        {
            AdministratorComponent ac = new AdministratorComponent();

            var _rightquery = from rt in ac.GetAllspAllowedRoleMenus()
                              where rt.Id == id
                              select new spUsersInRoleModel
                              {
                                  RightId = rt.Id,
                                  MenuItemId = rt.MenuItemId,
                                  RoleId = rt.RoleId
                              };
            spUsersInRoleModel model = _rightquery.FirstOrDefault();

            var _menusquery = from rl in ac.GetAllspMenus()
                              select rl;
            List<spMenus> _spMenus = _menusquery.ToList();
            model._Menus = _spMenus;

            var _rolesquery = from rl in ac.GetAllRoles()
                              select rl;
            List<spRole> _roles = _rolesquery.ToList();
            model._Roles = _roles;

            return View("RightDetailsView", model);
        }

        //[Authorize]
        public ActionResult EditRight(int id)
        {
            AdministratorComponent ac = new AdministratorComponent();

            var _rightquery = from rt in ac.GetAllspAllowedRoleMenus()
                              where rt.Id == id
                              select new spUsersInRoleModel
                              {
                                  RightId = rt.Id,
                                  MenuItemId = rt.MenuItemId,
                                  RoleId = rt.RoleId
                              };
            spUsersInRoleModel model = _rightquery.FirstOrDefault();

            var _menusquery = from rl in ac.GetAllspMenus()
                              select rl;
            List<spMenus> _spMenus = _menusquery.ToList();
            model._Menus = _spMenus;

            var _rolesquery = from rl in ac.GetAllRoles()
                              select rl;
            List<spRole> _roles = _rolesquery.ToList();
            model._Roles = _roles;

            return View("EditRightView", model);
        }

        [HttpPost]
        public ActionResult EditRight([Bind] spUsersInRoleModel model)
        {
            AdministratorComponent ac = new AdministratorComponent();

            spAllowedRoleMenus _Right = ac.GetspAllowedRoleMenusbyId(model.RightId);
            _Right.RoleId = model.RoleId;
            _Right.MenuItemId = model.MenuItemId;
            _Right.Allowed = model.Allowed;

            ac.UpdatespAllowedRoleMenus(_Right);

            return RedirectToAction("GetAllRights");
        }

        //[Authorize]
        public ActionResult DeleteRight(int roleid, int menuid, string description)
        {
            ConfirmDeleteModel model = new ConfirmDeleteModel();
            model.Id = roleid;
            model.Id2 = menuid;
            model.Description = description;

            return View("ConfirmDeleteRightView", model);
        }
        [HttpPost]
        public ActionResult DeleteRight([Bind] ConfirmDeleteModel model)
        {
            AdministratorComponent ac = new AdministratorComponent();

            spAllowedRoleMenus _Right = ac.GetRight(model.Id, model.Id2);
            ac.DeletespAllowedRoleMenusById(_Right.Id);

            return RedirectToAction("GetAllRights");
        }

        #endregion "Rights"



    }
}