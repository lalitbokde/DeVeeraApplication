using System;
using CRM.Core;
using CRM.Core.Domain.Users;

using CRM.Services.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeVeeraApp.Controllers
{

    public class BaseController : Controller
    {
        #region fields

        private readonly IWorkContext _workContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticationService _authenticationService;

        public BaseController(
            IWorkContext workContext,
            IHttpContextAccessor httpContextAccessor,
            IAuthenticationService authenticationService)
        {
            _workContext = workContext;
            _httpContextAccessor = httpContextAccessor;
            _authenticationService = authenticationService;
        }

        #endregion

        


        public virtual void AddNotification(string title, string message, string type)
        {
            TempData["title"] = title;
            TempData["message"] = message;
            TempData["type"] = type;
        }

        public virtual void AddBreadcrumbs(string Page, string Action, string PageUrl, string ActionUrl)
        {
            ViewBag.Page = Page;
            ViewBag.Action = Action;
            ViewBag.PageUrl = PageUrl;
            ViewBag.ActionUrl = ActionUrl;
        }

        public virtual void SetActiveMenu(string MenuName)
        {
            ViewBag.InventoryMenu = "";
            ViewBag.WarehouseTransactionMenu = "";
            ViewBag.DeliveryRequestsMenu = "";
            ViewBag.LocationMapppingMenu = "";
            ViewBag.UserManagementMenu = "";
            ViewBag.SystemControlsMenu = "";
            switch (MenuName)
            {
                case "InventoryMenu":
                    ViewBag.InventoryMenu = "active";
                    break;
                case "WarehouseTransactionMenu":
                    ViewBag.WarehouseTransactionMenu = "active";
                    break;
                case "DeliveryRequestsMenu":
                    ViewBag.DeliveryRequestsMenu = "active";
                    break;
                case "LocationMapppingMenu":
                    ViewBag.LocationMapppingMenu = "active";
                    break;
                case "UserManagementMenu":
                    ViewBag.UserManagementMenu = "active";
                    break;
                case "SystemControlsMenu":
                    ViewBag.UserManagementMenu = "active";
                    break;
            }
        }

        /// <summary>
        /// Access denied view
        /// </summary>
        /// <returns>Access denied view</returns>
        protected virtual IActionResult AccessDeniedView()
        {
            //return Challenge();
            return RedirectToAction("AccessDenied", "Security");
        }

        public virtual IActionResult Logout()
        {
            //standard logout 
            _authenticationService.SignOut();

            //raise logged out event       
            var UserLogOut = new UserLoggedOutEvent(_workContext.CurrentUser);

            if (_workContext.CurrentUser != null)
            {
                _workContext.CurrentUser.UserGuid = new Guid();
            }
            //delete current cookie value
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(".3HappyPals.User");

            return RedirectToAction("Login", "User");
         

        }
    }
}