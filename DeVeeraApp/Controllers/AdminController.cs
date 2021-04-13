using CRM.Core.Domain.Users;
using CRM.Services.Users;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels.Common;
using DeVeeraApp.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace DeVeeraApp.Controllers
{
    public class AdminController : Controller
    {
        #region fields
        private readonly IUserService _UserService;
        private readonly IUserPasswordService _userPasswordService;
        #endregion

        #region ctor
        public AdminController(IUserService userService,
                       IUserPasswordService userPasswordService)
        {
            _UserService = userService;
            _userPasswordService = userPasswordService;
        }

        #endregion



        #region Methods


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserModel model)
        {
            if (ModelState.IsValid)
            {
                //fill entity from model
                var user = model.ToEntity<User>();
                UserPassword password = null;
                user.UserGuid = Guid.NewGuid();
                user.CreatedOnUtc = DateTime.UtcNow;
                user.LastActivityDateUtc = DateTime.UtcNow;
                user.UserRoleId = 2;
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


                return RedirectToAction("List");
            }

            return View(model);

        }
        public IActionResult List(int roleId)
        {
            var model = new List<UserModel>();
            var data = _UserService.GetUserByUserRoleId(roleId);
            if(data.Count() != 0)
            {
                foreach (var item in data)
                {
                    model.Add(item.ToModel<UserModel>());
                }
                ViewBag.RoleId = roleId;
            return View(model);

            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit(int id)
        {
            if(id != 0)
            {
                var data = _UserService.GetUserById(id);

                if(data != null)
                {
                    var userdata = data.ToModel<UserModel>();
                    return View(userdata);
                }

                return View();
            }
            return View();
             
        }

        [HttpPost]
        public IActionResult Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                //fill entity from model
                var user = model.ToEntity<User>();
                UserPassword password = null;
                user.UserGuid = Guid.NewGuid();
                user.CreatedOnUtc = DateTime.UtcNow;
                user.LastActivityDateUtc = DateTime.UtcNow;
                user.UserRoleId = 2;
                user.Active = true;

                _UserService.UpdateUser(user);

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


                return RedirectToAction("List");
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

        #endregion
    }
}
