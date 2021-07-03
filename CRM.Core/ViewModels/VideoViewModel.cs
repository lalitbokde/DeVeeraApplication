using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Core.ViewModels
{
   public class VideoViewModel : BaseEntityModel
    {
      
        public string Name { get; set; }
        public string VideoUrl { get; set; }
        [NotMapped]
        public int TotalRecords { get; set; }
        public string Key { get; set; }

        public string FileName { get; set; }
        public bool IsNew { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        [NotMapped]
        public string Link_2_bannerImage { get; set; }
        [NotMapped]
        public string Link_2_Title { get; set; }

    }
}
