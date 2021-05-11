using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRM.Core.Domain.VideoModules
{
    public class Modules : BaseEntity
    {
        public int LevelId { get; set; }
        public int? VideoId { get; set; }
        public string Title { get; set; }
        public string FullDescription { get; set; }
        public virtual Level Level { get; set; }
        public virtual Video Video { get; set; }
    }
}
