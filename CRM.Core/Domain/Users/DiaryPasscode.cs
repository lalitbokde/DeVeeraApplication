using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain.Users
{
    public class DiaryPasscode:BaseEntity
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public DateTime? DiaryLoginDate { get; set; }
        public DateTime CreatedOn{ get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public bool Deleted { get; set; }
        public virtual User User { get; set; }

    }
}
