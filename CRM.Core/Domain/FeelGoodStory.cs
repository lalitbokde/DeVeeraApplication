using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain
{
    public class FeelGoodStory : BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Story { get; set; }
        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }

    }
}
