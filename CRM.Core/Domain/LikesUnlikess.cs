using CRM.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Core.Domain
{
  public  class LikesUnlikess :BaseEntity
    {
        public int UserId { get; set; }
        public int? LevelId { get; set; }
        public int? ModuleId { get; set; }
        //Like Unlike section
        public int LikeId { get; set; }

        public int DisLikeId { get; set; }

        public bool IsLike { get; set; }
        public bool IsDisLike { get; set; }
        public string Comments { get; set; }
        public bool Deleted { get; set; }
        public virtual User User { get; set; }
      
        public DateTime CreatedDate { get; set; }

    }
   

}
