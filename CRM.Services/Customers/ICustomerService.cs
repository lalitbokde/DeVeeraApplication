using CRM.Core.Domain.Users;

using System;
using System.Collections.Generic;

namespace CRM.Services.Users
{
    public partial interface IUserService
    {
        #region Users

        /// <summary>
        /// Gets all Users
        /// </summary>
        /// <param name="createdFromUtc">Created date from (UTC); null to load all records</param>
        /// <param name="createdToUtc">Created date to (UTC); null to load all records</param>
        /// <param name="affiliateId">Affiliate identifier</param>
        /// <param name="vendorId">Vendor identifier</param>
        /// <param name="UserRoleIds">A list of User role identifiers to filter by (at least one match); pass null or empty list in order to load all Users; </param>
        /// <param name="email">Email; null to load all Users</param>
        /// <param name="username">Username; null to load all Users</param>
        /// <param name="firstName">First name; null to load all Users</param>
        /// <param name="lastName">Last name; null to load all Users</param>
        /// <param name="dayOfBirth">Day of birth; 0 to load all Users</param>
        /// <param name="monthOfBirth">Month of birth; 0 to load all Users</param>
        /// <param name="company">Company; null to load all Users</param>
        /// <param name="phone">Phone; null to load all Users</param>
        /// <param name="zipPostalCode">Phone; null to load all Users</param>
        /// <param name="ipAddress">IP address; null to load all Users</param>
        /// <param name="loadOnlyWithShoppingCart">Value indicating whether to load Users only with shopping cart</param>
        /// <param name="sct">Value indicating what shopping cart type to filter; userd when 'loadOnlyWithShoppingCart' param is 'true'</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Users</returns>
        IList<User> GetAllUsers(DateTime? createdFromUtc = null,
                                DateTime? createdToUtc = null,
                                int[] UserRoleIds = null,
                                string email = null,
                                string username = null,
                                string firstName = null,
                                string lastName = null,
                                int dayOfBirth = 0,
                                int monthOfBirth = 0,
                                string company = null,
                                string phone = null,
                                string zipPostalCode = null,
                                string ipAddress = null);



        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="User">User</param>
        void DeleteUser(User User);

       

        /// <summary>
        /// Gets a User
        /// </summary>
        /// <param name="UserId">User identifier</param>
        /// <returns>A User</returns>
        User GetUserById(int UserId);

        /// <summary>
        /// Get Users by identifiers
        /// </summary>
        /// <param name="UserIds">User identifiers</param>
        /// <returns>Users</returns>
        IList<User> GetUsersByIds(int[] UserIds);

        /// <summary>
        /// Gets a User by User role
        /// </summary>
        /// <param name="UserRoleId">User role identifier</param>
        /// <returns>User role</returns>
        IList<User> GetUserByUserRoleId(int UserRoleId);


        /// <summary>
        /// Gets a User by GUID
        /// </summary>
        /// <param name="UserGuid">User GUID</param>
        /// <returns>A User</returns>
        User GetUserByGuid(Guid UserGuid);

        /// <summary>
        /// Get User by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>User</returns>
        User GetUserByEmail(string email);

        /// <summary>
        /// Get User by system role
        /// </summary>
        /// <param name="systemName">System name</param>
        /// <returns>User</returns>
        User GetUserBySystemName(string systemName);

        /// <summary>
        /// Get User by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>User</returns>
        User GetUserByUsername(string username);

        /// <summary>
        /// Insert a User
        /// </summary>
        /// <param name="User">User</param>
        void InsertUser(User User);

        /// <summary>
        /// Updates the User
        /// </summary>
        /// <param name="User">User</param>
        void UpdateUser(User User);
        User GetUserByMobileNo(string MobileNo);


        #endregion

        #region User roles

        /// <summary>
        /// Delete a User role
        /// </summary>
        /// <param name="UserRole">User role</param>
        void DeleteUserRole(UserRole UserRole);

        /// <summary>
        /// Gets a User role
        /// </summary>
        /// <param name="UserRoleId">User role identifier</param>
        /// <returns>User role</returns>
        UserRole GetUserRoleById(int UserRoleId);

        /// <summary>
        /// Gets a User role
        /// </summary>
        /// <param name="name">User role identifier</param>
        /// <returns>User role</returns>
        UserRole GetUserRoleByRoleName(string name);


        /// <summary>
        /// Gets a User role
        /// </summary>
        /// <param name="systemName">User role system name</param>
        /// <returns>User role</returns>
        UserRole GetUserRoleBySystemName(string systemName);

        /// <summary>
        /// Gets all User roles
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>User roles</returns>
        IList<UserRole> GetAllUserRoles(bool showHidden = false);

        /// <summary>
        /// Inserts a User role
        /// </summary>
        /// <param name="UserRole">User role</param>
        void InsertUserRole(UserRole UserRole);

        /// <summary>
        /// Updates the User role
        /// </summary>
        /// <param name="UserRole">User role</param>
        void UpdateUserRole(UserRole UserRole);

        #endregion

        #region User passwords

        /// <summary>
        /// Gets User passwords
        /// </summary>
        /// <param name="UserId">User identifier; pass null to load all records</param>
        /// <param name="passwordFormat">Password format; pass null to load all records</param>
        /// <param name="passwordsToReturn">Number of returning passwords; pass null to load all records</param>
        /// <returns>List of User passwords</returns>
        IList<UserPassword> GetUserPasswords(int? UserId = null,
            PasswordFormat? passwordFormat = null, int? passwordsToReturn = null);

        /// <summary>
        /// Get current User password
        /// </summary>
        /// <param name="UserId">User identifier</param>
        /// <returns>User password</returns>
        UserPassword GetCurrentPassword(int UserId);

        /// <summary>
        /// Insert a User password
        /// </summary>
        /// <param name="UserPassword">User password</param>
        void InsertUserPassword(UserPassword UserPassword);

        /// <summary>
        /// Update a User password
        /// </summary>
        /// <param name="UserPassword">User password</param>
        void UpdateUserPassword(UserPassword UserPassword);

        #endregion


    }
}
