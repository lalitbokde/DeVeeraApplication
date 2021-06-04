using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain
{
    public class Image : BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Key { get; set; }

    }
}
