using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain
{
    public class Video : BaseEntity
    {
        public string Name { get; set; }
        public string VideoUrl { get; set; }
        public string Key { get; set; }
        public bool IsNew { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
}
