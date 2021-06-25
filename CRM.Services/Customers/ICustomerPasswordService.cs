using CRM.Core.Domain.Users;

namespace CRM.Services.Users
{

    /// <summary>
    /// UserPasswordService service interface
    /// </summary>

    public partial interface IUserPasswordService
    {
        /// <summary>
        /// Inserts a vendor
        /// </summary>
        /// <param name="passwword">Password</param>
        void InsertUserPassword(UserPassword password);

        /// <summary>
        /// Gets an Password by User identifier
        /// </summary>
        /// <param name="PasswordId">Password identifier</param>
        /// <returns>Address</returns>
        UserPassword GetPasswordByUserId(int UserId);

        /// <summary>
        /// Updates the Password
        /// </summary>
        /// <param name="password">Password</param>
        void UpdatePassword(UserPassword password);
    }
}
