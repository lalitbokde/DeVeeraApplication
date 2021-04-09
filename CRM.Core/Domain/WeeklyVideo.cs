using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain
{
    public class WeeklyVideo : BaseEntity
    {
        public string Title { get; set; }
        public string VideoURL { get; set; }
        public string WeeklyText { get; set; }
    }
}
