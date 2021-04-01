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
        /// Prepare the User info model
        /// </summary>
        /// <param name="model">User info model</param>
        /// <param name="User">User</param>
        /// <param name="excludeProperties">Whether to exclude populating of model properties from the entity</param>
        /// <param name="overrideCustomUserAttributesXml">Overridden User attributes in XML format; pass null to use CustomUserAttributes of User</param>
        /// <returns>User info model</returns>
        UserInfoModel PrepareUserInfoModel(UserInfoModel model, User User,
            bool excludeProperties, string overrideCustomUserAttributesXml = "");

        /// <summary>
        /// Prepare the User register model
        /// </summary>
        /// <param name="model">User register model</param>
        /// <param name="excludeProperties">Whether to exclude populating of model properties from the entity</param>
        /// <param name="overrideCustomUserAttributesXml">Overridden User attributes in XML format; pass null to use CustomUserAttributes of User</param>
        /// <param name="setDefaultValues">Whether to populate model properties by default values</param>
        /// <returns>User register model</returns>
        RegistrationModel PrepareRegisterModel(RegistrationModel model, bool excludeProperties,
            string overrideCustomUserAttributesXml = "", bool setDefaultValues = false);

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
