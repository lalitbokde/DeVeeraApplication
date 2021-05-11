using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain.VideoModules
{
   public class Questions : BaseEntity
    {
        private ICollection<Question_Answer_Mapping> _Question_Answer_Mapping;
        public int ModuleId { get; set; }
        public string Question { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Deleted { get; set; }
        public virtual Modules Module { get; set; }

        public virtual ICollection<Question_Answer_Mapping> Question_Answer_Mapping
        {
            get { return _Question_Answer_Mapping ?? (_Question_Answer_Mapping = new List<Question_Answer_Mapping>()); }
            protected set { _Question_Answer_Mapping = value; }
        }
    }
}
