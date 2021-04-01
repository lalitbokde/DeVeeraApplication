using System;
using System.Collections.Generic;
using System.Linq;
using CRM.Core;
using CRM.Core.Caching;
using CRM.Core.Domain.Users;
using CRM.Core.Domain.Security;
using CRM.Data.Interfaces;
using CRM.Services.Users;
using CRM.Services.Security;

namespace CRM.Services.Security
{
    /// <summary>
    /// Permission service
    /// </summary>
    public partial class PermissionService : IPermissionService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : User role ID
        /// {1} : permission system name
        /// </remarks>
        private const string PERMISSIONS_ALLOWED_KEY = "Nop.permission.allowed-{0}-{1}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string PERMISSIONS_PATTERN_KEY = "Nop.permission.";

        #endregion

        #region Fields

        private readonly IRepository<PermissionRecord> _permissionRecordRepository;
        private readonly IRepository<PermissionRecord_Role_Mapping> _permissionRecord_Role_Mapping;
        private readonly IUserService _UserService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="permissionRecordRepository">Permission repository</param>
        /// <param name="UserService">User service</param>
        /// <param name="workContext">Work context</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="languageService">Language service</param>
        /// <param name="cacheManager">Static cache manager</param>
        public PermissionService(IRepository<PermissionRecord> permissionRecordRepository,
            IUserService UserService,
            IWorkContext workContext)
        {
            this._permissionRecordRepository = permissionRecordRepository;
            this._UserService = UserService;
            this._workContext = workContext;


        }

        #endregion

        #region Utilities

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <param name="UserRole">User role</param>
        /// <returns>true - authorized; otherwise, false</returns>
        protected virtual bool Authorize(string permissionRecordSystemName, UserRole UserRole)
        {
            if (string.IsNullOrEmpty(permissionRecordSystemName))
                return false;


            foreach (var permission1 in UserRole.PermissionRecord_Role_Mapping)
                if (permission1.PermissionRecord.SystemName.Equals(permissionRecordSystemName, StringComparison.InvariantCultureIgnoreCase))
                    return true;

            return false;

        }

        #endregion

        #region Methods

