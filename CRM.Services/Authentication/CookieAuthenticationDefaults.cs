namespace CRM.Services.Authentication
{
    public static class CookieAuthenticationDefaults
    {
        /// <summary>
        /// The default value used for authentication scheme
        /// </summary>
        public const string AuthenticationScheme = "Authentication";


        /// <summary>
        /// The prefix used to provide a default cookie name
        /// </summary>
        public static readonly string CookiePrefix = ".CRM.";

        /// <summary>
        /// The issuer that should be used for any claims that are created
        /// </summary>
        public static readonly string ClaimsIssuer = "3HappyPals";

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
