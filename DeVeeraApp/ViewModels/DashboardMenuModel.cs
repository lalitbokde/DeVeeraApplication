using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels
{
    public class DashboardMenuModel:BaseEntityModel
    {
        [Required]
        public string Menu_1 { get; set; }
        [Required]
        public string Menu_2 { get; set; }
        [Required]
        public string Menu_3 { get; set; }
    }
}
