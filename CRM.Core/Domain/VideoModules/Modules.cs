using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain.VideoModules
{
    public class Modules : BaseEntity
    { 
        public int VideoId { get; set; }
        public string VideoURL { get; set; }
        public string FullDescription { get; set; }
        public virtual Video Video { get; set; }
    }
}
