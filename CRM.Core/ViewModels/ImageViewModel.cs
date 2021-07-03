using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.ViewModels
{
  public  class ImageViewModel: BaseEntityModel
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int TotalImage { get; set; }
        public string Key { get; set; }
        //public string FileName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
