using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class WeeklyUpdateModel : BaseEntityModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
        public string VideoURL { get; set; }
        public string VideoName { get; set; }

        [Required]
        public Quote QuoteType { get; set; }
        public bool IsActive { get; set; } = false;
    }

    public enum Quote
    {
        Login = 1,
        Registration = 2
    }

}
