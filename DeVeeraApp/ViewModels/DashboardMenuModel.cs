using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
