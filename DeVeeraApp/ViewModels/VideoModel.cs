using CRM.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class VideoModel : BaseEntityModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string VideoUrl { get; set; }

        public string Key { get; set; }
    }
}
