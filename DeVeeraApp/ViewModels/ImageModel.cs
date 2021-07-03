
﻿using System;

using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels
{
    public class ImageModel : BaseEntityModel
    {
       
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Key { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

       
    }
}
