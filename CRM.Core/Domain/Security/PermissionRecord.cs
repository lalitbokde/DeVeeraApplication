using System.Collections.Generic;

namespace CRM.Core.Domain.Security
{
    /// <summary>
    /// Represents a permission record
    /// </summary>
    public partial class PermissionRecord : BaseEntity
    {
        private ICollection<PermissionRecord_Role_Mapping> _PermissionRecord_Role_Mapping;

        /// <summary>
        /// Gets or sets the permission name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the permission system name
        /// </summary>
        public string SystemName { get; set; }
        
        /// <summary>
        /// Gets or sets the permission category
        /// </summary>
        public string Category { get; set; }

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
