

using CRM.Core.Domain.Users;

namespace CRM.Services.Authentication
{
    /// <summary>
    /// Authentication service interface
    /// </summary>
    public partial interface IAuthenticationService 
    {
        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="User">User</param>
        /// <param name="isPersistent">Whether the authentication session is persisted across multiple requests</param>
        void SignIn(User User, bool isPersistent);

        /// <summary>
        /// Sign out
        /// </summary>
        void SignOut();

        /// <summary>
        /// Get authenticated User
        /// </summary>
        /// <returns>User</returns>
        User GetAuthenticatedUser();
    }
}