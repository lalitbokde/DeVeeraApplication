using CRM.Core.Domain.Users;
using DeVeeraApp.ViewModels.UserLogin;

namespace DeVeeraApp.Factories
{
    /// <summary>
    /// Represents the interface of the User model factory
    /// </summary>
    public partial interface IUserModelFactory
    {
       


        /// <summary>
        /// Prepare the login model
        /// </summary>
        /// <param name="checkoutAsGuest">Whether to checkout as guest is enabled</param>
        /// <returns>Login model</returns>
        ViewModels.User.LoginModel PrepareLoginModel();

        /// <summary>
        /// Prepare the password recovery model
        /// </summary>
        /// <returns>Password recovery model</returns>
        PasswordRecoveryModel PreparePasswordRecoveryModel();

        /// <summary>
        /// Prepare the password recovery confirm model
        /// </summary>
        /// <returns>Password recovery confirm model</returns>
        PasswordRecoveryConfirmModel PreparePasswordRecoveryConfirmModel();

        /// <summary>
        /// Prepare the change password model
        /// </summary>
        /// <returns>Change password model</returns>
        ChangePasswordModel PrepareChangePasswordModel();
        void PrepareResetPasswordEmail(User model);

    }
}
