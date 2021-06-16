using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.ViewModels
{
    public class DiaryViewModel:BaseEntityModel
    {
        public int UserId { get; set; }
        public int TotalRecords { get; set; }
        public string Title { get; set; }          
        public string Comment { get; set; }
        public string DiaryColor { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        
    
    }
}
