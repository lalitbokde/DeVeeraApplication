using System.Collections.Generic;
using CRM.Core.Domain.Users;
using CRM.Core.Domain.Security;

namespace CRM.Services.Security
{
    /// <summary>
    /// Permission service interface
    /// </summary>
    public partial interface IPermissionService
    {
 
        /// <summary>
        /// Gets a permission
        /// </summary>
        /// <param name="permissionId">Permission identifier</param>
        /// <returns>Permission</returns>
        PermissionRecord GetPermissionRecordById(int permissionId);

        /// <summary>
        /// Gets a permission
        /// </summary>
        /// <param name="systemName">Permission system name</param>
        /// <returns>Permission</returns>
        PermissionRecord GetPermissionRecordBySystemName(string systemName);

        /// <summary>
        /// Gets all permissions
        /// </summary>
        /// <returns>Permissions</returns>
        IList<PermissionRecord> GetAllPermissionRecords();

       

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <returns>true - authorized; otherwise, false</returns>
        bool Authorize(PermissionRecord permission);

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <param name="User">User</param>
        /// <returns>true - authorized; otherwise, false</returns>
        bool Authorize(PermissionRecord permission, User User);

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <returns>true - authorized; otherwise, false</returns>
        bool Authorize(string permissionRecordSystemName);

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <param name="User">User</param>
        /// <returns>true - authorized; otherwise, false</returns>
        bool Authorize(string permissionRecordSystemName, User User);
    }
}