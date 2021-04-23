using CRM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class ModulesModel : BaseEntityModel
    {
        public int LevelId { get; set; }
        public string VideoURL { get; set; }
        public string FullDescription { get; set; }
        public virtual Level Level { get; set; }
    }
}
