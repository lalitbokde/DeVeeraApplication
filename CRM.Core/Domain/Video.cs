using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain
{
    public class Video : BaseEntity
    {
        public string Name { get; set; }
        public string VideoUrl { get; set; }
    }
}
