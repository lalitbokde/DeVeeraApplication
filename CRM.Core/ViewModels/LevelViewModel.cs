using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.ViewModels
{
   public class LevelViewModel 
    {
        public int Id { get; set; }
        public int LevelNo { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string VideoName { get; set; }
        public int TotalRecords { get; set; }
        public Int32? LikeId { get; set; }
        public Int32? DisLikeId { get; set; }

    }
}
