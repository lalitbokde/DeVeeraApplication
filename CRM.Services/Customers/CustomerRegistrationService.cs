using CRM.Core;
using CRM.Core.Domain.Users;
using CRM.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Services.Users
{
    /// <summary>
    /// User registration service
    /// </summary>
    public partial class UserRegistrationService : IUserRegistrationService
    {
        #region Fields

        private const int SALT_KEY_SIZE = 5;

        private readonly IUserService _UserService;
        private readonly IEncryptionService _encryptionService;
        private readonly IWorkContext _workContext;
       
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="UserService">User service</param>
        /// <param name="encryptionService">Encryption service</param>
        /// <param name="newsLetterSubscriptionService">Newsletter subscription service</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="storeService">Store service</param>
        /// <param name="rewardPointService">Reward points service</param>
        /// <param name="genericAttributeService">Generic attribute service</param>
        /// <param name="workContext">Work context</param>
        /// <param name="workflowMessageService">Workflow message service</param>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="rewardPointsSettings">Reward points settings</param>
        /// <param name="UserSettings">User settings</param>
        public UserRegistrationService(IUserService UserService,
            IEncryptionService encryptionService,
          
            IWorkContext workContext
         )
        {
            this._UserService = UserService;
            this._encryptionService = encryptionService;
         
            this._workContext = workContext;
        
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Check whether the entered password matches with a saved one
        /// </summary>
        /// <param name="UserPassword">User password</param>
        /// <param name="enteredPassword">The entered password</param>
        /// <returns>True if passwords match; otherwise false</returns>
        protected bool PasswordsMatch(UserPassword UserPassword, string enteredPassword)
        {
            if (UserPassword == null || string.IsNullOrEmpty(enteredPassword))
                return false;

            var savedPassword = string.Empty;
            switch (UserPassword.PasswordFormat)
            {
                case PasswordFormat.Clear:
                    savedPassword = enteredPassword;
                    break;
                case PasswordFormat.Encrypted:
                    savedPassword = _encryptionService.EncryptText(enteredPassword);
                    break;
                case PasswordFormat.Hashed:
                    savedPassword = _encryptionService.CreatePasswordHash(enteredPassword, UserPassword.PasswordSalt, "");
                    break;
            }

            if (UserPassword.Password == null)
                return false;

            return UserPassword.Password.Equals(savedPassword);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="usernameOrEmail">Username or email</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        public virtual User ValidateUser(string usernameOrEmail, string password)
        {
            var User = /*_UserSettings.UsernamesEnabled*/ true ?
                _UserService.GetUserByUsername(usernameOrEmail) :
                _UserService.GetUserByEmail(usernameOrEmail);

            if (User == null)
                return null;
            if (User.Deleted)
                return null;
            if (!User.Active)
                return null;
            //only registered can login
            //if (!User.IsRegistered())
            //    return null;
            ////check whether a User is locked out
            //if (User.CannotLoginUntilDateUtc.HasValue && User.CannotLoginUntilDateUtc.Value > DateTime.UtcNow)
            //    return UserLoginResults.LockedOut;

            if (!PasswordsMatch(_UserService.GetCurrentPassword(User.Id), password))
            {
                ////wrong password
                //User.FailedLoginAttempts++;
                //if (_UserSettings.FailedPasswordAllowedAttempts > 0 &&
                //    User.FailedLoginAttempts >= _UserSettings.FailedPasswordAllowedAttempts)
                //{
                //    //lock out
                //    User.CannotLoginUntilDateUtc = DateTime.UtcNow.AddMinutes(_UserSettings.FailedPasswordLockoutMinutes);
                //    //reset the counter
                //    User.FailedLoginAttempts = 0;
                //}
                //_UserService.UpdateUser(User);

                return null;
            }

            ////update login details
            //User.FailedLoginAttempts = 0;
            //User.CannotLoginUntilDateUtc = null;
            //User.RequireReLogin = false;
            User.LastLoginDateUtc = DateTime.UtcNow;
            _UserService.UpdateUser(User);

            return User;
        }


        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="usernameOrEmail">Username or email</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        public virtual UserLoginResults ValidateUserLogin(string usernameOrEmail, string password)
        {
            var User = _UserService.GetUserByEmail(usernameOrEmail);

            if (User == null)
                return UserLoginResults.UserNotExist;
            if (User.Deleted)
                return UserLoginResults.Deleted;
            if (!User.Active)
                return UserLoginResults.NotActive;
            ////only registered can login
            //if (!User.IsRegistered())
            //    return UserLoginResults.NotRegistered;
            //check whether a User is locked out
            if (User.CannotLoginUntilDateUtc.HasValue && User.CannotLoginUntilDateUtc.Value > DateTime.UtcNow)
                return UserLoginResults.LockedOut;

            if (!PasswordsMatch(_UserService.GetCurrentPassword(User.Id), password))
            {
                //wrong password
                User.FailedLoginAttempts++;
                if ( User.FailedLoginAttempts >= 3)
                {
                    //lock out
                    User.CannotLoginUntilDateUtc = DateTime.UtcNow.AddMinutes(10);
                    //reset the counter
                    User.FailedLoginAttempts = 0;
                }
                _UserService.UpdateUser(User);

                return UserLoginResults.WrongPassword;
            }

            //update login details
            User.FailedLoginAttempts = 0;
            User.CannotLoginUntilDateUtc = null;
            User.RequireReLogin = false;
            User.LastLoginDateUtc = DateTime.UtcNow;
            _UserService.UpdateUser(User);

            return UserLoginResults.Successful;
        }


        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        public virtual UserRegistrationResult RegisterUser(UserRegistrationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.User == null)
                throw new ArgumentException("Can't load current User");

            var result = new UserRegistrationResult();
            //if (request.User.IsSearchEngineAccount())
            //{
            //    result.AddError("Search engine can't be registered");
            //    return result;
            //}
            //if (request.User.IsBackgroundTaskAccount())
            //{
            //    result.AddError("Background task account can't be registered");
            //    return result;
            //}
            //if (request.User.IsRegistered())
            //{
            //   // result.AddError("Current User is already registered");
            //    return result;
            //}
            if (string.IsNullOrEmpty(request.Email))
            {
               // result.AddError(_localizationService.GetResource("Account.Register.Errors.EmailIsNotProvided"));
                return result;
            }
            //if (!CommonHelper.IsValidEmail(request.Email))
            //{
            //    //result.AddError(_localizationService.GetResource("Common.WrongEmail"));
            //    return result;
            //}
            if (string.IsNullOrWhiteSpace(request.Password))
            {
               // result.AddError(_localizationService.GetResource("Account.Register.Errors.PasswordIsNotProvided"));
                return result;
            }
            //if (_UserSettings.UsernamesEnabled)
            //{
            //    if (string.IsNullOrEmpty(request.Username))
            //    {
            //        result.AddError(_localizationService.GetResource("Account.Register.Errors.UsernameIsNotProvided"));
            //        return result;
            //    }
            //}

            //validate unique user
            if (_UserService.GetUserByEmail(request.Email) != null)
            {
             //   result.AddError(_localizationService.GetResource("Account.Register.Errors.EmailAlreadyExists"));
                return result;
            }
            //if (_UserSettings.UsernamesEnabled)
            //{
            //    if (_UserService.GetUserByUsername(request.Username) != null)
            //    {
            //        result.AddError(_localizationService.GetResource("Account.Register.Errors.UsernameAlreadyExists"));
            //        return result;
            //    }
            //}

            //at this point request is valid
            request.User.Username = request.Username;
            request.User.Email = request.Email;

            var UserPassword = new UserPassword
            {
                User = request.User,
                PasswordFormat = request.PasswordFormat,
                CreatedOnUtc = DateTime.UtcNow
            };
            switch (request.PasswordFormat)
            {
                case PasswordFormat.Clear:
                    UserPassword.Password = request.Password;
                    break;
                case PasswordFormat.Encrypted:
                    UserPassword.Password = _encryptionService.EncryptText(request.Password);
                    break;
                case PasswordFormat.Hashed:
                    {
                        var saltKey = _encryptionService.CreateSaltKey(SALT_KEY_SIZE);
                        UserPassword.PasswordSalt = saltKey;
                        UserPassword.Password = _encryptionService.CreatePasswordHash(request.Password, saltKey, "");
                    }
                    break;
            }
            _UserService.InsertUserPassword(UserPassword);

            request.User.Active = request.IsApproved;

            //add to 'Registered' role
            var registeredRole = _UserService.GetUserRoleBySystemName(SystemUserRoleNames.Registered);
            if (registeredRole == null)
                throw new Exception("'Registered' role could not be loaded");
            request.User.UserRole = registeredRole;
            ////remove from 'Guests' role
            //var guestRole = (request.User.UserRole.SystemName == SystemUserRoleNames.Guests) ? request.User.UserRole;
            //if (guestRole != null)
            //    request.User.UserRoles.Remove(guestRole);

           

            _UserService.UpdateUser(request.User);

         
            return result;
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        public virtual ChangePasswordResult ChangePassword(ChangePasswordRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var result = new ChangePasswordResult();
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                result.AddError("EmailIsNotProvided");
                return result;
            }
            if (string.IsNullOrWhiteSpace(request.NewPassword))
            {
                result.AddError("PasswordIsNotProvided");
                return result;
            }

            var User = _UserService.GetUserByEmail(request.Email);
            if (User == null)
            {
                result.AddError("EmailNotFound");
                return result;
            }

            if (request.ValidateRequest)
            {
                //request isn't valid
                if (!PasswordsMatch(_UserService.GetCurrentPassword(User.Id), request.OldPassword))
                {
                    result.AddError("OldPasswordDoesntMatch");
                    return result;
                }
            }

            //at this point request is valid
            var UserPassword = new UserPassword
            {
                User = User,
                PasswordFormat = request.NewPasswordFormat,
                CreatedOnUtc = DateTime.UtcNow
            };
            switch (request.NewPasswordFormat)
            {
                case PasswordFormat.Clear:
                    UserPassword.Password = request.NewPassword;
                    break;
                case PasswordFormat.Encrypted:
                    UserPassword.Password = _encryptionService.EncryptText(request.NewPassword);
                    break;
                case PasswordFormat.Hashed:
                    {
                        var saltKey = _encryptionService.CreateSaltKey(SALT_KEY_SIZE);
                        UserPassword.PasswordSalt = saltKey;
                        UserPassword.Password = _encryptionService.CreatePasswordHash(request.NewPassword, saltKey,"");
                    }
                    break;
            }
            _UserService.InsertUserPassword(UserPassword);

            return result;
        }

        /// <summary>
        /// Sets a user email
        /// </summary>
        /// <param name="User">User</param>
        /// <param name="newEmail">New email</param>
        /// <param name="requireValidation">Require validation of new email address</param>
        public virtual void SetEmail(User User, string newEmail, bool requireValidation)
        {
            //if (User == null)
            //    throw new ArgumentNullException(nameof(User));

            //if (newEmail == null)
            //    throw new NopException("Email cannot be null");

            //newEmail = newEmail.Trim();
            //var oldEmail = User.Email;

            //if (!CommonHelper.IsValidEmail(newEmail))
            //    throw new NopException(_localizationService.GetResource("Account.EmailUsernameErrors.NewEmailIsNotValid"));

            //if (newEmail.Length > 100)
            //    throw new NopException(_localizationService.GetResource("Account.EmailUsernameErrors.EmailTooLong"));

            //var User2 = _UserService.GetUserByEmail(newEmail);
            //if (User2 != null && User.Id != User2.Id)
            //    throw new NopException(_localizationService.GetResource("Account.EmailUsernameErrors.EmailAlreadyExists"));

            //if (requireValidation)
            //{
            //    //re-validate email
            //    User.EmailToRevalidate = newEmail;
            //    _UserService.UpdateUser(User);

            //    //email re-validation message
            //    _genericAttributeService.SaveAttribute(User, SystemUserAttributeNames.EmailRevalidationToken, Guid.NewGuid().ToString());
            //    _workflowMessageService.SendUserEmailRevalidationMessage(User, _workContext.WorkingLanguage.Id);
            //}
            //else
            //{
            //    User.Email = newEmail;
            //    _UserService.UpdateUser(User);

            //    //update newsletter subscription (if required)
            //    if (!string.IsNullOrEmpty(oldEmail) && !oldEmail.Equals(newEmail, StringComparison.InvariantCultureIgnoreCase))
            //    {
            //        foreach (var store in _storeService.GetAllStores())
            //        {
            //            var subscriptionOld = _newsLetterSubscriptionService.GetNewsLetterSubscriptionByEmailAndStoreId(oldEmail, store.Id);
            //            if (subscriptionOld != null)
            //            {
            //                subscriptionOld.Email = newEmail;
            //                _newsLetterSubscriptionService.UpdateNewsLetterSubscription(subscriptionOld);
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Sets a User username
        /// </summary>
        /// <param name="User">User</param>
        /// <param name="newUsername">New Username</param>
        public virtual void SetUsername(User User, string newUsername)
        {
            if (User == null)
                throw new ArgumentNullException(nameof(User));

            //if (!_UserSettings.UsernamesEnabled)
            //    throw new NopException("Usernames are disabled");

            newUsername = newUsername.Trim();

            //if (newUsername.Length > 100)
            //    throw new NopException(_localizationService.GetResource("Account.EmailUsernameErrors.UsernameTooLong"));

            var user2 = _UserService.GetUserByUsername(newUsername);
            //if (user2 != null && User.Id != user2.Id)
            //    throw new NopException(_localizationService.GetResource("Account.EmailUsernameErrors.UsernameAlreadyExists"));

            User.Username = newUsername;
            _UserService.UpdateUser(User);
        }

        #endregion
    }
}
