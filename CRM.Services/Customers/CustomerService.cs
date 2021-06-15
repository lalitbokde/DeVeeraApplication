using CRM.Core.Domain.Users;

using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.Users
{
    public class UserService :IUserService
    {
        #region fields
        private readonly IRepository<User> _UserRepository;
        private readonly IRepository<UserRole> _UserRoleRepository;
        private readonly IRepository<UserPassword> _UserPasswordRepository;
        #endregion

        #region ctor
        public UserService(
           IRepository<User> UserRepository,
          IRepository<UserRole> UserRoleRepository,
          IRepository<UserPassword> UserPasswordRepository)
        {
            this._UserRepository = UserRepository;
            this._UserRoleRepository = UserRoleRepository;
            this._UserPasswordRepository = UserPasswordRepository;
        }
        #endregion

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
        public virtual IList<User> GetAllUsers(DateTime? createdFromUtc = null,
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
                                                       string ipAddress = null)
        {
            var query = _UserRepository.Table;
            if (createdFromUtc.HasValue)
                query = query.Where(c => createdFromUtc.Value <= c.CreatedOnUtc);
            if (createdToUtc.HasValue)
                query = query.Where(c => createdToUtc.Value >= c.CreatedOnUtc);
          
            query = query.Where(c => !c.Deleted);
            if (UserRoleIds != null && UserRoleIds.Length > 0)
                query = query.Where(c => UserRoleIds.Contains(c.UserRoleId));
            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(c => c.Email.Contains(email));
            if (!string.IsNullOrWhiteSpace(username))
                query = query.Where(c => c.Username.Contains(username));

            query = query.OrderByDescending(c => c.Id);

            var Users = query.ToList();
            return Users;
        }

       

        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="User">User</param>
        public virtual void DeleteUser(User User)
        {
            if (User == null)
                throw new ArgumentNullException(nameof(User));

            _UserRepository.Delete(User);

        }

        /// <summary>
        /// Search a User 
        /// </summary>
        /// <param name="User">The first name identifier</param>
        /// <returns>Vendor note</returns>
        public IList<User> SearchUser(string FirstName, int? ParentUserId=null)
        {

            if (false)
            {
                //stored procedures are enabled and supported by the database. 
                //It's much faster than the LINQ implementation below 
                //     products = SearchProductsUseStoredProcedure(ref filterableSpecificationAttributeOptionIds, loadFilterableSpecificationAttributeOptionIds, pageIndex, pageSize, categoryIds, manufacturerId, storeId, vendorId, warehouseId, productType, visibleIndividuallyOnly, markedAsNewOnly, featuredProducts, priceMin, priceMax, productTagId, keywords, searchDescriptions, searchManufacturerPartNumber, searchSku, allowedUserRolesIds, searchProductTags, searchLocalizedValue, languageId, filteredSpecs, orderBy, showHidden, overridePublished);
            }
            else
            {
                //stored procedures aren't supported. Use LINQ
                return SearchUserUseLinq(FirstName, ParentUserId);

            }
        }

        protected virtual IList<User> SearchUserUseLinq(string FirstName,int? ParentUserId)
        {
            var query = _UserRepository.Table;
            query = query.Where(p => !p.Deleted);

            if (FirstName != null)
            {
                query = from v in _UserRepository.Table
                        where v.UserAddress.FirstName == FirstName && ((ParentUserId==null) || (v.ParentUserId==ParentUserId))
                        select v;
            }

            var User = query.ToList();

            //return products
            return User;
        }

        /// <summary>
        /// Gets a User
        /// </summary>
        /// <param name="UserId">User identifier</param>
        /// <returns>A User</returns>
        public virtual User GetUserById(int UserId)
        {
            if (UserId == 0)
                return null;
            var data = _UserRepository.GetById(UserId);
            return data;
        }

        /// <summary>
        /// Get Users by identifiers
        /// </summary>
        /// <param name="UserIds">User identifiers</param>
        /// <returns>Users</returns>
        public virtual IList<User> GetUsersByIds(int[] UserIds)
        {
            if (UserIds == null || UserIds.Length == 0)
                return new List<User>();

            var query = from c in _UserRepository.Table
                        where UserIds.Contains(c.Id) && !c.Deleted
                        select c;
            var Users = query.ToList();
            //sort by passed identifiers
            var sortedUsers = new List<User>();
            foreach (var id in UserIds)
            {
                var User = Users.Find(x => x.Id == id);
                if (User != null)
                    sortedUsers.Add(User);
            }
            return sortedUsers;
        }

        /// <summary>
        /// Gets a User by UserRole
        /// </summary>
        /// <param name="UserRoleId">User Role</param>
        /// <returns>A User</returns>
        public IList<User> GetUserByUserRoleId(int UserRolerId)
        {
            if (UserRolerId == 0)
                throw new ArgumentNullException(nameof(UserRolerId));

            var query = _UserRepository.Table;
            query = query.Where(p => !p.Deleted && p.UserRoleId == UserRolerId);

            if (UserRolerId != 0)
                query = query.Where(p => p.UserRoleId == UserRolerId);

            return query.ToList();
        }


        /// <summary>
        /// Gets a User by GUID
        /// </summary>
        /// <param name="UserGuid">User GUID</param>
        /// <returns>A User</returns>
        public virtual User GetUserByGuid(Guid UserGuid)
        {
            if (UserGuid == Guid.Empty)
                return null;

            var query = from c in _UserRepository.Table
                        where c.UserGuid == UserGuid
                        orderby c.Id
                        select c;
            var User = query.FirstOrDefault();
            return User;
        }

        /// <summary>
        /// Get User by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>User</returns>
        public virtual User GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var query = from c in _UserRepository.Table
                        orderby c.Id
                        where c.Email == email
                        select c;
            var User = query.FirstOrDefault();
            return User;
        }

        /// <summary>
        /// Get User by system name
        /// </summary>
        /// <param name="systemName">System name</param>
        /// <returns>User</returns>
        public virtual User GetUserBySystemName(string systemName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
                return null;

            var query = from c in _UserRepository.Table
                        orderby c.Id
                        where c.SystemName == systemName
                        select c;
            var User = query.FirstOrDefault();
            return User;
        }

        /// <summary>
        /// Get User by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>User</returns>
        public virtual User GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var query = from c in _UserRepository.Table
                        orderby c.Id
                        where c.Username == username
                        select c;
            var User = query.FirstOrDefault();
            return User;
        }


        /// <summary>
        /// Insert a User
        /// </summary>
        /// <param name="User">User</param>
        public virtual void InsertUser(User User)
        {
            if (User == null)
                throw new ArgumentNullException(nameof(User));

            _UserRepository.Insert(User);

        }

        /// <summary>
        /// Updates the User
        /// </summary>
        /// <param name="User">User</param>
        public virtual void UpdateUser(User User)
        {
            if (User == null)
                throw new ArgumentNullException(nameof(User));

            _UserRepository.Update(User);

        }


        #endregion

        #region User roles

        /// <summary>
        /// Delete a User role
        /// </summary>
        /// <param name="UserRole">User role</param>
        public virtual void DeleteUserRole(UserRole UserRole)
        {
            if (UserRole == null)
                throw new ArgumentNullException(nameof(UserRole));

            //if (UserRole.IsSystemRole)
            //    throw new Exception("System role could not be deleted");

            _UserRoleRepository.Delete(UserRole);


        }

        /// <summary>
        /// Gets a User role
        /// </summary>
        /// <param name="UserRoleId">User role identifier</param>
        /// <returns>User role</returns>
        public virtual UserRole GetUserRoleById(int UserRoleId)
        {
            if (UserRoleId == 0)
                return null;

            return _UserRoleRepository.GetById(UserRoleId);
        }

        public virtual UserRole GetUserRoleByRoleName(string name)
        {
            if (name == null)
                return null;

            return _UserRoleRepository.Table.Where(n => n.Name == name).SingleOrDefault();
        }

        /// <summary>
        /// Gets a User role
        /// </summary>
        /// <param name="systemName">User role system name</param>
        /// <returns>User role</returns>
        public virtual UserRole GetUserRoleBySystemName(string systemName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
                return null;

            var query = from cr in _UserRoleRepository.Table
                        orderby cr.Id
                        where cr.SystemName == systemName
                        select cr;
            var UserRole = query.FirstOrDefault();
            return UserRole;

        }

        /// <summary>
        /// Gets all User roles
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>User roles</returns>
        public virtual IList<UserRole> GetAllUserRoles(bool showHidden = false)
        {

            var query = from cr in _UserRoleRepository.Table
                        orderby cr.Name
                        where showHidden || cr.Active
                        select cr;
            var UserRoles = query.ToList();
            return UserRoles;

        }

        /// <summary>
        /// Inserts a User role
        /// </summary>
        /// <param name="UserRole">User role</param>
        public virtual void InsertUserRole(UserRole UserRole)
        {
            if (UserRole == null)
                throw new ArgumentNullException(nameof(UserRole));

            _UserRoleRepository.Insert(UserRole);

        }

        /// <summary>
        /// Updates the User role
        /// </summary>
        /// <param name="UserRole">User role</param>
        public virtual void UpdateUserRole(UserRole UserRole)
        {
            if (UserRole == null)
                throw new ArgumentNullException(nameof(UserRole));

            _UserRoleRepository.Update(UserRole);
        }

        #endregion


        #region User passwords

        /// <summary>
        /// Gets User passwords
        /// </summary>
        /// <param name="UserId">User identifier; pass null to load all records</param>
        /// <param name="passwordFormat">Password format; pass null to load all records</param>
        /// <param name="passwordsToReturn">Number of returning passwords; pass null to load all records</param>
        /// <returns>List of User passwords</returns>
        public virtual IList<UserPassword> GetUserPasswords(int? UserId = null,
            PasswordFormat? passwordFormat = null, int? passwordsToReturn = null)
        {
            var query = _UserPasswordRepository.Table;

            //filter by User
            if (UserId.HasValue)
                query = query.Where(password => password.UserId == UserId.Value);

            //filter by password format
            if (passwordFormat.HasValue)
                query = query.Where(password => password.PasswordFormatId == (int)(passwordFormat.Value));

            //get the latest passwords
            if (passwordsToReturn.HasValue)
                query = query.OrderByDescending(password => password.CreatedOnUtc).Take(passwordsToReturn.Value);

            return query.ToList();
        }

        /// <summary>
        /// Get current User password
        /// </summary>
        /// <param name="UserId">User identifier</param>
        /// <returns>User password</returns>
        public virtual UserPassword GetCurrentPassword(int UserId)
        {
            if (UserId == 0)
                return null;

            //return the latest password
            return GetUserPasswords(UserId, passwordsToReturn: 1).FirstOrDefault();
        }

        /// <summary>
        /// Insert a User password
        /// </summary>
        /// <param name="UserPassword">User password</param>
        public virtual void InsertUserPassword(UserPassword UserPassword)
        {
            if (UserPassword == null)
                throw new ArgumentNullException(nameof(UserPassword));

            _UserPasswordRepository.Insert(UserPassword);

        }

        /// <summary>
        /// Update a User password
        /// </summary>
        /// <param name="UserPassword">User password</param>
        public virtual void UpdateUserPassword(UserPassword UserPassword)
        {
            if (UserPassword == null)
                throw new ArgumentNullException(nameof(UserPassword));

            _UserPasswordRepository.Update(UserPassword);

        }

        #endregion



    }
}
