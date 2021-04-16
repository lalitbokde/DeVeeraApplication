using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain
{
   public class DashboardQuote : BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsActive { get; set; }
    }
}
