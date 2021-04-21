using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class WeeklyUpdateModel : BaseEntityModel
    {

        //[Required]
        //public string Title { get; set; }
        //[Required]
        //public string Subtitle { get; set; }


        public WeeklyUpdateModel()
        {
            this.AvailableVideoUrl = new List<SelectListItem>();
        }

        public int VideoId { get; set; }

        //[Required]
        //public string Title { get; set; }
        //[Required]
        //public string Subtitle { get; set; }

        //[Required]
        public string VideoURL { get; set; }
        //public string VideoName { get; set; }

        [Required]
        public Quote QuoteType { get; set; }
        public bool IsActive { get; set; } = false;
        public IList<SelectListItem> AvailableVideoUrl { get; set; }

        [NotMapped]
        public int LastLevel { get; set; }

    }

    public enum Quote
    {
        Login = 1,
        Registration = 2
    }

}
