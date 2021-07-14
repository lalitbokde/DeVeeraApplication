using CRM.Core;
using CRM.Core.Domain.Users;
using CRM.Services.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DeVeeraApp.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        #region field   
        private readonly IAuthenticationService _authenticationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWorkContext _WorkContextService;
        #endregion

        #region 
        public UserController(IHttpContextAccessor httpContextAccessor,
                              IWorkContext WorkContextService,
                              IAuthenticationService authenticationService) : base(
                                    workContext: WorkContextService,
                                    httpContextAccessor: httpContextAccessor,
                                    authenticationService: authenticationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authenticationService = authenticationService;
            _WorkContextService = WorkContextService;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public new IActionResult Logout()
        {
            //standard logout 
            _authenticationService.SignOut();

            //raise logged out event       
            var UserLogOut = new UserLoggedOutEvent(_WorkContextService.CurrentUser);

            if (_WorkContextService.CurrentUser != null)
            {
                _WorkContextService.CurrentUser.UserGuid = new Guid();
            }
            //delete current cookie value
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(".MarketPlaceCRM.User");

            return RedirectToAction("Index", "Home", new { area = "default" });
        }
    }
}
