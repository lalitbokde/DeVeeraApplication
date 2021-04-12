using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain
{
    public class Video : BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string VideoURL { get; set; }
        public string FullDescription { get; set; }
    }
}
