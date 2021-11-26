using CRM.Core;
using CRM.Core.Domain;
using CRM.Core.Domain.Users;
using CRM.Services.Authentication;
using CRM.Services.Message;
using CRM.Services.Settings;
using CRM.Services.Users;
using DeVeeraApp.Filters;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels;
using DeVeeraApp.ViewModels.Admin;
using DeVeeraApp.ViewModels.Common;
using DeVeeraApp.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DeVeeraApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizeAdmin]
    public class AdminController : BaseController
    {
        #region fields
        private readonly IUserService _UserService;
        private readonly IUserPasswordService _userPasswordService;
        private readonly INotificationService _notificationService;
        private readonly IWorkContext _workContext;
        private readonly ISettingService _settingService;
        #endregion

        #region ctor
        public AdminController(IUserService userService,
                               IUserPasswordService userPasswordService,
                               IWorkContext workContext,
                               IHttpContextAccessor httpContextAccessor,
                               IAuthenticationService authenticationService,
                               ISettingService settingService,
                               INotificationService notificationService) : base(workContext: workContext,
                                                                                  httpContextAccessor: httpContextAccessor,
                                                                                  authenticationService: authenticationService)
        {
            _UserService = userService;
            _userPasswordService = userPasswordService;
            _notificationService = notificationService;
            _workContext = workContext;
            _settingService = settingService;
        }

        #endregion



        #region Methods


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            string roleName = "Admin";
            AddBreadcrumbs("Admin", "Create", $"/Admin/Admin/List/?{roleName}", "/Admin/Admin/Create");

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateAdminModel model, string email, string passwordd)
        {
            string userRoleName = "Admin";
            AddBreadcrumbs("Admin", "Create", $"/Admin/Admin/List/?{userRoleName}", "/Admin/Admin/Create");

            if (ModelState.IsValid)
            {
                if (_UserService.GetUserByEmail(model.Email) == null)
                {
                    var user = model.ToEntity<User>();
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
                    user.IsAllow = true;
                    _UserService.InsertUser(user);

                    // password
                    if (!string.IsNullOrWhiteSpace(model.UserPassword.Password))
                    {

                        UserPassword password = new UserPassword
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
                    return View(model);
                }
            }

            return View(model);


        }
        public IActionResult List(string roleName)
        {

            if (roleName == null)
            {
                roleName = "Admin";
            }
            var UserRole = _UserService.GetUserRoleByRoleName(roleName);
            
            AddBreadcrumbs($"{UserRole.Name}", "List", $"/Admin/Admin/List?roleName={roleName}", $"/Admin/Admin/List?roleName={roleName}");

            var model = new List<UserModel>();
            
            

            var data = _UserService.GetUserByUserRoleId(UserRole.Id);
            if (data.Count() != 0)
            {
                model = data.ToList().ToModelList<User, UserModel>(model);
              
                ViewBag.roleName = roleName;
                ViewBag.Admin = JsonConvert.SerializeObject(model);
                return View(model);

            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit(int id)
        {
            string roleName = "Admin";
            //AddBreadcrumbs("Admin", "Edit", $"/Admin/Edit/{id}", $"/Admin/Edit/{id}");
            AddBreadcrumbs("Admin", "Edit", $"/Admin/Admin/List/?{roleName}", $"/Admin/Admin/Edit/{id}");
            if (id != 0)
            {
                var data = _UserService.GetUserById(id);

                if (data != null)
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
                _UserService.DeleteUser(userData);

                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.Message = "userId is 0";

            }
            return Json(response);


        }



        public IActionResult CreateUser()
        {
            string roleName = "User";
            AddBreadcrumbs("User", "CreateUser", $"/Admin/Admin/List/?roleName={roleName}", "/Admin/Admin/CreateUser");

            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUserModel model)
        {
            string userRoleName = "User";
            AddBreadcrumbs("User", "CreateUser", $"/Admin/Admin/UserList/?{userRoleName}", "/Admin/Admin/CreateUser");

            if (ModelState.IsValid)
            {
                if (_UserService.GetUserByEmail(model.Email) == null)
                {
                    var user = model.ToEntity<User>();
                    user.UserGuid = Guid.NewGuid();
                    user.CreatedOnUtc = DateTime.UtcNow;
                    user.LastActivityDateUtc = DateTime.UtcNow;
                    user.RegistrationComplete = true;
                    user.TwoFactorAuthentication = true;
                   // user.UserRole.Name = model.UserRole.Name;
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

                        UserPassword password = new UserPassword
                        {
                            UserId = user.Id,
                            Password = model.UserPassword.Password,
                            CreatedOnUtc = DateTime.UtcNow,
                        };
                        _userPasswordService.InsertUserPassword(password);
                    }
                    _UserService.UpdateUser(user);

                    _notificationService.SuccessNotification("New User has been added successfully.");

                    return RedirectToAction("List", "Admin", new { roleName = userRoleName });
                }
                else
                {
                    ModelState.AddModelError("Email", "User Already Exists");
                    return View(model);
                }
            }

            return View(model);
        }

        public IActionResult EditUser(int id)
        {
            string roleName = "User";
            //AddBreadcrumbs("Admin", "Edit", $"/Admin/Edit/{id}", $"/Admin/Edit/{id}");

            AddBreadcrumbs("User", "EditUser", $"/Admin/Admin/List/?roleName={roleName}", $"/Admin/Admin/EditUser/{id}");
            if (id != 0)
            {
                var data = _UserService.GetUserById(id);

                if (data != null)
                {
                    var userdata = data.ToModel<CreateUserModel>();
                    return View(userdata);
                }

                return View();
            }
            return View();

        }

        [HttpPost]
        public IActionResult EditUser(CreateUserModel model)
        {
            string userRoleName = "User";
            AddBreadcrumbs("Admin", "EditUser", $"/Admin/Admin/List/?{userRoleName}", "/Admin/Admin/EditUser");


            if (ModelState.IsValid)
            {
                var admin = _UserService.GetUserById(model.Id);

                admin.Email = model.Email;
                admin.MobileNumber = model.MobileNumber;
                admin.IsAllow = model.IsAllow;


                _UserService.UpdateUser(admin);

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

                _notificationService.SuccessNotification(" User has been edited successfully.");

                return RedirectToAction("List", "Admin", new { roleName = userRoleName });
            }

            return View(model);

        }


        #endregion

        [HttpPost]
        public IActionResult Languange(UserModel model)
        {
            if (model.LandingPageModel.Language.Id != 0)
            {
                TempData["LangaugeId"] = null;
                if (Request.Cookies["SessionLangId"] != null)
                {
                    Response.Cookies.Delete("SessionLangId");
                }

                if (model.Id != 0)
                {
                    var userLanguage = _settingService.GetAllSetting().Where(s => s.UserId == model.Id).FirstOrDefault();
                    if (userLanguage != null)
                    {
                        userLanguage.UserId = model.Id;
                        userLanguage.LanguageId = model.LandingPageModel.Language.Id;
                        _settingService.UpdateSetting(userLanguage);
                    }
                    else
                    {
                        var settingData = new Setting
                        {
                            UserId = model.Id,
                            LanguageId = model.LandingPageModel.Language.Id
                        };
                        _settingService.InsertSetting(settingData);
                    }
                }
                else
                {
                    var guestLanguage = _settingService.GetSetting();
                    if (guestLanguage != null)
                    {
                        guestLanguage.LanguageId = model.LandingPageModel.Language.Id;
                        _settingService.UpdateSetting(guestLanguage);
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                }

            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
    }
}
