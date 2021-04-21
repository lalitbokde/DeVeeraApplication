using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain.VideoModules
{
    public class Modules : BaseEntity
    {
        public int LevelId { get; set; }
        public string VideoURL { get; set; }
        public string FullDescription { get; set; }
        public virtual Level Level { get; set; }
    }
}