        /// <summary>
        /// Delete a permission
        /// </summary>
        /// <param name="permission">Permission</param>
        public virtual void DeletePermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            _permissionRecordRepository.Delete(permission);

        }

        /// <summary>
        /// Gets a permission
        /// </summary>
        /// <param name="permissionId">Permission identifier</param>
        /// <returns>Permission</returns>
        public virtual PermissionRecord GetPermissionRecordById(int permissionId)
        {
            if (permissionId == 0)
                return null;

            return _permissionRecordRepository.GetById(permissionId);
        }

        /// <summary>
        /// Gets a permission
        /// </summary>
        /// <param name="systemName">Permission system name</param>
        /// <returns>Permission</returns>
        public virtual PermissionRecord GetPermissionRecordBySystemName(string systemName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
                return null;

            var query = from pr in _permissionRecordRepository.Table
                        where pr.SystemName == systemName
                        orderby pr.Id
                        select pr;

            var permissionRecord = query.FirstOrDefault();
            return permissionRecord;
        }

        /// <summary>
        /// Gets all permissions
        /// </summary>
        /// <returns>Permissions</returns>
        public virtual IList<PermissionRecord> GetAllPermissionRecords()
        {
            var query = from pr in _permissionRecordRepository.Table
                        orderby pr.Name
                        select pr;
            var permissions = query.ToList();
            return permissions;
        }

        /// <summary>
        /// Inserts a permission
        /// </summary>
        /// <param name="permission">Permission</param>
        public virtual void InsertPermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            _permissionRecordRepository.Insert(permission);

        }

        /// <summary>
        /// Updates the permission
        /// </summary>
        /// <param name="permission">Permission</param>
        public virtual void UpdatePermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            _permissionRecordRepository.Update(permission);

        }

        /// <summary>
        /// Install permissions
        /// </summary>
        /// <param name="permissionProvider">Permission provider</param>
        public virtual void InstallPermissions(IPermissionProvider permissionProvider)
        {
            //install new permissions
            var permissions = permissionProvider.GetPermissions();
            //default User role mappings
            var defaultPermissions = permissionProvider.GetDefaultPermissions().ToList();

            foreach (var permission in permissions)
            {
                var permission1 = GetPermissionRecordBySystemName(permission.SystemName);
                if (permission1 != null)
                    continue;

                //new permission (install it)
                permission1 = new PermissionRecord
                {
                    Name = permission.Name,
                    SystemName = permission.SystemName,
                    Category = permission.Category,
                };

                foreach (var defaultPermission in defaultPermissions)
                {
                    var UserRole = _UserService.GetUserRoleBySystemName(defaultPermission.UserRoleSystemName);
                    if (UserRole == null)
                    {
                        //new role (save it)
                        UserRole = new UserRole
                        {
                            Name = defaultPermission.UserRoleSystemName,
                            Active = true,
                            SystemName = defaultPermission.UserRoleSystemName
                        };
                        _UserService.InsertUserRole(UserRole);
                    }

                    var defaultMappingProvided = (from p in defaultPermission.PermissionRecords
                                                  where p.SystemName == permission1.SystemName
                                                  select p).Any();
                    var mappingExists = (from p in UserRole.PermissionRecord_Role_Mapping
                                         where p.UserRole.SystemName == permission1.SystemName
                                         select p).Any();
                    if (defaultMappingProvided && !mappingExists)
                    {
                        PermissionRecord_Role_Mapping permissionRecord_Role_Mapping = new PermissionRecord_Role_Mapping()
                        {
                             PermissionRecordId = permission1.Id,
                              UserRoleId = UserRole.Id
                        };
                        InsertPermissionRecordRoleMapping(permissionRecord_Role_Mapping);
                    }
                }

                //save new permission
                InsertPermissionRecord(permission1);


            }
        }

        public virtual void InsertPermissionRecordRoleMapping(PermissionRecord_Role_Mapping permissionRoleMappingProvider)
        {
            if (permissionRoleMappingProvider == null)
                throw new ArgumentNullException(nameof(permissionRoleMappingProvider));

            _permissionRecord_Role_Mapping.Insert(permissionRoleMappingProvider);
        }

        /// <summary>
        /// Uninstall permissions
        /// </summary>
        /// <param name="permissionProvider">Permission provider</param>
        public virtual void UninstallPermissions(IPermissionProvider permissionProvider)
        {
            var permissions = permissionProvider.GetPermissions();
            foreach (var permission in permissions)
            {
                var permission1 = GetPermissionRecordBySystemName(permission.SystemName);
                if (permission1 != null)
                {
                    DeletePermissionRecord(permission1);

                }
            }
        }

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(PermissionRecord permission)
        {
            return Authorize(permission, _workContext.CurrentUser);
        }

        ///// <summary>
        ///// Authorize permission
        ///// </summary>
        ///// <param name="permission">Permission record</param>
        ///// <param name="User">User</param>
        ///// <returns>true - authorized; otherwise, false</returns>
        //public virtual bool Authorize(PermissionRecord permission, User User)
        //{
        //    if (permission == null)
        //        return false;

        //    if (User == null)
        //        return false;

        //    //old implementation of Authorize method
        //    var UserRoles = User.UserRoles.Where(cr => cr.Active);
        //    var UserRole = _UserService.GetAllUserRoles().Where(a => a.Id == User.UserRoleId).FirstOrDefault();
        //    var permissionRecords = GetAllPermissionRecords();

        //    foreach (var permission1 in UserRole.PermissionRecord_Role_Mapping)
        //            if (permission1.PermissionRecord.SystemName == permission.SystemName)
        //                return true;

        //    //foreach (var pr in permissionRecords)
        //    //    foreach (var cr in UserRoles)
        //    //    {
        //    //        var allowed = pr.PermissionRecord_Role_Mapping.Count(x => x.UserRole.Id == cr.Id) > 0;
        //    //        if (!model.Allowed.ContainsKey(pr.SystemName))
        //    //            model.Allowed[pr.SystemName] = new Dictionary<int, bool>();
        //    //        model.Allowed[pr.SystemName][cr.Id] = allowed;
        //    //    }

        //    return Authorize(permission.SystemName, User); 

        //   // return Authorize(permission.SystemName, User);
        //}


        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <param name="User">User</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(PermissionRecord permission, User User)
        {
            if (permission == null)
                return false;

            if (User == null)
                return false;

            //old implementation of Authorize method
            //var UserRoles = User.UserRoles.Where(cr => cr.Active);
            //foreach (var role in UserRoles)
            //    foreach (var permission1 in role.PermissionRecords)
            //        if (permission1.SystemName.Equals(permission.SystemName, StringComparison.InvariantCultureIgnoreCase))
            //            return true;

            //return false;

            return Authorize(permission.SystemName, User);
        }

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(string permissionRecordSystemName)
        {
            return Authorize(permissionRecordSystemName, _workContext.CurrentUser);
        }

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <param name="User">User</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(string permissionRecordSystemName, User User)
        {
            if (string.IsNullOrEmpty(permissionRecordSystemName))
                return false;

            var UserRole = User.UserRole;
           // foreach (var role in UserRoles)
                if (Authorize(permissionRecordSystemName, UserRole))
                    //yes, we have such permission
                    return true;

            //no permission found
            return false;
        }
        #endregion
    }
}