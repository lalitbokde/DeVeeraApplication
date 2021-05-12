using CRM.Core.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class FeelGoodStoryModel : BaseEntityModel
    {
        public FeelGoodStoryModel()
        {
            AvailableImages = new List<SelectListItem>();
        }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Story { get; set; }
        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }
        public IList<SelectListItem> AvailableImages { get; set; }

    }
}
