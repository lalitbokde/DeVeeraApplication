using CRM.Core.Domain.VideoModules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class LevelModel : BaseEntityModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }

        [Required]
        public string Quote { get; set; }
        [Required]
        public string VideoURL { get; set; }
        public string VideoName { get; set; }

        [Required]
        public string FullDescription { get; set; }

        public Modules Modules { get; set; }
    }
}
