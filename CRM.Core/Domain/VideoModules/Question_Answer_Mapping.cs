using CRM.Core.Domain.Users;
using System;

namespace CRM.Core.Domain.VideoModules
{
    public class Question_Answer_Mapping : BaseEntity
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }     
        public string Answer { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Deleted { get; set; }
        public virtual User User { get; set; }
        public virtual Questions Question { get; set; }
    }
}
