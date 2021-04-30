using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain
{
    public class Level : BaseEntity
    {
        public int? VideoId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        //public string Quote { get; set; }
        public string FullDescription { get; set; }
        public virtual Video Video { get; set; }
    }
}
