
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services.Authentication
{
    public static class AutoDataImportCookieAuthenticationDefaults
    {
        /// <summary>
        /// The default value used for authentication scheme
        /// </summary>
        public const string AuthenticationScheme = "Authentication";

        /// <summary>
        /// The default value used for external authentication scheme
        /// </summary>
        public const string ExternalAuthenticationScheme = "ExternalAuthentication";

        /// <summary>
        /// The prefix used to provide a default cookie name
        /// </summary>
        public static readonly string CookiePrefix = ".CRM.";

        /// <summary>
        /// The issuer that should be used for any claims that are created
        /// </summary>
        public static readonly string ClaimsIssuer = "marketPlaceCRM";

        /// <summary>
        /// The default value for the login path
        /// </summary>
        public static readonly string LoginPath = "/User/Login/";

        /// <summary>
        /// The default value used for the logout path
        /// </summary>
        public static readonly string LogoutPath = "/logout";

        /// <summary>
        /// The default value for the access denied path
        /// </summary>
        public static readonly string AccessDeniedPath = "/page-not-found";

        /// <summary>
        /// The default value of the return URL parameter
        /// </summary>
        public static readonly string ReturnUrlParameter = "";
    }
}
