using System.Collections.Generic;
using CRM.Core.Domain.Users;
using CRM.Core.Domain.Security;

namespace CRM.Services.Security
{
    /// <summary>
    /// Standard permission provider
    /// </summary>
    public partial class StandardPermissionProvider : IPermissionProvider
    {
        //admin area permissions
        public static readonly PermissionRecord AccessAdminPanel = new PermissionRecord { Name = "Access admin area", SystemName = "AccessAdminPanel", Category = "Standard" };
        public static readonly PermissionRecord ManageAcl = new PermissionRecord { Name = "Manage ACL", SystemName = "ManageACL", Category = "Configuration" };
        public static readonly PermissionRecord Maintenance = new PermissionRecord { Name = "Manage Maintenance", SystemName = "ManageMaintenance", Category = "Configuration" };
        public static readonly PermissionRecord ManageUser = new PermissionRecord { Name = "Manage User", SystemName = "ManageUser", Category = "Users" };
        public static readonly PermissionRecord ManageUserRole = new PermissionRecord { Name = "Manage User Role", SystemName = "ManageUserRole", Category = "Users" };
        public static readonly PermissionRecord ManageAutoDataImport = new PermissionRecord { Name = "Manage Auto Data Import", SystemName = "ManageAutoDataImport", Category = "Standard" };
        public static readonly PermissionRecord ManageDataMart = new PermissionRecord { Name = "Manage Data Mart", SystemName = "ManageDataMart", Category = "Standard" };

        public static readonly PermissionRecord ManageWorkRequest = new PermissionRecord { Name = "Manage Work Request", SystemName = "ManageWorkRequest", Category = "WorkRequest" };
        public static readonly PermissionRecord ManageAddress = new PermissionRecord { Name = "Manage Address", SystemName = "ManageAddress", Category = "WorkRequest" };

        /// <summary>
        /// Get permissions
        /// </summary>
        /// <returns>Permissions</returns>
        public virtual IEnumerable<PermissionRecord> GetPermissions()
        {
            return new[]
            {
                AccessAdminPanel,
                ManageAcl,
                Maintenance,
                ManageUser,
                ManageUserRole
            };
        }

        /// <summary>
        /// Get default permissions
        /// </summary>
        /// <returns>Permissions</returns>
        public virtual IEnumerable<DefaultPermissionRecord> GetDefaultPermissions()
        {
            return new[]
            {
                new DefaultPermissionRecord
                {
                    UserRoleSystemName = SystemUserRoleNames.Admin,
                    PermissionRecords = new[]
                    {
                        AccessAdminPanel,
                        ManageAcl,
                  Maintenance,
                ManageUser,
                ManageUserRole

                    }
                },
                new DefaultPermissionRecord
                {
                    UserRoleSystemName = SystemUserRoleNames.User,
                    PermissionRecords = new[]
                    {
                        AccessAdminPanel,

                  Maintenance,
                ManageUser,
                ManageUserRole

                    }
                },
                
                new DefaultPermissionRecord
                {
                    UserRoleSystemName = SystemUserRoleNames.Registered,
                    PermissionRecords = new[]
                    {
                         AccessAdminPanel,

                  Maintenance,
                ManageUser,
                ManageUserRole
                    }
                }
               

            };
        }
    }
}