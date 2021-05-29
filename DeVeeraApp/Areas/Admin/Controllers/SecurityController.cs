using System;
using System.Collections.Generic;
using System.Linq;
using CRM.Core;
using CRM.Core.Domain.Security;
using CRM.Services.Users;
using CRM.Services.Security;
using DeVeeraApp.ViewModels.User;
using DeVeeraApp.ViewModels.Security;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using CRM.Services.Authentication;

namespace DeVeeraApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public partial class SecurityController : BaseController
    {
        #region Fields
        private readonly IWorkContext _workContext;
        private readonly IPermissionService _permissionService;
        private readonly IUserService _UserService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Ctor
        public SecurityController(IWorkContext workContext,
                                  IPermissionService permissionService,
                                  IUserService UserService,
                                  IHttpContextAccessor httpContextAccessor,
                                  IAuthenticationService authenticationService) : base(workContext: workContext,
                                                                                       httpContextAccessor: httpContextAccessor,
                                                                                       authenticationService: authenticationService)
        {
            this._workContext = workContext;
            this._permissionService = permissionService;
            this._UserService = UserService;
            this._httpContextAccessor = httpContextAccessor;
            this._authenticationService = authenticationService;
        }
        #endregion

        #region Methods
        public virtual IActionResult AccessDenied(string pageUrl)
        {
            return View();
        }

        public virtual IActionResult Permissions()
        {
            ViewBag.ActiveMenu = "Users";
            if (HttpContext.Session.GetInt32("isMaintenance") == null)
                return Logout();
          
            //Form Name
            ViewBag.FormName = "Permissions";
            AddBreadcrumbs("Users", "User Permissions ", "/Security/Permissions", "/Security/Permissions");
            //Permissions

            if (!_permissionService.Authorize(StandardPermissionProvider.ManageAcl))
                return AccessDeniedView();


            var model = new PermissionMappingModel();

            var permissionRecords = _permissionService.GetAllPermissionRecords();
            var UserRoles = _UserService.GetAllUserRoles(true);
            foreach (var pr in permissionRecords)
            {
                model.AvailablePermissions.Add(new PermissionRecordModel
                {
                    //Name = pr.Name,
                    Name = pr.Name,
                    SystemName = pr.SystemName
                });
            }
            foreach (var cr in UserRoles)
            {
                model.AvailableUserRoles.Add(new UserRoleModel
                {
                    Id = cr.Id,
                    Name = cr.Name
                });
            }
            foreach (var pr in permissionRecords)
                foreach (var cr in UserRoles)
                {
                    var allowed = pr.PermissionRecord_Role_Mapping.Count(x => x.UserRole.Id == cr.Id) > 0;
                    if (!model.Allowed.ContainsKey(pr.SystemName))
                        model.Allowed[pr.SystemName] = new Dictionary<int, bool>();
                    model.Allowed[pr.SystemName][cr.Id] = allowed;
                }

            return View(model);
        }

        [HttpPost, ActionName("Permissions")]
        public virtual IActionResult PermissionsSave(IFormCollection form)
        {
            ViewBag.ActiveMenu = "Users";
            //Form Name
            ViewBag.FormName = "Permissions";
            AddBreadcrumbs("Users", "User Permissions ", "/Security/Permissions", "/Security/Permissions");
            //Permissions

            if (!_permissionService.Authorize(StandardPermissionProvider.ManageAcl))
                return AccessDeniedView();
            try
            {
                var permissionRecords = _permissionService.GetAllPermissionRecords();
                var UserRoles = _UserService.GetAllUserRoles(true);

                foreach (var cr in UserRoles)
                {
                    var formKey = "allow_" + cr.Id;
                    var permissionRecordSystemNamesToRestrict = !StringValues.IsNullOrEmpty(form[formKey])
                        ? form[formKey].ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList()
                        : new List<string>();

                    foreach (var pr in permissionRecords)
                    {

                        var allow = permissionRecordSystemNamesToRestrict.Contains(pr.SystemName);
                        if (allow)
                        {
                            PermissionRecord_Role_Mapping permissionRecord_Role_Mapping = new PermissionRecord_Role_Mapping()
                            {
                                UserRoleId = cr.Id,
                                PermissionRecordId = pr.Id
                            };
                            if (pr.PermissionRecord_Role_Mapping.FirstOrDefault(x => x.UserRole.Id == cr.Id) == null)
                            {
                                pr.PermissionRecord_Role_Mapping.Add(permissionRecord_Role_Mapping);
                                _permissionService.UpdatePermissionRecord(pr);
                            }
                        }
                        else
                        {
                            if (pr.PermissionRecord_Role_Mapping.FirstOrDefault(x => x.UserRole.Id == cr.Id) != null)
                            {
                                pr.PermissionRecord_Role_Mapping.Remove(pr.PermissionRecord_Role_Mapping.FirstOrDefault(x => x.UserRole.Id == cr.Id));
                                _permissionService.UpdatePermissionRecord(pr);
                            }
                        }
                    }
                }

              //  AddNotification(NotificationMessage.TitleSuccess, NotificationMessage.msgSavePermission, NotificationMessage.TypeSuccess);

                return RedirectToAction("Permissions");
            }
            catch (Exception e)
            {
              //  AddNotification(NotificationMessage.TitleError, NotificationMessage.ErrormsgSavePermission, NotificationMessage.TypeError);

                return View(e);
            }
        }

        #endregion
    }
}