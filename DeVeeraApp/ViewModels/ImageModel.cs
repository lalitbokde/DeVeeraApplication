using System;
using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels
{
    public class ImageModel : BaseEntityModel
    {
        [StringLength(80)]
        [Required(ErrorMessage ="Please enter image name ")]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Key { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
}
