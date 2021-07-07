using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Core.ViewModels
{
  public class DashBoardQuoteViewModel
    {
        public int? LevelId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Level { get; set; }
        public bool IsDashboardQuote { get; set; }
        public bool IsRandom { get; set; }
        public bool? IsWeeklyInspiringQuotes { get; set; }
        public int TotalRecords { get; set; }
        

    }
}
