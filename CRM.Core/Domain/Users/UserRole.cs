using CRM.Core.Domain.Security;
using System.Collections.Generic;

namespace CRM.Core.Domain.Users
{

    /// <summary>
    /// Represents a User role
    /// </summary>
    public partial class UserRole : BaseEntity
    {
        private ICollection<PermissionRecord_Role_Mapping> _PermissionRecord_Role_Mapping;
        /// <summary>
        /// Gets or sets the User role name
        /// </summary>
        public string Name { get; set; }

        

        /// <summary>
        /// Gets or sets a value indicating whether the User role is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the User role is system
        /// </summary>
        public bool IsSystemRole { get; set; }

        /// <summary>
        /// Gets or sets the User role system name
        /// </summary>
        public string SystemName { get; set; }


       
      

        /// <summary>
        /// Gets or sets the permission records
        /// </summary>
        /// 
        /// <summary>
        /// Gets or sets discount usage history
        /// </summary>
        public virtual ICollection<PermissionRecord_Role_Mapping> PermissionRecord_Role_Mapping
        {
            get { return _PermissionRecord_Role_Mapping ??= new List<PermissionRecord_Role_Mapping>(); }
            protected set { _PermissionRecord_Role_Mapping = value; }
        }

    }
}
