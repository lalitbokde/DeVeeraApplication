using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CRM.Core.Domain
{
    public class WeeklyUpdate : BaseEntity
    {
        public int VideoId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public Quote QuoteType { get; set; }
        public bool IsActive { get; set; }
        public virtual Video Video { get; set; }

    }

    public enum Quote
    {
        [Description("Login")]
        Login = 1,
        [Description("Registration")]
        Registration = 2,
        [Description("Landing Page")]
        Landing = 3
    }

}
