using CRM.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Core.ViewModels
{
   public  class FeelGoodViewModel
    {
       
        public string Title { get; set; }
        public string Author { get; set; }
        public string Story { get; set; }
        public int ImageId { get; set; }

        [NotMapped]
        public virtual Image Image { get; set; }
      
        public int TotalRecords { get; set; }

    }
}
