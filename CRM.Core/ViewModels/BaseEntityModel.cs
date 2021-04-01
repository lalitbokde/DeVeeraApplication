using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Core.ViewModels
{
    public partial class BaseEntityModel
    {
       
        public virtual int Id { get; set; }
    }
}
