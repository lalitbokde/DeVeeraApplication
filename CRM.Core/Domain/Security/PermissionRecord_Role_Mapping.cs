using CRM.Core.Domain.Users;

namespace CRM.Core.Domain.Security
{
    public class PermissionRecord_Role_Mapping : BaseEntity
    {
        public int PermissionRecordId { get; set; }
        public virtual PermissionRecord PermissionRecord { get; set; }
        public int UserRoleId { get; set; }
        public virtual UserRole UserRole { get; set; }
        public bool Deleted { get; set; }

    }


}
