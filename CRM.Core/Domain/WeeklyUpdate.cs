using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain
{
    public class WeeklyUpdate : BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string VideoURL { get; set; }

        public string VideoName { get; set; }
        public Quote QuoteType { get; set; }
        public bool IsActive { get; set; }

    }

    public enum Quote
    {
        Login = 1,
        Registration = 2
    }

}
