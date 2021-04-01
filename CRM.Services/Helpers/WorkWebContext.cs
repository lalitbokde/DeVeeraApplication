using CRM.Core;
using CRM.Core.Domain.Users;
using CRM.Services.Authentication;
using CRM.Services.Users;

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services.Helpers
{
    public partial class WebWorkContext: IWorkContext
    {
        #region Const
       
        private const string User_COOKIE_NAME = ".MarketPlaceCRM.User";

        #endregion

        #region Fields

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IAuthenticationService _authenticationService;

        private readonly IUserService _UserService;




        private readonly IUserAgentHelper _userAgentHelper;



        private User _cachedUser;
        private User _originalUserIfImpersonated;


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpContextAccessor">HTTP context accessor</param>
        /// <param name="currencySettings">Currency settings</param>
        /// <param name="authenticationService">Authentication service</param>
        /// <param name="currencyService">Currency service</param>
        /// <param name="UserService">User service</param>
        /// <param name="genericAttributeService">Generic attribute service</param>
        /// <param name="languageService">Language service</param>
        /// <param name="storeContext">Store context</param>
        /// <param name="storeMappingService">Store mapping service</param>
        /// <param name="userAgentHelper">User gent helper</param>
        /// <param name="vendorService">Vendor service</param>
        /// <param name="localizationSettings">Localization settings</param>
        /// <param name="taxSettings">Tax settings</param>
        public WebWorkContext(IHttpContextAccessor httpContextAccessor,

            IAuthenticationService authenticationService,

            IUserService UserService,


            IUserAgentHelper userAgentHelper
          )
        {
            this._httpContextAccessor = httpContextAccessor;

            this._authenticationService = authenticationService;

            this._UserService = UserService;

            this._userAgentHelper = userAgentHelper;

        }

        #endregion

        #region Utilities

        /// <summary>
        /// Get nop User cookie
        /// </summary>
        /// <returns>String value of cookie</returns>
        protected virtual string GetUserCookie()
        {
            return _httpContextAccessor.HttpContext?.Request?.Cookies[User_COOKIE_NAME];
        }

        /// <summary>
        /// Set nop User cookie
        /// </summary>
        /// <param name="UserGuid">Guid of the User</param>
        protected virtual void SetUserCookie(Guid UserGuid)
        {
            if (_httpContextAccessor.HttpContext?.Response == null)
                return;

            //delete current cookie value
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(User_COOKIE_NAME);

            //get date of cookie expiration
            var cookieExpires = 24 * 365; //TODO make configurable
            var cookieExpiresDate = DateTime.Now.AddHours(cookieExpires);

            //if passed guid is empty set cookie as expired
            if (UserGuid == Guid.Empty)
                cookieExpiresDate = DateTime.Now.AddMonths(-1);

            //set new cookie value
            var options = new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = true,
                Expires = cookieExpiresDate
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(User_COOKIE_NAME, UserGuid.ToString(), options);
        }







        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current User
        /// </summary>
        public virtual User CurrentUser
        {
            get
            {
                //whether there is a cached value
                if (_cachedUser != null)
                    return _cachedUser;

                User User = null;

                //check whether request is made by a background (schedule) task
                if (_httpContextAccessor.HttpContext == null)
                {
                    //in this case return built-in User record for background task
                    User = _UserService.GetUserBySystemName(SystemUserNames.BackgroundTask);
                }

                //if (User == null || User.Deleted || !User.Active || User.RequireReLogin)
                //{
                //    //check whether request is made by a search engine, in this case return built-in User record for search engines
                //    if (_userAgentHelper.IsSearchEngine())
                //        User = _UserService.GetUserBySystemName(SystemUserNames.SearchEngine);
                //}

                if (User == null || User.Deleted || !User.Active || User.RequireReLogin)
                {
                    //try to get registered user
                    User = _authenticationService.GetAuthenticatedUser();

                }


                if (User == null || User.Deleted || !User.Active || User.RequireReLogin)
                {
                    //get guest User
                    var UserCookie = GetUserCookie();
                    if (!string.IsNullOrEmpty(UserCookie))
                    {
                        if (Guid.TryParse(UserCookie, out Guid UserGuid))
                        {
                            //get User from cookie (should not be registered)
                            var UserByCookie = _UserService.GetUserByGuid(UserGuid);
                            if (UserByCookie != null && !UserByCookie.IsRegistered())
                                User = UserByCookie;
                        }
                    }
                }

                if (User == null || User.Deleted || !User.Active || User.RequireReLogin)
                {
                    //create guest if not exists
                   // User = _UserService.InsertGuestUser();
                }


                //if (User == null || User.Deleted || !User.Active || User.RequireReLogin)
                //{
                //    //create guest if not exists
                //    User = _UserService.InsertGuestUser();
                //}

                if (User!=null)
                {
                    //set User cookie
                    SetUserCookie(User.UserGuid);

                    //cache the found User
                    _cachedUser = User;
                }

                return _cachedUser;
            }
            set
            {
                SetUserCookie(value.UserGuid);
                _cachedUser = value;
            }
        }

        /// <summary>
        /// Gets the original User (in case the current one is impersonated)
        /// </summary>
        public virtual User OriginalUserIfImpersonated
        {
            get { return _originalUserIfImpersonated; }
        }


        /// <summary>
        /// Gets or sets value indicating whether we're in admin area
        /// </summary>
        public virtual bool IsAdmin { get; set; }

      

        #endregion
    }
}
