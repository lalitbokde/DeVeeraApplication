using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain.VideoModules
{
    public class Diary : BaseEntity
    {
        public int UserId { get; set; }
        public int LevelId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Deleted { get; set; }
        public virtual Level Level { get; set; }
    }
}
