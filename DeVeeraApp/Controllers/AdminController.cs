using CRM.Core;
using CRM.Core.Domain.Users;
using CRM.Services.Authentication;
using CRM.Services.Message;
using CRM.Services.Users;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels.Admin;
using DeVeeraApp.ViewModels.Common;
using DeVeeraApp.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace DeVeeraApp.Controllers
{
    public class AdminController : BaseController
    {
        #region fields
        private readonly IUserService _UserService;
        private readonly IUserPasswordService _userPasswordService;
        private readonly INotificationService _notificationService;
        #endregion

        #region ctor
        public AdminController(IUserService userService,
                               IUserPasswordService userPasswordService, 
                               IWorkContext workContext,
                               IHttpContextAccessor httpContextAccessor,
                               IAuthenticationService authenticationService,
                               INotificationService notificationService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _UserService = userService;
            _userPasswordService = userPasswordService;
            _notificationService = notificationService;
        }

        #endregion



        #region Methods


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {

            AddBreadcrumbs("Admin", "Create", "/Admin/Create", "/Admin/Create");

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateAdminModel model)
        {
            AddBreadcrumbs("Admin", "Create", "/Admin/Create", "/Admin/Create");
            string userRoleName = "Admin";
            if (ModelState.IsValid)
            {
                if (_UserService.GetUserByEmail(model.Email) == null)
                {
                    var user = model.ToEntity<User>();
                    UserPassword password = null;
                    user.UserGuid = Guid.NewGuid();
                    user.CreatedOnUtc = DateTime.UtcNow;
                    user.LastActivityDateUtc = DateTime.UtcNow;
                    user.RegistrationComplete = true;
                    user.TwoFactorAuthentication = true;
                    var userRoleData = _UserService.GetAllUserRoles();
                    foreach (var item in userRoleData)
                    {
                        if (item.Name == userRoleName)
                        {
                            user.UserRoleId = item.Id;
                        }
                    }
                    user.Active = true;

                    _UserService.InsertUser(user);

                    // password
                    if (!string.IsNullOrWhiteSpace(model.UserPassword.Password))
                    {

                        password = new UserPassword
                        {
                            UserId = user.Id,
                            Password = model.UserPassword.Password,
                            CreatedOnUtc = DateTime.UtcNow,
                        };
                        _userPasswordService.InsertUserPassword(password);
                    }
                    _UserService.UpdateUser(user);

                    _notificationService.SuccessNotification("New Admin has been added successfully.");
                    return RedirectToAction("List", "Admin", new { roleName = userRoleName });

                }
                else
                {
                    ModelState.AddModelError("Email", "Email Already Exists");

                }
            }

            return View(model);

        }
        public IActionResult List(string roleName)
        {

            var UserRole = _UserService.GetUserRoleByRoleName(roleName);

            AddBreadcrumbs($"{UserRole.Name}", "List", $"/Admin/List?roleName={roleName}", $"/Admin/List?roleName={roleName}");

            var model = new List<UserModel>();
            var data = _UserService.GetUserByUserRoleId(UserRole.Id);
            if(data.Count() != 0)
            {
                model = data.ToList().ToModelList<User, UserModel>(model);

               
                ViewBag.roleName = roleName;
            return View(model);

            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit(int id)
        {
            AddBreadcrumbs("Admin", "Edit", $"/Admin/Edit/{id}", $"/Admin/Edit/{id}");

            if (id != 0)
            {
                var data = _UserService.GetUserById(id);

                if(data != null)
                {
                    var userdata = data.ToModel<CreateAdminModel>();
                    return View(userdata);
                }

                return View();
            }
            return View();
             
        }

        [HttpPost]
        public IActionResult Edit(CreateAdminModel model)
        {
            AddBreadcrumbs("Level", "List", "/UploadVideo/List", "/UploadVideo/List");
            string userRoleName = "Admin";

            if (ModelState.IsValid)
            {
                var admin = _UserService.GetUserById(model.Id);

                admin.Email = model.Email;

                _UserService.UpdateUser(admin);
                UserPassword password = null;
                admin.LastActivityDateUtc = DateTime.UtcNow;
                var userRoleData = _UserService.GetAllUserRoles();

                foreach (var item in userRoleData)
                { 
                    if (item.Name == userRoleName)
                    {
                        admin.UserRoleId = item.Id;
                    }
                }
                admin.Active = true;

                _UserService.UpdateUser(admin);

                // password
                if (!string.IsNullOrWhiteSpace(model.UserPassword.Password))
                {
                    var adminPassword = _userPasswordService.GetPasswordByUserId(admin.Id);
                    adminPassword.Password = model.UserPassword.Password;
                    adminPassword.UserId = model.Id;
                    _userPasswordService.UpdatePassword(adminPassword);
                }
                _UserService.UpdateUser(admin);

                _notificationService.SuccessNotification(" Admin has been edited successfully.");

                return RedirectToAction("List", "Admin", new { roleName = userRoleName });
            }

            return View(model);

        }

        public IActionResult Delete(int userId)
        {
            ResponseModel response = new ResponseModel();

            if (userId != 0)
            {
                var userData = _UserService.GetUserById(userId);
                if (userData == null)
                {
                    response.Success = false;
                    response.Message = "No user found";
                }
                else
                {
                    userData.Deleted = true;
                    response.Success = true;
                    _UserService.UpdateUser(userData);
                }
            }
            else
            {
                response.Success = false;
                response.Message = "userId is 0";

            }
            return Json(response);


        }

        #endregion
    }
}
