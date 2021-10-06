using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Core.Domain
{
    public class DashboardQuote : BaseEntity
    {
        public int? LevelId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Level { get; set; }
        public bool IsDashboardQuote { get; set; }
        public bool IsRandom { get; set; }
        public bool IsWeeklyInspiringQuotes { get; set; }
        [NotMapped]
        public string TitleSpanish { get; set; }
        
    }
}
