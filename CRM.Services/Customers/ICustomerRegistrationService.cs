﻿using CRM.Core.Domain.Users;

namespace CRM.Services.Users
{
    /// <summary>
    /// User registration interface
    /// </summary>
    public partial interface IUserRegistrationService
    {
        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="usernameOrEmail">Username or email</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        User ValidateUser(string usernameOrEmail, string password);

        /// <summary>
        /// Validate User Login
        /// </summary>
        /// <param name="usernameOrEmail">Username or email</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        UserLoginResults ValidateUserLogin(string usernameOrEmail, string password);

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        UserRegistrationResult RegisterUser(UserRegistrationRequest request);

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        ChangePasswordResult ChangePassword(ChangePasswordRequest request);

        /// <summary>
        /// Sets a user email
        /// </summary>
        /// <param name="User">User</param>
        /// <param name="newEmail">New email</param>
        /// <param name="requireValidation">Require validation of new email address</param>
        void SetEmail(User User, string newEmail, bool requireValidation);

        /// <summary>
        /// Sets a User username
        /// </summary>
        /// <param name="User">User</param>
        /// <param name="newUsername">New Username</param>
        void SetUsername(User User, string newUsername);
    }
}
