using CRM.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain.VideoModules
{
    public class Diary : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int? LevelId { get; set; }
        public int? ModuleId { get; set; }
        public string Comment { get; set; }
        public string DiaryColor { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public bool Deleted { get; set; }
        public virtual User User { get; set; }
    }
}
