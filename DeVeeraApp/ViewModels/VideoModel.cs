using System;
using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels
{
    public class VideoModel : BaseEntityModel
    {
        [StringLength(80)]
        [Required(ErrorMessage ="Please enter video name ")]
        public string Name { get; set; }
        public string VideoUrl { get; set; }


        public string Key { get; set; }

        public string FileName { get; set; }
        public bool IsNew { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }



    }
}
